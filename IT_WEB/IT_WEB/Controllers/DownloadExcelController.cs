using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using IT_WEB.Models;
using IT_WEB.Models.Combo_Model;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace IT_WEB.Controllers
{
    public class DownloadExcelController : Controller
    {
        public ActionResult DownloadExcel()
        {
            var user = (T_ADMINS)Session["User"];

            List<AssetModel> list = null;
            using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
            {

                list = (from data in dataBase.T_IT_ASSETS
                        orderby data.ID
                        where data.LOCATION == user.OFFICE
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
                            Po_No = data.PO_NO
                        }).ToList();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (SLDocument sl = new SLDocument())
                    {
                        // Headers
                        sl.SetCellValue(1, 1, "ID");
                        sl.SetCellValue(1, 2, "Asset Type");
                        sl.SetCellValue(1, 3, "Product Code");
                        sl.SetCellValue(1, 4, "Category");
                        sl.SetCellValue(1, 5, "Make");
                        sl.SetCellValue(1, 6, "Serial Number");
                        sl.SetCellValue(1, 7, "Asset Number");
                        sl.SetCellValue(1, 8, "Taggable");
                        sl.SetCellValue(1, 9, "Allocated To");
                        sl.SetCellValue(1, 10, "Po No");

                        // Add the data to the cells
                        int row = 2;

                        foreach (var item in list)
                        {
                            sl.SetCellValue(row, 1, item.Id);
                            sl.SetCellValue(row, 2, item.AssetType);
                            sl.SetCellValue(row, 3, item.ProductCode);
                            sl.SetCellValue(row, 4, item.Category);
                            sl.SetCellValue(row, 5, item.Make);
                            sl.SetCellValue(row, 6, item.SerialNumber);
                            //sl.SetCellValue(row, 7, item.AssetNumber);
                            sl.SetCellValue(row, 8, item.Taggable);
                            sl.SetCellValue(row, 9, item.AllocatedTo);
                            sl.SetCellValue(row, 10, item.Po_No);
                            row++;
                        }

                        // Save document
                        sl.SaveAs(ms);
                    }
                    ms.Position = 0;

                    // Return file
                    string fileName = "Report.xlsx";
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        public ActionResult Admin_Report(string location)
        {
            string fileName = "";
            List<AssetModel> list = null;
            using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
            {
                if (location == "ATL01") 
                {
                    list = (from data in dataBase.ATL_IT_ASSETS
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
                                Po_No = data.PO_NO
                            }).ToList();
                    fileName = "ATL01_Report.xlsx";
                }
                else if(location == "SJO01")
                {
                    list = (from data in dataBase.CR_IT_ASSETS
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
                                Po_No = data.PO_NO
                            }).ToList();
                    fileName = "SJO01_Report.xlsx";
                }
                else
                {
                    list = (from data in dataBase.PNQ_IT_ASSETS
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
                                Po_No = data.PO_NO
                            }).ToList();
                    fileName = "PNQ01_Report.xlsx";
                }
                

                using (MemoryStream ms = new MemoryStream())
                {
                    using (SLDocument sl = new SLDocument())
                    {
                        // Headers
                        sl.SetCellValue(1, 1, "Sr No");
                        sl.SetCellValue(1, 2, "Asset Type");
                        sl.SetCellValue(1, 3, "Product Code");
                        sl.SetCellValue(1, 4, "Category");
                        sl.SetCellValue(1, 5, "Make");
                        sl.SetCellValue(1, 6, "Serial Number");
                        sl.SetCellValue(1, 7, "Asset Number");
                        sl.SetCellValue(1, 8, "Taggable");
                        sl.SetCellValue(1, 9, "Allocated To");
                        sl.SetCellValue(1, 10, "Po No");

                        // Add the data to the cells
                        int row = 2;

                        foreach (var item in list)
                        {
                            sl.SetCellValue(row, 1, item.Id);
                            sl.SetCellValue(row, 2, item.AssetType);
                            sl.SetCellValue(row, 3, item.ProductCode);
                            sl.SetCellValue(row, 4, item.Category);
                            sl.SetCellValue(row, 5, item.Make);
                            sl.SetCellValue(row, 6, item.SerialNumber);
                            //sl.SetCellValue(row, 7, item.AssetNumber);
                            sl.SetCellValue(row, 8, item.Taggable);
                            sl.SetCellValue(row, 9, item.AllocatedTo);
                            sl.SetCellValue(row, 10, item.Po_No);
                            row++;
                        }

                        // Save document
                        sl.SaveAs(ms);
                    }
                    ms.Position = 0;

                    // Return file
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }
    }
}