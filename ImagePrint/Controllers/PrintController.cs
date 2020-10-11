using ImagePrint.Models;
using ImagePrint.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ImagePrint.Controllers
{
    public class PrintController : Controller
    {
        const int DEFAULT_SIZE_ID = 1;
        private MyImageDBEntities db = new MyImageDBEntities();
        // GET: Print
        public ActionResult UploadImage()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");
            var user = (Customer)Session["user"];

            var printViewModel = UpdateViewModel(user);

            return View(printViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(string model, HttpPostedFileBase uploadImg)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");
            var user = (Customer)Session["user"];

            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = JsonConvert.DeserializeObject<PrintViewModel>(model);

            if (uploadImg == null || uploadImg.ContentLength == 0)
            {
                ViewBag.Error = "Error uploading file";
                return View(UpdateViewModel(user));
            }

            Image image = new Image();
            // Save image info to database
            image.ImageName = "~/Content/image/image_print/" + user.CusId + "/" + uploadImg.FileName;
            var existImg = db.Images.FirstOrDefault(i => i.ImageName == image.ImageName);

            // Only add image if there is no same img path in database
            if (existImg == null)
            {
                image = db.Images.Add(image);
                // Update order detail (after having image ID auto generated)
                OrderDetail detail = new OrderDetail();
                detail.Image = image;
                detail.OrderId = viewModel.UserOrder.OrderId;
                detail.SizeId = DEFAULT_SIZE_ID;
                db.OrderDetails.Add(detail);
                db.SaveChanges();
            }
            else // If existed, set existed img as current working object
                image = existImg;

            // save image to server
            string urlImage = Server.MapPath(image.ImageName);
            Directory.CreateDirectory(Server.MapPath("~/Content/image/image_print/" + user.CusId));
            uploadImg.SaveAs(urlImage);
            
            // Return view with updated view model
            return View("UploadImage", UpdateViewModel(user));
        }

        public List<SelectListItem> GetSizeList()
        {
            var sizeList = db.Sizes.Select(
                    s => new SelectListItem
                    {
                        Value = s.SizeId.ToString(),
                        Text = "Size: " + s.Size1 + ", Price: " + s.Price
                    }
                ).ToList();
            return sizeList;
        }

        public PrintViewModel UpdateViewModel(Customer user)
        {
            var printViewModel = new PrintViewModel();
            // find order with user ID and not complete (payment)
            printViewModel.UserOrder = db.Orders.Where(ord => ord.CusId == user.CusId && !ord.IsComplete).FirstOrDefault();

            // If user order not present, create new
            if (printViewModel.UserOrder == null)
            {
                Order newOrder = new Order();
                newOrder.CusId = user.CusId;
                newOrder.IsComplete = false;
                printViewModel.UserOrder = db.Orders.Add(newOrder);
                db.SaveChanges();
            }

            // get list of order details and images of user order
            printViewModel.ImageDetailList = db.OrderDetails.Where(orderDetail => orderDetail.OrderId == printViewModel.UserOrder.OrderId)
                .Select(od => new ImageDetail
                {
                    OrderDetail = od,
                    Image = od.Image,
                    Size = od.Size
                }).ToList();

            // get size list
            ViewBag.SizeList = GetSizeList();

            return printViewModel;
        }

        public ActionResult DeleteImage(int orderId, int imageId)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");
            var user = (Customer)Session["user"];

            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var selectedOrderDetail = db.OrderDetails.FirstOrDefault(o => o.OrderId == orderId && o.ImageId == imageId);
            var selectedImg = db.Images.Find(imageId);
            // If cannot find img / order detail => return to view
            if (selectedImg == null || selectedOrderDetail == null)
                return RedirectToAction("UploadImage", "Print");

            var filePath = Server.MapPath(selectedImg.ImageName);
            try
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                db.OrderDetails.Remove(selectedOrderDetail);
                db.Images.Remove(selectedImg);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            ModelState.Clear();

            return RedirectToAction("UploadImage","Print");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetImageDetails(IEnumerable<ImageDetail> imageDetails)
        {
            decimal cost = 0;
            foreach (ImageDetail imgDetail in imageDetails)
            {
                var orderDetail = db.OrderDetails.FirstOrDefault(od =>  od.OrderId == imgDetail.OrderDetail.OrderId && od.ImageId == imgDetail.OrderDetail.ImageId);
                orderDetail.NumberOfPrints = imgDetail.OrderDetail.NumberOfPrints;
                orderDetail.SizeId = imgDetail.OrderDetail.SizeId;
                db.SaveChanges();
                cost = cost + (orderDetail.NumberOfPrints * orderDetail.Size.Price.GetValueOrDefault());
            }

            Session["Cost"] = cost;
            return RedirectToAction("CardDetails", "Payment");
        }
    }
}