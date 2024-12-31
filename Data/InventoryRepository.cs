using Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class InventoryRepository :ConnectDBMysql
    {
        public InventoryRepository(string connectionString) : base(connectionString)
        {
        }
        public bool  AddInventory(Inventory inventory)
        {
            try
            {
                string insertQuery = "INSERT INTO Inventories (Inv_Date, Final_Date, UserName, Status) " +
                     "VALUES (@inv_date, @final_date, " +
                     "(SELECT Name_User FROM Users WHERE Name_User = @user AND Id_role = '1'), @statu)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@inv_date", inventory.Inv_Date);
                    command.Parameters.AddWithValue("@Final_date", inventory.FinalDate);
                    command.Parameters.AddWithValue("@user", inventory.UserName);
                    command.Parameters.AddWithValue("@statu", "1");
                    ConnectDB.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ConnectDB.Close();
                    return true;
                }
            }
            catch (Exception exe)
            {
                Console.WriteLine(exe.Message);
                return false;
            }
        }
        // Method to add repository
        public Inventory Mapper_Inventory( MySqlDataReader dataReader)
        {
            Inventory inventory = new Inventory();
            inventory.Id_Inventory = dataReader.GetInt32(0);
            inventory.Inv_Date = dataReader.GetDateTime(1);
            inventory.FinalDate = dataReader.GetDateTime(2);
            inventory.UserName = dataReader.GetString(3);
            inventory.statu = dataReader.GetString(4);  
            return inventory;
        }

        public List<Inventory> GetAllInventory()
        {
            try
            {
                List<Inventory> list = new List<Inventory>();
                var command = ConnectDB.CreateCommand();
                command.CommandText = "select * from Inventories WHERE Status= '1'";
                ConnectDB.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Mapper_Inventory(reader));
                }
                ConnectDB.Close();
                return list;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    } 
}

