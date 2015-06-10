using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wn_web.Models;
using wn_web.Models.Dropdowns;
using PagedList;

namespace wn_web.Controllers
{
    /// <summary>
    /// This controller is used for view, edit, create, delete FieldData
    /// </summary>
    public class FieldDataController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private wn_webContext db = new wn_webContext();

        // GET: FieldData
        [Authorize(Roles = "super admin")]
        [HttpGet]
        public ActionResult Index(string sValue0, string sValue1, string sValue2, 
            string sValue3, string sValue4, string sValue5, string sValue6, 
            string sValue7, string dateFrom, string dateTo, int? page)
        {
            SetSearchData();


            // Got search criteria, do search
            string group1 = sValue0;
            string crossingType = sValue1;
            string erosion = sValue2;
            string fpc = sValue3;
            string risk = sValue4;
            string safetyConcern = sValue5;
            string sedimentation = sValue6;
            string stream = sValue7;
            string dateFrom1 = dateFrom;
            string dateTo1 = dateTo;


            IQueryable<FieldData> filteredData = db.FieldDatas;
            if (!string.IsNullOrWhiteSpace(group1) && !group1.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.Group.ToUpper() == group1.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(crossingType) && !crossingType.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.CROSS_TYPE.ToUpper() == crossingType.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(erosion) && !erosion.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.EROSION.ToUpper() == erosion.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(fpc) && !fpc.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.FISH_PCONC.ToUpper() == fpc.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(risk) && !risk.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.RISK.ToUpper() == risk.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(safetyConcern) && !safetyConcern.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.STU_PROBS.ToUpper() == safetyConcern.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(sedimentation) && !sedimentation.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.SEDEMENTAT.ToUpper() == sedimentation.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(stream) && !stream.Contains("Select"))
            {
                filteredData = filteredData.Where(s => s.STR_CLASS.ToUpper().Equals( stream.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(dateFrom1))
            {
                string[] formats = { "yyyy-MM-dd" };
                DateTime from = DateTime.ParseExact(dateFrom1, formats, null, DateTimeStyles.None);
                filteredData = filteredData.Where(s => s.INSP_DATE >= from);
            }
            if (!string.IsNullOrWhiteSpace(dateTo1) && !group1.Contains("Select"))
            {
                string[] formats = { "yyyy-MM-dd" };
                DateTime to = DateTime.ParseExact(dateTo1, formats, null, DateTimeStyles.None);
                filteredData = filteredData.Where(s => s.INSP_DATE <= to);
            }



            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(filteredData.OrderByDescending(o => o.INSP_DATE).ToPagedList(pageNumber, pageSize));

        }


        [Authorize(Roles = "super admin")]
        private void SetSearchData()
        {
            List<SearchType> searchtypes = db.SearchTypes.OrderBy(o => o.SearchTypeName).ToList();
            List<AdvancedSearchItem> searchObjects = new List<AdvancedSearchItem>();

            AdvancedSearchItem asiGroup = new AdvancedSearchItem();
            asiGroup.Name = "Group";
            asiGroup.Options = new List<string>();
            var roles = context.Roles.ToList();
            foreach (IdentityRole role in roles)
            {
                asiGroup.Options.Add(role.Name);
            }
            searchObjects.Add(asiGroup);

            foreach (SearchType s in searchtypes)
            {
                AdvancedSearchItem asi = new AdvancedSearchItem();
                asi.Name = s.SearchTypeName;
                asi.Options = GetOptions(s.SearchTypeName.ToLower());

                searchObjects.Add(asi);

            }

            ViewBag.AdvSearchItems = searchObjects;
        }

        [Authorize(Roles = "super admin")]
        private List<string> GetOptions(string name)
        {
            if (name.Equals("risk"))
            {
                Risk r = new Risk();
                return r.getOptions();

            }
            else if (name.Equals("crossing type"))
            {
                CrossingType r = new CrossingType();
                return r.getOptions();

            }
            else if (name.Equals("erosion"))
            {
                Erosion r = new Erosion();
                return r.getOptions();

            }
            else if (name.Equals("fish passage concerns"))
            {
                FishPCType r = new FishPCType();
                return r.getOptions();

            }
            else if (name.Equals("sedimentation"))
            {
                Sedimentation r = new Sedimentation();
                return r.getOptions();

            }
            else if (name.Equals("stream type"))
            {
                StreamType r = new StreamType();
                return r.getOptions();

            }
            else if (name.Equals("safety concerns"))
            {
                SaftyConcernsType r = new SaftyConcernsType();
                return r.getOptions();
            }

            return null;
        }

        // GET: FieldData/Details/5
        [Authorize(Roles = "super admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldData fieldData = db.FieldDatas.Find(id);
            if (fieldData == null)
            {
                return HttpNotFound();
            }
            return View(fieldData);
        }

        // GET: FieldData/Create
        [Authorize(Roles = "super admin")]
        public ActionResult Create()
        {
            FieldData f = new FieldData();
            DateTime d = DateTime.Now;
            //string s = d.ToString("yyyy-MM-dd");
            //string[] formats = {"yyyy-MM-dd"};

            //d = DateTime.ParseExact(s, formats, null, DateTimeStyles.None);

            f.INSP_DATE = d;
            return View(f);
        }

        // POST: FieldData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "super admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Group,Client,INSP_DATE,TimeStamp,INSP_CREW,ACCESS,CROSS_NM,CROSS_ID,LAT,LONG,STR_ID,STR_CLASS,STR_WIDTH,STR_WIDTHM,CHANNEL_CREEK_DEPTH_LEFT,CHANNEL_CREEK_DEPTH_RIGHT,CHANNEL_CREEK_DEPTH_CENTER,FIRST_RIFFLE_DISTANCE,ROAD_FILL_ABOVE_CULVERT,DISPOSITION_ID,CROSS_TYPE,EROSION,EROSINO_TY1,EROSINO_TY2,EROSION_SO,EROSION_DE,EROSION_AR,BLOCKAGE,BLOC_MATR,BLOC_CAUS,CULV_SUBS,CULV_SLOPE,SCOUR_POOL,DELINEATOR,FISH_SAMP,FISH_SM,FISH_SPP,FISH_PCONC,FISH_SPP2,FISH_PCONCREASON,REMARKS,PHOTO_INUP,PHOTO_INDW,PHOTO_OTUP,PHOTO_OTDW,PHOTO_ROAD_LEFT,PHOTO_ROAD_RIGHT,PHOTO_1,PHOTO_2,CULV_LEN,CULV_SUBSP,CULV_SUBSTYPE,CULV_SUBSPROPORTION,CULV_BACKWATERPROPORTION,CULV_OUTLETTYPE,CULV_DIA_1,CULV_DIA_2,CULV_DIA_3,BRDG_LEN,EMG_REP_RE,STU_PROBS,SEDEMENTAT,CULV_OPOOD,CULV_OPGAP,HAZMARKR,APROCHSIGR,APROCHRAIL,RDSURFR,RDDRAINR,VISIBILITY,WEARSURF,RAILCURBR,GIRDEBRACR,CAPBEAMR,PILESR,ABUTWALR,WINGWALR,BANKSTABR,SLOPEPROTR,CHANNELOPEN,OBSTRUCTIO,ATTACHMENT,FUTURE2,FUTURE3,FUTURE4,FUTURE5,CULV_SUBSTYPE1,CULV_SUBSTYPE2,CULV_SUBSTYPE3,CULV_SUBSPROPORTION1,CULV_SUBSPROPORTION2,CULV_SUBSPROPORTION3,OUTLET_SCORE,RISKF,RISK")] FieldData fieldData)
        {
            if (ModelState.IsValid)
            {
                db.FieldDatas.Add(fieldData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fieldData);
        }

        // GET: FieldData/Edit/5
        [Authorize(Roles = "super admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldData fieldData = db.FieldDatas.Find(id);
            if (fieldData == null)
            {
                return HttpNotFound();
            }

            //ViewBag.roles = getOptions("Role");
            ViewBag.access = getOptions("Access");
            ViewBag.streamClass = getOptions("StreamClass");
            ViewBag.streamWidthMeasured = getOptions("StreamWM");
            ViewBag.crossingType = getOptions("CrossingType");
            ViewBag.erosionTY = getOptions("erosionTY");
            ViewBag.erosionSource = getOptions("erosionSource");
            ViewBag.erosionDegree = getOptions("erosionDegree");

            ViewBag.YesNo = getOptions("YesNo");
            ViewBag.YesNoPot = getOptions("YesNoPot");



            return View(fieldData);
        }

        private List<string> getOptions(string name)
        {
            List<string> options = new List<string>();

            if (name.Equals("Role"))
            {
                var roles = context.Roles.ToList();
                foreach (var r in roles)
                {
                    options.Add(r.Name);
                }
            }
            else if (name.Equals("Access"))
            {
                return new Access().getOptions();
            }
            else if (name.Equals("StreamClass"))
            {
                return new StreamType().getOptions();
            }
            else if (name.Equals("StreamWM"))
            {
                return new StreamWidthMeasured().getOptions();
            }
            else if (name.Equals("CrossingType"))
            {
                return new CrossingType().getOptions();
            }
            else if (name.Equals("YesNoPot"))
            {
                return new YesNoPot().getOptions();
            }
            else if (name.Equals("erosionTY"))
            {
                return new ErosionTY().getOptions();
            }
            else if (name.Equals("erosionSource"))
            {
                return new ErosionSource().getOptions();
            }
            else if (name.Equals("erosionDegree"))
            {
                return new ErosionDegree().getOptions();
            }
            else if (name.Equals("YesNo"))
            {
                List<string> o = new YesNoPot().getOptions();
                o.Remove("Pot");
                return o;
            }


            return options;
        }

        // POST: FieldData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "super admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Group,Client,INSP_DATE,TimeStamp,INSP_CREW,ACCESS,CROSS_NM,CROSS_ID,LAT,LONG,STR_ID,STR_CLASS,STR_WIDTH,STR_WIDTHM,CHANNEL_CREEK_DEPTH_LEFT,CHANNEL_CREEK_DEPTH_RIGHT,CHANNEL_CREEK_DEPTH_CENTER,FIRST_RIFFLE_DISTANCE,ROAD_FILL_ABOVE_CULVERT,DISPOSITION_ID,CROSS_TYPE,EROSION,EROSINO_TY1,EROSINO_TY2,EROSION_SO,EROSION_DE,EROSION_AR,BLOCKAGE,BLOC_MATR,BLOC_CAUS,CULV_SUBS,CULV_SLOPE,SCOUR_POOL,DELINEATOR,FISH_SAMP,FISH_SM,FISH_SPP,FISH_PCONC,FISH_SPP2,FISH_PCONCREASON,REMARKS,PHOTO_INUP,PHOTO_INDW,PHOTO_OTUP,PHOTO_OTDW,PHOTO_ROAD_LEFT,PHOTO_ROAD_RIGHT,PHOTO_1,PHOTO_2,CULV_LEN,CULV_SUBSP,CULV_SUBSTYPE,CULV_SUBSPROPORTION,CULV_BACKWATERPROPORTION,CULV_OUTLETTYPE,CULV_DIA_1,CULV_DIA_2,CULV_DIA_3,BRDG_LEN,EMG_REP_RE,STU_PROBS,SEDEMENTAT,CULV_OPOOD,CULV_OPGAP,HAZMARKR,APROCHSIGR,APROCHRAIL,RDSURFR,RDDRAINR,VISIBILITY,WEARSURF,RAILCURBR,GIRDEBRACR,CAPBEAMR,PILESR,ABUTWALR,WINGWALR,BANKSTABR,SLOPEPROTR,CHANNELOPEN,OBSTRUCTIO,ATTACHMENT,FUTURE2,FUTURE3,FUTURE4,FUTURE5,CULV_SUBSTYPE1,CULV_SUBSTYPE2,CULV_SUBSTYPE3,CULV_SUBSPROPORTION1,CULV_SUBSPROPORTION2,CULV_SUBSPROPORTION3,OUTLET_SCORE,RISKF,RISK")] FieldData fieldData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fieldData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.roles = getOptions("Role");
            ViewBag.access = getOptions("Access");
            ViewBag.streamClass = getOptions("StreamClass");
            ViewBag.streamWidthMeasured = getOptions("StreamWM");
            ViewBag.crossingType = getOptions("CrossingType");
            return View(fieldData);
        }

        // GET: FieldData/Delete/5
        [Authorize(Roles = "super admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldData fieldData = db.FieldDatas.Find(id);
            if (fieldData == null)
            {
                return HttpNotFound();
            }
            return View(fieldData);
        }

        // POST: FieldData/Delete/5
        [Authorize(Roles = "super admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FieldData fieldData = db.FieldDatas.Find(id);
            db.FieldDatas.Remove(fieldData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "super admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "super admin")]
        public ActionResult DeleteAll()
        {
            db.FieldDatas.RemoveRange(db.FieldDatas.Where(s => s.ID == s.ID));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ContentResult All()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            string uid = User.Identity.GetUserId();
            string role = "unknown";
            if (uid != null)
            {
                IList<string> roles = userManager.GetRoles(uid);
                if (roles != null && roles.Count > 0)
                {
                    role = roles[0];
                }
            }

            List<FieldData> data;
            if (role.Equals("super admin") || role.Equals("WOODN"))
            {
                data = db.FieldDatas.Where(d => d.ID == d.ID).ToList();
                
            }
            else
            {
                data = db.FieldDatas.Where(d => d.ID == d.ID && d.Group.Equals(role)).ToList();
            }
            String j = "";
            if (data != null && data.Count() > 0)
            {
                Hashtable ht = DataController.getLookupTable();
                j = new JavaScriptSerializer().Serialize(data);
                Type type = data[0].GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (ht.ContainsKey(property.Name))
                    {
                        j = j.Replace("\"" + property.Name + "\"", '"' + ht[property.Name].ToString() + '"');
                    }
                }
            }
            //Response.Write(j);

            //return Json(data, JsonRequestBehavior.AllowGet);
            return Content(j, "application/json");
        }
    }
}
