using DeBetoverdeDoolhof.Model;
using DeBetoverdeDoolhof.Stores;
using DeBetoverdeDoolhof.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof
{
    class ViewModelLocator
    {
        private static PlayerStore playerStore = new PlayerStore();
        private static WizardDataService wizardDataService = new WizardDataService();
        private static MazeCardDataService mazeCardDataService = new MazeCardDataService();
        private static PlayerDataService playerDataService = new PlayerDataService();
        private static PlayerPositionDataService playerPositionDataService = new PlayerPositionDataService();
        private static MainViewModel mainViewModel = new MainViewModel(playerStore, wizardDataService, mazeCardDataService, playerDataService, playerPositionDataService);

        public static MainViewModel MainViewModel
        {
            get { return mainViewModel; }
        }

        private static PlayersViewModel playersViewModel = new PlayersViewModel(playerStore);

        public static PlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
        }
    }
}
