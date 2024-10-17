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
    public class TuyenBaysController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: TuyenBays
        public ActionResult Index()
        {
            var tuyenBays = db.TuyenBays.Include(t => t.SanBayDen).Include(t => t.SanBayDi);
            return View(tuyenBays.ToList());
        }

        // GET: TuyenBays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenBay tuyenBay = db.TuyenBays.Find(id);
            if (tuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(tuyenBay);
        }

        // GET: TuyenBays/Create
        public ActionResult Create()
        {
            ViewBag.Id_SbDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.Id_SbDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            return View();
        }

        // POST: TuyenBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTuyenBay,Id_SbDi,Id_SbDen")] TuyenBay tuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.TuyenBays.Add(tuyenBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_SbDen = new SelectList(db.SanBays, "MaSB", "TenSB", tuyenBay.Id_SbDen);
            ViewBag.Id_SbDi = new SelectList(db.SanBays, "MaSB", "TenSB", tuyenBay.Id_SbDi);
            return View(tuyenBay);
        }

        // GET: TuyenBays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenBay tuyenBay = db.TuyenBays.Find(id);
            if (tuyenBay == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_SbDen = new SelectList(db.SanBays, "MaSB", "TenSB", tuyenBay.Id_SbDen);
            ViewBag.Id_SbDi = new SelectList(db.SanBays, "MaSB", "TenSB", tuyenBay.Id_SbDi);
            return View(tuyenBay);
        }

        // POST: TuyenBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTuyenBay,Id_SbDi,Id_SbDen")] TuyenBay tuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuyenBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_SbDen = new SelectList(db.SanBays, "MaSB", "TenSB", tuyenBay.Id_SbDen);
            ViewBag.Id_SbDi = new SelectList(db.SanBays, "MaSB", "TenSB", tuyenBay.Id_SbDi);
            return View(tuyenBay);
        }

        // GET: TuyenBays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenBay tuyenBay = db.TuyenBays.Find(id);
            if (tuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(tuyenBay);
        }

        // POST: TuyenBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TuyenBay tuyenBay = db.TuyenBays.Find(id);
            db.TuyenBays.Remove(tuyenBay);
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
