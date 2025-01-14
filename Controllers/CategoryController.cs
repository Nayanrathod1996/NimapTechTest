using NimaApp.CommanFunction;
using NimaApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace NimaApp.Controllers
{
    public class CategoryController : Controller
    {
        Commanfun commanfun=new Commanfun();
        DbContext _dbContext = new DbContext();

        //retrive data from Category Master
        [HttpGet]
        public ActionResult GetCategory(int pageNumber = 1, int pageSize = 10)
        {
            int totalRecords;
            var categories = _dbContext.GetCategories(pageNumber, pageSize, out totalRecords);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return View(categories);
        }

        //insert Category Get Method 

        [HttpGet]
        public ActionResult CreateCategory()
        {


            return View();
        }

        // insert Post Method
        [HttpPost]
        public ActionResult CreateCategory(CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                var data = _dbContext.CreateCategory(categoryMaster);
                if (data != null)
                {
                    TempData["Massage"] = "Category Add Successfully";
                    return RedirectToAction("GetCategory");
                }
                else
                {
                    return View();

                }
            }
            else
            {
                return View();
            }

           
        }
        // Delete Action Method To Delete Data 
        public ActionResult DeleteCategory(int id)
        {
            var datedelte = _dbContext.DeleteCategory(id);
            if (datedelte != null)
            {
                TempData["deletemassage"] = "Delete category Succsessfully";
                return RedirectToAction("GetCategory");
            }
            else
            {
                return RedirectToAction("GetCategory");
            }
        }

//This Method for get the data by id

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var list = _dbContext.GetById(id);

            return View(list);
        }


        //update post Method
        [HttpPost]
        public ActionResult EditCategory(CategoryMaster categoryMaster)
        {
            var data = _dbContext.Edit(categoryMaster);

            if (data != null)
            {
                TempData["massageupdate"] = "Category Update Successfully";
                return RedirectToAction("GetCategory");

            }
            else
            {

                return View();
            }

        }


        // Get Details by Id
        [HttpGet]
        public ActionResult Details(int id)
        {
          var deta=  _dbContext.GetById(id);    

            return View(deta);
        }



    }
}