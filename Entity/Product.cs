using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product
    {
        public Product()
        {
        }
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Brand { get; set; }
        public string statu { get; set; }

        public Product(string code, string barcode, string name, string reference, string brand, string statu)
        {
            Code = code;
            Barcode = barcode;
            Name = name;
            Reference = reference;
            Brand = brand;
            this.statu = statu;
        }
    }
}
