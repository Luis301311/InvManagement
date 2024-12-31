using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IUserManagement<T>
    {
        bool Add(T var);
        bool Exist(T var);
        bool Update(T var);
        List<T> GetAll();
        List<T> GetByName(string name);
        T GetByID(string id);
    }
}
