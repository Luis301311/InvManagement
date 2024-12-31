using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product_InventoryJoin
    {
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Name_product { get; set; }
        public string Refence { get; set; }
        public string Brand { get; set; } 
        public int Id_Inventory { get; set; }
        public double Price { get; set; }
        public double Margin { get; set; }
        public double PriceToCost { get; set; }
        public double Amount { get; set; }
        public string Iva { get; set; }

        public Product_InventoryJoin()
        {
        }

        public Product_InventoryJoin(string code, string barcode, string name_product, string refence, string brand, int id_Inventory, double price, double margin, double priceToCost, double amount, string iva)
        {
            Code = code;
            Barcode = barcode;
            Name_product = name_product;
            Refence = refence;
            Brand = brand;
            Id_Inventory = id_Inventory;
            Price = price;
            Margin = margin;
            PriceToCost = priceToCost;
            Amount = amount;
            Iva = iva;
        }
    }
}
