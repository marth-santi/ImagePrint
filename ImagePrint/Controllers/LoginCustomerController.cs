using ImagePrint.Areas;
using ImagePrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImagePrint.Controllers
{
    public class LoginCustomerController : Controller
    {
        private MyImageDBEntities db = new MyImageDBEntities();
        // GET: LoginCustomer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Customer customer)
        {
            customer.PassCus = Utility.EncodeMD5(customer.PassCus);
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            customer.PassCus = Utility.EncodeMD5(customer.PassCus);
            var rs = db.Customers.Where(x => x.UserCus == customer.UserCus && x.PassCus == customer.PassCus).SingleOrDefault();
            if (rs != null)
            {
                Session["user"] = rs;
                return RedirectToAction("Index", "GoToPrint");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password Incorrect !");
            }
            return View(customer);
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}