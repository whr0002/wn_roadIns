using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using wn_web.Models;
using wn_web.Models.Reclaimation;
using wn_web.Models.Reclaimation.Report;

namespace wn_web.Controllers
{
    /// <summary>
    /// Used for testing
    /// </summary>
    [Authorize(Roles="super admin")]
    public class TestController : Controller
    {
        //private string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SCARI.accdb";
        private string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Excel.mdb";
        private string sql = "SELECT * FROM ExcelData";
        private wn_webContext db = new wn_webContext();
        // GET: Test
        public void Index()
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        

                        reader.Read();
                        int fieldCount = reader.FieldCount;
                        Response.Write("Field Count: " + fieldCount + "<br />");

                        Object[] values = new Object[fieldCount];
                        reader.GetValues(values);

                        for (int i = 0; i < fieldCount; i++)
                        {

                            Response.Write(""+i+"::: "+reader.GetName(i) + ": " + values[i].ToString().Trim() + "<br />");
                        }

 


                    }
                }
            }

            //return View();
        }

        public void Print()
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        int fieldCount = reader.FieldCount;
                        Response.Write("Field Count: " + fieldCount + "<br />");

                        Object[] values = new Object[fieldCount];
                        reader.GetValues(values);

                        for (int i = 0; i < fieldCount; i++)
                        {

                            Response.Write("public String " + reader.GetName(i) + ";<br />");
                        }

                    }
                }
            }
        }

        public void Print2()
        {
            //DesktopReview d = new DesktopReview();
            ReviewSite d = new ReviewSite();
            PropertyInfo[] properties = d.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Response.Write("public String " + p.Name + ";<br />");
            }
        }

        public void Print3()
        {
            //DesktopReview d = new DesktopReview();
            ReviewSite d = new ReviewSite();
            PropertyInfo[] properties = d.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Response.Write("public static final String COLUMN_" + p.Name.ToUpper() + " = \"" + p.Name + "\";<br />");
            }
        }

        public void Print4()
        {
            //DesktopReview d = new DesktopReview();
            ReviewSite d = new ReviewSite();
            PropertyInfo[] properties = d.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Response.Write("+ COLUMN_" + p.Name.ToUpper() + " + \" text, \"<br />");
            }
        }

        public void Print5()
        {
            //DesktopReview d = new DesktopReview();
            ReviewSite d = new ReviewSite();
            PropertyInfo[] properties = d.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Response.Write("DR_Properties.COLUMN_" + p.Name.ToUpper() + ",<br />");
            }
        }

        public void Print6()
        {
            //DesktopReview d = new DesktopReview();
            ReviewSite d = new ReviewSite();
            PropertyInfo[] properties = d.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Response.Write("values.put(DR_Properties.COLUMN_" + p.Name.ToUpper() + ", dr." + p.Name + ");<br />");
            }
        }

        public void Print7()
        {
            //DesktopReview d = new DesktopReview();
            ReviewSite d = new ReviewSite();
            PropertyInfo[] properties = d.GetType().GetProperties();
            int counter = 0;
            foreach (PropertyInfo p in properties)
            {
                Response.Write("o." + p.Name + " = cursor.getString(" + counter + ");<br />");
                counter++;
            }
        }

        public void AddTableData()
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) { 
                            int fieldCount = reader.FieldCount;
                            Object[] values = new Object[fieldCount];
                            reader.GetValues(values);

                            AddData(values);

                        }

                    }
                }
            }
        }

        public void PrintProperties()
        {
            SiteVisitReport f = new SiteVisitReport();
            Type t = f.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                Response.Write(p.Name + ",");
            }
        }

        public void PrintProperties2()
        {
            SiteVisitReport f = new SiteVisitReport();
            Type t = f.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                if (p.Name.EndsWith("PF"))
                {
                    Response.Write("p.put(\"" + p.Name + "\", getPassOrFail(f." + p.Name + ")); <br />" );
                }

                if (p.Name.EndsWith("Comment"))
                {
                    Response.Write("p.put(\"" + p.Name + "\", f." + p.Name + "); <br />");
                }
                
            }
        }



        private void AddData(Object[] values)
        {
            DesktopReview d = new DesktopReview();
            ReviewSite r = new ReviewSite();
           

            d.SiteID = values[0].ToString().Trim();
            r.ReviewSiteID = values[0].ToString().Trim();

            FacilityType f = new FacilityType();           
            f.FacilityTypeName = values[1].ToString().Trim();

            var ftemp = db.FacilityTypes.Where(w => w.FacilityTypeName.Equals(f.FacilityTypeName)).FirstOrDefault();
            if (ftemp != null)
            {
                d.FacilityType = ftemp;
            }
            else
            {
                d.FacilityType = f;
            }

            

            d.Notes = values[2].ToString().Trim();
            d.WorkPhase = values[3].ToString().Trim();
            r.AFE = values[4].ToString().Trim();
            d.Occupant = values[5].ToString().Trim();

            d.OccupantInfo = values[6].ToString().Trim();

            ProvincialArea pa = new ProvincialArea();
            pa.ProvincialAreaName = values[7].ToString().Trim();

            var ftemp2 = db.ProvincialAreas.Where(w => w.ProvincialAreaName.Equals(pa.ProvincialAreaName)).FirstOrDefault();
            if (ftemp2 != null)
            {
                r.ProvincialArea = ftemp2;
            }
            else
            {
                r.ProvincialArea = pa;
            }

            

            ProvincialAreaType pat = new ProvincialAreaType();
            pat.ProvincialAreaTypeName = values[8].ToString().Trim();

            var ftemp3 = db.ProvincialAreaTypes.Where(w => w.ProvincialAreaTypeName.Equals(pat.ProvincialAreaTypeName)).FirstOrDefault();
            if (ftemp3 != null)
            {
                r.ProvincialAreaType = ftemp3;
            }
            else
            {
                r.ProvincialAreaType = pat;
            }


            OperatingArea oa = new OperatingArea();
            oa.OperatingAreaName = values[9].ToString().Trim();
            var ftemp4 = db.OperatingAreas.Where(w => w.OperatingAreaName.Equals(oa.OperatingAreaName)).FirstOrDefault();
            if (ftemp4 != null)
            {
                r.OperatingArea = ftemp4;
            }
            else
            {
                r.OperatingArea = oa;
            }

            County c = new County();
            c.CountyName = values[10].ToString().Trim();
            var ftemp5 = db.Countys.Where(w => w.CountyName.Equals(c.CountyName)).FirstOrDefault();
            if (ftemp5 != null)
            {
                r.County = ftemp5;
            }
            else
            {
                r.County = c;
            }


            NaturalRegion nr = new NaturalRegion();
            nr.NaturalRegionName = values[11].ToString().Trim();
            var ftemp6 = db.NaturalRegions.Where(w => w.NaturalRegionName.Equals(nr.NaturalRegionName)).FirstOrDefault();
            if (ftemp6 != null)
            {
                r.NaturalRegion = ftemp6;
            }
            else
            {
                r.NaturalRegion = nr;
            }


            NaturalSubRegion nsr = new NaturalSubRegion();
            nsr.NaturalSubRegionName = values[12].ToString().Trim();
            var ftemp7 = db.NaturalSubRegions.Where(w => w.NaturalSubRegionName.Equals(nsr.NaturalSubRegionName)).FirstOrDefault();
            if (ftemp7 != null)
            {
                r.NaturalSubRegion = ftemp7;
            }
            else
            {
                r.NaturalSubRegion = nsr;
            }


            FMAHolder fma = new FMAHolder();
            fma.FMAHolderName = values[13].ToString().Trim();
            var ftemp8 = db.FMAHolders.Where(w => w.FMAHolderName.Equals(fma.FMAHolderName)).FirstOrDefault();
            if (ftemp8 != null)
            {
                r.FMAHolder = ftemp8;
            }
            else
            {
                r.FMAHolder = fma;
            }

            r.SeedZone = values[14].ToString().Trim();
            d.SoilClass = values[15].ToString().Trim();
            d.SoilGroup = values[16].ToString().Trim();
            r.DispositionNumber = values[17].ToString().Trim();
            r.UWI = values[18].ToString().Trim();
            d.ERCBLic = values[19].ToString().Trim();
            r.WellboreID = values[20].ToString().Trim();
            r.UTMZone = values[21].ToString().Trim();
            r.WellsiteName = values[22].ToString().Trim();
            d.Width = castDouble(values[23]);
            d.Length = castDouble(values[24]);
            d.AreaHA = castDouble(values[25]);
            d.AreaAC = castDouble(values[26]);
            d.Northing = castDouble(values[27]);
            d.Easting = castDouble(values[28]);
            d.Latitude = castDouble(values[29]);
            d.Longitude = castDouble(values[30]);
            if (d.Longitude != null && d.Longitude > 0)
            {
                d.Longitude *= -1;
            }
            d.Elevation = castDouble(values[31]);

            Aspect a = new Aspect();
            a.AspectName = values[32].ToString().Trim();
            var ftemp9 = db.Aspects.Where(w => w.AspectName.Equals(a.AspectName)).FirstOrDefault();
            if (ftemp9 != null)
            {
                d.LSDQuarter = ftemp9;
            }
            else
            {
                d.LSDQuarter = a;
            }


            d.LSD = values[33].ToString().Trim();
            d.SurveyDate = convertDate(values[34]);
            d.ConstructionDate = convertDate(values[35]);
            d.SpudDate = convertDate(values[36]);
            d.AbandonmentDate = convertDate(values[37]);
            d.ReclamationDate = convertDate(values[38]);

            RelevantCriteria rc = new RelevantCriteria();           
            rc.RelevantCriteriaName = values[39].ToString().Trim();
            var ftemp10 = db.RelevantCriterias.Where(w => w.RelevantCriteriaName.Equals(rc.RelevantCriteriaName)).FirstOrDefault();
            if (ftemp10 != null)
            {
                d.RelevantCriteria = ftemp10;
            }
            else
            {
                d.RelevantCriteria = rc;
            }


            Landscape l = new Landscape();
            l.LandscapeName = values[40].ToString().Trim();
            var ftemp11 = db.Landscapes.Where(w => w.LandscapeName.Equals(l.LandscapeName)).FirstOrDefault();
            if (ftemp11 != null)
            {
                d.Landscape = ftemp11;
            }
            else
            {
                d.Landscape = l;
            }



            Soil s = new Soil();
            s.SoilName = values[41].ToString().Trim();
            var ftemp12 = db.Soils.Where(w => w.SoilName.Equals(s.SoilName)).FirstOrDefault();
            if (ftemp12 != null)
            {
                d.Soil = ftemp12;
            }
            else
            {
                d.Soil = s;
            }


            Vegetation v = new Vegetation();
            v.VegetationName = values[42].ToString().Trim();
            var ftemp13 = db.Vegetations.Where(w => w.VegetationName.Equals(v.VegetationName)).FirstOrDefault();
            if (ftemp13 != null)
            {
                d.Vegetation = ftemp13;
            }
            else
            {
                d.Vegetation = v;
            }



            d.RCADate = convertDate(values[43]);
            d.RCNumber = values[44].ToString().Trim();

            d.DSAComments = values[45].ToString().Trim();
            d.Exemptions = values[46].ToString().Trim();
            d.AmendDate = convertDate(values[47]);
            d.AmendDetail = values[48].ToString().Trim();
            d.RevegDate = convertDate(values[49]);
            d.RevegDetail = values[50].ToString().Trim();


            db.DesktopReviews.Add(d);
            db.SaveChanges();

            //db.ReviewSites.Find(r);
            //db.SaveChanges();
            var re = db.ReviewSites.Find(r.ReviewSiteID);
            if (re == null)
            {
                //db.ReviewSites.Add(r);
                //db.SaveChanges();
            }
        }

        private DateTime? convertDate(Object d) {

            try
            {
                return Convert.ToDateTime(d);
            }
            catch (Exception e)
            {
                
                return null;
            }

        
        }

        private double? castDouble(Object d)
        {
            try
            {
                return (double)d;
            }
            catch (Exception e)
            {
                return null;
            }

           
        }

        public void deleteAll()
        {
            db.Aspects.RemoveRange(db.Aspects);
            db.Countys.RemoveRange(db.Countys);
            db.DesktopReviews.RemoveRange(db.DesktopReviews);
            db.FacilityTypes.RemoveRange(db.FacilityTypes);
            db.FMAHolders.RemoveRange(db.FMAHolders);
            db.Landscapes.RemoveRange(db.Landscapes);
            db.NaturalRegions.RemoveRange(db.NaturalRegions);
            db.NaturalSubRegions.RemoveRange(db.NaturalSubRegions);
            db.OperatingAreas.RemoveRange(db.OperatingAreas);
            db.ProvincialAreas.RemoveRange(db.ProvincialAreas);
            db.ProvincialAreaTypes.RemoveRange(db.ProvincialAreaTypes);
            db.RelevantCriterias.RemoveRange(db.RelevantCriterias);
            db.ReviewSites.RemoveRange(db.ReviewSites);
            db.Soils.RemoveRange(db.Soils);
            db.Vegetations.RemoveRange(db.Vegetations);
            db.SaveChanges();


        }
    }

    
}