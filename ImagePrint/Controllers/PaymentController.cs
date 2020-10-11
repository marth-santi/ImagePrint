using ImagePrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ImagePrint.Controllers
{
    public class PaymentController : Controller
    {
        private MyImageDBEntities db = new MyImageDBEntities();
        // GET: Payment
        public ActionResult CardDetails()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "LoginCustomer");
            var user = (Customer)Session["user"];
            var order = db.Orders.Where(ord => ord.CusId == user.CusId && !ord.IsComplete).FirstOrDefault();

            ViewBag.Cost = Session["Cost"].ToString();
            return View(order);
        }

        [HttpPost]
        public ActionResult VerifyCard(Order order)
        {

            return new EmptyResult();
        }
    }
}