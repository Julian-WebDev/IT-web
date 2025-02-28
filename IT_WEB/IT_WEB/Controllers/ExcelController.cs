using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_WEB.Data;
using IT_WEB.Models;
using SpreadsheetLight;

namespace IT_WEB.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult Index(HttpPostedFileBase excelFile)
        {
            try
            {
                if (excelFile != null && excelFile.ContentLength > 0)
                {
                    string path = Server.MapPath("~/App_Data/") + excelFile.FileName;
                    if (Path.GetExtension(path).ToLower() == ".xlsx")
                    {
                        excelFile.SaveAs(path);
                        int response = ProcessExcelFile(path);

                        // Validation
                        if (response == 1)
                        {
                            ViewBag.Message = "Data uploaded successfully!";
                        }
                        else if(response == 2)
                        {
                            ViewBag.Message = "Something went wrong, please check the document and try again!";
                        }
                        else
                        {
                            ViewBag.Message = "An error occurred, please try again later!";
                        }
                        // End of validation
                    }
                    else
                    {
                        ViewBag.Message = "Make sure you upload a .xlsx document!";
                    }
                }
                else
                {
                    ViewBag.Message = "Please select a file";
                }

                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        private int ProcessExcelFile(string filePath)
        {
            C_Data ob_data = new C_Data();
            DataTable dataTable = new DataTable();
            var user = (T_ADMINS)Session["User"];

            Path.GetExtension(filePath).ToLower();

            try
            {
                using (SLDocument sl = new SLDocument(filePath))
                {
                    int colCount = sl.GetWorksheetStatistics().EndColumnIndex;
                    int rowCount = sl.GetWorksheetStatistics().EndRowIndex;

                    for (int col = 1; col <= colCount; col++)
                    {
                        dataTable.Columns.Add(sl.GetCellValueAsString(1, col));
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            dataRow[col - 1] = sl.GetCellValueAsString(row, col);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
                int response = ob_data.InsertExcelData(dataTable, user.OFFICE);

                // Validation of the response
                if(response == 1)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
                // End of validation
            }
            catch (Exception ex) 
            {
                return 3;
            }
        }
    }
}