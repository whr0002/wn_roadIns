using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_web.Models
{
    public class Record
    {
        public string ID { get; set; }

        public string GROUP { get; set; }

        public string UserName { get; set; }

        public string TimeStamp { get; set; }

        [DisplayName("Client")]
        public string CLIENT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        [DisplayName("Inspection Date: ")]
        public DateTime INSP_DATE { get; set; }

        [DisplayName("Crew: ")]
        public string INSP_CREW { get; set; }

        [DisplayName("Access: ")]
        public string ACCESS { get; set; }

        [DisplayName("Water Crossing Name or ID: ")]
        public string CROSS_NM { get; set; }

        [DisplayName("Water Crossing Name: ")]
        public string CROSS_ID { get; set; }

        [DisplayName("Disposition No.: ")]
        public string CROSS_STR { get; set; }

        [DisplayName("Latitude")]
        public string LAT { get; set; }

        [DisplayName("Longitude")]
        public string LONG { get; set; }

        [DisplayName("Northing")]
        public string NORT { get; set; }
        [DisplayName("Easting")]
        public string EAST { get; set; }

        public string STR_ID { get; set; }

        [DisplayName("Stream Classification: ")]
        public string STR_CLASS { get; set; }

        [DisplayName("Bankfull Width: ")]
        public string STR_WIDTH { get; set; }
        public string DISPOSITION_ID { get; set; }
        public string EROSION_TY1 { get; set; }
        public string EROSION_TY2 { get; set; }
        [DisplayName("Bankfull Width measured?")]
        public string STR_WIDTHM { get; set; }

        [DisplayName("Crossing Type: ")]
        public string CROSS_TYPE { get; set; }

        [DisplayName("Erosion at Site?")]
        public string EROSION { get; set; }

        [DisplayName("Location of Erosion: ")]
        public string EROSION_TY { get; set; }

        [DisplayName("Source of Erosion: ")]
        public string EROSION_SO { get; set; }

        [DisplayName("Degree of Erosion: ")]
        public string EROSION_DE { get; set; }

        [DisplayName("Area of Erosion: ")]
        public string EROSION_AR { get; set; }

        [DisplayName("Blockage: ")]
        public string BLOCKAGE { get; set; }

        [DisplayName("Blocking Material: ")]
        public string BLOC_MATR { get; set; }

        [DisplayName("Blocking Cause: ")]
        public string BLOC_CAUS { get; set; }

        [DisplayName("Culvert Substrate: ")]
        public string CULV_SUBS { get; set; }

        [DisplayName("Greater than 10% of diameter blocked by debris")]
        public string C_SUBS_D { get; set; }

        [DisplayName("Substrate Type: ")]
        public string CULV_SUBSTYPE { get; set; }

        [DisplayName("For what length of culvert?")]
        public string CULV_SUBSPROPORTION { get; set; }

        [DisplayName("What proportion of back water?")]
        public string C_BCKWT_PR { get; set; }

        [DisplayName("Culvert Slope: ")]
        public string CULV_SLOPE { get; set; }

        [DisplayName("Culvert Outlet Type: ")]
        public string CULV_OUTLE { get; set; }

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

        [DisplayName("Fish Passage: ")]
        public string FISH_PASS { get; set; }

        [DisplayName("Fish Passage Concerns: ")]
        public string FISH_PCONC { get; set; }

        [DisplayName("Fish Species 2: ")]
        public string FISH_SPP2 { get; set; }

        
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

        [DisplayName("Photo Other 1: ")]
        public string PHOTO_1 { get; set; }

        [DisplayName("Photo Other 2: ")]
        public string PHOTO_2 { get; set; }

        [DisplayName("Source of Erosion 2: ")]
        public string EROSION_S2 { get; set; }

        [DisplayName("Culvert Length: ")]
        public string CULV_LEN { get; set; }

        public string CULV_SUBSP { get; set; }

        public string CULV_BACKWATERPROPORTION { get; set; }

        public string CULV_OUTLETYPE { get; set; }

        [DisplayName("Bridge Length: ")]
        public string BRDG_LEN { get; set; }

        [DisplayName("1")]
        public string CULV_DIA_1 { get; set; }

        [DisplayName("2")]
        public string CULV_DIA_2 { get; set; }

        [DisplayName("3")]
        public string CULV_DIA_3 { get; set; }

        [DisplayName("Emergency Respairs Req: ")]
        public string EMG_REP_RE { get; set; }

        [DisplayName("Structural Problems: ")]
        public string STU_PROBS { get; set; }

        [DisplayName("Outlet Scour: ")]
        public string OUTLET_SCO { get; set; }

        [DisplayName("Sedimentation: ")]
        public string SEDEMENTAT { get; set; }

        [DisplayName("Culvert Pool Depth: ")]
        public string CULV_OPOOD { get; set; }

        [DisplayName("Culvert Outlet Gap: ")]
        public string CULV_OPGAP { get; set; }

        [DisplayName("Bridge Hazard Markers: ")]
        public string HAZMARKR { get; set; }


        public string APROCHSIGR { get; set; }
        public string APROCHRAIL { get; set; }
        [DisplayName("Bridge Deck: ")]
        public string RDSURFR { get; set; }
        public string RDDRAINR { get; set; }
        public string VISIBILITY { get; set; }
        public string SIGNAGECOM { get; set; }
        public string WEARSURF { get; set; }

        [DisplayName("Railing: ")]
        public string RAILCURBR { get; set; }
        public string GIRDEBRACR { get; set; }
        public string STRUCTCOM { get; set; }
        public string CAPBEAMR { get; set; }
        public string PILESR { get; set; }

        [DisplayName("Abutment: ")]
        public string ABUTWALR { get; set; }
        public string WINGWALR { get; set; }
        public string FOUNDATCOM { get; set; }
        public string BANKSTABR { get; set; }
        public string SLOPEPROTR { get; set; }
        public string CHANNELOPE { get; set; }
        public string CHOPENINGR { get; set; }
        public string OBSTRUCTIO { get; set; }
        public string CHANNELCOM { get; set; }


        public string RISKF { get; set; }

        [DisplayName("Risk ")]
        public string RISK { get; set; }
        public string LEN { get; set; }

        public string ATTACHMENT { get; set; }
        public string FUTURE1 { get; set; }
        public string FUTURE2 { get; set; }
        public string FUTURE3 { get; set; }
        public string FUTURE4 { get; set; }
        public string FUTURE5 { get; set; }
        public string CULV_SUBSTYPE1 { get; set; }
        public string CULV_SUBSTYPE2 { get; set; }
        public string CULV_SUBSTYPE3 { get; set; }
        public string CULV_SUBSPROPORTION1 { get; set; }
        public string CULV_SUBSPROPORTION2 { get; set; }
        public string CULV_SUBSPROPORTION3 { get; set; }
        public string OUTLET_SCORE { get; set; }
        public string SHAPE { get; set; }
        
            
    }
}