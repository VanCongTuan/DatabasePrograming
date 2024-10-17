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
    public class ChuyenBaysController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: ChuyenBays
        public ActionResult Index()
        {
            var chuyenBays = db.ChuyenBays.Include(c => c.TuyenBay);
            return View(chuyenBays.ToList());
        }

        // GET: ChuyenBays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Create
        public ActionResult Create()
        {
            ViewBag.tuyenBayId = new SelectList(db.TuyenBays, "MaTuyenBay", "MaTuyenBay");
            return View();
        }

        // POST: ChuyenBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCB,TenCB,tuyenBayId")] ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.ChuyenBays.Add(chuyenBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tuyenBayId = new SelectList(db.TuyenBays, "MaTuyenBay", "MaTuyenBay", chuyenBay.tuyenBayId);
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            ViewBag.tuyenBayId = new SelectList(db.TuyenBays, "MaTuyenBay", "MaTuyenBay", chuyenBay.tuyenBayId);
            return View(chuyenBay);
        }

        // POST: ChuyenBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCB,TenCB,tuyenBayId")] ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tuyenBayId = new SelectList(db.TuyenBays, "MaTuyenBay", "MaTuyenBay", chuyenBay.tuyenBayId);
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);
        }

        // POST: ChuyenBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            db.ChuyenBays.Remove(chuyenBay);
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
