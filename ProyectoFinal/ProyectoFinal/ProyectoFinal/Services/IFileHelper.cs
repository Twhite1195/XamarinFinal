using ProyectoFinal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoFinal.Services
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string fileName);
    }
}
