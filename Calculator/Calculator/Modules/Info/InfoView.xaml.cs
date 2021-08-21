using Calculator.Modules.Calculator;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator.Modules.Info.AppInformation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoView : TabbedPage
    {
        public InfoView(InfoViewModel model, HistoryView historyView, AppInformationView infoView)
        {
            InitializeComponent();
            BindingContext = model;
            
            historyView.BindingContext = model.HistoryViewModel;
            historyView.IconImageSource = "https://cdn3.iconfinder.com/data/icons/google-material-design-icons/48/ic_history_48px-512.png";
            infoView.IconImageSource = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e4/Infobox_info_icon.svg/1200px-Infobox_info_icon.svg.png";
            Children.Add(historyView);
            Children.Add(infoView);
        }
    }
}