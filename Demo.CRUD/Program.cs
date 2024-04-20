using Dapper;
using Demo.CRUD.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //GetAll();
            //Console.WriteLine("Enter product ID: ");
            //int id = Convert.ToInt32(Console.ReadLine());
            //GetById(id);

            Product product = new Product()
            {
                ProductID = 2,
                Name = "Galaxy",
                ProductNumber = "123456",
                Color = "Red",
                StandardCost = 15000,
                ListPrice = 10000,
                Size = "L",
                Weight = 120
            };

            //Add(product);
            //Delete(1);
            Update(product);
        }

        public static void GetAll()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                IEnumerable<Product> products = sqlConnection.Query<Product>("select * from saleslt.product");

                foreach (Product product in products)
                {
                    Console.WriteLine(product.Name);
                }

                Console.ReadLine();
            }
        }

        public static void GetById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                Product product = sqlConnection
                    .QueryFirstOrDefault<Product>("select * from saleslt.product where ProductID = @id", new { id = id });

                Console.WriteLine(product.Name);
                Console.WriteLine(product.ProductNumber);
                Console.ReadLine();
            }
        }

        public static void Add(Product product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string query = "INSERT INTO dbo.Product (Name, ProductNumber, Color, StandardCost, ListPrice, Size, Weight) VALUES (@Name, @ProductNumber, @Color, @StandardCost, @ListPrice, @Size, @Weight)";

                sqlConnection.Execute(query, product);

                Console.WriteLine("Product added");
                Console.ReadLine();
            }
        }

        public static void Delete(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string query = "DELETE FROM dbo.Product WHERE ProductID = @id";

                sqlConnection.Execute(query, new { id = id });
                Console.WriteLine("Product deleted");
                Console.ReadLine();
            }
        }

        public static void Update(Product product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string query = "UPDATE dbo.Product set Name=@Name, ProductNumber=@ProductNumber, Color=@Color, StandardCost=@StandardCost, ListPrice=@ListPrice, Size=@Size, Weight=@Weight WHERE ProductID=@ProductID";

                sqlConnection.Execute(query, product);

                Console.WriteLine("Product updated!");
                Console.ReadLine();
            }
        }
    }
}
