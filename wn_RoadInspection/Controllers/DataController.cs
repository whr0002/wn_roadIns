using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using wn_web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Globalization;
using wn_web.Models.Reclaimation;

namespace wn_web.Controllers
{
    /// <summary>
    /// This controller is used for getting data in web app
    /// </summary>
    [Authorize]
    public class DataController : Controller
    {

        //private string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\SCARI.mdb";
        private string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Excel.mdb";
        private string SqlString = "";
        private string SqlString2 = "";
        public Hashtable mTable;
        private ApplicationDbContext context = new ApplicationDbContext();
        private wn_webContext db = new wn_webContext();

        // GET: Data
        public void Index()
        {

        }

        public void OneRow(string id)
        {
            setConnAndSql();


            var row = new List<object>();
            this.mTable = getLookupTable();


            using (OleDbConnection conn = new OleDbConnection(ConnString))
            {
                using (OleDbCommand cmd = new OleDbCommand(SqlString2, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", id);

                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read() == true)
                        {

                            StringBuilder sb = new StringBuilder();
                            StringWriter sw = new StringWriter(sb);
                            JsonWriter jsonWriter = new JsonTextWriter(sw);

                            // Get columns names
                            int fieldcount = reader.FieldCount; // count how many columns are in the row
                            object[] values = new object[fieldcount]; // storage for column values
                            reader.GetValues(values); // extract the values in each column
                            jsonWriter.WriteStartObject();

                            for (int index = 0; index < fieldcount; index++)
                            { // iterate through all columns

                                string t = reader.GetName(index).ToString();

                                if (mTable[t] != null)
                                {
                                    jsonWriter.WritePropertyName(mTable[t].ToString()); // column name
                                    jsonWriter.WriteValue(values[index]); // value in column
                                }

                            }
                            jsonWriter.WriteEndObject();

                            Response.Write(sw.ToString());

                        }
                    }
                }
            }

            //return Json(row, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OneRowD(int id)
        {
            OneRowViewModel o = new OneRowViewModel();
            DesktopReview v = null;
            ReviewSite r = null;
            string role = getUserRole();
            if (role != null)
            {
                if (role.ToUpper().Equals("SUPER ADMIN"))
                {
                    v = db.DesktopReviews.Where(w => w.DesktopReviewID == id).FirstOrDefault();
                    if (v != null)
                    {
                        r = db.ReviewSites.Where(w => w.ReviewSiteID.Equals(v.SiteID)).FirstOrDefault();

                        o.Part1 = v;
                        o.Part2 = r;
                        return Json(o, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    v = db.DesktopReviews.Where(w => w.DesktopReviewID == id && w.Client.Equals(role)).FirstOrDefault();
                }
            }


            return Json(v, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void PostPositions(string data)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            Position[] positions = js.Deserialize<Position[]>(data);

            setConnAndSql();
            this.mTable = getLookupTable();

            try
            {

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                JsonWriter jsonWriter = new JsonTextWriter(sw);
                OleDbConnection conn = new OleDbConnection(ConnString);
                OleDbCommand cmd = new OleDbCommand(SqlString2, conn);

                conn.Open();
                jsonWriter.WriteStartArray();
                for (int i = 0; i < positions.Length; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", positions[i].ID);

                    OleDbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read() == true)
                    {
                        // Get columns names
                        int fieldcount = reader.FieldCount; // count how many columns are in the row
                        object[] values = new object[fieldcount]; // storage for column values
                        reader.GetValues(values); // extract the values in each column

                        jsonWriter.WriteStartObject();
                        for (int index = 0; index < fieldcount; index++)
                        { // iterate through all columns

                            string t = reader.GetName(index).ToString();

                            if (mTable[t] != null)
                            {
                                jsonWriter.WritePropertyName(mTable[t].ToString()); // column name
                                jsonWriter.WriteValue(values[index]); // value in column
                            }

                        }
                        jsonWriter.WriteEndObject();

                    }

                    reader.Close();

                }

                conn.Close();
                cmd.Dispose();
                jsonWriter.WriteEnd();

                Response.Write(sw.ToString());


            }
            catch (Exception e)
            {
                Response.Write(e);
            }
        }

        public JsonResult Coordinates()
        {
            setConnAndSql();


            var coords = new List<object>();

            using (OleDbConnection conn = new OleDbConnection(ConnString))
            {
                using (OleDbCommand cmd = new OleDbCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            coords.Add(new
                            {
                                ID = reader["ID"].ToString(),
                                Latitude = reader["LAT"].ToString(),
                                Longitude = reader["LONG"].ToString(),
                                Risk = reader["RISK"],
                                Client = reader["GROUP"]
                            });
                        }

                    }
                }
            }



            return Json(coords, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CoordinatesD()
        {
            IQueryable<CoordsViewModel> data = null;
            string role = getUserRole();

            if (role != null)
            {

                if (role.ToUpper().Equals("SUPER ADMIN"))
                {
                    data = db.DesktopReviews
                    .Select(s => new CoordsViewModel
                    {
                        ID = s.DesktopReviewID,
                        Latitude = s.Latitude,
                        Longitude = s.Longitude,
                        Client = s.Client
                    })
                    .Where(w => w.Latitude != null);
                }
                else
                {
                    data = db.DesktopReviews
                    .Select(s => new CoordsViewModel
                    {
                        ID = s.DesktopReviewID,
                        Latitude = s.Latitude,
                        Longitude = s.Longitude,
                        Client = s.Client
                    })
                    .Where(w =>
                        (w.Client != null && w.Client.Equals(role))
                        && w.Latitude != null);
                }


            }
            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }

        private JsonResult Coordinates(string query, Hashtable dict)
        {
            if (query != null && query.Length > 0)
            {
                // Check current user group
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string currentUserId = User.Identity.GetUserId();
                    //ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    List<string> roles = userManager.GetRoles(currentUserId) as List<string>;

                    if (roles.Count() > 0)
                    {
                        string firstRole = roles[0].ToUpper();
                        string baseQuery = "SELECT ID, LAT, LONG, RISK, GROUP From SCARI_DATA";

                        if (firstRole.Equals("SUPER ADMIN") || firstRole.Equals("ADMIN"))
                        {
                            // User is admin
                            this.SqlString = baseQuery + " WHERE " + query;
                        }
                        else
                        {
                            this.SqlString = baseQuery + " WHERE UCASE(GROUP) = '" + firstRole + "' AND " + query;
                        }
                    }

                    var coords = new List<object>();


                    using (OleDbConnection conn = new OleDbConnection(ConnString))
                    {
                        using (OleDbCommand cmd = new OleDbCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            if (dict != null && dict.Count > 0)
                            {
                                for (int i = 0; i < dict.Count; i++)
                                {
                                    var value = dict[i.ToString()];

                                    if (value != null)
                                    {
                                        cmd.Parameters.AddWithValue(i.ToString(), value.ToString());
                                    }
                                }
                            }


                            conn.Open();
                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    StringBuilder sb = new StringBuilder();
                                    StringWriter sw = new StringWriter(sb);
                                    JsonWriter jsonWriter = new JsonTextWriter(sw);

                                    coords.Add(new { ID = reader["ID"].ToString(), Latitude = reader["LAT"].ToString(), Longitude = reader["LONG"].ToString(), Risk = reader["RISK"], Client = reader["GROUP"] });

                                }

                                reader.Close();

                            }

                            conn.Close();
                        }
                    }


                    return Json(coords, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ViewResult ShowOnNewTab(int tabid)
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult DisplayDetails(int ID)
        {
            
            return View();

        }

        public Record getRecord(string id)
        {
            setConnAndSql();
            Record theRecord = null;

            using (OleDbConnection conn = new OleDbConnection(ConnString))
            {
                using (OleDbCommand cmd = new OleDbCommand(SqlString2, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", id);

                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read() == true)
                        {

                            // Get columns names
                            int fieldcount = reader.FieldCount; // count how many columns are in the row
                            object[] values = new object[fieldcount]; // storage for column values
                            reader.GetValues(values); // extract the values in each column

                            List<string> mPhotos = getPhotoUrl(new string[]{values[37].ToString(), 
                                                            values[38].ToString(), values[39].ToString(), 
                                                            values[40].ToString(), values[41].ToString(),
                                                            values[42].ToString()});

                            for (int i = 0; i < mPhotos.Count; i++)
                            {
                                values[37 + i] = mPhotos[i];
                            }


                            theRecord = new Record() { ID = values[0].ToString(), CLIENT = values[1].ToString(), INSP_DATE = Convert.ToDateTime(values[2]), INSP_CREW = values[3].ToString(), ACCESS = values[4].ToString(), CROSS_NM = values[5].ToString(), CROSS_ID = values[6].ToString(), LAT = values[7].ToString(), LONG = values[8].ToString(), NORT = values[9].ToString(), EAST = values[10].ToString(), STR_ID = values[11].ToString(), STR_CLASS = values[12].ToString(), STR_WIDTH = values[13].ToString(), STR_WIDTHM = values[14].ToString(), DISPOSITION_ID = values[15].ToString(), CROSS_TYPE = values[16].ToString(), EROSION = values[17].ToString(), EROSION_TY1 = values[18].ToString(), EROSION_TY2 = values[19].ToString(), EROSION_SO = values[20].ToString(), EROSION_DE = values[21].ToString(), EROSION_AR = values[22].ToString(), BLOCKAGE = values[23].ToString(), BLOC_MATR = values[24].ToString(), BLOC_CAUS = values[25].ToString(), CULV_SUBS = values[26].ToString(), CULV_SLOPE = values[27].ToString(), SCOUR_POOL = values[28].ToString(), DELINEATOR = values[29].ToString(), FISH_SAMP = values[30].ToString(), FISH_SM = values[31].ToString(), FISH_SPP = values[32].ToString(), FISH_PCONC = values[33].ToString(), FISH_SPP2 = values[34].ToString(), FISH_PCONCREASON = values[35].ToString(), REMARKS = values[36].ToString(), PHOTO_INUP = values[37].ToString().Trim(), PHOTO_INDW = values[38].ToString().Trim(), PHOTO_OTUP = values[39].ToString().Trim(), PHOTO_OTDW = values[40].ToString().Trim(), PHOTO_1 = values[41].ToString().Trim(), PHOTO_2 = values[42].ToString().Trim(), CULV_LEN = values[43].ToString(), CULV_SUBSP = values[44].ToString(), CULV_BACKWATERPROPORTION = values[45].ToString(), CULV_OUTLETYPE = values[46].ToString(), CULV_DIA_1 = values[47].ToString(), CULV_DIA_2 = values[48].ToString(), CULV_DIA_3 = values[49].ToString(), BRDG_LEN = values[50].ToString(), EMG_REP_RE = values[51].ToString(), STU_PROBS = values[52].ToString(), SEDEMENTAT = values[53].ToString(), CULV_OPOOD = values[54].ToString(), CULV_OPGAP = values[55].ToString(), HAZMARKR = values[56].ToString(), APROCHSIGR = values[57].ToString(), APROCHRAIL = values[58].ToString(), RDSURFR = values[59].ToString(), RDDRAINR = values[60].ToString(), VISIBILITY = values[61].ToString(), WEARSURF = values[62].ToString(), RAILCURBR = values[63].ToString(), GIRDEBRACR = values[64].ToString(), CAPBEAMR = values[65].ToString(), PILESR = values[66].ToString(), ABUTWALR = values[67].ToString(), WINGWALR = values[68].ToString(), BANKSTABR = values[69].ToString(), SLOPEPROTR = values[70].ToString(), CHANNELOPE = values[71].ToString(), OBSTRUCTIO = values[72].ToString(), ATTACHMENT = values[73].ToString(), FUTURE2 = values[74].ToString(), FUTURE3 = values[75].ToString(), FUTURE4 = values[76].ToString(), FUTURE5 = values[77].ToString(), RISKF = values[78].ToString(), RISK = values[79].ToString(), CULV_SUBSTYPE1 = values[80].ToString(), CULV_SUBSTYPE2 = values[81].ToString(), CULV_SUBSTYPE3 = values[82].ToString(), CULV_SUBSPROPORTION1 = values[83].ToString(), CULV_SUBSPROPORTION2 = values[84].ToString(), CULV_SUBSPROPORTION3 = values[85].ToString(), OUTLET_SCORE = values[86].ToString(), SHAPE = values[87].ToString() };
                        }
                    }
                }
            }

            return theRecord;
        }


        public List<string> getPhotoUrl(string[] values)
        {
            List<string> photos = new List<string>();

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Length > 0)
                {
                    var lastIndex = values[i].LastIndexOf("\\");
                    if (lastIndex > -1)
                    {

                        photos.Add(values[i].Substring(lastIndex + 1));

                    }
                    else
                    {
                        photos.Add(values[i]);
                    }
                }
                else
                {
                    photos.Add("");
                }

            }

            return photos;
        }

        public static Hashtable getLookupTable()
        {
            Hashtable lookupTable = new Hashtable();


            lookupTable.Add("ID", "ID");
            lookupTable.Add("CLIENT", "Client");
            lookupTable.Add("INSP_DATE", "Inspection Date");
            lookupTable.Add("INSP_CREW", "Crew");
            lookupTable.Add("ACCESS", "Access");
            lookupTable.Add("CROSS_NM", "Water Crossing Name or ID");
            lookupTable.Add("CROSS_ID", "Water Crossing Name");
            lookupTable.Add("CROSS_STR", "Disposition No.");
            lookupTable.Add("LAT", "Latitude");
            lookupTable.Add("LONG", "Longitude");
            lookupTable.Add("EAST", "Easting");
            lookupTable.Add("NORT", "Northing");
            lookupTable.Add("STR_CLASS", "Stream Classification");
            lookupTable.Add("STR_WIDTH", "Bankfull Width");
            lookupTable.Add("STR_WIDTHM", "Bankfull Width measured?");
            lookupTable.Add("CROSS_TYPE", "Crossing Type");
            lookupTable.Add("EROSION", "Erosion at Site?");
            lookupTable.Add("EROSION_TY", "Location of Erosion");
            lookupTable.Add("EROSION_SO", "Source of Erosion");
            lookupTable.Add("EROSION_DE", "Degree of Erosion");
            lookupTable.Add("EROSION_AR", "Area of Erosion");
            lookupTable.Add("BLOCKAGE", "Blockage");
            lookupTable.Add("BLOC_MATR", "Blocking Material");
            lookupTable.Add("BLOC_CAUS", "Blocking Cause");
            lookupTable.Add("CULV_SUBS", "Culvert Substrate");
            lookupTable.Add("C_SUBS_D", "Greater than 10% of diameter blocked by debris");
            lookupTable.Add("C_SUBS_TYP", "Culvert Substrate Type");
            lookupTable.Add("C_SUBS_POR", "For what length of culvert?");
            lookupTable.Add("C_BCKWT_PR", "What proportion of back water?");
            lookupTable.Add("CULV_SLOPE", "Culvert Slope");
            lookupTable.Add("CULV_OUTLE", "Culvert Outlet Type");
            lookupTable.Add("SCOUR_POOL", "Scour Pool Present");
            lookupTable.Add("DELINEATOR", "Delineators");
            lookupTable.Add("FISH_SAMP", "Fish Sampling");
            lookupTable.Add("FISH_SM", "Sampling Method");
            lookupTable.Add("FISH_SPP", "Fish Species 1");
            lookupTable.Add("FISH_PASS", "Fish Passage");
            lookupTable.Add("FISH_PCONC", "Fish Passage Concerns");
            lookupTable.Add("FISH_SPP2", "Fish Species 2");
            lookupTable.Add("REMARKS", "Remarks");
            lookupTable.Add("PHOTO_INUP", "Photo Inlet Upstream");
            lookupTable.Add("PHOTO_INDW", "Photo Inlet Downstream");
            lookupTable.Add("PHOTO_OTUP", "Photo Outlet Upstream");
            lookupTable.Add("PHOTO_OTDW", "Photo Outlet Downstream");
            lookupTable.Add("PHOTO_1", "Photo Other 1");
            lookupTable.Add("PHOTO_2", "Photo Other 2");
            lookupTable.Add("EROSION_S2", "Source of Erosion 2");
            lookupTable.Add("CULV_LEN", "Culvert Length");
            lookupTable.Add("BRDG_LEN", "Bridge Length");
            lookupTable.Add("CULV_DIA_1", "Culvert Diameter 1");
            lookupTable.Add("CULV_DIA_2", "Culvert Diameter 2");
            lookupTable.Add("CULV_DIA_3", "Culvert Diameter 3");
            lookupTable.Add("EMG_REP_RE", "Emergency Repairs Req");
            lookupTable.Add("STU_PROBS", "Structural Problems");
            lookupTable.Add("OUTLET_SCO", "Outlet Scour");
            lookupTable.Add("SEDEMENTAT", "Sedimentation");
            lookupTable.Add("CULV_OPOOD", "Culvert Pool Depth");
            lookupTable.Add("CULV_OPGAP", "Culvert Outlet Gap");
            lookupTable.Add("HAZMARKR", "Bridge Hazard Markers");
            lookupTable.Add("APROCHSIGR", "Bridge Approach Signs");
            lookupTable.Add("RDSURFR", "Bridge Road Surface");
            lookupTable.Add("RDDRAINR", "Bridge Road Drainage");
            lookupTable.Add("SIGNAGECOM", "Bridge Signage Comments");
            lookupTable.Add("WEARSURF", "Bridge Wearing Surface");
            lookupTable.Add("RAILCURBR", "Bridge Rail & Curb");
            lookupTable.Add("GIRDEBRACR", "Bridge  Girders & Bracing");
            lookupTable.Add("STRUCTCOM", "Bridge Structure Comments");
            lookupTable.Add("CAPBEAMR", "Bridge Cap Beam");
            lookupTable.Add("PILESR", "Bridge Piles");
            lookupTable.Add("ABUTWALR", "Bridge Abutment Wall");
            lookupTable.Add("WINGWALR", "Bridge Wing Wall");
            lookupTable.Add("FOUNDATCOM", "Bridge Foundation Comments");
            lookupTable.Add("BANKSTABR", "Bridge Bank Stability");
            lookupTable.Add("SLOPEPROTR", "Bridge Slope Protection");
            lookupTable.Add("CHOPENINGR", "Bridge Channel Opening");
            lookupTable.Add("OBSTRUCTIO", "Bridge Obstructions");
            lookupTable.Add("CHANNELCOM", "Bridge Channel Comments");
            lookupTable.Add("RISKF", "Risk Factor");
            lookupTable.Add("RISK", "Risk");
            lookupTable.Add("LEN", "");

            return lookupTable;
        }

        private static Hashtable getSearchTable()
        {
            Hashtable ht = new Hashtable();
            ht.Add("Date", "INSP_DATE");
            ht.Add("Crossing Type", "CROSS_TYPE");
            ht.Add("Stream Type", "STR_CLASS");
            ht.Add("Erosion", "EROSION");
            ht.Add("Fish Passage Concerns", "FISH_PCONC");
            ht.Add("Safety Concerns", "STU_PROBS");
            ht.Add("Sedimentation", "SEDEMENTAT");
            ht.Add("Risk", "RISK");

            return ht;
        }

        public JsonResult search(string SearchType, string SearchValue)
        {
            //Response.Write(SearchType + "," + SearchValue);
            if (SearchType != null && SearchValue != null
                && !SearchType.Contains("Select")
                && !SearchValue.Contains("Select"))
            {
                string query = getSearchQuery(SearchType, SearchValue);
                if (query != null)
                {
                    Hashtable h = new Hashtable();
                    h.Add("0", SearchValue);
                    //Response.Write("key: 0" + ", value: " + h[0].ToString());
                    return Coordinates(query, h);
                }
            }

            return Json(null, JsonRequestBehavior.AllowGet);

        }

        public JsonResult searchDate(string SearchType, string from, string to)
        {
            //Response.Write(SearchType + "," + from + " , " + to);
            if (SearchType != null && !SearchType.Contains("Select"))
            {
                if (from != null && to != null && !from.Equals("") && !to.Equals(""))
                {
                    try
                    {

                        string[] formats = { "yyyy-MM-dd" };
                        var dt = DateTime.ParseExact(from, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                        string formattedDateFrom = dt.ToString("MM/dd/yyyy");
                        dt = DateTime.ParseExact(to, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                        string formattedDateTo = dt.ToString("MM/dd/yyyy");

                        //string query = "Select LAT, LONG, RISK, CLIENT From SCARI_All_table WHERE INSP_DATE >= #" + formattedDateFrom + "# AND INSP_DATE <= #" + formattedDateTo + "# ";
                        string query = " INSP_DATE >= #" + formattedDateFrom + "# AND INSP_DATE <= #" + formattedDateTo + "# ";


                        return Coordinates(query, null);
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }

        private string getSearchQuery(string st, string sv)
        {
            if (st != null && sv != null
                    && !st.Contains("Select")
                    && !sv.Contains("Select"))
            {


                Hashtable ht = getSearchTable();
                if (ht.ContainsKey(st))
                {
                    //string q = "Select LAT, LONG, RISK, CLIENT From SCARI_All_table";
                    //string addition = " UCASE("+ ht[st].ToString() +") = UCASE(@'" + sv + "')";
                    string addition = " UCASE(" + ht[st].ToString() + ") = UCASE(@0) ";
                    //q += addition;
                    return addition;
                }


            }
            return null;
        }

        public JsonResult SearchType(string type)
        {
            if (type != null)
            {
                string typel = type.ToLower();
                if (typel.Equals("risk"))
                {
                    Risk r = new Risk();

                    return Json(r.getOptions(), JsonRequestBehavior.AllowGet);
                }
                else if (typel.Equals("crossing type"))
                {
                    CrossingType ct = new CrossingType();

                    return Json(ct.getOptions(), JsonRequestBehavior.AllowGet);

                }
                else if (typel.Equals("erosion"))
                {
                    Erosion et = new Erosion();
                    return Json(et.getOptions(), JsonRequestBehavior.AllowGet);
                }
                else if (typel.Equals("fish passage concerns"))
                {
                    FishPCType ft = new FishPCType();
                    return Json(ft.getOptions(), JsonRequestBehavior.AllowGet);
                }
                else if (typel.Equals("sedimentation"))
                {
                    Sedimentation s = new Sedimentation();
                    return Json(s.getOptions(), JsonRequestBehavior.AllowGet);
                }
                else if (typel.Equals("stream type"))
                {
                    StreamType s = new StreamType();
                    return Json(s.getOptions(), JsonRequestBehavior.AllowGet);
                }
                else if (typel.Equals("safety concerns"))
                {
                    SaftyConcernsType s = new SaftyConcernsType();
                    return Json(s.getOptions(), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AdvSearch(string data)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            SearchObject[] sos = js.Deserialize<SearchObject[]>(data);
            Hashtable ht = getSearchTable();

            string query = "";
            string dateQuery = "";
            if (sos != null && sos.Count() > 0)
            {

                int i = 0;
                Hashtable h = new Hashtable();

                foreach (var so in sos)
                {
                    string key = so.key;
                    string value1 = so.value1;

                    if (ht.Contains(key))
                    {

                        string realKey = ht[key].ToString();

                        if (!key.Equals("Date"))
                        {
                            h.Add(i.ToString(), value1);

                            if (i == 0)
                            {
                                //query += " UCASE(" + realKey + ") = UCASE('" + value1 + "') ";
                                query += " UCASE(" + realKey + ") = UCASE(@" + i + ") ";
                            }
                            else
                            {
                                //query += " AND UCASE(" + realKey + ") = UCASE('" + value1 + "') ";
                                query += " AND UCASE(" + realKey + ") = UCASE(@" + i + ") ";
                            }
                            i++;
                        }
                        else
                        {
                            string value2 = so.value2;
                            string[] formats = { "yyyy-MM-dd" };

                            if (value1 != null && !value1.Equals("") && value2 != null && !value2.Equals(""))
                            {

                                // Date 'From' and Date 'To' have values
                                try
                                {
                                    var dt = DateTime.ParseExact(value1, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                                    string formattedFrom = dt.ToString("MM/dd/yyyy");
                                    dt = DateTime.ParseExact(value2, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                                    string formattedTo = dt.ToString("MM/dd/yyyy");

                                    dateQuery = " " + realKey + " >= #" + formattedFrom + "# AND " + realKey + " <= #" + formattedTo + "# ";


                                }
                                catch (Exception e)
                                {
                                    // Exceptions when parse Date
                                }
                            }
                            else if (value1 != null && !value1.Equals(""))
                            {
                                // Date 'From' only
                                var dt = DateTime.ParseExact(value1, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                                string formattedFrom = dt.ToString("MM/dd/yyyy");

                                dateQuery = " " + realKey + " >= #" + formattedFrom + "# ";

                            }
                            else if (value2 != null && !value2.Equals(""))
                            {
                                // Date 'To' Only
                                var dt = DateTime.ParseExact(value2, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                                string formattedTo = dt.ToString("MM/dd/yyyy");

                                dateQuery = " " + realKey + " <= #" + formattedTo + "# ";

                            }
                        }

                    }

                }
                if (!dateQuery.Equals(""))
                {
                    // Date query is set
                    if (query.Equals(""))
                    {
                        // Other query are not set, append date directly 
                        query += dateQuery;
                    }
                    else
                    {
                        // Have other queries
                        query += " AND " + dateQuery;
                    }
                }


                //query += dateQuery;
                if (!query.Equals(""))
                {

                    //foreach (DictionaryEntry pair in h)
                    //{
                    //    Response.Write("key: " + pair.Key.ToString() + ", Value: " + pair.Value.ToString() + "\n");
                    //}

                    //Response.Write(query + "\n");
                    return Coordinates(query, h);
                }
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchCroosingID(string q)
        {


            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public void setConnAndSql()
        {
            // Check current user group
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string currentUserId = User.Identity.GetUserId();
                //ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                List<string> roles = userManager.GetRoles(currentUserId) as List<string>;
                if (roles.Count() > 0)
                {
                    string firstRole = roles[0].ToUpper();

                    if (firstRole.Equals("SUPER ADMIN") || firstRole.Equals("ADMIN"))
                    {
                        // User is admin
                        this.SqlString = "SELECT SiteID, Latitude, Longitude FROM ExcelData";
                        this.SqlString2 = "SELECT * FROM ExcelData WHERE SiteID=@id";
                    }
                    else
                    {
                        this.SqlString = "SELECT SiteID, Latitude, Longitude, Client FROM ExcelData WHERE UCASE(Client)='" + firstRole + "'";
                        this.SqlString2 = "SELECT * FROM ExcelData WHERE UCASE(Client)='" + firstRole + "'" + " AND SiteID=@id ";
                    }
                }
            }

        }

        private string getUserRole()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                List<string> roles = userManager.GetRoles(id) as List<string>;
                if (roles != null && roles.Count() > 0)
                {
                    return roles[0];
                }
            }
            return null;
        }
    }
}