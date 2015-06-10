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
    public class SearchTypesController : Controller
    {
        private wn_webContext db = new wn_webContext();

        // GET: SearchTypes
        public ActionResult Index()
        {
            return View(db.SearchTypes.ToList());
        }

        // GET: SearchTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchType searchType = db.SearchTypes.Find(id);
            if (searchType == null)
            {
                return HttpNotFound();
            }
            return View(searchType);
        }

        // GET: SearchTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SearchTypeName")] SearchType searchType)
        {
            if (ModelState.IsValid)
            {
                db.SearchTypes.Add(searchType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(searchType);
        }

        // GET: SearchTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchType searchType = db.SearchTypes.Find(id);
            if (searchType == null)
            {
                return HttpNotFound();
            }
            return View(searchType);
        }

        // POST: SearchTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SearchTypeName")] SearchType searchType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(searchType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(searchType);
        }

        // GET: SearchTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchType searchType = db.SearchTypes.Find(id);
            if (searchType == null)
            {
                return HttpNotFound();
            }
            return View(searchType);
        }

        // POST: SearchTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SearchType searchType = db.SearchTypes.Find(id);
            db.SearchTypes.Remove(searchType);
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
