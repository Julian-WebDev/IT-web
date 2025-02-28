using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;
using IT_WEB.Data;
using IT_WEB.Models;
using IT_WEB.Models.Combo_Model;
using Microsoft.Ajax.Utilities;

namespace IT_WEB.Controllers
{
    public class HomeController : Controller
    {
        C_Data ob_data = new C_Data();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAssets()
        {
            var user = (T_ADMINS)Session["User"];
            var location = user.OFFICE;

            List<AssetModel> list = null;
            using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
            {
                // Filter to show devices according to the location
                if(user.OFFICE == "SJO01")
                {
                    list = (from data in dataBase.T_IT_ASSETS
                            orderby data.ID
                            where data.LOCATION == location
                            select new AssetModel
                            {
                                Id = data.ID,
                                AssetType = data.ASSET_TYPE,
                                ProductCode = data.PRODUCT_CODE,
                                Category = data.CATEGORY,
                                Make = data.MAKE,
                                SerialNumber = data.SERIAL_NUMBER,
                                AssetNumber = data.ASSET_NUMBER,
                                Taggable = data.TAGGABLE,
                                AllocatedTo = data.ALLOCATED_TO,
                                Po_No = data.PO_NO,
                                EolEos = data.EOL_EOS,
                            }).ToList();

                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }
                else if(user.OFFICE == "ATL01") // Atlanta assets
                {
                    list = (from data in dataBase.T_IT_ASSETS
                            orderby data.ID
                            where data.LOCATION == location
                            select new AssetModel
                            {
                                Id = data.ID,
                                AssetType = data.ASSET_TYPE,
                                ProductCode = data.PRODUCT_CODE,
                                Category = data.CATEGORY,
                                Make = data.MAKE,
                                SerialNumber = data.SERIAL_NUMBER,
                                AssetNumber = data.ASSET_NUMBER,
                                Taggable = data.TAGGABLE,
                                AllocatedTo = data.ALLOCATED_TO,
                                Po_No = data.PO_NO,
                                EolEos = data.EOL_EOS,
                            }).ToList();

                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }
                else if (user.OFFICE == "PNQ01")
                {
                    list = (from data in dataBase.T_IT_ASSETS
                            orderby data.ID
                            where data.LOCATION == location
                            select new AssetModel
                            {
                                Id = data.ID,
                                AssetType = data.ASSET_TYPE,
                                ProductCode = data.PRODUCT_CODE,
                                Category = data.CATEGORY,
                                Make = data.MAKE,
                                SerialNumber = data.SERIAL_NUMBER,
                                AssetNumber = data.ASSET_NUMBER,
                                Taggable = data.TAGGABLE,
                                AllocatedTo = data.ALLOCATED_TO,
                                Po_No = data.PO_NO,
                                EolEos = data.EOL_EOS,
                            }).ToList();

                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult DeleteAsset(FormCollection collection)
        {
            var user = (T_ADMINS)Session["User"];
            int id = Convert.ToInt32(collection["item"]);
            try
            {
                ob_data.deleteAsset(user.OFFICE, id);
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(2);
            }
        }

        // Obtain owner's data by email
        public ActionResult AssetData(int id)
        {
            var user = (T_ADMINS)Session["User"];

            List<AssetModel> list = null;
            using (IT_Assets_Entities dataBase = new IT_Assets_Entities()) {
                list = (from data in dataBase.T_IT_ASSETS
                        where data.ID == id
                        select new AssetModel
                        {
                            //I select the spaces that I need
                            Id = data.ID,
                            AssetType = data.ASSET_TYPE,
                            ProductCode = data.PRODUCT_CODE,
                            Category = data.CATEGORY,
                            Make = data.MAKE,
                            SerialNumber = data.SERIAL_NUMBER,
                            AssetNumber = data.ASSET_NUMBER,
                            Taggable = data.TAGGABLE,
                            AllocatedTo = data.ALLOCATED_TO,
                            EmailId = data.EMAIL_ID,
                            Po_No = data.PO_NO,
                            EolEos = data.EOL_EOS,
                            EolEosDate = data.EOL_EOS_DATE
                        }).ToList();

                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateAsset(FormCollection collection)
        {
            var user = (T_ADMINS)Session["User"];
            var location = user.OFFICE;

            AssetModel item = new AssetModel();
            C_Data ob_data = new C_Data();

            item.Id = Convert.ToInt32(collection["u_id"]);
            item.AssetType = collection["u_type"];
            item.ProductCode = collection["u_p_code"];
            item.Category = collection["u_category"];
            item.Make = collection["u_make"];
            item.SerialNumber = collection["u_sn"];
            item.AssetNumber = Convert.ToInt32(collection["u_asset_no"]); // Count how many items exist
            item.Taggable = collection["u_taggable"];
            item.AllocatedTo = collection["u_allocated"];
            item.EmailId = collection["u_email"];
            item.Po_No = Convert.ToInt32(collection["u_po_no"]);
            item.EolEos = collection["u_eol"];
            item.EolEosDate = collection["u_eol_date"];

            if (item.AssetType.IsNullOrWhiteSpace() || item.ProductCode.IsNullOrWhiteSpace() ||
                item.SerialNumber.IsNullOrWhiteSpace()){
                return Content("3");
            }
            else
            {
                string response = ob_data.updateAsset(item.Id, item.AssetType, item.ProductCode, item.Category, item.Make,
                item.SerialNumber, Convert.ToInt32(item.AssetNumber), item.Taggable, item.AllocatedTo,
                item.EmailId, item.Po_No, item.EolEos, item.EolEosDate
                );
                return Content(response);
            }
        }

        public ActionResult InsertAsset(FormCollection collection) 
        {
            var user = (T_ADMINS)Session["User"];
            var location = user.OFFICE;

            AssetModel item = new AssetModel();
            C_Data ob_data = new C_Data();

            item.Id = Convert.ToInt32(collection["id"]);
            item.AssetType = collection["type"];
            item.ProductCode = collection["p_code"];
            item.Category = collection["category"];
            item.Make = collection["make"];
            item.SerialNumber = collection["sn"];
            item.AssetNumber = Convert.ToInt32(collection["asset_no"]); // Count how many items exist
            item.Taggable = collection["taggable"];
            item.AllocatedTo = collection["allocated"];
            item.EmailId = collection["email"];
            item.Po_No = Convert.ToInt32(collection["po_no"]);
            item.EolEos = collection["eol"];
            item.EolEosDate = collection["eol_date"];

            if (item.AssetType.IsNullOrWhiteSpace() || item.ProductCode.IsNullOrWhiteSpace() ||
                item.SerialNumber.IsNullOrWhiteSpace())
            {
                return Content("3");
            }
            else
            {
                string response = ob_data.insertAsset(
                location, item.AssetType, item.ProductCode, item.Category, item.Make,
                item.SerialNumber, Convert.ToInt32(item.AssetNumber), item.Taggable, item.AllocatedTo,
                item.EmailId, item.Po_No, item.EolEos, item.EolEosDate
                );
                return Content(response);
            }
        }
    }
}