using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Chonete.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Products = GetProducts();
            MenuList = GetMenus();
        }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; }
        }
        private Menu selectedMenu;

        public Menu SelectedMenu
        {
            get { return selectedMenu; }
            set { selectedMenu = value; }
        }


        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }

        public void ShowDetails()
        {
            var page = new DetailsPage() { BindingContext = new DetailsViewModel { SelectedProduct = SelectedProduct } };
            App.Current.MainPage.Navigation.PushAsync(page);
        }
      

        private ObservableCollection<Menu> GetMenus()
        {
            return new ObservableCollection<Menu>
            {
                  new Menu { Icon = "favorite.png", Name = "Favoritos"},
                new Menu { Icon = "shopping.png", Name = "Carrito"},
                new Menu { Icon = "user.png", Name = "Usuario"},
                new Menu { Icon = "settings_.png", Name = "configuración"},
            };
        }

        private ObservableCollection<Product> GetProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product { Name = "Aretes", Price = 1789.00f, Image = "Aretes.jpg", Model = "Oro", Rating = 4.5, Views = 4.5, Description = "Oro de 14k, 100% artesanales"},
                new Product { Name = "Ropa de bebe", Price = 16197.9f, Image = "Ropabebe.jpg", Model = "3-6 meses", Rating = 4.5, Views = 4.5, Description = "Ropa de bebe de calidad, hasta 6 meses de edad "},
                new Product { Name = "Tazas", Price = 897.00f, Image = "Tzas.jpg", Model = "450ml", Rating = 4.5, Views = 4.5, Description = "Tazas personalizadas "},
                new Product { Name = "Omega RD Watch", Price = 567.99f, Image = "rutgeWatch.png", Model = "Model 1997", Rating = 4.5, Views = 4.5, Description = "Reloj omega modelo 1997 original"},
                 new Product { Name = "Camisa", Price = 697.00f, Image = "Camisa.jpg", Model = "Personalizada", Rating = 4.5, Views = 4.5, Description = "Ropa  "},
                new Product { Name = "Bolso", Price = 18900.00f, Image = "Bolso.jpg", Model = "Mimbre", Rating = 4.5, Views = 4.5, Description = "Tazas personalizadas "},
                new Product { Name = "Cheeesecake", Price = 5600.00f, Image = "Cheeesecake.jpg", Model = "chocolate", Rating = 4.5, Views = 4.5, Description = "Cheeesecake chocolate bajo en grasas"},
                 new Product { Name = "Juguete", Price = 6970.00f, Image = "Infantil.jpg", Model = "Madera", Rating = 4.5, Views = 4.5, Description = "Juguete de madera, tren "},
                new Product { Name = "Traje de baño", Price = 18907.00f, Image = "Vba_o.jpg", Model = "Talla M", Rating = 4.5, Views = 4.5, Description = "Traje de baño 2 piezas  "},
                new Product { Name = "Pañoleta ", Price = 1567.00f, Image = "pa_uel.jpg", Model = "Model Artesanal", Rating = 4.5, Views = 4.5, Description = "Pañoleta 100% Tica"},
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    public class Product
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public double Views { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }

    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        
    }
}
