using ImagePrint.Models;
using ImagePrint.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
                db.Orders.Add(printViewModel.UserOrder);
                db.SaveChanges();
            }

            // get list of images of user order
            printViewModel.OrderDetailList = db.OrderDetails.Where(orderDetail => orderDetail.OrderId == printViewModel.UserOrder.OrderId).ToList();

            return View(printViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage([Bind(Include = "UploadedImage")] PrintViewModel model, HttpPostedFileBase uploadImg)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");
            var user = (Customer)Session["user"];

            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (uploadImg == null && uploadImg.ContentLength == 0)
            {
                ViewBag["Error"] = "Error uploading file";
                return View();
            }

            Image image = model.UploadedImage;
            // Save image info to database
            image.ImageName = "~/Content/image/image_print/" + user.CusId + "/" + uploadImg.FileName;
            var existImg = db.Images.SingleOrDefault(i => i.ImageName == image.ImageName);
            // Only add image if there is no same img path in database
            if (existImg == null)
                db.Images.Add(image);
            db.SaveChanges();
            ViewBag.Image = image;

            // save image to server
            string urlImage = Server.MapPath(image.ImageName);
            uploadImg.SaveAs(urlImage);

            // Update order detail (after having image ID auto generated)
            OrderDetail detail = new OrderDetail();
            detail.Image = image;
            db.OrderDetails.Add(detail);
            db.SaveChanges();

            return View(image);
        }
    }
}