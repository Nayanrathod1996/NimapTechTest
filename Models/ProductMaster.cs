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
            public int ProductId { get; set; }
            [Required]
            [Display(Name="Product Name")]
            public string ProductName { get; set; }
            [Display(Name = "Category Id")]
            [Required]
            public int CategoryId { get; set; }
           [Display(Name = "Category Name")]
           [Required]
           public string CategoryName { get; set; }

       
    }
}