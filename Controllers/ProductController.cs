using NimaApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using NimaApp.CommanFunction;

namespace NimaApp.Controllers
{
    public class ProductController : Controller
    {
       public Commanfun Commanfun=new Commanfun();

      DbContextProdcm DbContextProdcm=new DbContextProdcm();

        public ActionResult getdata(int pageNumber = 1, int pageSize = 10)
        {
            int offset = (pageNumber - 1) * pageSize;
            List<ProductMaster> products = new List<ProductMaster>();

            // Define your connection string here
            string cs = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString;

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("GetDataTwo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Read the data from the SQL query
                    while (reader.Read())
                    {
                        products.Add(new ProductMaster
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }
                }
            }

            
            int totalCount = Commanfun.GetTotalProductCount();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(products);
        }

        






        // get Categories 
        public ActionResult CreateProduct() 
        {
            List<CategoryMaster> categories = new List<CategoryMaster>();
            string connectionString = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCategories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        categories.Add(new CategoryMaster
                        {
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }
                }
            }

            // Send categories to the view using ViewBag
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");




            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductMaster product)
        {
            var s = DbContextProdcm.CreateCategory(product);

            if (s!= null)
            {
                TempData["CreateProduct"] = "Product Add successfully";
                
                return RedirectToAction("getdata");  
            }
            else
            {
                return View();
            }
        }

        //Delete Product

        [HttpGet]
        public ActionResult DeletebyId(int id)
        {
          var productDelete=  DbContextProdcm.DeleteProduct(id);
            if (productDelete != null)
            {
                TempData["deleteMsg"] = "Delete Product Succsefully";
                return RedirectToAction("getdata");
            }
            return RedirectToAction("getdata");
        }

        [HttpGet]
        public ActionResult Edit(int id) 
        {
            List<CategoryMaster> categories = new List<CategoryMaster>();
            string connectionString = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCategories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        categories.Add(new CategoryMaster
                        {
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }
                }
            }

            // Send categories to the view using ViewBag
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");




            var list=DbContextProdcm.Edit(id);
        
        
        return View(list);
        }

        [HttpPost]
        public ActionResult Edit(ProductMaster productMaster) 
        {
           var update= DbContextProdcm.Edit(productMaster);
            if (update !=null)
            {
                TempData["Updatemsg"] = "Data Update Successfully";
                return RedirectToAction("getdata");
            }
            else
            {
            return View();
            }
        }


        [HttpGet]
        public ActionResult Details(int id) 
        { 
            var details= DbContextProdcm.Edit(id);
                
        return View(details);
        }







    }
}