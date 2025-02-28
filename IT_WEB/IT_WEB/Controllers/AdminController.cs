using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using IT_WEB.Data;
using IT_WEB.Models;
using IT_WEB.Models.Combo_Model;
using IT_WEB.Models.User_Model;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_WEB.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var user = (T_ADMINS)Session["User"];

            if(user.PERMITS == "2")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        public ActionResult AdminDetails()
        {
            List<UserModel> list = null;
            var user = (T_ADMINS)Session["User"];

            try
            {
                using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
                {
                    // Filter to show devices according to the location
                    if (user.PERMITS == "2")
                    {
                        list = (from data in dataBase.T_ADMINS
                                orderby data.ID
                                select new UserModel
                                {
                                    Id = data.ID,
                                    Name = data.NAME,
                                    Lastname = data.LAST_NAME,
                                    Email = data.EMAIL,
                                    Office = data.OFFICE
                                }).ToList();

                        return Json(new { data = list }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public ActionResult UserInfo(int id)
        {
            var user = (T_ADMINS)Session["User"];

            List<UserModel> list = null;
            using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
            {
                list = (from data in dataBase.T_ADMINS
                        where data.ID == id
                        select new UserModel
                        {
                            //I select the spaces that I need
                            Id = data.ID,
                            Name = data.NAME,
                            Lastname = data.LAST_NAME,
                            Email = data.EMAIL,
                            Office = data.OFFICE
                        }).ToList();

                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateUser(FormCollection collection)
        {
            UserModel user = new UserModel();
            C_Data ob_data = new C_Data();

            user.Name = collection["u_name"];
            user.Lastname = collection["u_lastname"];
            user.Email = collection["u_email"];
            user.Office = collection["u_office"];

            try
            {
                string response = ob_data.updateUserInfo(user.Name, user.Lastname, user.Email, user.Office);
                return Content(response);
            }
            catch (Exception ex) 
            {
                return Content("3");
            }
        }

        public ActionResult IT_Assets()
        {
            List<AssetModel> list = null;

            using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
            {
                list = (from data in dataBase.T_IT_ASSETS
                orderby data.ID
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
        }

        public ActionResult GetAsset(int param1)
        {
            List<AssetModel> list = null;

            try
            {
                using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
                {
                    list = (from data in dataBase.T_IT_ASSETS
                            where data.ID == param1
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
            catch (Exception ex) 
            {
                return Content("2");
            }
        }

        public ActionResult UpdateAsset(FormCollection collection)
        {
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
                item.SerialNumber.IsNullOrWhiteSpace())
            {
                return Content("3");
            }
            else
            {
                string response = ob_data.updateAsset(item.Id, item.AssetType, item.ProductCode, item.Category, item.Make,
                item.SerialNumber, Convert.ToInt32(item.AssetNumber), item.Taggable, item.AllocatedTo,
                item.EmailId, item.Po_No, item.EolEos, item.EolEosDate);
                return Content(response);
            }
        }

        public ActionResult DeleteAsset(FormCollection collection)
        {
            C_Data ob_data = new C_Data(); 

            var user = (T_ADMINS)Session["User"];
            string location = user.OFFICE;

            int id = Convert.ToInt32(collection["item"]);

            try
            {
                ob_data.deleteAsset(location, id);
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(2);
            }
        }

        public ActionResult InsertAsset(FormCollection collection)
        {
            var user = (T_ADMINS)Session["User"];
            //var location = user.OFFICE;

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

            string location = collection["location"];

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

        public ActionResult DeleteUser(FormCollection collection)
        {
            C_Data ob_data = new C_Data();
            int id = Convert.ToInt32(collection["ID"]);

            string response = ob_data.deleteUser(id);

            return Content(response);
        }

        public ActionResult InsertUser(FormCollection collection)
        {
            UserModel model = new UserModel();
            C_Data ob_data = new C_Data();

            model.Name = collection["name"];
            model.Lastname = collection["lastname"];
            model.Email = collection["email"];
            model.Office = collection["office"];

            string response = ob_data.insertUser(model.Name, model.Lastname, model.Email,model.Office);

            return Content(response);
        }
    }
}