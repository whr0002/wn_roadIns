using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_web.Models.Reclaimation.Report
{
    public class SiteVisitReport
    {
        public int SiteVisitReportID { get; set; }

        public string ReviewSiteID { get; set; }
        public string FacilityTypeName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Username { get; set; }

        public string Group { get; set; }

        public string Client { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string RefusePF { get; set; }
        public string RefuseComment { get; set; }
        public string DrainagePF { get; set; }
        public string DrainageComment { get; set; }
        public string RockGravelPF { get; set; }
        public string RockGravelComment { get; set; }
        public string BareGroundPF { get; set; }
        public string BareGroundComment { get; set; }
        public string SoilStabilityPF { get; set; }
        public string SoilStabilityComment { get; set; }
        public string ContoursPF { get; set; }
        public string ContoursComment { get; set; }
        public string CWDPF { get; set; }
        public string CWDComment { get; set; }
        public string ErosionPF { get; set; }
        public string ErosionComment { get; set; }

        public string SoilCharPF { get; set; }
        public string SoilCharComment { get; set; }
        public string TopsoilDepthPF { get; set; }
        public string TopsoilDepthComment { get; set; }
        public string RootingPF { get; set; }
        public string RootingComment { get; set; }
        public string WSDPF { get; set; }
        public string WSDComment { get; set; }
        public string TreeHealthPF { get; set; }
        public string TreeHealthComment { get; set; }
        public string WeedsInvasivesPF { get; set; }
        public string WeedsInvasivesComment { get; set; }
        public string NSCPF { get; set; }
        public string NSCComment { get; set; }
        public string LitterPF { get; set; }
        public string LitterComment { get; set; }

        public string Recommendation { get; set; }


        public virtual ReviewSite ReviewSite { get; set; }
        public virtual FacilityType FacilityType { get; set; }
    }
}