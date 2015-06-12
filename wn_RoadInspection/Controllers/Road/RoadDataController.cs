using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using wn_RoadInspection.Models.RoadInspection;
using Microsoft.AspNet.Identity.Owin;
using wn_web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;
using wn_web.Models.Reclaimation.Report;

namespace wn_RoadInspection.Controllers.Road
{
    public class RoadDataController : Controller
    {
        private static readonly string STORAGE_BASE_URL = "https://roadinspection.blob.core.windows.net/";
        private wn_webContext db = new wn_webContext();

        [HttpPost]
        public async Task RoadFormSubmit(FormCollection fc, [Bind(Include = "NumberOfImages")]int NumberOfImages, [Bind(Include = "RoadFormID,UserName,Group,Client,InspectorName,INSP_DATE,Licence,RoadName,DLO,KmFrom,KmTo,RoadStatus,StatusMatch,RS_Condition,RS_Notification,RS_RoadSurface,RS_GravelCondition,RS_VegetationCover,RS_CoverType,DI_Ditches,DI_VegetationCover,DI_CoverType,OT_Signage,OT_Crossings,OT_GroundAccess,OT_RoadMR,OT_RoadRIA,OT_Comments,Locations")]RoadInspection roadForm)
        {
            if (Request != null)
            {
                var username = fc["Email"];
                var password = fc["Password"];
                ApplicationSignInManager signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                var result = await signInManager.PasswordSignInAsync(username, password, false, false);

                if (result == SignInStatus.Success)
                {
                    var role = getRole(username);
                    CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                    CloudBlobClient client = account.CreateCloudBlobClient();
                    CloudBlobContainer container = client.GetContainerReference(getContainerName(role));

                    if (container.CreateIfNotExists())
                    {
                        var permission = container.GetPermissions();
                        permission.PublicAccess = BlobContainerPublicAccessType.Container;
                        container.SetPermissions(permission);
                    }


                    if(ModelState.IsValid){
                        RoadInspection temp = db.RoadInspections.Where(a => a.UserName.Equals(roadForm.UserName)
                            && a.INSP_DATE.Equals(roadForm.INSP_DATE)).FirstOrDefault();

                        int formID;
                        if (temp == null)
                        {
                            db.RoadInspections.Add(roadForm);
                            db.SaveChanges();

                            formID = roadForm.RoadInspectionID;
                        }
                        else
                        {
                            formID = temp.RoadInspectionID;
                        }

                        // Store images
                        bool isSuccess = true;

                        for (int i = 0; i < NumberOfImages; i++)
                        {
                            Photo p = new Photo();

                            p.Path = STORAGE_BASE_URL + container.Name + "/" + fc["ImageName" + i];
                            p.FormTypeName = fc["ImageFormType" + i];
                            p.FormID = formID;
                            p.Description = fc["ImageDesc" + i];
                            p.Classification = fc["ImageClass" + i];

                            Photo tempPhoto = db.Photos.Where(w => w.Path.Equals(p.Path)).FirstOrDefault();

                            if (tempPhoto == null)
                            {
                                db.Photos.Add(p);
                            }

                            HttpPostedFileBase file = Request.Files["Image" + i];
                            if (file != null && file.ContentLength > 0 && !String.IsNullOrEmpty(file.FileName))
                            {
                                CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);
                                blob.Properties.ContentType = file.ContentType;
                                blob.UploadFromStream(file.InputStream);

                                if (!blob.Exists())
                                {
                                    isSuccess = false;
                                }
                            }
                        } // End of Image Upload loop

                        if (isSuccess)
                        {
                            db.SaveChanges();
                            Response.Write("Form Submitted");
                        }
                        else
                        {
                            Response.Write("fail");
                        }
                    }
                    else
                    {
                        Response.Write("Model State is not valid");
                    }
                    

                }
                else
                {
                    Response.Write("fail");
                }
               
            }
            else
            {
                Response.Write("fail");
            }

        }

        private String getContainerName(String role) {

            if (role != null)
            {
                if (role.Equals("super admin"))
                {
                    return "superadmin";
                }
                else
                {
                    return role.ToLower();
                }
            }


            return "unknown";
        }

        private String getRole(String email) {

            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser user = context.Users.Where(w => w.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            List<String> roles = userManager.GetRoles(user.Id) as List<String>;

            if (roles != null && roles.Count() > 0)
            {
                return roles[0];
            }

            return null;
        }

    }

    
}