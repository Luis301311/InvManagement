using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "server=bj2zucdmbfdgydatuphw-mysql.services.clever-cloud.com;   port=21024;   user id=umflxylc6mvw0jlc;   password=fFF6Kd4xVzVYwzx3zbM   ;database=bj2zucdmbfdgydatuphw";
            UserRepository Createstudents = new UserRepository (connectionString);
            //List<Inventory> inventories = new List<Inventory> ();
            //User inventory = new User();
            //inventory.Identification = "121435";
            //inventory.First_Name = "Deimis";
            //inventory.Last_Name = "Henao";
            //inventory.User_Password = "password";
            //inventory.Name_User = "Deimis1234";
            //inventory.Email = null;
            //inventory.Id_Role = 1;
            //inventory.Statu = "1";
            //Console.WriteLine(Createstudents.UpdateUser(inventory));
            //Console.WriteLine(Createstudents.GetAllUser()[0].Name_User);
            //Console.WriteLine(inventories[1].UserName);

            //ProductRepository productRepository = new ProductRepository (connectionString);
            //Product product = new Product();
            //product.Code = "123";
            //product.Name = "Maquina de agua";
            //product.Barcode = "123";
            //product.Reference = "Sr martillo";
            //product.Brand = "Martillo sas";
            //product.statu = "1";
            //productRepository.UpdateProduct(product);

            //Console.WriteLine(productRepository.GetAllProduct()[0].Name); 
            //Console.ReadKey();

            String codigo = "1"; 
            ProductInventoryRepository productInventoryReposiory = new ProductInventoryRepository (connectionString);
            List<Product_InventoryJoin> product_InventoryJoins = new List<Product_InventoryJoin> ();
            product_InventoryJoins = (productInventoryReposiory.GetAllproduct_InventoryJoin(codigo));
            Console.WriteLine(product_InventoryJoins[0].Name_product);
            
            //Product_Inventory product_Inventory = new Product_Inventory ();

            //product_Inventory.Id_Inventory = 1;
            //product_Inventory.Code = "2";
            //product_Inventory.Price = 12000;
            //product_Inventory.Margin = 10.123;
            //product_Inventory.Iva = 0.19; 
            //product_Inventory.PriceToCost = 10000;  
            //product_Inventory.Amount = 100;

            //Console.WriteLine(productInventoryRepository.remove(product_Inventory));

            Console.ReadKey();

        }
    }
}
