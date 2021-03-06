using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator.Modules.Info.AppInformation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppInformationView : ContentPage
    {
        public AppInformationView(AppInformationViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}