using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class CrossingType
    {
        public List<string> options;
        public CrossingType()
        {
            options = new List<string>();
            options.Add("Bridge - permanent");
            options.Add("Bridge - temporary");
            options.Add("Culvert - single");
            options.Add("Culvert - multiple");
            options.Add("Culvert - open Bottom");
            options.Add("Log - fill");
            options.Add("Snow - fill");
            options.Add("Ford");
            options.Add("Suspended");
        }
        public List<string> getOptions(){
            return options;
        }
    }
}