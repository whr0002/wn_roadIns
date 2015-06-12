using System;
using System.Collections.Generic;
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
        public String InspectorName{get; set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode=true)]
        public DateTime INSP_DATE{get; set;}

        public String Licence{get; set;}
        public String RoadName{get; set;}
        public String DLO{get; set;}
        public String KmFrom{get; set;}
        public String KmTo{get; set;}
        public String RoadStatus{get; set;}
        public String StatusMatch{get; set;}
        public String RS_Condition{get; set;}
        public String RS_Notification{get; set;}
        public String RS_RoadSurface{get; set;}
        public String RS_GravelCondition{get; set;}
        public String RS_VegetationCover{get; set;}
        public String RS_CoverType{get; set;}

        public String DI_Ditches{get; set;}
        public String DI_VegetationCover{get; set;}
        public String DI_CoverType{get; set;}

        public String OT_Signage{get; set;}
        public String OT_Crossings{get; set;}
        public String OT_GroundAccess{get; set;}
        public String OT_RoadMR{get; set;}
        public String OT_RoadRIA{get; set;}
        public String OT_Comments{get; set;}

        public String Locations{get; set;}
    }
}