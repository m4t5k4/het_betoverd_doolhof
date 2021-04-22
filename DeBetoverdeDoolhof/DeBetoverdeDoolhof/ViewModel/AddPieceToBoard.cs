using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class AddPieceToBoard : ICommand
    {
        public MainViewModel ViewModel { get; set; }
        public AddPieceToBoard(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            this.ViewModel.AddPieceToBoard(parameter as Button);
        }
    }
}
