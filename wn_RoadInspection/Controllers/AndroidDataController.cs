using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
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
using wn_web.Models;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using wn_RoadInspection;


namespace wn_web.Controllers
{
    /// <summary>
    /// This controller is used for getting data from mobile devices
    /// </summary>
    public class AndroidDataController : Controller
    {

        private string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\SCARI.mdb";
        private string SqlString = "";
        private string SqlString2 = "";
        public Hashtable mTable;
        private ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationSignInManager signInManager;
        private wn_webContext mDb = new wn_webContext();

        /// <summary>
        /// Get one row of data from SCARI.mdb based on given ID
        /// </summary>
        /// <param name="fc">Form collections</param>
        /// <returns>one row data</returns>
        [HttpPost]
        public async Task OneRow(FormCollection fc)
        {
            string email = fc["Email"];
            string password = fc["Password"];
            string rememberMe = fc["RememberMe"];
            string role = fc["Role"];
            //string latitude = fc["Latitude"];
            //string longitude = fc["Longitude"];
            string id = fc["ID"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    role = getUserRole(email);
                    setConnAndSql(role);

                    var row = new List<object>();
                    this.mTable = DataController.getLookupTable();

                    //Response.Write("Lat: " + latitude + "Long: " + longitude);

                    using (OleDbConnection conn = new OleDbConnection(ConnString))
                    {
                        using (OleDbCommand cmd = new OleDbCommand(SqlString2, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            //cmd.Parameters.AddWithValue("latitude", latitude);
                            //cmd.Parameters.AddWithValue("longitude", longitude);
                            cmd.Parameters.AddWithValue("id", id);
                            if (!role.Equals("super admin")) { 
                                cmd.Parameters.AddWithValue("role", role);
                            }
                            conn.Open();
                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {


                                reader.Read();

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

                                //row.Add(new { INSP_DATE = "", Latitude = reader["LAT"].ToString(), Longitude = reader["LONG"].ToString(), Risk = reader["RISK"] });

                                Response.Write(sw.ToString());
                            }
                        }
                    }
                    break;

                case SignInStatus.Failure:

                    break;

                default:

                    break;
            }


           

            //return Json(row, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get a set of coordinates based on user group
        /// </summary>
        /// <param name="fc"></param>
        /// <returns>a set of corrdinates</returns>
        [HttpPost]
        public async Task Coordinates(FormCollection fc)
        {

            var coords = new List<object>();

            string email = fc["Email"];
            string password = fc["Password"];
            string rememberMe = fc["RememberMe"];
            string role = fc["Role"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    role = getUserRole(email);
                    setConnAndSql(role);

                    using (OleDbConnection conn = new OleDbConnection(ConnString))
                    {
                        using (OleDbCommand cmd = new OleDbCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("role", role);

                            conn.Open();
                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    StringBuilder sb = new StringBuilder();
                                    StringWriter sw = new StringWriter(sb);
                                    JsonWriter jsonWriter = new JsonTextWriter(sw);

                                    coords.Add(new { ID = reader["ID"], 
                                        Latitude = reader["LAT"].ToString(), 
                                        Longitude = reader["LONG"].ToString(), 
                                        Risk = reader["RISK"], 
                                        Group = reader["GROUP"] });

                                }
                            }
                        }
                    }
                    string json = new JavaScriptSerializer().Serialize(Json(coords, JsonRequestBehavior.AllowGet).Data);
                    Response.Write(json);
                    break;

                case SignInStatus.Failure:
                    Response.Write("Failed");
                    break;

                default:
                    Response.Write("Unknown Error");
                    break;
            }
            
                
            



            
        }

        [HttpPost]
        public async Task KMLs(FormCollection fc)
        {
            
            string email = fc["Email"];
            string password = fc["Password"];
            string rememberMe = fc["RememberMe"];
            string role = fc["Role"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    role = getUserRole(email);
                    List<Kml> kmls;
                    if (role.Equals("super admin")) {
                        kmls = mDb.Kmls.Where(k => k.ID == k.ID).ToList();
                    }
                    else
                    {
                        kmls = mDb.Kmls.Where(k => k.Client.Equals(role)).ToList();
                    }
                    string json = new JavaScriptSerializer().Serialize(Json(kmls, JsonRequestBehavior.AllowGet).Data);
                    Response.Write(json);
                    break;

                case SignInStatus.Failure:

                    break;

                default:

                    break;
            }
        }

        public List<string> getPhotoUrl(string[] values)
        {
            List<string> photos = new List<string>();

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Length >= 10)
                    photos.Add(values[i].Substring(10));
                else
                    photos.Add("");
            }

            return photos;
        }
        
        public void setConnAndSql(String role)
        {
            if (role != null) { 

                if (role.Equals("super admin") || role.Equals("WOODN"))
                {
                    // User is admin
                    this.SqlString = "SELECT ID,LAT, LONG, RISK, GROUP FROM SCARI_DATA";
                    this.SqlString2 = "SELECT * FROM SCARI_DATA WHERE ID=@id";
                }
                else{
                    //this.SqlString = "Select LAT, LONG, RISK, CLIENT From SCARI_All_table WHERE CLIENT=@role";
                    //this.SqlString2 = "SELECT * FROM SCARI_All_table WHERE LAT=@latitude AND LONG=@longitude AND CLIENT=@role";
                    this.SqlString = "SELECT ID,LAT, LONG, RISK, GROUP FROM SCARI_DATA WHERE UCASE(GROUP)=@role";
                    this.SqlString2 = "SELECT * FROM SCARI_DATA WHERE ID=@id AND UCASE(GROUP)=@role";

                }


            }
        }

        public async Task signInCheck(FormCollection fc)
        {
            string email = fc["Email"];
            string password = fc["Password"];
            string rememberMe = fc["RememberMe"];
            string role = fc["Role"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    break;

                case SignInStatus.Failure:

                    break;

                default:

                    break;
            }
        }

        public async Task RawData(FormCollection fc)
        {
            string email = fc["Email"];
            string password = fc["Password"];
            string rememberMe = fc["RememberMe"];
            string role = fc["Role"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);
            
            switch (result)
            {
                case SignInStatus.Success:
                    role = getUserRole(email);
                    List<Coordinate> data;
                    if (role.Equals("super admin") || role.Equals("WOODN"))
                    {
                        data = mDb.FieldDatas              
                                .Select(s => new Coordinate { Latitude = s.LAT, Longitude = s.LONG, Risk = "", Client = s.Group })
                                .ToList();

                    }
                    else
                    {
                        data = mDb.FieldDatas
                            .Where(d => d.Group.Equals(role))
                            .Select(s => new Coordinate { Latitude = s.LAT, Longitude = s.LONG, Risk = "", Client = s.Group })
                            .ToList();
                    }
                    string json = new JavaScriptSerializer().Serialize(Json(data, JsonRequestBehavior.AllowGet).Data);
                    Response.Write(json);
                    break;

                case SignInStatus.Failure:

                    break;

                default:

                    break;
            }
        }

        private string getUserRole(string email)
        {
            if (email != null)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                ApplicationUser user = db.Users.Where(s => s.UserName.Equals(email, 
                    StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();
                var userstore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userstore);

                // Get all roles belong to this user
                List<string> roles = userManager.GetRoles(user.Id) as List<string>;

                if (roles != null && roles.Count > 0)
                {
                    // Return the first role
                    return roles[0];
                }

            }

            return "UNKNOWN";
        }

        [Authorize(Roles="super admin")]
        public void Downloads()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/Overlays/"));
            System.IO.FileInfo[] files = dir.GetFiles("*.zip");

            List<string> items = new List<string>();

            foreach (var file in files)
            {
                Response.Write("<a href='" + "http://localhost:49994/AndroidData/Download?fileName=" + file.Name + "'" + ">" + file.Name + "</a><br />");

            }
        }

        [HttpPost]
        public async Task Download(FormCollection fc)
        {

            //string noExtName = fileName.Substring(0, fileName.LastIndexOf("."));
            string email = fc["Email"];
            string password = fc["Password"];
            string role = fc["Role"];
            string fileName = fc["fileName"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            var result = await signInManager.PasswordSignInAsync(email, password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    role = getUserRole(email);
                    var zips = mDb.Kmls.Where(w => w.Client.Equals(role)).ToList();

                    foreach (var z in zips)
                    {
                        //Response.Write(z.Name + "<br />");
                        if (fileName.Equals(z.Name))
                        {
                            // Found it, return
                            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                            Response.ContentType = "application/zip";
                            //Response.WriteFile(Server.MapPath("~/App_Data/Overlays/" + fileName));
                            Response.WriteFile(Server.MapPath("~/App_Data/Overlays/" + fileName));
                            break;
                        }
                    }
                    break;

                default:
                    break;
            }


            //Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            //Response.ContentType = "application/zip";
            //Response.WriteFile(Server.MapPath("~/App_Data/Overlays/" + fileName));

            //Response.WriteFile( File(Server.MapPath("~/App_Data/Overlays/" + fileName), "application/zip", fileName));
        }

        [HttpPost]
        public async Task Clients(FormCollection fc)
        {
            string email = fc["Email"];
            string password = fc["Password"];

            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
            if (result == SignInStatus.Success)
            {
                string userRole = getUserRole(email);
                if (userRole.Equals("super admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Return list of clients only if this user is super admin

                    // Get a list of clients
                    List<string> clients = context.Roles.Select(s => s.Name).ToList();

                    string json = new JavaScriptSerializer().Serialize(Json(clients, JsonRequestBehavior.AllowGet).Data);
                    Response.Write(json);
                }
            }

        }
    }
}