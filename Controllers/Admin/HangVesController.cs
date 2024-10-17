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
    public class HangVesController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: HangVes
        public ActionResult Index()
        {
            return View(db.HangVes.ToList());
        }

        // GET: HangVes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangVe hangVe = db.HangVes.Find(id);
            if (hangVe == null)
            {
                return HttpNotFound();
            }
            return View(hangVe);
        }

        // GET: HangVes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangVes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoaiHang")] HangVe hangVe)
        {
            if (ModelState.IsValid)
            {
                db.HangVes.Add(hangVe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangVe);
        }

        // GET: HangVes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangVe hangVe = db.HangVes.Find(id);
            if (hangVe == null)
            {
                return HttpNotFound();
            }
            return View(hangVe);
        }

        // POST: HangVes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoaiHang")] HangVe hangVe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangVe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangVe);
        }

        // GET: HangVes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangVe hangVe = db.HangVes.Find(id);
            if (hangVe == null)
            {
                return HttpNotFound();
            }
            return View(hangVe);
        }

        // POST: HangVes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangVe hangVe = db.HangVes.Find(id);
            db.HangVes.Remove(hangVe);
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
