using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entity.ProductsJson.ProductResult;
using Newtonsoft.Json;
using System.IO;
using static Entity.ProductsJson;

namespace Logic
{
    public class ProductManagement : IProductManagement<Product>
    {
        ProductsJson ProductsJson = new ProductsJson();
        private List<Product> Products;

        Data.ProductRepository productRepository;
        public ProductManagement(string connection)
        {
            productRepository = new Data.ProductRepository(connection);
            Refresh();
        }

        void Refresh()
        {
            Products = productRepository.GetAllProduct();
        }

        public bool Add(Product var)
        {
            return productRepository.AddProduct(var);
        }
        public bool AddAllProductosJson(List<Product> JsonProducts)
        {
            try
            {
                foreach (Product Product in JsonProducts)
                {
                    Add(Product);
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool Exist(Product var)
        {
            foreach (var item in GetAll())
            {
                if (item.Code == var.Code) return true;
            }
            return false;
        }
        

        public List<Product> GetAll()
        {
            return productRepository.GetAllProduct();
        }

        public List<ProductResult> DeserializeJson(string jsonString)
        {
            try
            {
                var productsJsons = JsonConvert.DeserializeObject<ProductResponse>(jsonString)?.Results;
                return productsJsons ?? new List<ProductResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al deserializar el JSON: {ex.Message}", "Error");
                return new List<ProductResult>();
            }
        }

        public void LoadProductsFromJsonAndSaveToDatabase(string jsonFilePath)
        {
            try
            {
                string jsonString = File.ReadAllText(jsonFilePath);
                var productsJsons = DeserializeJson(jsonString);
                if (productsJsons != null)
                {
                    var products = ConvertToProducts(productsJsons);
                    foreach (var product in products)
                    {
                        Add(product);
                    }
                    Console.WriteLine("Datos cargados y guardados correctamente en la base de datos.");
                }
                else
                {
                    Console.WriteLine("Error al deserializar el JSON.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar y guardar datos: {ex.Message}", "Error");
            }
        }

        private List<Product> ConvertToProducts(List<ProductResult> productsJsons)
        {
            List<Product> products = new List<Product>();
            foreach (var item in productsJsons)
            {
                Product product = new Product
                {
                    Code = item.Code,
                    Name = item.Name,
                    Reference = item.Reference,
                    Barcode = !string.IsNullOrEmpty(item.AdditionalFields?.Barcode) ? item.AdditionalFields.Barcode : item.Code,
                    Brand = !string.IsNullOrEmpty(item.Name) ? GetLastWord(item.Name) : string.Empty
                };
                products.Add(product);
            }
            return products;
        }

        public string GetLastWord(string input)
        {
            string[] words = input.Split(' ');
            return words.Length > 0 ? words[words.Length - 1] : string.Empty;
        }

        public List<Product> GetByBarcode(string barcode)
        {
            List<Product> products = new List<Product>();
            foreach (var item in productRepository.GetAllProduct())
            {
                if (item.Barcode.Contains(barcode))
                {
                    return products;
                }
            }
            return null;
        }

        public Product GetByCode(string code)
        {
            Product product = new Product();
            foreach (var item in productRepository.GetAllProduct())
            {
                if (code == item.Code)
                {
                    return product = item;
                }
            }
            return null;
        }

        public List<Product> GetByName(string name)
        {
            List<Product> products = new List<Product>();
            foreach (var item in productRepository.GetAllProduct())
            {
                if (item.Name.Contains(name.ToUpper()))
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public List<Product> GetByReference(string reference)
        {
            List<Product> products = new List<Product>();
            foreach (var item in productRepository.GetAllProduct())
            {
                if (item.Reference.Contains(reference))
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public bool Update(Product var)
        {
            return productRepository.UpdateProduct(var);
        }

        public List<Product> GetByBrand(string brand)
        {
            List<Product> products = new List<Product>();
            foreach (var item in productRepository.GetAllProduct())
            {
                if (item.Brand.Contains(brand))
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public List<Product> SearchXProducts(string worth)
        {
            try
            {
                List<Product> filter = new List<Product>();
                foreach (var item in Products)
                {
                    if (item.Name.ToUpper().Contains(worth) || item.Code.ToUpper().Contains(worth) || item.Brand.ToUpper().Contains(worth) || (item.Barcode != null && item.Barcode.ToUpper().Contains(worth))
                    || (item.Reference != null && item.Reference.ToUpper().Contains(worth)))
                    {
                        filter.Add(item);
                    }
                }
                return filter;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);  
                return null; }
  
        }
    }
}
