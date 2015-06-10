using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class BaseDropdown
    {
        public List<string> options { get; set; }

        public BaseDropdown()
        {
            options = new List<string>();
        }

        public List<string> getOptions()
        {
            return options;
        }

    }
}