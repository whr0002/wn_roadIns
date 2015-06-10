using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models.Dropdowns
{
    public class ErosionTY : BaseDropdown
    {
        public ErosionTY()
            : base()
        {
            options.Add("Inlet");
            options.Add("Outlet");
            options.Add("Both");
        }
    }
}