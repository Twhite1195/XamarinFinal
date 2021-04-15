﻿using ProyectoFinal.iOS.Services;
using ProyectoFinal.Services;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;


[assembly: Dependency(typeof(FileHelper))]
namespace ProyectoFinal.iOS.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder =
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string libFolder =
                Path.Combine(docFolder, "..", "Database");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, fileName);
        }
    }
}