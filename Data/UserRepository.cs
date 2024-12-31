using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Data
{
    public class UserRepository: ConnectDBMysql
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public bool AddUser(User user)
        {
            try
            {
                string insertQuery = " INSERT INTO Users (Identification, First_name, Surname, Name_User, Id_role, Email, User_password, Id_Status) " +
                 "VALUE (@Identification, @First_name, @Surname, @Name_user, @Role, @Email, @Password, @Statu)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Identification", user.Identification);
                    command.Parameters.AddWithValue("@First_name", user.First_Name);
                    command.Parameters.AddWithValue("@Surname", user.Last_Name);
                    command.Parameters.AddWithValue("@Name_user", user.Name_User);
                    command.Parameters.AddWithValue("@Role", user.Id_Role);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.User_Password);
                    command.Parameters.AddWithValue("@Statu", user.Statu);
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

        public User Mapper_User(MySqlDataReader dataReader)
        {
            try
            {
                User user = new User();
                user.Identification = dataReader.GetString(0);
                user.First_Name = dataReader.GetString(1);
                user.Last_Name = dataReader.GetString(2);
                user.Name_User = dataReader.GetString(3);
                user.Id_Role = dataReader.GetInt32(4);
                user.Email = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
                user.User_Password = dataReader.GetString(6);
                user.Statu = dataReader.GetInt32(7);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public List<User> GetAllUser()
        {
            List<User> list = new List<User>();
            try
            {
                var command = ConnectDB.CreateCommand();
                command.CommandText = "select  *from Users";
                ConnectDB.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Mapper_User(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
            }
            ConnectDB.Close();
            return list;
        }
        
        public bool UpdateUser(User user)
        {
            try
            {
                string insertQuery = "UPDATE Users" +
                 " SET  Identification = @Identification, First_name = @First_name, Surname = @Surname, Name_User = @Name_User, Id_role= @Role, Email= @Email, User_password=@Password, Id_Status = @Statu  WHERE Identification = @Identification";
                using (MySqlCommand command = new MySqlCommand(insertQuery, ConnectDB))
                {
                    command.Parameters.AddWithValue("@Identification", user.Identification);
                    command.Parameters.AddWithValue("@First_name", user.First_Name);
                    command.Parameters.AddWithValue("@Surname", user.Last_Name);
                    command.Parameters.AddWithValue("@Name_user", user.Name_User);
                    command.Parameters.AddWithValue("@Role", user.Id_Role);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.User_Password);
                    command.Parameters.AddWithValue("@Statu", user.Statu);
                    ConnectDB.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ConnectDB.Close();
                    if (rowsAffected > 0) return true; 
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;   

            }

        }
    }
}
