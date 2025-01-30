using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NimaApp.Models
{
    public class CategoryMaster
    {
            [Display(Name ="Category Id")]
            public int CategoryId { get; set; }

             [Display(Name ="Add Category")]
             [Required(ErrorMessage ="Category Name Is Required")]
        
            public string CategoryName { get; set; }
    }
}