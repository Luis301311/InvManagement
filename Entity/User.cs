using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        public User() { }
        public string Identification { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Name_User { get; set; }
        public int Id_Role { get; set; }
        public string Email { get; set; }
        public string User_Password { get; set; }
        public int Statu { get; set; }

        public User(string identification, string first_Name, string last_Name, string name_User, int id_Role, string email, string user_Password, int statu)
        {
            Identification = identification;
            First_Name = first_Name;
            Last_Name = last_Name;
            Name_User = name_User;
            Id_Role = id_Role;
            Email = email;
            User_Password = user_Password;
            Statu = statu;
        }
    }
}
