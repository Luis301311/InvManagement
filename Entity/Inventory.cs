using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Inventory
    {
        public Inventory() { }
        public int Id_Inventory { get; set; }
        public DateTime Inv_Date { get; set; }
        public DateTime FinalDate { get; set; }
        public string UserName { get; set; }
        public string statu { get; set; }

        public Inventory(int id_Inventory, DateTime inv_Date, DateTime finalDate, string userName, string statu)
        {
            Id_Inventory = id_Inventory;
            Inv_Date = inv_Date;
            FinalDate = finalDate;
            UserName = userName;
            this.statu = statu;
        }
    }
}
