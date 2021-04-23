using DeBetoverdeDoolhof.Model;
using DeBetoverdeDoolhof.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class ConfirmPlayersCommand : ICommand
    {
        private readonly PlayersViewModel _viewModel;
        private readonly PlayerStore _playerStore;

        public ConfirmPlayersCommand(PlayersViewModel viewModel, PlayerStore playerStore)
        {
            _viewModel = viewModel;
            _playerStore = playerStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            List<Player> players = _viewModel.Players.ToList();
            _playerStore.CreatePlayers(players);
        }
    }
}
