using ImagePrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImagePrint.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        private MyImageDBEntities db = new MyImageDBEntities();
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Member member)
        {
            string encode = Utility.EncodeMD5(member.PassM);
            member.PassM = encode;

            var result = db.Members.Where(a => a.UserM == member.UserM && a.PassM == member.PassM).SingleOrDefault();
            if (result != null)
            {
                Session["admin"] = member;
                return RedirectToAction("Index", "Members");
            }
            else
            {
                ModelState.AddModelError("", "Login Fail! Wrong username or password");
                return View();
            }
        }

        public ActionResult LogOut()
        {
            Session["admin"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}