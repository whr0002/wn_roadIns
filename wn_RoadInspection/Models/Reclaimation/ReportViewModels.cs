using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wn_web.Models.Reclaimation;
using wn_web.Models.Reclaimation.Report;

namespace WN_Reclaimation.Models.Reclaimation
{
    public class SiteVisitViewModel
    {
        public SiteVisitReport report { get; set; }
        public List<Photo> photos { get; set; }
        public ReviewSite common { get; set; }
    }
}