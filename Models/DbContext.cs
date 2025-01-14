using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc.Ajax;
using System.Runtime.InteropServices;

namespace NimaApp.Models
{
    public class DbContext
    {
        public string cs = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString;
        SqlConnection conn=null;
        SqlCommand cmd=null;


        // Retrive Data From CategoryMaster

        public List<CategoryMaster> GetCategories(int pageNumber, int pageSize, out int totalRecords)
        {
            List<CategoryMaster> categories = new List<CategoryMaster>();
            totalRecords = 0; // Initialize total record count

            // Calculate the offset
            int offset = (pageNumber - 1) * pageSize;

            
            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("Readcategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Read category data
                    while (reader.Read())
                    {
                        categories.Add(new CategoryMaster
                        {
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }

                    reader.Close();

                    // Retrieve the total record count
                    cmd.CommandText = "SELECT COUNT(*) FROM CategoryMaster";
                    cmd.CommandType = CommandType.Text;

                    totalRecords = (int)cmd.ExecuteScalar();
                }
            }

            return categories;
        }



        //Insert data On CategoryMaster
        public bool CreateCategory(CategoryMaster categoryMaster)
        { int a = 0;

            using (conn = new SqlConnection(cs))
            { 
                using(cmd= new SqlCommand("InsertCategory", conn))
                {
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryName", categoryMaster.CategoryName);
                    conn.Open();
                    a= cmd.ExecuteNonQuery();
                }
            }
            return a> 0 ?true : false;  
        }

        //delete category
        public bool DeleteCategory(int id)
        {
            int a = 0;  
            using (conn = new SqlConnection(cs))
            {
                using ( cmd =new SqlCommand("DeleteCate", conn))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue ("id", id);
                    conn.Open();
                    a= cmd.ExecuteNonQuery();
                    return a > 0? true:false;
                }
            }
        }


        // read for Update by id
        public CategoryMaster GetById(int id)
        {
            using (conn = new SqlConnection(cs))
            {
                using (cmd = new SqlCommand("GetById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return  new CategoryMaster()
                        {
                            CategoryId= Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString(),   
                        };
                          

                    }
                }
            }
                    return null;
        }




        //Update category
        public bool Edit(CategoryMaster categoryMaster)
        {
            int a = 0;  
            using (conn = new SqlConnection(cs))
            {
                using ( cmd =new SqlCommand("UpdateData", conn))
                {
                    cmd.CommandType= CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", categoryMaster.CategoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryMaster.CategoryName);
                    conn.Open();
                    a= cmd.ExecuteNonQuery();
                    return a > 0? true:false;
                }
            }
        }



    }
}