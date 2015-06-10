using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using wn_web.Models;

namespace wn_web.Controllers
{
    /// <summary>
    /// This controller is used for Home page
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private wn_webContext kContext = new wn_webContext(); 
        
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            
            string currentUserId = User.Identity.GetUserId();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            List<Kml> kmls = new List<Kml>();

            setDropdowns();
            List<string> roles = new List<string>();
            roles = userManager.GetRoles(currentUserId) as List<string>;
            if (roles.Count() > 0)
            {
                if (userManager.IsInRole(currentUserId, "super admin"))
                {
                    ViewBag.isSA = "Yes";
                    kmls = kContext.Kmls.OrderBy(o => o.Name).Where(w => !w.Name.Contains(".zip")).ToList();
                }
                else
                {
                    var role = roles[0];
                    kmls = kContext
                        .Kmls
                        .OrderBy(o => o.Name)
                        .Where(k => k.Client.Equals(role, 
                            StringComparison.CurrentCultureIgnoreCase) && !k.Name.Contains(".zip"))
                        .ToList();
                }
            }
            return View(kmls);
            
        }

        private void setDropdowns()
        {
            var searchTypes = kContext.SearchTypes.OrderBy(s => s.SearchTypeName).ToList()
                .Select(a => new SelectListItem { Value = a.SearchTypeName.ToString(), Text = a.SearchTypeName }).ToList();
            ViewBag.SearchTypes = searchTypes;

            List<SearchType> sts = kContext.SearchTypes.OrderBy(s => s.SearchTypeName).ToList();
            List<AdvancedSearchItem> advList = new List<AdvancedSearchItem>();
            for (int i = 0; i < sts.Count;i++ )
            {
                AdvancedSearchItem item = new AdvancedSearchItem();
                List<string> listItems = new List<string>();
                item.Name = sts[i].SearchTypeName;
                string typel = sts[i].SearchTypeName.ToLower();
                if (typel.Equals("risk"))
                {           
                    Risk r = new Risk();                   
                    item.Options = r.getOptions();

                }
                else if (typel.Equals("crossing type"))
                {
                    CrossingType r = new CrossingType();
                    item.Options = r.getOptions();

                }
                else if (typel.Equals("erosion"))
                {
                    Erosion r = new Erosion();
                    item.Options = r.getOptions();

                }
                else if (typel.Equals("fish passage concerns"))
                {
                    FishPCType r = new FishPCType();
                    item.Options = r.getOptions();

                }
                else if (typel.Equals("sedimentation"))
                {
                    Sedimentation r = new Sedimentation();
                    item.Options = r.getOptions();

                }
                else if (typel.Equals("stream type"))
                {
                    StreamType r = new StreamType();
                    item.Options = r.getOptions();

                }
                else if (typel.Equals("safety concerns"))
                {
                    SaftyConcernsType r = new SaftyConcernsType();
                    item.Options = r.getOptions();
                }

                advList.Add(item);

            }
            ViewBag.AdvanceSearch = advList;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}