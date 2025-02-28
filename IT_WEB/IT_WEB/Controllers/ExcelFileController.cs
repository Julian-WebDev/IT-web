using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_WEB.Controllers
{
    public class ExcelFileController : Controller
    {
        // GET: ExcelFile
        public ActionResult Index(HttpPostedFileBase excelFile)
        {
            try 
            {
                if (excelFile != null && excelFile.ContentLength > 0)
                {
                    string path = Server.MapPath("~/App_Data/") + excelFile.FileName;
                    excelFile.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";

                    // Aquí puedes llamar a una función para procesar el archivo Excel
                    ProcessExcelFile(path);
                }
                else
                {
                    ViewBag.Message = "Please select a file";
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        private void ProcessExcelFile(string filePath)
        {
            DataTable dataTable = new DataTable();

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
            InsertData(dataTable);
        }

        static void InsertData(DataTable dataTable)
        {
            string connectionString = "Data Source=DESKTOP-1U8HJ7G\\SQLEXPRESS;Initial Catalog=IT_Assets;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataRow row in dataTable.Rows)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO CR_IT_ASSETS (ASSET_TYPE, PRODUCT_CODE, CATEGORY, MAKE, SERIAL_NUMBER, ASSET_NUMBER, TAGGABLE, ALLOCATED_TO, EMAIL_ID, PO_NO, EOL_or_EOS, EOL_EOS_DATE) VALUES" +
                        " (@ASSET_TYPE, @PRODUCT_CODE, @CATEGORY, @MAKE, @SERIAL_NUMBER, @ASSET_NUMBER, @TAGGABLE, " +
                        "@ALLOCATED_TO, @EMAIL_ID, @PO_NO, @EOL_or_EOS, @EOL_EOS_DATE)", connection))
                    {
                        command.Parameters.AddWithValue("@ASSET_TYPE", row["Asset Type"]);
                        command.Parameters.AddWithValue("@PRODUCT_CODE", row["Product Code"]);
                        command.Parameters.AddWithValue("@CATEGORY", row["Category"]);
                        command.Parameters.AddWithValue("@MAKE", row["Make"]);
                        command.Parameters.AddWithValue("@SERIAL_NUMBER", row["Serial Number"]);
                        command.Parameters.AddWithValue("@ASSET_NUMBER", row["Asset Number"]);
                        command.Parameters.AddWithValue("@TAGGABLE", row["Taggable or Non Taggable"]);
                        command.Parameters.AddWithValue("@ALLOCATED_TO", row["Allocated To"]);
                        command.Parameters.AddWithValue("@EMAIL_ID", row["Email ID"]);
                        command.Parameters.AddWithValue("@PO_NO", row["PO No"]);
                        command.Parameters.AddWithValue("@EOL_or_EOS", row["EOL/EOS"]);
                        command.Parameters.AddWithValue("@EOL_EOS_DATE", row["EOL/EOS Date"]);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}