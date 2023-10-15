using CrudwithDrapper.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace CrudwithDrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string connectionstring;
        public ProductController(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection")!;
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "insert into products" +
                        "(name,Brand,Category,Price,Description,Createdat)" +
                        "Output Inserted.* " +
                        "values(@name,@Brand,@Category,@Price,@Description,@Createdat)";

                    var product = new Product()
                    {
                        name = productDto.name,
                        Brand = productDto.Brand,
                        Category = productDto.Category,
                        Price = productDto.Price,
                        Description = productDto.Description,
                        Createdat = DateTime.Now,

                    };

                    var newProduct = connection.QuerySingleOrDefault<Product>(sql, product);

                    if (newProduct != null)
                    {
                        return Ok(newProduct);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("We have an a exception :\n" + ex.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "Select * from products";
                    var data = connection.Query<Product>(sql);
                    products = data.ToList();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an a exception :\n" + ex.Message);
                return BadRequest();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "Select * from products where id=@Id";
                    var product = connection.QuerySingle<Product>(sql, new { Id = id });
                    if (product != null)
                    {
                        return Ok(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an a exception :\n" + ex.Message);
                return BadRequest();
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "Update products set name=@name,Brand=@Brand,Category=@Category,Price=@Price,Description=@Description where id=@Id";


                    var product = new Product()
                    {
                        Id = id,
                        name = productDto.name,
                        Brand = productDto.Brand,
                        Category = productDto.Category,
                        Price = productDto.Price,
                        Description = productDto.Description,



                    };
                    int count = connection.Execute(sql, product);
                    if (count < 1)
                    {
                        return NotFound();
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an a exception :\n" + ex.Message);
                return BadRequest();
            }


            return GetProduct(id);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "Delete from products where Id=@Id";
                    int count=connection.Execute(sql, new { Id = id });
                    if(count < 1)
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an a exception :\n" + ex.Message);
                return BadRequest();
            }
            return Ok();
        }
    }
}
