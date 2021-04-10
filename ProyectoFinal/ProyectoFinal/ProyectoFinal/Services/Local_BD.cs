using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProyectoFinal.Models;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ProyectoFinal.Services
{
    class Local_BD
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Métodos para conectar la base de datos

        // Constructor de pantalla
        private Local_BD(String dbPath)
        {
            con = new SqliteConnection(dbPath);
            //con.DropTable<Usuario>();
            //con.DropTable<FichaTecnica>();
            //con.DropTable<Cita>();
            //con.CreateTable<Usuario>();
            //con.CreateTable<FichaTecnica>();
            //con.CreateTable<Cita>();
        }
        // Conexión de la base de datos local
        private SQLiteConnection con;
        // Instancia de la base de datos local 
        private static Local_BD instancia;
        /// <summary>
        /// Clase para el manejo de errores de la instancia de la base de datos
        /// </summary>
        public static Local_BD Instancia
        {
            get
            {
                if (instancia == null)
                    throw new Exception("Debe llamar al inicializador, acción detenida");
                return instancia;
            }
        }

        /// <summary>
        /// Inicializador de la base de datos
        /// </summary>
        /// <param name="filename"></param>
        public static void Inicializador(String filename)
        {
            if (filename == null)
            {
                throw new ArgumentException();
            }
            if (instancia != null)
            {
                instancia.con.Close();
            }
            instancia = new Local_BD(filename);
        }

        public string EstadoMensaje;

        // Fin de los metodos para conectar la base de datos
        ///////////////////////////////////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Inicio de los metodos del login

        public Usuario InicioSesion(string usu, string pass)
        {
            var usuario = from USUARIO in con.Table<Usuario>() select USUARIO;

            var consulta = from USUARIO in con.Table<Usuario>()
                           where USUARIO.USR_CORREO == usu
                           where USUARIO.USR_CONTRASENA == pass
                           select USUARIO;
            return consulta.FirstOrDefault();
        }


        public static Usuario sesionActiva;

        public Boolean EsAdmin(string id)
        {
            var consulta = from USUARIO in con.Table<Usuario>()
                           where USUARIO.USR_PERSONA_ID.Equals(id)
                           select USUARIO;
            sesionActiva = consulta.FirstOrDefault();
            return consulta.FirstOrDefault().USR_ADMIN;
        }

        // Fin de los metodos del login
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Inicio de los metodos de usuarios

        /// <summary>
        ///  Método para agregar el usuario a la base de datos
        /// </summary>
        /// <param name="ID">ID del usuario, ya sea la cédula o el número de pasaporte</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="ciudad">Ciudad de residencia</param>
        /// <param name="direccion">Dirección de su domicilio</param>
        /// <param name="correo">Correo del usuario</param>
        /// <param name="contrasena">Contraseña de la cuenta</param>
        public void AgregarUsuario(string ID, string nombre, string ciudad, string direccion, string correo, string contrasena, Boolean EsAdmin)
        {
            con.Insert(new Usuario
            {
                USR_PERSONA_ID = ID,
                USR_NOMBRE = nombre,
                USR_CIUDAD = ciudad,
                USR_DIRECCION = direccion,
                USR_CORREO = correo,
                USR_CONTRASENA = contrasena,
                USR_ADMIN = EsAdmin
            });
        }


        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Un inumerable con todos los usuarios</returns>
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            try
            {
                return con.Table<Usuario>();
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<Usuario>();
        }

        /// <summary>
        /// Se utiliza para buscar un usuario específico
        /// </summary>
        /// <param name="usuarioID">Cédula o número de pasaporte ingresado en el registro de la BD</param>
        /// <returns>EL usuario buscado</returns>
        public Usuario BuscarUsuario(string usuarioID)
        {
            var consulta = from USUARIO in con.Table<Usuario>()
                           where USUARIO.USR_PERSONA_ID.Equals(usuarioID)
                           select USUARIO;
            return consulta.FirstOrDefault();
        }

        /// <summary>
        /// Se utiliza para buscar un usuario específico
        /// </summary>
        /// <param name="usuarioID">Cédula o número de pasaporte ingresado en el registro de la BD</param>
        /// <returns>Verdadero o falso</returns>
        public bool ExisteUsuario(string usuarioID)
        {
            var consulta = from USUARIO in con.Table<Usuario>()
                           where USUARIO.USR_PERSONA_ID.Equals(usuarioID)
                           select USUARIO;
            return (consulta.Count() != 0);
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Una lista con todos los usuarios</returns>
        public List<Usuario> GetUsuario()
        {
            var consulta = from USUARIO in con.Table<Usuario>()
                           select USUARIO;
            return consulta.ToList();
        }

        /// <summary>
        /// Elimina un usuario en específico de la BD
        /// </summary>
        /// <param name="id">ID del usuario</param>
        public void EliminaPerfil(int id)
        {
            // Se eliminan las citas que tiene este vehículo
            var citasDeLaFicha = (from CITA in con.Table<Cita>()
                                  where CITA.CIT_USR_PERSONA_ID.Equals(id)
                                  select CITA).ToList();
            foreach (var cita in citasDeLaFicha)
            {
                con.Delete<Cita>(cita.CIT_ID);
            }

            // Se eliminan los vehículos que tiene este usuario
            var vehiculos = (from FichaTecnica in con.Table<FichaTecnica>()
                             where FichaTecnica.FTC_USR_PERSONA_ID.Equals(id)
                             select FichaTecnica).ToList();
            foreach (var vehiculo in vehiculos)
            {
                con.Delete<FichaTecnica>(vehiculo.FTC_ID);
            }
            // Se elimina el usuario
            con.Delete<Usuario>(id);
        }
        // Fin de los metodos de usuarios
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Inicio de los metodos de ficha técnica

        /// <summary>
        /// Agrega un registro a la tabla FICHA_TECNICA
        /// </summary>
        /// <param name="marca">Marca del vehículo</param>
        /// <param name="color">Color del vehículo</param>
        /// <param name="year">Año de fabricación del vehículo</param>
        /// <param name="estilo">Estilo del vehículo (4x4, etc)</param>
        /// <param name="combustible">Tipo de combustible que utiliza (Gasolina, Diesel, etc)</param>
        /// <param name="kilometraje">Kilometraje actual que tiene el vehículo</param>
        /// <param name="dimensiones">Dimensiones del vehículo</param>
        /// <param name="peso">Peso del vehículo</param>
        /// <returns>El número de registros que se realizaron en el método inserte dentro de este método</returns>
        public int AgregarFichaTecnica(string personaID, string marca, string color, int year, string estilo, string combustible, int kilometraje, string dimensiones, int peso)
        {
            int result = 0;
            try
            {
                result = con.Insert(new FichaTecnica
                {
                    FTC_USR_PERSONA_ID = personaID,
                    FTC_MARCA = marca,
                    FTC_COLOR = color,
                    FTC_YEAR = year,
                    FTC_ESTILO = estilo,
                    FTC_COMBUSTIBLE = combustible,
                    FTC_KILOMETRAJE = kilometraje,
                    FTC_DIMENSIONES = dimensiones,
                    FTC_PESO = peso
                });
            }
            catch (Exception e)
            { EstadoMensaje = e.Message; }
            return result;
        }

        /// <summary>
        /// Para obtener todas las fichas técnicas
        /// </summary>
        /// <returns>Una cadena de caracteres con todos los registros de la ficha técnica</returns>
        public string GetFichaTecnica()
        {
            var consulta = from FICHA_TECNICA in con.Table<FichaTecnica>()
                           select FICHA_TECNICA.FTC_ID;
            return consulta.ToString();
        }

        /// <summary>
        /// Método para obtener los vehículos después de que se realiza alguna acción
        /// </summary>
        /// <returns>Lista de vehículos</returns>
        public async Task<IEnumerable<FichaTecnica>> GetVehiculoAsync()
        {
            return await Task.FromResult(GetAllFichaTecnica());
        }

        /// <summary>
        /// Busca una ficha técnica en específico
        /// </summary>
        /// <param name="id">ID de la ficha técnica a buscar</param>
        /// <returns>La ficha técnica buscada o un valor nulo si no la encuentra</returns>
        public FichaTecnica BuscarFichaTecnica(int id)
        {
            var consulta = from FICHA_TECNICA in con.Table<FichaTecnica>()
                           where FICHA_TECNICA.FTC_ID == id
                           select FICHA_TECNICA;
            return consulta.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene todas las fichas técnicas
        /// </summary>
        /// <returns>Retorna un inumerable de todas las fichas técnicas</returns>
        public IEnumerable<FichaTecnica> GetAllFichaTecnica()
        {
            try
            {
                if (sesionActiva.USR_ADMIN)
                {
                    return con.Table<FichaTecnica>();
                }
                else
                {
                    return con.Table<FichaTecnica>().Where(x => x.FTC_USR_PERSONA_ID == sesionActiva.USR_PERSONA_ID);
                }
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<FichaTecnica>();
        }

        /// <summary>
        /// Elimina una ficha tecnica en específico de la BD
        /// </summary>
        /// <param name="id">ID de la ficha</param>
        public void EliminaFichaTecnica(int id)
        {
            // Se eliminan las citas que tiene este vehículo
            var citasDeLaFicha = (from CITA in con.Table<Cita>()
                                  where CITA.CIT_FTC_ID == id
                                  select CITA).ToList();
            foreach (var cita in citasDeLaFicha)
            {
                con.Delete<Cita>(cita.CIT_ID);
            }
            // Se elimina el vehículo
            con.Delete<FichaTecnica>(id);
        }

        // Fin de los metodos de ficha técnica
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Inicio de los metodos de citas

        /// <summary>
        ///  Método para agregar una cita a la base de datos
        /// </summary>
        /// <param name="ID">ID del usuario</param>
        /// <param name="vehiculoID">ID del vehículo (ficha técnica)</param>
        /// <param name="fecha">Fecha</param>

        public void AgregarCita(string ID, int vehiculoID, DateTime fecha)
        {
            con.Insert(new Cita
            {
                CIT_USR_PERSONA_ID = ID,
                CIT_FTC_ID = vehiculoID,
                CIT_FECHA = fecha
            });
        }

        /// <summary>
        /// Método para obtener las Citas después de que se realiza alguna acción
        /// </summary>
        /// <returns>Lista de Citas</returns>
        public async Task<IEnumerable<Cita>> GetCitasAsync()
        {
            return await Task.FromResult(GetAllCitas());
        }


        /// <summary>
        /// Obtiene todas las citas
        /// </summary>
        /// <returns>Un inumerable con todas las citas</returns>
        public IEnumerable<Cita> GetAllCitas()
        {
            try
            {
                if (sesionActiva.USR_ADMIN)
                {
                    return con.Table<Cita>();
                }
                else
                {
                    return con.Table<Cita>().Where(x => x.CIT_USR_PERSONA_ID == sesionActiva.USR_PERSONA_ID);
                }
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<Cita>();
        }

        /// <summary>
        /// Elimina una cita en específico de la BD
        /// </summary>
        /// <param name="usuarioID">Cédula o número de pasaporte ingresado en el registro de la BD</param>
        /// <param name="idVehiculo">ID del vehículo</param>
        /// <param name="fecha">Fecha de la cita</param>
        public void EliminaCita(string usuarioID, int idVehiculo, DateTime fecha)
        {
            var consulta = (from CITA in con.Table<Cita>()
                            where CITA.CIT_USR_PERSONA_ID.Equals(usuarioID)
                            && CITA.CIT_FTC_ID == idVehiculo
                            && CITA.CIT_FECHA.Equals(fecha)
                            select CITA).FirstOrDefault();
            con.Delete<Cita>(consulta.CIT_ID);
        }

        // Fin de los metodos de citas
        ///////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
