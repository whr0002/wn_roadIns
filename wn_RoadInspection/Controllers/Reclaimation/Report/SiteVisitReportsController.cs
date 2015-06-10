using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_web.Models;
using wn_web.Models.Reclaimation.Report;

namespace WN_Reclaimation.Controllers.Reclaimation.Report
{
    public class SiteVisitReportsController : Controller
    {
        private wn_webContext db = new wn_webContext();

        // GET: SiteVisitReports
        public ActionResult Index()
        {
            var siteVisitReports = db.SiteVisitReports.Include(s => s.FacilityType).Include(s => s.ReviewSite);
            return View(siteVisitReports.ToList());
        }

        // GET: SiteVisitReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteVisitReport siteVisitReport = db.SiteVisitReports.Find(id);
            if (siteVisitReport == null)
            {
                return HttpNotFound();
            }
            return View(siteVisitReport);
        }

        // GET: SiteVisitReports/Create
        public ActionResult Create()
        {
            ViewBag.FacilityTypeName = new SelectList(db.FacilityTypes, "FacilityTypeName", "FacilityTypeName");
            ViewBag.ReviewSiteID = new SelectList(db.ReviewSites, "ReviewSiteID", "ReviewSiteID");
            return View();
        }

        // POST: SiteVisitReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteVisitReportID,ReviewSiteID,FacilityTypeName,Date,Username,Group,Client,Latitude,Longitude,RefusePF,RefuseComment,DrainagePF,DrainageComment,RockGravelPF,RockGravelComment,BareGroundPF,BareGroundComment,SoilStabilityPF,SoilStabilityComment,ContoursPF,ContoursComment,CWDPF,CWDComment,ErosionPF,ErosionComment,SoilCharPF,SoilCharComment,TopsoilDepthPF,TopsoilDepthComment,RootingPF,RootingComment,WSDPF,WSDComment,TreeHealthPF,TreeHealthComment,WeedsInvasivesPF,WeedsInvasivesComment,NSCPF,NSCComment,LitterPF,LitterComment,Recommendation")] SiteVisitReport siteVisitReport)
        {
            if (ModelState.IsValid)
            {
                db.SiteVisitReports.Add(siteVisitReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacilityTypeName = new SelectList(db.FacilityTypes, "FacilityTypeName", "FacilityTypeName", siteVisitReport.FacilityTypeName);
            ViewBag.ReviewSiteID = new SelectList(db.ReviewSites, "ReviewSiteID", "ReviewSiteID", siteVisitReport.ReviewSiteID);
            return View(siteVisitReport);
        }

        // GET: SiteVisitReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteVisitReport siteVisitReport = db.SiteVisitReports.Find(id);
            if (siteVisitReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacilityTypeName = new SelectList(db.FacilityTypes, "FacilityTypeName", "FacilityTypeName", siteVisitReport.FacilityTypeName);
            ViewBag.ReviewSiteID = new SelectList(db.ReviewSites, "ReviewSiteID", "ReviewSiteID", siteVisitReport.ReviewSiteID);
            return View(siteVisitReport);
        }

        // POST: SiteVisitReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteVisitReportID,ReviewSiteID,FacilityTypeName,Date,Username,Group,Client,Latitude,Longitude,RefusePF,RefuseComment,DrainagePF,DrainageComment,RockGravelPF,RockGravelComment,BareGroundPF,BareGroundComment,SoilStabilityPF,SoilStabilityComment,ContoursPF,ContoursComment,CWDPF,CWDComment,ErosionPF,ErosionComment,SoilCharPF,SoilCharComment,TopsoilDepthPF,TopsoilDepthComment,RootingPF,RootingComment,WSDPF,WSDComment,TreeHealthPF,TreeHealthComment,WeedsInvasivesPF,WeedsInvasivesComment,NSCPF,NSCComment,LitterPF,LitterComment,Recommendation")] SiteVisitReport siteVisitReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteVisitReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityTypeName = new SelectList(db.FacilityTypes, "FacilityTypeName", "FacilityTypeName", siteVisitReport.FacilityTypeName);
            ViewBag.ReviewSiteID = new SelectList(db.ReviewSites, "ReviewSiteID", "ReviewSiteID", siteVisitReport.ReviewSiteID);
            return View(siteVisitReport);
        }

        // GET: SiteVisitReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteVisitReport siteVisitReport = db.SiteVisitReports.Find(id);
            if (siteVisitReport == null)
            {
                return HttpNotFound();
            }
            return View(siteVisitReport);
        }

        // POST: SiteVisitReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteVisitReport siteVisitReport = db.SiteVisitReports.Find(id);
            db.SiteVisitReports.Remove(siteVisitReport);
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
