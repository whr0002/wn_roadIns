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

namespace WN_Reclaimation.Controllers.Reclaimation
{
    public class FormTypesController : Controller
    {
        private wn_webContext db = new wn_webContext();

        // GET: FormTypes
        public ActionResult Index()
        {
            return View(db.FormTypes.ToList());
        }

        // GET: FormTypes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormType formType = db.FormTypes.Find(id);
            if (formType == null)
            {
                return HttpNotFound();
            }
            return View(formType);
        }

        // GET: FormTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormTypeName")] FormType formType)
        {
            if (ModelState.IsValid)
            {
                db.FormTypes.Add(formType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formType);
        }

        // GET: FormTypes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormType formType = db.FormTypes.Find(id);
            if (formType == null)
            {
                return HttpNotFound();
            }
            return View(formType);
        }

        // POST: FormTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormTypeName")] FormType formType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formType);
        }

        // GET: FormTypes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormType formType = db.FormTypes.Find(id);
            if (formType == null)
            {
                return HttpNotFound();
            }
            return View(formType);
        }

        // POST: FormTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FormType formType = db.FormTypes.Find(id);
            db.FormTypes.Remove(formType);
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
