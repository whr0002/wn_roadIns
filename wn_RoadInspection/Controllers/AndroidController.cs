using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using wn_web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using WN_Reclaimation;
using Microsoft.AspNet.Identity;
using wn_RoadInspection;

namespace wn_web.Controllers
{
    /// <summary>
    /// This controller is used for mobile app signin, form post.
    /// </summary>
    public class AndroidController : Controller
    {
        private ApplicationSignInManager signInManager;
        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpPost]
        [AllowAnonymous]
        public async Task Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.Write("Model state is not valid.");
                return;
            }
            signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            Dictionary<string, string> udemy = new Dictionary<string, string>();
            switch (result)
            {
                case SignInStatus.Success:                    
                    // Return Status, Username, Role in JSON         
                    //Dictionary<string, List<string>> kd = new Dictionary<string, List<string>>();

                    udemy.Add("Status", "success");
                    udemy.Add("Email", model.Email);
                    udemy.Add("Role", getRole(model.Email));
                    Response.Write(JsonConvert.SerializeObject(udemy));
                    break;

                case SignInStatus.Failure:
                    udemy.Add("Status", "failure");
                    udemy.Add("Message", "Invalid Email or password!");
                    Response.Write(JsonConvert.SerializeObject(udemy));
                    break;

                default:
                    udemy.Add("Status", "error");
                    udemy.Add("Message", "Server Errors");
                    Response.Write(JsonConvert.SerializeObject(udemy));
                    break;
            }
        }

        private string getRole(string username)
        {
            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            List<string> roles = new List<string>();
            if (user.Id != null)
            {
                roles = userManager.GetRoles(user.Id) as List<string>;
                if (roles.Count > 0)
                {
                    return roles[0];
                }
            }

            return null;

        }

        [Authorize(Roles="super admin")]
        public ActionResult FilePost()
        {
            return View();
        }


        [HttpPost]
        public async Task FilePost(FormCollection formCollection, [Bind(Include = "ID,UserName,Group,Client,INSP_DATE,TimeStamp,INSP_CREW,ACCESS,CROSS_NM,CROSS_ID,LAT,LONG,STR_ID,STR_CLASS,STR_WIDTH,STR_WIDTHM,CHANNEL_CREEK_DEPTH_LEFT,CHANNEL_CREEK_DEPTH_RIGHT,CHANNEL_CREEK_DEPTH_CENTER,FIRST_RIFFLE_DISTANCE,ROAD_FILL_ABOVE_CULVERT,DISPOSITION_ID,CROSS_TYPE,EROSION,EROSION_TY1,EROSION_TY2,EROSION_SO,EROSION_DE,EROSION_AR,BLOCKAGE,BLOC_MATR,BLOC_CAUS,CULV_SUBS,CULV_SLOPE,SCOUR_POOL,DELINEATOR,FISH_SAMP,FISH_SM,FISH_SPP,FISH_PCONC,FISH_SPP2,FISH_PCONCREASON,REMARKS,PHOTO_INUP,PHOTO_INDW,PHOTO_OTUP,PHOTO_OTDW,PHOTO_ROAD_LEFT,PHOTO_ROAD_RIGHT,PHOTO_1,PHOTO_2,CULV_LEN,CULV_SUBSP,CULV_SUBSTYPE,CULV_SUBSPROPORTION,CULV_BACKWATERPROPORTION,CULV_OUTLETTYPE,CULV_DIA_1,CULV_DIA_2,CULV_DIA_3,BRDG_LEN,EMG_REP_RE,STU_PROBS,SEDEMENTAT,CULV_OPOOD,CULV_OPGAP,HAZMARKR,APROCHSIGR,APROCHRAIL,RDSURFR,RDDRAINR,VISIBILITY,WEARSURF,RAILCURBR,GIRDEBRACR,CAPBEAMR,PILESR,ABUTWALR,WINGWALR,BANKSTABR,SLOPEPROTR,CHANNELOPEN,OBSTRUCTIO,ATTACHMENT,FUTURE2,FUTURE3,FUTURE4,FUTURE5,CULV_SUBSTYPE1,CULV_SUBSTYPE2,CULV_SUBSTYPE3,CULV_SUBSPROPORTION1,CULV_SUBSPROPORTION2,CULV_SUBSPROPORTION3,OUTLET_SCORE,RISKF,RISK")]FieldData fieldData)
        {

            if (Request != null)
            {
                // Sign in check first
                string username = formCollection["Email"];
                string password = formCollection["Password"];
                ApplicationSignInManager signinManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                var result = await signinManager.PasswordSignInAsync(username, password, false, false);
                if (result == SignInStatus.Success) { 

                    List<string> imageNames = new List<string>();
                    imageNames.Add("image1");
                    imageNames.Add("image2");
                    imageNames.Add("image3");
                    imageNames.Add("image4");
                    imageNames.Add("image5");
                    imageNames.Add("image6");
                    imageNames.Add("image7");
                    imageNames.Add("image8");
                    imageNames.Add("image9");

                    //string role = formCollection["Role"];
                    string role = getRole(username);

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("unknown");

                    if (role != null && role.Length > 0)
                    {
                        role = role.ToLower();
                        if (role.Equals("super admin"))
                        {
                            //if (fieldData != null) { 
                            //    string temp = fieldData.Client;
                            //    if (temp != null && temp.Length > 0)
                            //    {
                            //        // Collected data for other client, save them based on 'Client'
                            //        temp = temp.ToLower();

                            //        if (temp.Equals("super admin")) {
                            //            temp = "superadmin";
                                    
                            //        }
                            //        container = blobClient.GetContainerReference(temp);
                            //    }
                            //}

                            // Collected data for ourselves, save them to 'superadmin' folder
                            container = blobClient.GetContainerReference("superadmin");

                        }
                        else
                        {
                            container = blobClient.GetContainerReference(role);
                        }

                        if (container.CreateIfNotExists())
                        {
                            var permissions = container.GetPermissions();
                            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                            container.SetPermissions(permissions);
                        }

                    }

                    bool isSuccess = true;
                    for (int i = 0; i < imageNames.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[imageNames[i]];

                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;

                            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

                            blob.Properties.ContentType = fileContentType;
                            blob.UploadFromStream(file.InputStream);

                            //Response.Write("File Name: " + fileName + "<br />File Upload Done!<br />");
                            //Response.Write("Uri: " + blob.Uri.ToString());

                            if (!blob.Exists())
                            {
                                isSuccess = false;
                            }

                        }
                    }

                    if (!isSuccess)
                    {
                        Response.Write("Fail");
                        return;
                    }

                    wn_webContext db = new wn_webContext();
                    if (ModelState.IsValid)
                    {
                        FieldData fd = db.FieldDatas.Where(w => 
                            w.UserName.Equals(fieldData.UserName)
                            && w.TimeStamp.Equals(fieldData.TimeStamp)
                            && w.LAT == fieldData.LAT
                            && w.LONG == fieldData.LONG).FirstOrDefault();

                        if (fd == null) { 
                            db.FieldDatas.Add(fieldData);
                            db.SaveChanges();
                        }else if (fieldData.TimeStamp == null 
                            || (fieldData.TimeStamp != null && fieldData.TimeStamp.Equals(""))
                            || fieldData.LAT == 0 
                            || fieldData.LONG == 0)
                        {
                            db.FieldDatas.Add(fieldData);
                            db.SaveChanges();
                        }
                        
                        Response.Write("Form submitted");
                    }
                    else
                    {
                        Response.Write("Cannot submit form ");
                    }

                }
                else
                {
                    Response.Write("Photo upload fail");
                }


            }
            else
            {
                Response.Write("Form upload fail");
            }
            
        }
      


        [Authorize(Roles="super admin")]
        [HttpPost]
        public void FilePostTest(FormCollection fc,[Bind(Include="work")]string work)
        {
            HttpPostedFileBase file = Request.Files["document"];
            if (file != null)
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();


                CloudBlobContainer container = blobClient.GetContainerReference("superadmin");
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

                blob.Properties.ContentType = fileContentType;
                blob.UploadFromStream(file.InputStream);

                Response.Write("File Name: " + fileName + "<br />File Upload Done!<br />");
                Response.Write("Uri: " + blob.Uri.ToString());
            }
            //if (work != null) { 
            //Response.Write("Work: " + work);
            //}
            //else
            //{
            //    Response.Write("Work is null");
            //}
        }
        

    }
}