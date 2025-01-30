using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NimaApp.Models
{
    public class DbContextProdcm
    {
        public string cs = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString;
        SqlConnection conn = null;
        SqlCommand cmd = null;


        // Retrive Data From Product Master

        public List<ProductMaster> ProducCateList()
        {
            
            List<ProductMaster> productMasters = new List<ProductMaster>();

            using (conn = new SqlConnection(cs))
            {
                using (cmd = new SqlCommand("GetDataTwo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductMaster product = new ProductMaster()
                        {
                            ProductId= Convert.ToInt32(reader["ProductId"]),
                            ProductName= reader["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32( reader["CategoryId"]) ,
                            CategoryName = reader["CategoryName"].ToString() ,
                        };
                        productMasters.Add(product);
                    }
                    return productMasters;
                }

            }
        }
        //Insert data On ProductMaster
        
        public bool CreateCategory(ProductMaster productMaster)
        {
            int a = 0;

            using (conn = new SqlConnection(cs))
            {
                using (cmd = new SqlCommand("InsertProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", productMaster.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryId", productMaster.CategoryId);
                    conn.Open();
                    a = cmd.ExecuteNonQuery();
                }
            }
            return a > 0 ? true : false;
        }

        //delete category
        public bool DeleteProduct(int id)
        {
            int a = 0;
            using (conn = new SqlConnection(cs))
            {
                using (cmd = new SqlCommand("Deleteprod", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    conn.Open();
                    a = cmd.ExecuteNonQuery();
                    return a > 0 ? true : false;
                }
            }
        }


        // read for Update by id
        public ProductMaster Edit(int id)
        {
            using (conn = new SqlConnection(cs))
            {
                using (cmd = new SqlCommand("ProductGetbyID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return new ProductMaster()
                        {
                            CategoryName = reader["CategoryName"].ToString(),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName=reader["ProductName"].ToString(),
                        };


                    }
                }
            }
            return null;
        }




        //Update category
        public bool Edit(ProductMaster productMaster)
        {
            int a = 0;
            using (conn = new SqlConnection(cs))
            {
                using (cmd = new SqlCommand("UpdateProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", productMaster.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", productMaster.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryId", productMaster.CategoryId);
                    conn.Open();
                    a = cmd.ExecuteNonQuery();
                    return a > 0 ? true : false;
                }
            }
        }



    }
}