using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace ProyectoFinal.Model
{
    public class Producto : INotifyPropertyChanged
    {
        private string nombre_Prod;
        private int precio_Prod;
        private int descuento_Prod;
        private int contacto_Prod;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nombre_Prod
        {
            get => nombre_Prod;
            set
            {
                nombre_Prod = value;
                OnPropertyChanged();
            }
        }

        public int Precio_Prod
        {
            get => precio_Prod;
            set
            {
                precio_Prod = value;
                OnPropertyChanged();
            }
        }

        public int Descuento_Prod
        {
            get => descuento_Prod;
            set
            {
                descuento_Prod = value;
                OnPropertyChanged();
            }
        }

        public int Contacto_Prod
        {
            get => contacto_Prod;
            set
            {
                contacto_Prod = value;
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
