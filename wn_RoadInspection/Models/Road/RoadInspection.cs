using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_RoadInspection.Models.RoadInspection
{
    public class RoadInspection
    {
        public int RoadInspectionID { get; set; }
        public String UserName{get; set;}
        public String Group{get; set;}
        public String Client{get; set;}

        [DisplayName("Inspector Name")]
        public String InspectorName{get; set;}

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime INSP_DATE{get; set;}

        public String Licence{get; set;}

        [DisplayName("Road Name")]
        public String RoadName{get; set;}

        [DisplayName("DLO #")]
        public String DLO{get; set;}

        [DisplayName("Km's From")]
        public String KmFrom{get; set;}

        [DisplayName("To")]
        public String KmTo{get; set;}

        [DisplayName("Road Status")]
        public String RoadStatus{get; set;}

        [DisplayName("Status matches GIS System?")]
        public String StatusMatch{get; set;}

        [DisplayName("Condition (overall)")]
        public String RS_Condition{get; set;}

        [DisplayName("Notification Required?")]
        public String RS_Notification{get; set;}

        [DisplayName("Road Surface")]
        public String RS_RoadSurface{get; set;}

        [DisplayName("Gravel Condition")]
        public String RS_GravelCondition{get; set;}

        [DisplayName("Vegetation Cover")]
        public String RS_VegetationCover{get; set;}

        [DisplayName("Cover Type")]
        public String RS_CoverType{get; set;}

        [DisplayName("Ditches")]
        public String DI_Ditches{get; set;}
        [DisplayName("Vegetation Cover")]
        public String DI_VegetationCover{get; set;}
        [DisplayName("Cover Type")]
        public String DI_CoverType{get; set;}

        [DisplayName("Signage")]
        public String OT_Signage{get; set;}
        [DisplayName("Crossings")]
        public String OT_Crossings{get; set;}
        [DisplayName("Ground Access")]
        public String OT_GroundAccess{get; set;}
        [DisplayName("Road maintenance required?")]
        public String OT_RoadMR{get; set;}
        [DisplayName("Road requires immediate action*?")]
        public String OT_RoadRIA{get; set;}
        [DisplayName("Comments")]
        public String OT_Comments{get; set;}

        public String Locations{get; set;}
    }
}