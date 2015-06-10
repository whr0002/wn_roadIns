using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wn_web.Controllers.Reclaimation
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error_Create()
        {
            ViewBag.errorMessage = "Errors creating new record.";
            return View();
        }
    }
}