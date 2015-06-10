using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class Access : BaseDropdown
    {

        public Access() : base()
        {
            
            options.Add("ATV");
            options.Add("Foot");
            options.Add("Truck");
        }

    }
}