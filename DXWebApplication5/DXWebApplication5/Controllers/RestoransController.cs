using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DXWebApplication5.Models;

namespace DXWebApplication5.Controllers
{
    public class RestoransController : Controller
    {
        private NeredeYesekDBEntities3 db = new NeredeYesekDBEntities3();

        // GET: Restorans
        public ActionResult Index()
        {
            var restorans = db.Restorans.Include(r => r.Istatistik);
            return View(restorans.ToList());
        }

        // GET: Restorans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restoran restoran = db.Restorans.Find(id);
            if (restoran == null)
            {
                return HttpNotFound();
            }
            return View(restoran);
        }

        // GET: Restorans/Create
        public ActionResult Create()
        {
            ViewBag.RID = new SelectList(db.Istatistiks, "RID", "RID");
            return View();
        }

        // POST: Restorans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RID,RestoranAdi")] Restoran restoran)
        {
            if (ModelState.IsValid)
            {
                db.Restorans.Add(restoran);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RID = new SelectList(db.Istatistiks, "RID", "RID", restoran.RID);
            return View(restoran);
        }

        // GET: Restorans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restoran restoran = db.Restorans.Find(id);
            if (restoran == null)
            {
                return HttpNotFound();
            }
            ViewBag.RID = new SelectList(db.Istatistiks, "RID", "RID", restoran.RID);
            return View(restoran);
        }

        // POST: Restorans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RID,RestoranAdi")] Restoran restoran)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restoran).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RID = new SelectList(db.Istatistiks, "RID", "RID", restoran.RID);
            return View(restoran);
        }

        // GET: Restorans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restoran restoran = db.Restorans.Find(id);
            if (restoran == null)
            {
                return HttpNotFound();
            }
            return View(restoran);
        }

        // POST: Restorans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restoran restoran = db.Restorans.Find(id);
            db.Restorans.Remove(restoran);
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
