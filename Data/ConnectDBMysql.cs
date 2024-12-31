using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Data
{
    public class ConnectDBMysql
    {
        internal MySqlConnection ConnectDB;
        public ConnectDBMysql(string connectionString)
        {
            ConnectDB = new MySqlConnection(connectionString);
        }
        public void open()
        {
            try
            {
                ConnectDB.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al abrir BD " + ex.Message);
            }
        }

        public void close()
        {
            ConnectDB.Close();
        }
    }
}
