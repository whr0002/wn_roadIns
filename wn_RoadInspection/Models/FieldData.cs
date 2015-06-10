using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class FieldData
    {
        
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        //[Index("ix_Group", IsClustered=false, IsUnique=false)]
        [Required]
        public string Group { get; set; }

        public string Client { get; set; }

        [Required]
        [DisplayName("Inspection Date: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        public DateTime INSP_DATE { get; set; }

        [DisplayName("Time Stamp")]
        public string TimeStamp {get; set;}

        [Required]
        [DisplayName("Crew: ")]
        public string INSP_CREW { get; set; }

        [DisplayName("Access: ")]
        public string ACCESS { get; set; }

        [DisplayName("Water Crossing Name or ID: ")]
        public string CROSS_NM { get; set; }

        [DisplayName("Water Crossing Name: ")]
        public string CROSS_ID { get; set; }



        [Required]
        [DisplayName("Latitude")]
        //[DisplayFormat(DataFormatString = "{0:#.######}")]
        public double LAT { get; set; }

        [Required]
        [DisplayName("Longitude")]
        public double LONG { get; set; }


        [DisplayName("Stream ID")]
        public string STR_ID { get; set; }



        [DisplayName("Stream Classification: ")]
        public string STR_CLASS { get; set; }

        [DisplayName("Bankfull Width: ")]
        public double? STR_WIDTH { get; set; }

        [DisplayName("Bankfull Width measured?")]
        public string STR_WIDTHM { get; set; }

        [DisplayName("Channel Creek Depth Left: ")]
        public double? CHANNEL_CREEK_DEPTH_LEFT { get; set; }
        [DisplayName("Channel Creek Depth Right: ")]
        public double? CHANNEL_CREEK_DEPTH_RIGHT { get; set; }
        [DisplayName("Channel Creek Depth Center: ")]
        public double? CHANNEL_CREEK_DEPTH_CENTER { get; set; }
        [DisplayName("First Riffle Distance: ")]
        public double? FIRST_RIFFLE_DISTANCE { get; set; }
        [DisplayName("Road Fill Above Culvert: ")]
        public double? ROAD_FILL_ABOVE_CULVERT { get; set; }

        [DisplayName("Disposition ID")]
        public string DISPOSITION_ID { get; set; }


        [Required]
        [DisplayName("Crossing Type: ")]
        public string CROSS_TYPE { get; set; }

        [DisplayName("Erosion at Site?")]
        public string EROSION { get; set; }

        [DisplayName("Erosion Type 1")]
        public string EROSION_TY1 { get; set; }

        [DisplayName("Erosion Type 2")]
        public string EROSION_TY2 { get; set; }

        [DisplayName("Source of Erosion: ")]
        public string EROSION_SO { get; set; }

        [DisplayName("Degree of Erosion: ")]
        public string EROSION_DE { get; set; }




        [DisplayName("Area of Erosion: ")]
        public double? EROSION_AR { get; set; }



        [DisplayName("Blockage: ")]
        public string BLOCKAGE { get; set; }

        [DisplayName("Blocking Material: ")]
        public string BLOC_MATR { get; set; }

        [DisplayName("Blocking Cause: ")]
        public string BLOC_CAUS { get; set; }

        [DisplayName("Culvert Substrate: ")]
        public string CULV_SUBS { get; set; }

        [DisplayName("Culvert Slope: ")]
        public string CULV_SLOPE { get; set; }


        [DisplayName("Scour Pool Present: ")]
        public string SCOUR_POOL { get; set; }

        [DisplayName("Delineators: ")]
        public string DELINEATOR { get; set; }

        [DisplayName("Fish Sampling: ")]
        public string FISH_SAMP { get; set; }

        [DisplayName("Sampling Method: ")]
        public string FISH_SM { get; set; }

        [DisplayName("Fish Species 1: ")]
        public string FISH_SPP { get; set; }


        [DisplayName("Fish Passage Concerns: ")]
        public string FISH_PCONC { get; set; }

        [DisplayName("Fish Species 2: ")]
        public string FISH_SPP2 { get; set; }

        [DisplayName("Fish Passage Concern Reason")]
        public string FISH_PCONCREASON { get; set; }

        [DisplayName("Remarks: ")]
        public string REMARKS { get; set; }




        [DisplayName("Photo Inlet Upstream: ")]
        public string PHOTO_INUP { get; set; }

        [DisplayName("Photo Inlet Downstream: ")]
        public string PHOTO_INDW { get; set; }

        [DisplayName("Photo Outlet Upstream: ")]
        public string PHOTO_OTUP { get; set; }

        [DisplayName("Photo Outlet Downstream: ")]
        public string PHOTO_OTDW { get; set; }

        [DisplayName("Photo Road Left: ")]
        public string PHOTO_ROAD_LEFT { get; set; }

        [DisplayName("Photo Road Right: ")]
        public string PHOTO_ROAD_RIGHT { get; set; }

        [DisplayName("Photo Other 1: ")]
        public string PHOTO_1 { get; set; }

        [DisplayName("Photo Other 2: ")]
        public string PHOTO_2 { get; set; }






        [DisplayName("Culvert Length: ")]
        public double? CULV_LEN { get; set; }
        [DisplayName("Culvert Substrate Proportion: ")]
        public string CULV_SUBSP { get; set; }
        [DisplayName("Substrate Type: ")]
        public string CULV_SUBSTYPE { get; set; }
        [DisplayName("For what length of culvert?")]
        public string CULV_SUBSPROPORTION { get; set; }
        [DisplayName("What proportion of back water?")]
        public string CULV_BACKWATERPROPORTION { get; set; }
        [DisplayName("Culvert Outlet Type: ")]
        public string CULV_OUTLETTYPE { get; set; }


        [DisplayName("Culvert Diameter 1")]
        public double? CULV_DIA_1 { get; set; }

        [DisplayName("Culvert Diameter 2")]
        public double? CULV_DIA_2 { get; set; }

        [DisplayName("Culvert Diameter 3")]
        public double? CULV_DIA_3 { get; set; }


        [DisplayName("Bridge Length: ")]
        public double? BRDG_LEN { get; set; }



        [DisplayName("Emergency Respairs Req: ")]
        public string EMG_REP_RE { get; set; }

        [DisplayName("Structural Problems: ")]
        public string STU_PROBS { get; set; }


        [DisplayName("Sedimentation: ")]
        public string SEDEMENTAT { get; set; }

        [DisplayName("Culvert Pool Depth: ")]
        public double? CULV_OPOOD { get; set; }

        [DisplayName("Culvert Outlet Gap: ")]
        public double? CULV_OPGAP { get; set; }

        [DisplayName("Bridge Hazard Markers: ")]
        public string HAZMARKR { get; set; }

        [DisplayName("Bridge Approach Signs")]
        public string APROCHSIGR { get; set; }

        public string APROCHRAIL { get; set; }

        [DisplayName("Bridge Road Surface: ")]
        public string RDSURFR { get; set; }
        [DisplayName("Bridge Road Drainage")]
        public string RDDRAINR { get; set; }

        public string VISIBILITY { get; set; }
        [DisplayName("Bridge Wearing Surface")]
        public string WEARSURF { get; set; }

        [DisplayName("Railing: ")]
        public string RAILCURBR { get; set; }
        [DisplayName("Bridge Girders & Bracing")]
        public string GIRDEBRACR { get; set; }

        [DisplayName("Bridge Cap Beam")]
        public string CAPBEAMR { get; set; }

        [DisplayName("Bridge Piles")]
        public string PILESR { get; set; }

        [DisplayName("Abutment: ")]
        public string ABUTWALR { get; set; }
        [DisplayName("Bridge Wing Wall")]
        public string WINGWALR { get; set; }
        [DisplayName("Bridge Bank Stability")]
        public string BANKSTABR { get; set; }
        [DisplayName("Bridge Slope Protection")]
        public string SLOPEPROTR { get; set; }
        [DisplayName("Bridge Channel Opening")]
        public string CHANNELOPEN { get; set; }
        [DisplayName("Bridge Obstructions")]
        public string OBSTRUCTIO { get; set; }

        [DisplayName("Attachment")]
        public string ATTACHMENT { get; set; }
        public string FUTURE2 { get; set; }
        public string FUTURE3 { get; set; }
        public string FUTURE4 { get; set; }
        public string FUTURE5 { get; set; }

        [DisplayName("Culvert Substrate Type 1")]
        public string CULV_SUBSTYPE1 { get; set; }
        [DisplayName("Culvert Substrate Type 2")]
        public string CULV_SUBSTYPE2 { get; set; }
        [DisplayName("Culvert Substrate Type 2")]
        public string CULV_SUBSTYPE3 { get; set; }

        [DisplayName("Culvert Substrate Proportion 1")]
        public string CULV_SUBSPROPORTION1 { get; set; }
        [DisplayName("Culvert Substrate Proportion 1")]
        public string CULV_SUBSPROPORTION2 { get; set; }
        [DisplayName("Culvert Substrate Proportion 1")]
        public string CULV_SUBSPROPORTION3 { get; set; }

        [DisplayName("Outlet Score")]
        public string OUTLET_SCORE { get; set; }

        [DisplayName("Risk Factor")]
        public double? RISKF { get; set; }
        [DisplayName("Risk")]
        public string RISK { get; set; }
        
    }
}