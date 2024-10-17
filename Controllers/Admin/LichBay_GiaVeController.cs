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
    public class LichBay_GiaVeController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: LichBay_GiaVe
        public ActionResult Index()
        {
            var lichBay_GiaVes = db.LichBay_GiaVes.Include(l => l.GiaVe).Include(l => l.HangVe).Include(l => l.LichBay);
            return View(lichBay_GiaVes.ToList());
        }

        // GET: LichBay_GiaVe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichBay_GiaVe lichBay_GiaVe = db.LichBay_GiaVes.Find(id);
            if (lichBay_GiaVe == null)
            {
                return HttpNotFound();
            }
            return View(lichBay_GiaVe);
        }

        // GET: LichBay_GiaVe/Create
        public ActionResult Create()
        {
            ViewBag.giaVeId = new SelectList(db.GiaVes, "Id", "Id");
            ViewBag.hangVeId = new SelectList(db.HangVes, "Id", "LoaiHang");
            ViewBag.lichBayId = new SelectList(db.LichBays, "MaLB", "TrangThai");
            return View();
        }

        // POST: LichBay_GiaVe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NgayApDung,NgayKetThuc,SoLuongGhe,hangVeId,lichBayId,giaVeId")] LichBay_GiaVe lichBay_GiaVe)
        {
            if (ModelState.IsValid)
            {
                db.LichBay_GiaVes.Add(lichBay_GiaVe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.giaVeId = new SelectList(db.GiaVes, "Id", "Id", lichBay_GiaVe.giaVeId);
            ViewBag.hangVeId = new SelectList(db.HangVes, "Id", "LoaiHang", lichBay_GiaVe.hangVeId);
            ViewBag.lichBayId = new SelectList(db.LichBays, "MaLB", "TrangThai", lichBay_GiaVe.lichBayId);
            return View(lichBay_GiaVe);
        }

        // GET: LichBay_GiaVe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichBay_GiaVe lichBay_GiaVe = db.LichBay_GiaVes.Find(id);
            if (lichBay_GiaVe == null)
            {
                return HttpNotFound();
            }
            ViewBag.giaVeId = new SelectList(db.GiaVes, "Id", "Id", lichBay_GiaVe.giaVeId);
            ViewBag.hangVeId = new SelectList(db.HangVes, "Id", "LoaiHang", lichBay_GiaVe.hangVeId);
            ViewBag.lichBayId = new SelectList(db.LichBays, "MaLB", "TrangThai", lichBay_GiaVe.lichBayId);
            return View(lichBay_GiaVe);
        }

        // POST: LichBay_GiaVe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NgayApDung,NgayKetThuc,SoLuongGhe,hangVeId,lichBayId,giaVeId")] LichBay_GiaVe lichBay_GiaVe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichBay_GiaVe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.giaVeId = new SelectList(db.GiaVes, "Id", "Id", lichBay_GiaVe.giaVeId);
            ViewBag.hangVeId = new SelectList(db.HangVes, "Id", "LoaiHang", lichBay_GiaVe.hangVeId);
            ViewBag.lichBayId = new SelectList(db.LichBays, "MaLB", "TrangThai", lichBay_GiaVe.lichBayId);
            return View(lichBay_GiaVe);
        }

        // GET: LichBay_GiaVe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichBay_GiaVe lichBay_GiaVe = db.LichBay_GiaVes.Find(id);
            if (lichBay_GiaVe == null)
            {
                return HttpNotFound();
            }
            return View(lichBay_GiaVe);
        }

        // POST: LichBay_GiaVe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichBay_GiaVe lichBay_GiaVe = db.LichBay_GiaVes.Find(id);
            db.LichBay_GiaVes.Remove(lichBay_GiaVe);
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
