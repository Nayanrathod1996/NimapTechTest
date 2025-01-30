using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NimaApp.Models
{
    public class ProductMaster
    {
            [Display(Name = "Product Id")]
            [Required]
            public int? ProductId { get; set; }
            [Required(ErrorMessage ="Product Name Is Required")]
            [Display(Name="Product Name")]
            public string ProductName { get; set; }
            [Display(Name = "Category Id")]
            
            public int? CategoryId  { get; set; }

           [Display(Name = "Category Name")]
           [Required(ErrorMessage="Category Name Is Required")]
           public string CategoryName { get; set; }

       
    }
}