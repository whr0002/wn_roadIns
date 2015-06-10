using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models.Reclaimation
{
    public class CoordsViewModel
    {
        public int ID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Client { get; set; }
    }
}