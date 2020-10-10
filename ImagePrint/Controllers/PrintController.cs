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

            if (uploadImg == null && uploadImg.ContentLength == 0)
            {
                ViewBag["Error"] = "Error uploading file";
                return View();
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
            }
            else // If existed, set existed img as current working object
                image = existImg;
            db.SaveChanges();

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
                        Value = s.Size1,
                        Text = "Size: " + s.Size1 + ", Price: " + s.Price
                    }
                ).ToList();
            return sizeList;
        }

        public PrintViewModel UpdateViewModel(Customer user)
        {
            var printViewModel = new PrintViewModel();
            // find order with user ID
            printViewModel.UserOrder = db.Orders.Where(ord => ord.CusId == user.CusId).FirstOrDefault();

            // If user order not present, create new
            if (printViewModel.UserOrder == null)
            {
                Order newOrder = new Order();
                newOrder.CusId = user.CusId;
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
    }
}