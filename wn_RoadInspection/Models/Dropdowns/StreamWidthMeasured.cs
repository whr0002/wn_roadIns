using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class StreamWidthMeasured : BaseDropdown
    {
        public StreamWidthMeasured()
            : base()
        {
            options.Add("Estimated");
            options.Add("Measured");
        }
    }
}