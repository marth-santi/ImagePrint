using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImagePrint.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print
        public ActionResult UploadImage()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");

            return View();
        }
    }
}