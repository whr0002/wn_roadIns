using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Risk { get; set; }
        public string Client { get; set; }
    }
}