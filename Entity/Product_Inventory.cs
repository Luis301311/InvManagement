using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product_Inventory
    {
        public Product_Inventory()
        {
        }

        public string Code { get; set; }
        public int Id_Inventory { get; set; }
        public double Price { get; set; }
        public double Margin { get; set; }
        public double PriceToCost { get; set; }
        public double Amount { get; set; }
        public string Iva { get; set; }
        public string NameUser { get; set; }

        public Product_Inventory(string code, int id_Inventory, double price, double margin, double priceToCost, double amount, string iva, string nameUser)
        {
            Code = code;
            Id_Inventory = id_Inventory;
            Price = price;
            Margin = margin;
            PriceToCost = priceToCost;
            Amount = amount;
            Iva = iva;
            NameUser = nameUser;
        }

        public double CalculatePriceToCost()
        {
            PriceToCost = Price * Margin;
            return PriceToCost;
        }
    }
}
