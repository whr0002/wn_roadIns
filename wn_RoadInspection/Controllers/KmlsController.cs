using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_web.Models;

namespace wn_web.Controllers
{
    [Authorize(Roles = "super admin")]
    public class KmlsController : Controller
    {
        private wn_webContext db = new wn_webContext();

        // GET: Kmls
        public ActionResult Index()
        {
            return View(db.Kmls.ToList());
        }

        // GET: Kmls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kml kml = db.Kmls.Find(id);
            if (kml == null)
            {
                return HttpNotFound();
            }
            return View(kml);
        }

        // GET: Kmls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kmls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Client,Url")] Kml kml)
        {
            if (ModelState.IsValid)
            {
                db.Kmls.Add(kml);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kml);
        }

        // GET: Kmls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kml kml = db.Kmls.Find(id);
            if (kml == null)
            {
                return HttpNotFound();
            }
            return View(kml);
        }

        // POST: Kmls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Client,Url")] Kml kml)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kml).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kml);
        }

        // GET: Kmls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kml kml = db.Kmls.Find(id);
            if (kml == null)
            {
                return HttpNotFound();
            }
            return View(kml);
        }

        // POST: Kmls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kml kml = db.Kmls.Find(id);
            db.Kmls.Remove(kml);
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
