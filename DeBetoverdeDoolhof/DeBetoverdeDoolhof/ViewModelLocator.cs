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
        private static MainViewModel mainViewModel = new MainViewModel();

        public static MainViewModel MainViewModel
        {
            get { return mainViewModel; }
        }

        private static PlayersViewModel playersViewModel = new PlayersViewModel();

        public static PlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
        }
    }
}
