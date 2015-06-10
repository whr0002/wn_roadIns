using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class StreamType
    {
        public List<string> options;
        public StreamType()
        {
            options = new List<string>();
            options.Add("Ephemeral");
            options.Add("Non-fluvial");
            options.Add("Fluvial - intermittent");
            options.Add("Fluvial - small permanent");
            options.Add("Fluvial - large permanent");
        }
        public List<string> getOptions(){
            return options;
        }
    }
}