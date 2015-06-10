using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_web.Models.Reclaimation
{
    public class ReviewSite
    {
        [Required]
        public string ReviewSiteID { get; set; }

        public string DataOwner { get; set; }

        public string DispositionNumber { get; set; }

        public string SWPNumber { get; set; }

        public string AFE { get; set; }

        public string ProvincialAreaName { get; set; }

        public string ProvincialAreaTypeName { get; set; }

        public string OperatingAreaName { get; set; }

        public string CountyName { get; set; }

        public string NaturalRegionName { get; set; }

        public string NaturalSubRegionName { get; set; }

        public string FMAHolderName { get; set; }

        public string SeedZone { get; set; }

        public string WellboreID { get; set; }

        public string UWI { get; set; }

        public string WellsiteName { get; set; }

        public string UTMZone { get; set; }



        public virtual ProvincialArea ProvincialArea { get; set; }
        public virtual ProvincialAreaType ProvincialAreaType { get; set; }
        public virtual OperatingArea OperatingArea { get; set; }
        public virtual County County { get; set; }
        public virtual NaturalRegion NaturalRegion { get; set; }
        public virtual NaturalSubRegion NaturalSubRegion { get; set; }
        public virtual FMAHolder FMAHolder { get; set; }

    }
}