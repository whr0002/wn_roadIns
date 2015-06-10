using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_web.Models.Reclaimation
{
    public class DesktopReview
    {
        public int DesktopReviewID { get; set; }

        [Required]
        public string SiteID { get; set; }

        public string FacilityTypeName { get; set; }

        public string Notes { get; set; }

        public string Client { get; set; }

        public string ApprovalStatus { get; set; }

        public string WorkPhase { get; set; }

        public string Occupant { get; set; }

        public string OccupantInfo { get; set; }

        public string SoilClass { get; set; }

        public string SoilGroup { get; set; }

        public string ERCBLic { get; set; }

        public double? Width { get; set; }

        public double? Length { get; set; }

        public double? AreaHA { get; set; }

        public double? AreaAC { get; set; }

        public double? Northing { get; set; }

        public double? Easting { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Elevation { get; set; }


        public string AspectName { get; set; }

        public string LSD { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? SurveyDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? ConstructionDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? SpudDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? AbandonmentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? ReclamationDate { get; set; }

        public string RelevantCriteriaName { get; set; }

        public string LandscapeName { get; set; }

        public string SoilName { get; set; }

        public string VegetationName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? RCADate { get; set; }

        public string RCNumber { get; set; }

        public string DSAComments { get; set; }
        public string Exemptions { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? AmendDate  { get; set; }

        public string AmendDetail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime? RevegDate { get; set; }

        public string RevegDetail { get; set; }



        [ForeignKey("SiteID")]
        public virtual ReviewSite Site { get; set; }
        public virtual FacilityType FacilityType { get; set; }
        public virtual Aspect LSDQuarter { get; set; }       
        public virtual RelevantCriteria RelevantCriteria { get; set; }
        public virtual Landscape Landscape { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual Vegetation Vegetation { get; set; }



    }
}