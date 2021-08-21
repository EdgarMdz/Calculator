using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Calculator.Modules.Info.AppInformation
{
   public class AppInformationViewModel
    {
        public string AppName => $"App Name: {AppInfo.Name}";
        public string AppVersion => $"App Version: {AppInfo.VersionString}";
        public string Author => "Author: Edgar Méndez";
    }
}
