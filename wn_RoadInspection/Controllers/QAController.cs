using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using wn_web.Models;

namespace wn_web.Controllers
{
    /// <summary>
    /// This controller is used for viewing and deleting files in Azure Storage Account
    /// </summary>
    [Authorize(Roles="super admin")]
    public class QAController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: QA
        public ActionResult Index()
        {


            var roles = db.Roles.ToList();
            return View(roles);
        }

        public ActionResult ViewContainer(string roleName)
        {
            ViewBag.roleName = roleName;
            List<Uri> imageUrls = new List<Uri>();
            if (roleName != null) { 
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                string r = roleName.ToLower();
                string containerName = "unknown";
                if (r.Equals("super admin"))
                {
                    containerName = "superadmin";
                }
                else
                {
                    containerName = r;
                }

                CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                if (container.CreateIfNotExists())
                {
                    var permissions = container.GetPermissions();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions);
                }

                
                foreach (IListBlobItem item in container.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        imageUrls.Add(blob.Uri);
                    }
                }


            }
            return View(imageUrls);
        }

        public ActionResult Clear(string roleName)
        {
            if (roleName != null)
            {
                string roleNameCpy = roleName.ToLower();
                string containerName = null;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                if (roleNameCpy.Equals("super admin"))
                {
                    containerName = "superadmin";
                }
                else
                {
                    containerName = roleNameCpy;
                }

                if (containerName != null)
                {
                    CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                    if (container.Exists())
                    {
                        // Get distinct image urls from this group

                        // Loop through each image in container
                        foreach (IListBlobItem b in container.ListBlobs(null, false))
                        {
                            string url = b.Uri.ToString();
                            int index = url.LastIndexOf("/");
                            if(index > -1){
                                string fileName = url.Substring(index+1);
                                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                                if (blockBlob != null)
                                {
                                    blockBlob.Delete();
                                }
                            }
                            
                            
                            
                        }
                    }
                }
            }

            return RedirectToAction("ViewContainer", "QA", new { roleName = roleName });
        }
    }
}