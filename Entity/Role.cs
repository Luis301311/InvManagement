using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Role
    {
        public Role() { }
        public int Id_Rol { get; set; }
        public string Description_Rol { get; set; }

        public Role(int id_Rol, string description_Rol)
        {
            Id_Rol = id_Rol;
            Description_Rol = description_Rol;
        }
    }
}
