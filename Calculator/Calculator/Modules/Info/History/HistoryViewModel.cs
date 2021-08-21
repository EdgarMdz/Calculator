using Calculator.Common.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.Modules.Calculator
{
    public class HistoryViewModel : BaseViewModel
    {

        public ObservableCollection<string> Items { get; set; }

        public HistoryViewModel()
        {
            Items = new ObservableCollection<string>();
        }

        public override Task InitializeAsync(object parameter)
        {
            Items = new ObservableCollection<string>(parameter as List<string>);
            OnPropertyChanged(nameof(Items)); 
            return base.InitializeAsync(parameter);
        }

        public ICommand DeleteCommand { get => new Command<string>(DeleteItem); }

        private void DeleteItem(string item)
        {
            Items.Remove(item);
            MessagingCenter.Send(this, "Items", new List<string>(Items));
        }
    }
}
