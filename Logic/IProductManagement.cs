using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IProductManagement<T>
    {
        bool Add(T var);
        bool Exist(T var);
        bool Update(T var);
        List<T> GetAll();
        List<T> GetByName(string name);
        T GetByCode(string id);
        List<T> GetByReference(string reference);
        List<T> GetByBarcode(string barcode);
        List<T> GetByBrand(string brand);
    }
}
