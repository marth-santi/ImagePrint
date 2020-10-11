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

            // Get current order
            var order = Session["order"];
            if (order == null)
                return RedirectToAction("UploadImage", "Print");

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCard(Order order)
        {
            bool isCardVerified = VerifyCard(order.CreditCardNumber.GetValueOrDefault());
            bool isPaymentCompleted = false;

            if (isCardVerified)
                isPaymentCompleted = ProcessPayment(order, (decimal)Session["Cost"]);

            if (isPaymentCompleted)
            {
                Session["cost"] = null;
                Session["order"] = null;
                TempData["status"] = "Payment success!!";
                return RedirectToAction("UploadImage", "Print");
            }

            return RedirectToAction("CardDetails", "Payment");
        }

        public bool VerifyCard(decimal cardNumber)
        {
            return true;
        }

        public bool ProcessPayment(Order order, decimal cost)
        {
            try
            {
                var currentOrder = db.Orders.Find(order.OrderId);
                currentOrder.Email_Id = order.Email_Id;
                currentOrder.CreditCardNumber = order.CreditCardNumber;
                currentOrder.IsComplete = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return false;
            }
        }
    }
}