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
    class TreasureCardViewModel : BaseViewModel
    {
        private ObservableCollection<TreasureCard> treasureCards;

        public ObservableCollection<TreasureCard> TreasureCards
        {
            get { return treasureCards; }
            set { treasureCards = value; }
        }

        private int max;

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        private int xFound;

        public int XFound
        {
            get { return xFound; }
            set { xFound = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private static TreasureCardDataService _treasureCardDataService;

        public TreasureCardViewModel(TreasureCardDataService treasureCardDataService)
        {
            _treasureCardDataService = treasureCardDataService;
           
            Messenger.Default.Register<int>(this, OnPlayerIdReceived);
        }

        private void OnPlayerIdReceived(int playerId)
        {
            TreasureCards = _treasureCardDataService.GetByPlayer(playerId).ToObservableCollection();
            Max = TreasureCards.Count;
            List<TreasureCard> found = TreasureCards.Where(i => i.IsFound == true).ToList();
            XFound = found.Count;
            Text = "Al " + XFound + " van de " + Max + " items gevonden.";
        }
    }
}
