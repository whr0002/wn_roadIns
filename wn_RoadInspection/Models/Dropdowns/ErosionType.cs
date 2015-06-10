using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class Erosion
    {
        public List<string> options;
        public Erosion()
        {
            options = new List<string>();
            options.Add("Yes");
            options.Add("No");
            options.Add("Potential");
        }
        public List<string> getOptions(){
            return options;
        }
    }
}