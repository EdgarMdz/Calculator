using Calculator.Common.Navigation;
using Calculator.Modules.Calculator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Modules.Info
{
    public class InfoViewModel : BaseViewModel
    {
        public HistoryViewModel HistoryViewModel { get; set; }
     
        public InfoViewModel(HistoryViewModel historyViewModel)
        {
            HistoryViewModel = historyViewModel;
        }

        public override Task InitializeAsync(object parameter)
        {
            return HistoryViewModel.InitializeAsync(parameter);
        }
    }
}
