using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class ParameterCommand : ICommand
    {
        public MainViewModel ViewModel { get; set; }
        public ParameterCommand(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.ParameterMethod(parameter);
        }
    }
}
