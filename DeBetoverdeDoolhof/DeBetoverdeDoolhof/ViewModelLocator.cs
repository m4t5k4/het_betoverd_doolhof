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
        private static TreasureCardDataService treasureCardDataService = new TreasureCardDataService();

        private static MainViewModel mainViewModel = new MainViewModel(playerStore, wizardDataService, mazeCardDataService, playerDataService, playerPositionDataService, treasureCardDataService);

        public static MainViewModel MainViewModel
        {
            get { return mainViewModel; }
        }

        private static PlayersViewModel playersViewModel = new PlayersViewModel(playerStore);

        public static PlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
        }

        private static TreasureCardViewModel treasureCardViewModel = new TreasureCardViewModel(treasureCardDataService);

        public static TreasureCardViewModel TreasureCardViewModel
        {
            get { return treasureCardViewModel; }
        }

        private static ScoresViewModel scoresViewModel = new ScoresViewModel();

        public static ScoresViewModel ScoresViewModel
        {
            get { return scoresViewModel;  }
        }
    }
}
