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
    public class MembersController : Controller
    {
        private MyImageDBEntities db = new MyImageDBEntities();

        // GET: Admin/Members
        public ActionResult Index()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");
            return View(db.Members.ToList());
        }

        // GET: Admin/Members/Details/5
        public ActionResult Details(int? id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Admin/Members/Create
        public ActionResult Create()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            return View();
        }

        // POST: Admin/Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserM,PassM")] Member member)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            string encode = Utility.EncodeMD5(member.PassM);
            member.PassM = encode;

            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Admin/Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserM,PassM")] Member member)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");


            if (ModelState.IsValid)
            {
                string encode = Utility.EncodeMD5(member.PassM);
                member.PassM = encode;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Admin/Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Index", "AdminLogin");

            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
