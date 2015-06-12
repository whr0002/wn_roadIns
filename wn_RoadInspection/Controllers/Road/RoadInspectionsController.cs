using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_RoadInspection.Models.RoadInspection;
using wn_web.Models;

namespace wn_RoadInspection.Controllers.Road
{
    public class RoadInspectionsController : Controller
    {
        private wn_webContext db = new wn_webContext();

        // GET: RoadInspections
        public ActionResult Index()
        {
            return View(db.RoadInspections.ToList());
        }

        // GET: RoadInspections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoadInspection roadInspection = db.RoadInspections.Find(id);
            if (roadInspection == null)
            {
                return HttpNotFound();
            }
            return View(roadInspection);
        }

        // GET: RoadInspections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoadInspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoadInspectionID,UserName,Group,Client,InspectorName,INSP_DATE,Licence,RoadName,DLO,KmFrom,KmTo,RoadStatus,StatusMatch,RS_Condition,RS_Notification,RS_RoadSurface,RS_GravelCondition,RS_VegetationCover,RS_CoverType,DI_Ditches,DI_VegetationCover,DI_CoverType,OT_Signage,OT_Crossings,OT_GroundAccess,OT_RoadMR,OT_RoadRIA,OT_Comments,Locations")] RoadInspection roadInspection)
        {
            if (ModelState.IsValid)
            {
                db.RoadInspections.Add(roadInspection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roadInspection);
        }

        // GET: RoadInspections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoadInspection roadInspection = db.RoadInspections.Find(id);
            if (roadInspection == null)
            {
                return HttpNotFound();
            }
            return View(roadInspection);
        }

        // POST: RoadInspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoadInspectionID,UserName,Group,Client,InspectorName,INSP_DATE,Licence,RoadName,DLO,KmFrom,KmTo,RoadStatus,StatusMatch,RS_Condition,RS_Notification,RS_RoadSurface,RS_GravelCondition,RS_VegetationCover,RS_CoverType,DI_Ditches,DI_VegetationCover,DI_CoverType,OT_Signage,OT_Crossings,OT_GroundAccess,OT_RoadMR,OT_RoadRIA,OT_Comments,Locations")] RoadInspection roadInspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roadInspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roadInspection);
        }

        // GET: RoadInspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoadInspection roadInspection = db.RoadInspections.Find(id);
            if (roadInspection == null)
            {
                return HttpNotFound();
            }
            return View(roadInspection);
        }

        // POST: RoadInspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoadInspection roadInspection = db.RoadInspections.Find(id);
            db.RoadInspections.Remove(roadInspection);
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
