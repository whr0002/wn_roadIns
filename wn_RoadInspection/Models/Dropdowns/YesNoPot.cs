using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models.Dropdowns
{
    public class YesNoPot : BaseDropdown
    {
        public YesNoPot()
            : base()
        {
            options.Add("Yes");
            options.Add("No");
            options.Add("Potential");
        }
    }
}