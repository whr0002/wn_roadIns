using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_web.Models;
using wn_web.Models.Reclaimation;
using wn_web.Models.Reclaimation.Report;

namespace WN_Reclaimation.Controllers
{
    [Authorize()]
    public class RawDataController : Controller
    {
        private wn_webContext db = new wn_webContext();
        private ApplicationDbContext context = new ApplicationDbContext();

        public JsonResult SiteVisit()
        {        

            //var data = from sv in db.SiteVisitReports
            //           join dr in db.DesktopReviews on
            //           new
            //           {
            //               j1 = sv.ReviewSiteID,
            //               j2 = sv.FacilityTypeName
            //           }
            //           equals
            //           new
            //           {
            //               j1 = dr.SiteID,
            //               j2 = dr.FacilityTypeName
            //           }
            //           select new { sv.SiteVisitReportID, sv.ReviewSiteID, sv.FacilityTypeName, dr.Latitude, dr.Longitude};
            var role = getUserRole();
            if (role != null)
            {
                if (role.Equals("super admin")) { 
                    var data = db.SiteVisitReports.ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = db.SiteVisitReports.Where(w => w.Group.Equals(role, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Images(int formID, string formType)
        {
            if (formType != null) {
                // Get current user role
                var role = getUserRole();
                var temp = db.SiteVisitReports.Where(w => w.SiteVisitReportID == formID).FirstOrDefault();
                if (temp != null)
                {
                    var tempRole = temp.Group;

                    // Check if current role has access to the data
                    if (tempRole != null && role != null && tempRole.Equals(role))
                    {
                        var data = db.Photos.Where(w => w.FormID == formID && w.FormTypeName.Equals(formType)).ToList();
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }


            }

            return null;
        }

            
        public ViewResult SiteVisitReport(int? ID)
        {
            if (ID != null) { 

                SiteVisitReport report = db.SiteVisitReports.Find(ID);
                List<Photo> photos = db.Photos.Where(w => w.FormID == ID && w.FormTypeName.Equals("SiteVisit")).ToList();

                if (report != null) { 
                    
                    ReviewSite common = db.ReviewSites.Where(w => w.ReviewSiteID.Equals(report.ReviewSiteID)).FirstOrDefault();
                }
                ViewBag.report = report;
                ViewBag.photos = photos;
                //ViewBag.common = common;

            }
            return View();

        }

        private string getUserRole()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                List<string> roles = userManager.GetRoles(id) as List<string>;
                if (roles != null && roles.Count() > 0)
                {
                    return roles[0];
                }
            }
            return null;
        }
    }
}