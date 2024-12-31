using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RegisterHistory
    {

        public int Id_Register { get; set; }
        public int Id_Inventory { get; set; }
        public string Id_Product { get; set; }
        public string UserName { get; set; }
        public DateTime Date_Register { get; set; }
        public string Description { get; set; }

        public RegisterHistory(int id_Register, int id_Inventory, string id_Product, string userName, DateTime date_Register, string description)
        {
            Id_Register = id_Register;
            Id_Inventory = id_Inventory;
            Id_Product = id_Product;
            UserName = userName;
            Date_Register = date_Register;
            Description = description;
        }

        public RegisterHistory()
        {
        }
    }
}
