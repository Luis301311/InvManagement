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
    public class ProductInventoryRepository : ConnectDBMysql
    {
        public ProductInventoryRepository(string connectionString) : base(connectionString)
        {
        }


        //Method to add ProductInventory
        public bool AddProductInventoryRepository(Product_Inventory productInventory)
        {
            try
            {
                string insertQuery = "INSERT INTO Product_Inventory (Code, Id_Inventory, Price, Margin, PriceToCost, Amount, Iva, NameUser)" +
                "VALUES (@Code, @Id_inventory, @Price, @Margin, @PriceToCost, @Amount, @Iva, @NameUser) ";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Code", productInventory.Code);
                    command.Parameters.AddWithValue("@Id_inventory", productInventory.Id_Inventory);
                    command.Parameters.AddWithValue("@Price", productInventory.Price);
                    command.Parameters.AddWithValue("@Margin", productInventory.Margin);
                    command.Parameters.AddWithValue("@PriceToCost", productInventory.PriceToCost);
                    command.Parameters.AddWithValue("@Amount", productInventory.Amount);
                    command.Parameters.AddWithValue("@Iva", productInventory.Iva);
                    command.Parameters.AddWithValue("@NameUser", productInventory.NameUser);

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
        public Product_Inventory Mapper_Product_Inventory(MySqlDataReader dataReader)
        {
            try
            {
                Product_Inventory product_Inventory = new Product_Inventory();
                product_Inventory.Code = dataReader.GetString(0);
                product_Inventory.Id_Inventory = dataReader.GetInt32(1);
                product_Inventory.Price = dataReader.GetDouble(2);
                product_Inventory.Margin = dataReader.GetDouble(3);
                product_Inventory.PriceToCost = dataReader.GetDouble(4);
                product_Inventory.Amount = dataReader.GetDouble(5);
                product_Inventory.Iva = dataReader.GetString(6);
                return product_Inventory;
            }
            catch(Exception ex) { 
                Console.WriteLine(ex.Message);  
                return null;
            }
        }

        //Method to search all products
        public List<Product_Inventory> GetAllproduct_Inventory()
        {
            List<Product_Inventory> product_Inventory = new List<Product_Inventory>();
            try
            {
                var command = ConnectDB.CreateCommand();
                command.CommandText = "select * from Product_Inventory";
                open();
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    product_Inventory.Add(Mapper_Product_Inventory(dataReader));
                }
                close();
                return product_Inventory;
            }
            catch(Exception e) { 
                Console.WriteLine(e.Message);
                return product_Inventory;
            }
        }

        public List<Product_InventoryJoin> GetAllproduct_InventoryJoin(string Id_inventory)
        {
            try
            {
                List<Product_InventoryJoin> product_Inventory = new List<Product_InventoryJoin>();
                string selectQuery = "SELECT P.Code, P.Barcode, P.Name_Product, P.Reference, P.Brand, PI.Id_Inventory, PI.Price, PI.Margin, PI.PriceToCost, PI.Amount, PI.Iva FROM Products P JOIN" +
                    " Product_Inventory PI on P.Code= PI.Code JOIN Inventories I on I.Id_Inventory = PI.Id_Inventory" +
                    " WHERE  PI.Id_Inventory = @Id_inventory";

                using (MySqlCommand command = new MySqlCommand(selectQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Id_inventory", Id_inventory);
                    ConnectDB.Open();

                    using (MySqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            product_Inventory.Add(Mapper_Product_InventoryJoin(dataReader));
                        }
                    }

                    ConnectDB.Close();

                    // Devolver null si la lista está vacía
                    if (product_Inventory.Count == 0)
                    {
                        return null;
                    }

                    return product_Inventory;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public Product_InventoryJoin Mapper_Product_InventoryJoin(MySqlDataReader dataReader)
        {
            try
            {
                Product_InventoryJoin product_Inventory = new Product_InventoryJoin();

                product_Inventory.Code = dataReader.GetString(0);
                product_Inventory.Barcode = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                product_Inventory.Name_product = dataReader.GetString(2);
                // Verificar si la referencia es nula antes de intentar acceder a ella
                product_Inventory.Refence = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                // Verificar si la marca es nula antes de intentar acceder a ella
                product_Inventory.Brand = dataReader.IsDBNull(4) ? string.Empty : dataReader.GetString(4);
                product_Inventory.Id_Inventory = dataReader.GetInt32(5);
                product_Inventory.Price = dataReader.GetDouble(6);
                product_Inventory.Margin = dataReader.GetDouble(7);
                product_Inventory.PriceToCost = dataReader.GetDouble(8);
                product_Inventory.Amount = dataReader.GetDouble(9);
                product_Inventory.Iva = dataReader.GetString(10);
                return product_Inventory;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public double GetAmount(string code) {
            try
            {
                foreach (var item in GetAllproduct_Inventory())
                {
                    if (item.Code == code) return item.Amount;
                }
                return 0;
            }catch { return 0; }
        }

        //Method to modify productInventory
        public bool UpdateproductInventory(Product_Inventory productInventory)
        {
            try
            {
                string insertQuery = "UPDATE Product_Inventory " +
                "SET Price = @Price, Margin = @Margin, PriceToCost = @PriceToCost, Amount = @Amount, Iva = @Iva WHERE Code = @Code AND Id_Inventory = @Id_inventory";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Code", productInventory.Code);
                    command.Parameters.AddWithValue("@Id_inventory", productInventory.Id_Inventory);
                    command.Parameters.AddWithValue("@Price", productInventory.Price);
                    command.Parameters.AddWithValue("@Margin", productInventory.Margin);
                    command.Parameters.AddWithValue("@PriceToCost", productInventory.PriceToCost);
                    command.Parameters.AddWithValue("@Amount", productInventory.Amount + GetAmount(productInventory.Code));
                    command.Parameters.AddWithValue("@Iva", productInventory.Iva);
                    ConnectDB.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ConnectDB.Close();
                    if (rowsAffected > 0) return true;
                    return false;
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine(ex);
                return false;
            }

        }
        public bool remove(Product_Inventory productInventory)
        {
            try
            {
                string insertQuery = "DELETE FROM  Product_Inventory" +
                " WHERE  Code = @Code";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Code", productInventory.Code);
                    ConnectDB.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ConnectDB.Close();
                    if (rowsAffected > 0) return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool AddRegisterHistory(RegisterHistory registerHistory)
        {
            try
            {
                string insertQuery = "INSERT INTO Registers (Id_Inventory, Product_Code, NameUser, DateRegister, DescriptionRegister)" +
                "VALUES (@Id_Inventory, @Product_Code, @NameUser, @DateRegister, @DescriptionRegister) ";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Id_Inventory", registerHistory.Id_Inventory);
                    command.Parameters.AddWithValue("@Product_Code", registerHistory.Id_Product);
                    command.Parameters.AddWithValue("@NameUser", registerHistory.UserName);
                    command.Parameters.AddWithValue("@DateRegister", registerHistory.Date_Register);
                    command.Parameters.AddWithValue("@DescriptionRegister", registerHistory.Description);
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
    }
}



       
    

