using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Chonete.ViewModel
{
    class UsuarioViewModel : INotifyPropertyChanged
    {
        private Menu selectedMenu;

        public Menu SelectedMenu
        {
            get { return selectedMenu; }
            set { selectedMenu = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
