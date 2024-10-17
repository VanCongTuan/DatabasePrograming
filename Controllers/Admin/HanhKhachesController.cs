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
    public class HanhKhachesController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: HanhKhaches
        public ActionResult Index()
        {
            return View(db.HanhKhachs.ToList());
        }

        // GET: HanhKhaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HanhKhach hanhKhach = db.HanhKhachs.Find(id);
            if (hanhKhach == null)
            {
                return HttpNotFound();
            }
            return View(hanhKhach);
        }

        // GET: HanhKhaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HanhKhaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GioiTinh,HoTen,NgSinh")] HanhKhach hanhKhach)
        {
            if (ModelState.IsValid)
            {
                db.HanhKhachs.Add(hanhKhach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hanhKhach);
        }

        // GET: HanhKhaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HanhKhach hanhKhach = db.HanhKhachs.Find(id);
            if (hanhKhach == null)
            {
                return HttpNotFound();
            }
            return View(hanhKhach);
        }

        // POST: HanhKhaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GioiTinh,HoTen,NgSinh")] HanhKhach hanhKhach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hanhKhach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hanhKhach);
        }

        // GET: HanhKhaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HanhKhach hanhKhach = db.HanhKhachs.Find(id);
            if (hanhKhach == null)
            {
                return HttpNotFound();
            }
            return View(hanhKhach);
        }

        // POST: HanhKhaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HanhKhach hanhKhach = db.HanhKhachs.Find(id);
            db.HanhKhachs.Remove(hanhKhach);
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
