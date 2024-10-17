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
    public class LichBaysController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: LichBays
        public ActionResult Index()
        {
            var lichBays = db.LichBays.Include(l => l.ChuyenBay).Include(l => l.MayBay).Include(l => l.NhanVien);
            return View(lichBays.ToList());
        }

        // GET: LichBays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichBay lichBay = db.LichBays.Find(id);
            if (lichBay == null)
            {
                return HttpNotFound();
            }
            return View(lichBay);
        }

        // GET: LichBays/Create
        public ActionResult Create()
        {
            ViewBag.chuyenBayId = new SelectList(db.ChuyenBays, "MaCB", "TenCB");
            ViewBag.mayBayId = new SelectList(db.MayBays, "MaMb", "TenMB");
            ViewBag.nhanVienId = new SelectList(db.NhanViens, "Id", "HoVaTen");
            return View();
        }

        // POST: LichBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLB,NgayBay,ThoiGianBay,TrangThai,mayBayId,chuyenBayId,nhanVienId")] LichBay lichBay)
        {
            if (ModelState.IsValid)
            {
                db.LichBays.Add(lichBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.chuyenBayId = new SelectList(db.ChuyenBays, "MaCB", "TenCB", lichBay.chuyenBayId);
            ViewBag.mayBayId = new SelectList(db.MayBays, "MaMb", "TenMB", lichBay.mayBayId);
            ViewBag.nhanVienId = new SelectList(db.NhanViens, "Id", "HoVaTen", lichBay.nhanVienId);
            return View(lichBay);
        }

        // GET: LichBays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichBay lichBay = db.LichBays.Find(id);
            if (lichBay == null)
            {
                return HttpNotFound();
            }
            ViewBag.chuyenBayId = new SelectList(db.ChuyenBays, "MaCB", "TenCB", lichBay.chuyenBayId);
            ViewBag.mayBayId = new SelectList(db.MayBays, "MaMb", "TenMB", lichBay.mayBayId);
            ViewBag.nhanVienId = new SelectList(db.NhanViens, "Id", "HoVaTen", lichBay.nhanVienId);
            return View(lichBay);
        }

        // POST: LichBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLB,NgayBay,ThoiGianBay,TrangThai,mayBayId,chuyenBayId,nhanVienId")] LichBay lichBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.chuyenBayId = new SelectList(db.ChuyenBays, "MaCB", "TenCB", lichBay.chuyenBayId);
            ViewBag.mayBayId = new SelectList(db.MayBays, "MaMb", "TenMB", lichBay.mayBayId);
            ViewBag.nhanVienId = new SelectList(db.NhanViens, "Id", "HoVaTen", lichBay.nhanVienId);
            return View(lichBay);
        }

        // GET: LichBays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichBay lichBay = db.LichBays.Find(id);
            if (lichBay == null)
            {
                return HttpNotFound();
            }
            return View(lichBay);
        }

        // POST: LichBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichBay lichBay = db.LichBays.Find(id);
            db.LichBays.Remove(lichBay);
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
