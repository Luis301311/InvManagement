using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data; 

namespace Logic
{
    public class UserManagement : IUserManagement<User>
    {
        Data.UserRepository userRepository;
        private List<User> Users;
        public UserManagement(string connection)
        {
            userRepository = new Data.UserRepository(connection);
            Refresh();
        }
        void Refresh()
        {
            Users = userRepository.GetAllUser();
        }
        public bool Add(User var)
        {
            return userRepository.AddUser(var);
        }

        public bool Exist(User var)
        {
            foreach (var item in GetAll())
            {
                if (item.Identification == var.Identification) return true;
            }
            return false;
        }


        public List<User> GetAll()
        {
            return userRepository.GetAllUser();
        }

        public User GetByID(string id)
        {
            User user = new User();
            foreach (var item in userRepository.GetAllUser())
            {
                if (id == item.Identification)
                {
                    return user = item;

                }
            }
            return null;
        }

        public List<User> GetByName(string name)
        {
            List<User> user = new List<User>();
            foreach (var item in userRepository.GetAllUser())
            {
                if (item.First_Name.Contains(name))
                {
                    user.Add(item);
                }
            }
            return user;
        }

        public bool Update(User var)
        {
            return userRepository.UpdateUser(var);
        }
        public List<User> SearchXUsers(string worth)
        {
            List<User> filter = new List<User>();
            foreach (var item in Users)
            {
                if (item.Identification.Contains(worth)|| item.First_Name.ToUpper().Contains(worth) || item.Last_Name.ToUpper().Contains(worth) || item.Name_User.ToUpper().Contains(worth))
                {
                    filter.Add(item);
                }
            }
            return filter;
        }
    }
}
