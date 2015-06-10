using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wn_web.Controllers
{
    /// <summary>
    /// Used for management the site.
    /// </summary>
    [Authorize(Roles="super admin")]
    public class SuperAdminController : Controller
    {
        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}