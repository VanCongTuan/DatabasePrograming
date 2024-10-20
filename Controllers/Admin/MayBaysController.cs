﻿using System;
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
    public class MayBaysController : Controller
    {
        private ApplicationDBcontext db = new ApplicationDBcontext();

        // GET: MayBays
        public ActionResult Index()
        {
            return View(db.MayBays.ToList());
        }

       

        // GET: MayBays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MayBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMb,TenMB")] MayBay mayBay)
        {
            if (ModelState.IsValid)
            {
                db.MayBays.Add(mayBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mayBay);
        }

        // GET: MayBays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MayBay mayBay = db.MayBays.Find(id);
            if (mayBay == null)
            {
                return HttpNotFound();
            }
            return View(mayBay);
        }

        // POST: MayBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMb,TenMB")] MayBay mayBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mayBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mayBay);
        }

        // GET: MayBays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MayBay mayBay = db.MayBays.Find(id);
            if (mayBay == null)
            {
                return HttpNotFound();
            }
            return View(mayBay);
        }

        // POST: MayBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MayBay mayBay = db.MayBays.Find(id);
            db.MayBays.Remove(mayBay);
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
