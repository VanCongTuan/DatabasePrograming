using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LTCSDLMayBay.Models;

namespace LTCSDLMayBay.Controllers.Admin
{
    public class GiaVesController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: GiaVes
        public ActionResult Index()
        {
            return View(db.GiaVes.ToList());
        }

        // GET: GiaVes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaVe giaVe = db.GiaVes.Find(id);
            if (giaVe == null)
            {
                return HttpNotFound();
            }
            return View(giaVe);
        }

        // GET: GiaVes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gia")] GiaVe giaVe)
        {
            if (ModelState.IsValid)
            {
                db.GiaVes.Add(giaVe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giaVe);
        }

        // GET: GiaVes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaVe giaVe = db.GiaVes.Find(id);
            if (giaVe == null)
            {
                return HttpNotFound();
            }
            return View(giaVe);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gia")] GiaVe giaVe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giaVe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giaVe);
        }

        // GET: GiaVes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaVe giaVe = db.GiaVes.Find(id);
            if (giaVe == null)
            {
                return HttpNotFound();
            }
            return View(giaVe);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiaVe giaVe = db.GiaVes.Find(id);
            db.GiaVes.Remove(giaVe);
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
    }
}
