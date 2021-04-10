using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProyectoFinal.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        //Llave primaria, ID de usuario
        [PrimaryKey, AutoIncrement]
        public int Usr_ID { get; set; }

        public string Usr_Nombre { get; set; }

        public string Usr_Apellido1 { get; set; }

        public string Usr_Apellido2 { get; set; }

        public int Usr_Telefono { get; set; }

        public string Usr_Correo { get; set; }

        public string Usr_Password { get; set; }



    }
}
