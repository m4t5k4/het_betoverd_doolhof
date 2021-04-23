using DeBetoverdeDoolhof.Extensions;
using DeBetoverdeDoolhof.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class ScoresViewModel : BaseViewModel
    {
        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set { players = value; }
        }


        public ScoresViewModel()
        {
            Messenger.Default.Register<ObservableCollection<Player>>(this, OnPlayersReceived);
        }

        private void OnPlayersReceived(ObservableCollection<Player> players)
        {
            Players = players;
        }

    }
}
