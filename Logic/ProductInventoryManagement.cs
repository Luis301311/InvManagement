using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductInventoryManagement : IProductManagement<Product_Inventory>
    {
        Data.ProductInventoryRepository productInventoryRepository;
        public ProductInventoryManagement(string connection)
        {
            productInventoryRepository = new Data.ProductInventoryRepository(connection);
        }

        public bool AddRegisterHistory(RegisterHistory registerHistory)
        {
            return productInventoryRepository.AddRegisterHistory(registerHistory);
        }
        public bool Add(Product_Inventory var)
        {
            return productInventoryRepository.AddProductInventoryRepository(var);
        }
        public bool remove(Product_Inventory var)
        {
            return productInventoryRepository.remove(var);
        }
        public bool Exist(Product_Inventory var)
        {
            foreach (var item in GetAll())
            {
                if ((var.Code == item.Code) && (var.Id_Inventory == item.Id_Inventory))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Product_Inventory> GetAll()
        {
            List<Product_Inventory> list = new List<Product_Inventory>();
            return list = productInventoryRepository.GetAllproduct_Inventory();
        }
        public List<Product_InventoryJoin> GetAllProductInventorys(string id_invetory)
        {
            List<Product_InventoryJoin> list = new List<Product_InventoryJoin>();
            return list = productInventoryRepository.GetAllproduct_InventoryJoin(id_invetory);
        }
        public List<Product_Inventory> GetByCost(string price)
        {
            try
            {
                List<Product_Inventory> list = new List<Product_Inventory>();
                foreach (var item in GetAll())
                {
                    if (item.PriceToCost == Convert.ToDouble(price)  || item .Price == Convert.ToDouble(price) || item.Id_Inventory == int.Parse(price) || item.Code == price)
                    {
                        list.Add(item);
                    }
                }

                return list;
            }
            catch  (Exception e)
            {
                return null; 
            }
        }
        public Product_Inventory GetByBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        public List<Product_Inventory> GetByBrand(string brand)
        {
            throw new NotImplementedException();
        }

        public Product_Inventory GetByCode(string id)
        {
            foreach (var item in GetAll())
            {
                if (item.Code == id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<Product_Inventory> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product_Inventory> GetByReference(string reference)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product_Inventory var)
        {
            return productInventoryRepository.UpdateproductInventory(var);
        }

        List<Product_Inventory> IProductManagement<Product_Inventory>.GetByBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        Product_Inventory IProductManagement<Product_Inventory>.GetByCode(string id)
        {
            throw new NotImplementedException();
        }
    }
}
