using ImagePrint.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImagePrint.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private MyImageDBEntities db = new MyImageDBEntities();

        // GET: Admin/Customers
        public ActionResult Index()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            return View(db.Customers.ToList());
        }

        // GET: Admin/Customers/Details/5
        public ActionResult Details(decimal id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CusId,F_name,L_name,Dob,Gender,Phone_No,Address,UserCus,PassCus")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                string encode = Utility.EncodeMD5(customer.PassCus);
                customer.PassCus = encode;

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CusId,F_name,L_name,Dob,Gender,Phone_No,Address,UserCus,PassCus")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                string encode = Utility.EncodeMD5(customer.PassCus);
                customer.PassCus = encode;

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsAdminLoggedIn()
        {
            if (Session["admin"] == null)
                return false;
            return true;
        }
    }
}
