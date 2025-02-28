using IT_WEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IT_WEB.Data
{
    public class C_Data
    {
        // SQL Connection
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        public void deleteAsset(string location, int sr_no)
        {
            SqlCommand command = new SqlCommand("DELETE_ASSET", connection);
            connection.Open();

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@LOCATION", System.Data.SqlDbType.NChar).Value = location;
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = sr_no;

            command.ExecuteNonQuery();
            connection.Close();
        }

        public string updateAsset(int id, string type, string code, string category, string make, string serial_no, int number,
            string taggable, string allocated_to, string emailId, int po_no, string eol_eos, string eol_eos_date)
        {
            SqlCommand command = new SqlCommand("UPDATE_ASSET", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandType = System.Data.CommandType.StoredProcedure;
                //command.Parameters.Add("@LOCATION", System.Data.SqlDbType.NChar).Value = location;
                command.Parameters.Add("@ID ", System.Data.SqlDbType.NChar).Value = id;
                command.Parameters.Add("@TYPE ", System.Data.SqlDbType.NChar).Value = type;
                command.Parameters.Add("@PRODUCT_CODE ", System.Data.SqlDbType.NChar).Value = code;
                command.Parameters.Add("@CATEGORY ", System.Data.SqlDbType.NChar).Value = category;
                command.Parameters.Add("@MAKE ", System.Data.SqlDbType.NChar).Value = make;
                command.Parameters.Add("@SN ", System.Data.SqlDbType.NChar).Value = serial_no;
                command.Parameters.Add("@ASSET_NO ", System.Data.SqlDbType.NChar).Value = number;
                command.Parameters.Add("@TAGGABLE ", System.Data.SqlDbType.NChar).Value = taggable;
                command.Parameters.Add("@ALLOCATED ", System.Data.SqlDbType.NChar).Value = allocated_to;
                command.Parameters.Add("@EMAIL ", System.Data.SqlDbType.NChar).Value = emailId;
                command.Parameters.Add("@PO_NO ", System.Data.SqlDbType.Int).Value = po_no;
                command.Parameters.Add("@EOL_EOS ", System.Data.SqlDbType.NChar).Value = eol_eos;
                command.Parameters.Add("@EOL_EOS_DATE ", System.Data.SqlDbType.NChar).Value = eol_eos_date;

                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "2";
            }
        }

        public string insertAsset(string location, string type, string code, string category, string make, string serial_no, int number,
            string taggable, string allocated_to, string emailId, int po_no, string eol_eos, string eol_eos_date)
        {
            SqlCommand command = new SqlCommand("INSERT_ASSET", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@TYPE ", System.Data.SqlDbType.NChar).Value = type;
                command.Parameters.Add("@PRODUCT_CODE ", System.Data.SqlDbType.NChar).Value = code;
                command.Parameters.Add("@CATEGORY ", System.Data.SqlDbType.NChar).Value = category;
                command.Parameters.Add("@MAKE ", System.Data.SqlDbType.NChar).Value = make;
                command.Parameters.Add("@SN ", System.Data.SqlDbType.NChar).Value = serial_no;
                command.Parameters.Add("@ASSET_NO ", System.Data.SqlDbType.Int).Value = number;
                command.Parameters.Add("@TAGGABLE ", System.Data.SqlDbType.NChar).Value = taggable;
                command.Parameters.Add("@ALLOCATED ", System.Data.SqlDbType.NChar).Value = allocated_to;
                command.Parameters.Add("@EMAIL ", System.Data.SqlDbType.NChar).Value = emailId;
                command.Parameters.Add("@PO_NO ", System.Data.SqlDbType.Int).Value = po_no;
                command.Parameters.Add("@EOL_EOS ", System.Data.SqlDbType.NChar).Value = eol_eos;
                command.Parameters.Add("@EOL_EOS_DATE ", System.Data.SqlDbType.NChar).Value = eol_eos_date;
                command.Parameters.Add("@LOCATION ", System.Data.SqlDbType.NChar).Value = location;

                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "2";
            }
        }

        public string updateUser(string email, string name, string lastname, string password)
        {
            SqlCommand command = new SqlCommand("UPDATE_USER", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.NChar).Value = email;
                command.Parameters.Add("@NAME ", System.Data.SqlDbType.NChar).Value = name;
                command.Parameters.Add("@LASTNAME ", System.Data.SqlDbType.NChar).Value = lastname;
                command.Parameters.Add("@PASSWORD ", System.Data.SqlDbType.NChar).Value = password;

                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "2";
            }
        }

        public int InsertExcelData(DataTable dataTable, string office)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT_ASSET", connection);

                foreach (DataRow row in dataTable.Rows)
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add("@LOCATION ", System.Data.SqlDbType.NChar).Value = row["LOCATION"];
                    command.Parameters.Add("@TYPE ", System.Data.SqlDbType.NChar).Value = row["Asset Type"];
                    command.Parameters.Add("@PRODUCT_CODE ", System.Data.SqlDbType.NChar).Value = row["Product Code"];
                    command.Parameters.Add("@CATEGORY ", System.Data.SqlDbType.NChar).Value = row["Category"];
                    command.Parameters.Add("@MAKE ", System.Data.SqlDbType.NChar).Value = row["Make"];
                    command.Parameters.Add("@SN ", System.Data.SqlDbType.NChar).Value = row["Serial Number"];
                    command.Parameters.Add("@ASSET_NO ", System.Data.SqlDbType.Int).Value = row["Asset Number"];
                    command.Parameters.Add("@TAGGABLE ", System.Data.SqlDbType.NChar).Value = row["Taggable or Non Taggable"];
                    command.Parameters.Add("@ALLOCATED ", System.Data.SqlDbType.NChar).Value = row["Allocated To"];
                    command.Parameters.Add("@EMAIL ", System.Data.SqlDbType.NChar).Value = row["Email ID"];
                    command.Parameters.Add("@PO_NO ", System.Data.SqlDbType.Int).Value = row["PO No"];
                    command.Parameters.Add("@EOL_EOS ", System.Data.SqlDbType.NChar).Value = row["EOL/EOS"];
                    command.Parameters.Add("@EOL_EOS_DATE ", System.Data.SqlDbType.NChar).Value = row["EOL/EOS Date"];
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    connection.Close();
                }

                return 1;
            }
            catch (Exception ex) 
            {
                return 2;
            }
        }

        // Admin method to update the user's information
        public string updateUserInfo(string name, string lastname, string email, string office)
        {
            SqlCommand command = new SqlCommand("A_UPDATE_USER", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@NAME", System.Data.SqlDbType.NChar).Value = name;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.NChar).Value = lastname;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.NChar).Value = email;
                command.Parameters.Add("@OFFICE", System.Data.SqlDbType.NChar).Value = office;

                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "2";
            }
        }

        public string a_updateAsset(int id, string type, string code, string category, string make, string serial_no, int number,
            string taggable, string allocated_to, string emailId, int po_no, string eol_eos, string eol_eos_date)
        {
            SqlCommand command = new SqlCommand("UPDATE_ASSET", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@ID ", System.Data.SqlDbType.NChar).Value = id;
                command.Parameters.Add("@TYPE ", System.Data.SqlDbType.NChar).Value = type;
                command.Parameters.Add("@PRODUCT_CODE ", System.Data.SqlDbType.NChar).Value = code;
                command.Parameters.Add("@CATEGORY ", System.Data.SqlDbType.NChar).Value = category;
                command.Parameters.Add("@MAKE ", System.Data.SqlDbType.NChar).Value = make;
                command.Parameters.Add("@SN ", System.Data.SqlDbType.NChar).Value = serial_no;
                command.Parameters.Add("@ASSET_NO ", System.Data.SqlDbType.NChar).Value = number;
                command.Parameters.Add("@TAGGABLE ", System.Data.SqlDbType.NChar).Value = taggable;
                command.Parameters.Add("@ALLOCATED ", System.Data.SqlDbType.NChar).Value = allocated_to;
                command.Parameters.Add("@EMAIL ", System.Data.SqlDbType.NChar).Value = emailId;
                command.Parameters.Add("@PO_NO ", System.Data.SqlDbType.Int).Value = po_no;
                command.Parameters.Add("@EOL_EOS ", System.Data.SqlDbType.NChar).Value = eol_eos;
                command.Parameters.Add("@EOL_EOS_DATE ", System.Data.SqlDbType.NChar).Value = eol_eos_date;

                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "2";
            }
        }

        public string deleteUser(int id)
        {
            SqlCommand command = new SqlCommand("DELETE_USER", connection);

            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@ID ", System.Data.SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex) {
                return "3";
            }
        }

        public string insertUser(string name, string lastname, string email, string office) 
        {
            SqlCommand command = new SqlCommand("INSERT_USER", connection);

            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@NAME", System.Data.SqlDbType.NChar).Value = name;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.NChar).Value = lastname;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.NChar).Value = email;
                command.Parameters.Add("@OFFICE", System.Data.SqlDbType.NChar).Value = office;
                command.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "3";
            }
        }
    }
}