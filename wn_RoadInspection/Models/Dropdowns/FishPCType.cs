using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class FishPCType
    {
        public List<string> options;
        public FishPCType()
        {
            options = new List<string>();
            options.Add("Hanging culvert");
            options.Add("Blocked culvert");
            options.Add("Perched culvert");
        }
        public List<string> getOptions(){
            return options;
        }
    }
}