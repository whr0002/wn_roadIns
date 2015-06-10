using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class Risk
    {
        public List<string> options;
        public Risk()
        {
            options = new List<string>();
            options.Add("High");
            options.Add("Mod");
            options.Add("Low");
            options.Add("No");
        }
        public List<string> getOptions()
        {
            return options;
        }
    }
}