using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProyectoFinal.Models
{
    [Table("Item")]
    class Item
    {
        //Llave primaria, ID del item
        [PrimaryKey, AutoIncrement]
        public int Itm_ID { get; set; }

        public int Itm_User { get; set; }

        public string Itm_Nombre { get; set; }

        public int Itm_Precio { get; set; }

        public int Itm_Contacto_tel { get; set; }

        public int Itm_descuento { get; set; }
    }
}
