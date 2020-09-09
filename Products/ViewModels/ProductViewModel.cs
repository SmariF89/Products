using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please specify a name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please add a description!")]
        public string Description { get; set; }
        [Range(0, 9999999999999, ErrorMessage = "Please enter a nonnegative value!")]
        public int Stock { get; set; }
        [Range(0, 9999999999999, ErrorMessage = "Please enter a nonnegative value!")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please pick a category!")]
        public int categoryID { get; set; }

        public List<SelectListItem> availableCategories { get; set; }
    }
}