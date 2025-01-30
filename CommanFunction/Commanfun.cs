using NimaApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NimaApp.CommanFunction
{
    public class Commanfun
    {
//for  pagination
        public int GetTotalProductCount()
        {
            int totalCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString; ;

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM ProductMaster", conn))
                {
                    conn.Open();
                    totalCount = (int)cmd.ExecuteScalar();
                }
            }

            return totalCount;
        }

//For  connection with Database
        public string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["dbms"].ConnectionString ;
        }

        public int GetCategoryCount(CategoryMaster category)
        {
            int totalCount = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                string query = "SELECT COUNT(*) FROM categoryMaster WHERE categoryName = @categoryName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                    conn.Open();

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        totalCount = Convert.ToInt32(result);
                    }
                }
            }
            return totalCount;
        }


            



    }
}