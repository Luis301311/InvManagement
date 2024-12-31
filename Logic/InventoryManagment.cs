using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class InventoryManagment : IProductManagement<Inventory>
    {
        Data.InventoryRepository inventoryRepository;
        public InventoryManagment(string connection)
        {
            inventoryRepository = new Data.InventoryRepository(connection);
        }

        public bool Add(Inventory var)
        {
            return inventoryRepository.AddInventory(var);
        }

        public bool Exist(Inventory var)
        {
            foreach (var item in GetAll())
            {
                if (var.Id_Inventory == item.Id_Inventory)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Inventory> GetAll()
        {
            return inventoryRepository.GetAllInventory();
        }
        public List<int> GetAllInventoryIds()
        {
            try
            {
                List<int> ids = new List<int>();
                foreach (var item in GetAll())
                {
                    ids.Add(item.Id_Inventory);
                }
                return ids;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public Inventory GetByBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        public List<Inventory> GetByBrand(string brand)
        {
            throw new NotImplementedException();
        }

        public Inventory GetByCode(string id)
        {
            throw new NotImplementedException();
        }

        public List<Inventory> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Inventory> GetByReference(string reference)
        {
            throw new NotImplementedException();
        }

        public bool Update(Inventory var)
        {
            throw new NotImplementedException();
        }

        List<Inventory> IProductManagement<Inventory>.GetByBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        Inventory IProductManagement<Inventory>.GetByCode(string id)
        {
            throw new NotImplementedException();
        }


    }
}
