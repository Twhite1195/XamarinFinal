using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProyectoFinal.Model
{
    public class Usuario : INotifyPropertyChanged
    {
        private string nombre;
        private string apellido1;
        private string apellido2;
        private int telefono;
        private string correo;
        private string password;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        public string Apellido1
        {
            get => apellido1;
            set
            {
                apellido1 = value;
                OnPropertyChanged();
            }
        }

        public string Apellido2
        {
            get => apellido2;
            set
            {
                apellido2 = value;
                OnPropertyChanged();
            }
        }

        public int Telefono
        {
            get => telefono;
            set
            {
                telefono = value;
                OnPropertyChanged();
            }
        }

        public string Correo
        {
            get => correo;
            set
            {
                correo = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
