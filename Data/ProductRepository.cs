using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Data
{
    public class ProductRepository : ConnectDBMysql
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }


        //Method to add product
        public bool AddProduct(Product product)
        {
            try
            {
                string insertQuery = "INSERT INTO Products (Code, Barcode, Name_product, Reference, Brand, Statu)" +
                  "VALUES (@Code, @Barcode, @Name_product, @Reference, @Brand, @Statu) ";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Code", product.Code);
                    command.Parameters.AddWithValue("@Barcode", product.Barcode);
                    command.Parameters.AddWithValue("@Name_product", product.Name);
                    command.Parameters.AddWithValue("@Reference", product.Reference);
                    command.Parameters.AddWithValue("@Brand", product.Brand);
                    command.Parameters.AddWithValue("@Statu", product.statu);
                    ConnectDB.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ConnectDB.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                return false;   
            }

        }

        //Method to map a product
        public Product Mapper_Product(MySqlDataReader dataReader)
        {
            try
            {
                Product product = new Product();
                product.Code = dataReader.GetString(0);
                product.Barcode = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
                product.Name = dataReader.GetString(2);
                product.Reference = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
                product.Brand = dataReader.GetString(4);
                product.statu = dataReader.GetString(5);
                return product;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                return null;    
            }

        }


        //Method to search all products
        public List<Product> GetAllProduct()
        {
            try
            {
                List<Product> products = new List<Product>();
                var command = ConnectDB.CreateCommand();
                command.CommandText = "select  * from Products where Statu = '1'";
                open();
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    products.Add(Mapper_Product(dataReader));
                }
                close();
                return products;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                return null;    
            }

        }

        public List<Product> GetAllProductDB(string text)
        {
            try
            {
                List<Product> products = new List<Product>();
                var command = ConnectDB.CreateCommand();
                command.CommandText = "SELECT * FROM Products WHERE Name_product LIKE @searchTerm OR Code LIKE @searchTerm OR Barcode LIKE @searchTerm OR Reference LIKE @searchTerm AND Statu = '1'"; ;
                command.Parameters.AddWithValue("@searchTerm", $"%{text}%");
                open();
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    products.Add(Mapper_Product(dataReader));
                }
                close();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        //Method to modify a product
        public bool UpdateProduct(Product product)
        {
            string insertQuery = "Update Products" +
                    " set Code = @Code, Barcode = @Barcode, Name_product = @Name_product, Reference = @Reference, Brand = @Brand, Statu = @Statu WHERE Code = @Code";
            using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
            {
                command.Parameters.AddWithValue("@Code", product.Code);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@Name_product", product.Name);
                command.Parameters.AddWithValue("@Reference", product.Reference);
                command.Parameters.AddWithValue("@Brand", product.Brand);
                command.Parameters.AddWithValue("@Statu", product.statu);
                ConnectDB.Open();
                int rowsAffected = command.ExecuteNonQuery();
                ConnectDB.Close();
                if(rowsAffected > 0) return true;
                return false;
            }
        }
    }
}
