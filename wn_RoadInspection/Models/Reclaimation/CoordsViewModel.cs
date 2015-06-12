using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wn_RoadInspection.Models.RoadInspection;
using wn_web.Models.Reclaimation.Report;

namespace wn_web.Models.Reclaimation
{
    public class CoordsViewModel
    {
        public int ID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Client { get; set; }
    }

    public class RoadInspectionViewModel
    {
        public int ID { get; set; }
        public String Path { get; set; }
        public String Client { get; set; }
    }

    public class OneRowRoadInspectionViewModel
    {
        public RoadInspection RoadInspection { get; set; }
        public List<Photo> Photos { get; set; }
    }
}