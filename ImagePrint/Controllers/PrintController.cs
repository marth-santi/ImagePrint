using ImagePrint.Models;
using ImagePrint.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            // get list of images of user order
            printViewModel.OrderDetailList = db.OrderDetails.Where(orderDetail => orderDetail.OrderId == printViewModel.UserOrder.OrderId).ToList();

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
                image = db.Images.Add(image);
            db.SaveChanges();

            // save image to server
            string urlImage = Server.MapPath(image.ImageName);
            Directory.CreateDirectory(Server.MapPath("~/Content/image/image_print/" + user.CusId));
            uploadImg.SaveAs(urlImage);

            // Update order detail (after having image ID auto generated)
            OrderDetail detail = new OrderDetail();
            detail.Image = image;
            detail.OrderId = viewModel.UserOrder.OrderId;
            detail.SizeId = DEFAULT_SIZE_ID;
            db.OrderDetails.Add(detail);
            db.SaveChanges();

            return View("UploadImage", viewModel);
        }
    }
}