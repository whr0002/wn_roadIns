using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models.Dropdowns
{
    public class ErosionDegree : BaseDropdown
    {
        public ErosionDegree()
            : base()
        {
            options.Add("Low");
            options.Add("Moderate");
            options.Add("High");
        }
    }
}