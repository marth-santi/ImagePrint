using ImagePrint.Models;
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
        private MyImageDBEntities db = new MyImageDBEntities();
        // GET: Print
        public ActionResult UploadImage()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage([Bind(Include = "ImageId,ImageName")] Image image, HttpPostedFileBase uploadImg)
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

            image.ImageName = "/Content/image/image_print/" + user.CusId + "/" + uploadImg.FileName;
            db.Images.Add(image);
            db.SaveChanges();
            ViewBag.Image = image;

            string urlImage = Server.MapPath("~" + image.ImageName);
            uploadImg.SaveAs(urlImage);
            return View(image);
        }
    }
}