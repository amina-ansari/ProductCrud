﻿
using System.ComponentModel.DataAnnotations;

namespace Product2.Controllers.Product2.Controllers
{
    // Product.cs
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }

}