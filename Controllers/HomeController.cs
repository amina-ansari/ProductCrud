using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Product2.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Dapper;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Data.Common;

    namespace Product2.Controllers
    {
        public class HomeController : Controller
        {
            private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            public ActionResult Index()
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var products = connection.Query<Product>("SELECT * FROM Products");
                    return View(products);
                }
            }

            [HttpGet]
            public ActionResult AddProduct()
            {
                return View();
            }

            [HttpPost]
            public ActionResult AddProduct(Product product)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", product);
                }
                return RedirectToAction("Index");
            }
            public ActionResult EditProduct(int id)
            {
                // Retrieve the product with the specified id from the database using Dapper
                Product product;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    product = connection.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
                }

                // Check if the product exists
                if (product == null)
                {
                    // Handle the case when the product does not exist (e.g., show an error message, redirect, etc.)
                    // Return an appropriate response
                }

                // Pass the product to the view for editing
                return View(product);
            }

            [HttpPost]
            public ActionResult EditProduct(Product product)
            {
                if (ModelState.IsValid)
                {
                    // Update the product in the database using Dapper
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        connection.Execute("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", product);
                    }

                    // Redirect to the homepage or any other appropriate page
                    return RedirectToAction("Index");
                }

                // If the model state is invalid, return the view with validation errors
                return View(product);
            }


            [HttpGet]
            public ActionResult DeleteProduct(int id)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });
                }
                return RedirectToAction("Index");
            }



        }
    }
}

