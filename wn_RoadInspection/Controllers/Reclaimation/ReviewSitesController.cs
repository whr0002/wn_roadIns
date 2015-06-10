using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_web.Models;
using wn_web.Models.Reclaimation;

namespace wn_web.Controllers.Reclaimation
{
    public class ReviewSitesController : Controller
    {
        private wn_webContext db = new wn_webContext();

        // GET: ReviewSites
        public ActionResult Index()
        {
            //var reviewSites = db.ReviewSites.Include(r => r.County).Include(r => r.FMAHolder).Include(r => r.NaturalRegion).Include(r => r.NaturalSubRegion).Include(r => r.OperatingArea).Include(r => r.ProvincialArea).Include(r => r.ProvincialAreaType);
            var reviewSites = db.ReviewSites;
            return View(reviewSites.ToList());
        }

        // GET: ReviewSites/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewSite reviewSite = db.ReviewSites.Find(id);
            if (reviewSite == null)
            {
                return HttpNotFound();
            }
            return View(reviewSite);
        }

        // GET: ReviewSites/Create
        public ActionResult Create()
        {
            ViewBag.CountyName = new SelectList(db.Countys, "CountyName", "CountyName");
            ViewBag.FMAHolderName = new SelectList(db.FMAHolders, "FMAHolderName", "FMAHolderName");
            ViewBag.NaturalRegionName = new SelectList(db.NaturalRegions, "NaturalRegionName", "NaturalRegionName");
            ViewBag.NaturalSubRegionName = new SelectList(db.NaturalSubRegions, "NaturalSubRegionName", "NaturalSubRegionName");
            ViewBag.OperatingAreaName = new SelectList(db.OperatingAreas, "OperatingAreaName", "OperatingAreaName");
            ViewBag.ProvincialAreaName = new SelectList(db.ProvincialAreas, "ProvincialAreaName", "ProvincialAreaName");
            ViewBag.ProvincialAreaTypeName = new SelectList(db.ProvincialAreaTypes, "ProvincialAreaTypeName", "ProvincialAreaTypeName");
            return View();
        }

        // POST: ReviewSites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewSiteID,AFE,ProvincialAreaName,ProvincialAreaTypeName,OperatingAreaName,CountyName,NaturalRegionName,NaturalSubRegionName,FMAHolderName,SeedZone,WellboreID,UWI,WellsiteName,UTMZone")] ReviewSite reviewSite)
        {
            if (ModelState.IsValid)
            {
                db.ReviewSites.Add(reviewSite);
                db.SaveChanges();
                return RedirectToAction("Index", "Tutorial");
                //return RedirectToAction("Index");
            }

            ViewBag.CountyName = new SelectList(db.Countys, "CountyName", "CountyName", reviewSite.CountyName);
            ViewBag.FMAHolderName = new SelectList(db.FMAHolders, "FMAHolderName", "FMAHolderName", reviewSite.FMAHolderName);
            ViewBag.NaturalRegionName = new SelectList(db.NaturalRegions, "NaturalRegionName", "NaturalRegionName", reviewSite.NaturalRegionName);
            ViewBag.NaturalSubRegionName = new SelectList(db.NaturalSubRegions, "NaturalSubRegionName", "NaturalSubRegionName", reviewSite.NaturalSubRegionName);
            ViewBag.OperatingAreaName = new SelectList(db.OperatingAreas, "OperatingAreaName", "OperatingAreaName", reviewSite.OperatingAreaName);
            ViewBag.ProvincialAreaName = new SelectList(db.ProvincialAreas, "ProvincialAreaName", "ProvincialAreaName", reviewSite.ProvincialAreaName);
            ViewBag.ProvincialAreaTypeName = new SelectList(db.ProvincialAreaTypes, "ProvincialAreaTypeName", "ProvincialAreaTypeName", reviewSite.ProvincialAreaTypeName);
            return View(reviewSite);
        }

        // GET: ReviewSites/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewSite reviewSite = db.ReviewSites.Find(id);
            if (reviewSite == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountyName = new SelectList(db.Countys, "CountyName", "CountyName", reviewSite.CountyName);
            ViewBag.FMAHolderName = new SelectList(db.FMAHolders, "FMAHolderName", "FMAHolderName", reviewSite.FMAHolderName);
            ViewBag.NaturalRegionName = new SelectList(db.NaturalRegions, "NaturalRegionName", "NaturalRegionName", reviewSite.NaturalRegionName);
            ViewBag.NaturalSubRegionName = new SelectList(db.NaturalSubRegions, "NaturalSubRegionName", "NaturalSubRegionName", reviewSite.NaturalSubRegionName);
            ViewBag.OperatingAreaName = new SelectList(db.OperatingAreas, "OperatingAreaName", "OperatingAreaName", reviewSite.OperatingAreaName);
            ViewBag.ProvincialAreaName = new SelectList(db.ProvincialAreas, "ProvincialAreaName", "ProvincialAreaName", reviewSite.ProvincialAreaName);
            ViewBag.ProvincialAreaTypeName = new SelectList(db.ProvincialAreaTypes, "ProvincialAreaTypeName", "ProvincialAreaTypeName", reviewSite.ProvincialAreaTypeName);
            return View(reviewSite);
        }

        // POST: ReviewSites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewSiteID,AFE,ProvincialAreaName,ProvincialAreaTypeName,OperatingAreaName,CountyName,NaturalRegionName,NaturalSubRegionName,FMAHolderName,SeedZone,WellboreID,UWI,WellsiteName,UTMZone")] ReviewSite reviewSite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewSite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountyName = new SelectList(db.Countys, "CountyName", "CountyName", reviewSite.CountyName);
            ViewBag.FMAHolderName = new SelectList(db.FMAHolders, "FMAHolderName", "FMAHolderName", reviewSite.FMAHolderName);
            ViewBag.NaturalRegionName = new SelectList(db.NaturalRegions, "NaturalRegionName", "NaturalRegionName", reviewSite.NaturalRegionName);
            ViewBag.NaturalSubRegionName = new SelectList(db.NaturalSubRegions, "NaturalSubRegionName", "NaturalSubRegionName", reviewSite.NaturalSubRegionName);
            ViewBag.OperatingAreaName = new SelectList(db.OperatingAreas, "OperatingAreaName", "OperatingAreaName", reviewSite.OperatingAreaName);
            ViewBag.ProvincialAreaName = new SelectList(db.ProvincialAreas, "ProvincialAreaName", "ProvincialAreaName", reviewSite.ProvincialAreaName);
            ViewBag.ProvincialAreaTypeName = new SelectList(db.ProvincialAreaTypes, "ProvincialAreaTypeName", "ProvincialAreaTypeName", reviewSite.ProvincialAreaTypeName);
            return View(reviewSite);
        }

        // GET: ReviewSites/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewSite reviewSite = db.ReviewSites.Find(id);
            if (reviewSite == null)
            {
                return HttpNotFound();
            }
            return View(reviewSite);
        }

        // POST: ReviewSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ReviewSite reviewSite = db.ReviewSites.Find(id);
            db.ReviewSites.Remove(reviewSite);
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
