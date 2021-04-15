using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProyectoFinal.Droid.Services;
using ProyectoFinal.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace ProyectoFinal.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}