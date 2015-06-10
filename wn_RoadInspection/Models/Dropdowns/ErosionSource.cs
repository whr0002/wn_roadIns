using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models.Dropdowns
{
    public class ErosionSource : BaseDropdown
    {
        public ErosionSource()
            : base()
        {
            options.Add("Ditch");
            options.Add("Bank slump");
            options.Add("Fill slope");
            options.Add("Road surface");
            options.Add("Bridge deck");
            options.Add("Other");
        }
    }
}