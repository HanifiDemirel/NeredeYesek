﻿using DevExpress.Web.Mvc;
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
    public class UyelersController : Controller
    {
        private NeredeYesekDBEntities3 db = new NeredeYesekDBEntities3();

        // GET: Uyelers
        public ActionResult Index()
        {
            var uyelers = db.Uyelers.Include(u => u.Grup);
            return View(uyelers.ToList());
        }

        // GET: Uyelers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uyeler uyeler = db.Uyelers.Find(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            return View(uyeler);
        }

        // GET: Uyelers/Create
        public ActionResult Create()
        {
            ViewBag.GID = new SelectList(db.Grups, "GID", "GrupAdi");
            return View();
        }

        // POST: Uyelers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UID,GID,Isim,SoyIsim,Email")] Uyeler uyeler)
        {
            if (ModelState.IsValid)
            {
                db.Uyelers.Add(uyeler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GID = new SelectList(db.Grups, "GID", "GrupAdi", uyeler.GID);
            return View(uyeler);
        }

        // GET: Uyelers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uyeler uyeler = db.Uyelers.Find(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            ViewBag.GID = new SelectList(db.Grups, "GID", "GrupAdi", uyeler.GID);
            return View(uyeler);
        }

        // POST: Uyelers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UID,GID,Isim,SoyIsim,Email")] Uyeler uyeler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uyeler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GID = new SelectList(db.Grups, "GID", "GrupAdi", uyeler.GID);
            return View(uyeler);
        }

        // GET: Uyelers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uyeler uyeler = db.Uyelers.Find(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            return View(uyeler);
        }

        // POST: Uyelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uyeler uyeler = db.Uyelers.Find(id);
            db.Uyelers.Remove(uyeler);
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

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = new object[0];
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(DXWebApplication5.Models.Grup item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(DXWebApplication5.Models.Grup item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 GID)
        {
            var model = new object[0];
            if (GID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model);
        }
    }
}
