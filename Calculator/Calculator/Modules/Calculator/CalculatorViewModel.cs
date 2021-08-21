using Calculator.Common.Navigation;
using Calculator.Modules.Calculator;
using Calculator.Modules.Info;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator
{
    public enum CalculatorState
    {
        PopulatingFirstNumber,
        PopulatingSecondNumber
    }

    public enum Operation
    {
        None,
        Add,
        Subtract,
        Divide,
        Multiply,
        Equal
    }

    public class CalculatorViewModel : BindableObject
    {
        private string _displayText;
        private string _firstNumber = string.Empty;
        private string _secondNumber = string.Empty;
        private List<string> _calculatorHistory = new List<string>();
        private CalculatorState _state;
        private Operation _currentOperation;
        private INavigationService _navigation;

        public CalculatorViewModel(INavigationService navigation)
        {
            _state = CalculatorState.PopulatingFirstNumber;
            _currentOperation = Operation.None;
            _navigation = navigation;

            SubscribeToMessage();
        }

        private void SubscribeToMessage()
        {
            MessagingCenter.Subscribe<HistoryViewModel, List<string>>(this, "Items", (vm, historyList) => { _calculatorHistory = historyList; }); 
        }

        public string DisplayText
        {
            get => _displayText;
            set
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClearCommand => new Command(ClearText);

        public ICommand AddCharCommand => new Command<string>(AddChar);

        public ICommand OperationCommand => new Command<Operation>(PerformOperation);

        public ICommand ShowHistoryCommand => new Command(async () => { await GoToHistory(); });
        private async Task GoToHistory()
        {
            await _navigation.PushAsync<InfoViewModel>(_calculatorHistory);
        }

        private void AddChar(string character)
        {
            if (_state == CalculatorState.PopulatingFirstNumber)
            {
                if (_firstNumber.Contains(",") && character == ",")
                {
                    return;
                }
                _firstNumber += character;
                DisplayText = _firstNumber;
                return;
            }

            if (_secondNumber.Contains(",") && character == ",")
            {
                return;
            }
            _secondNumber += character;
            DisplayText = _secondNumber;
        }

        private void ClearText()
        {
            if (_state == CalculatorState.PopulatingFirstNumber)
            {
                _firstNumber = string.Empty;
            }
            else
            {
                _secondNumber = string.Empty;
            }
            DisplayText = string.Empty;
        }

        private void PerformOperation(Operation operation)
        {
            if (operation == Operation.Equal &&
                !string.IsNullOrWhiteSpace(_firstNumber) &&
                !string.IsNullOrWhiteSpace(_secondNumber))
            {
                Calculate();
                return;
            }
            _currentOperation = operation;
            DisplayText = string.Empty;
            _state = CalculatorState.PopulatingSecondNumber;
        }

        private void Calculate()
        {
            var first = decimal.Parse(_firstNumber);
            var second = decimal.Parse(_secondNumber);
            decimal result = 0;
            switch (_currentOperation)
            {
                case Operation.Add:
                    result = first + second;
                    break;
                case Operation.Subtract:
                    result = first - second;
                    break;
                case Operation.Divide:
                    result = first / second;
                    break;
                case Operation.Multiply:
                    result = first * second;
                    break;
                default:
                    break;
            }
            DisplayText = result.ToString();
            _calculatorHistory.Add($"{ _firstNumber} {GetOperationString()} {_secondNumber} = {result.ToString("n2")}");
            _currentOperation = Operation.None;
            _state = CalculatorState.PopulatingFirstNumber;
            _firstNumber = string.Empty;
            _secondNumber = string.Empty;
        }

        private string GetOperationString()
        {
            if (Operation.Add == _currentOperation)
                return "+";
            if (Operation.Subtract == _currentOperation)
                return "-";
            if (Operation.Multiply == _currentOperation)
                return "*";
            if (Operation.Divide == _currentOperation)
                return "/";
            else
                return string.Empty;
        }
    }
}
