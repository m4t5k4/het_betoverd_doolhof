using DeBetoverdeDoolhof.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeBetoverdeDoolhof.Extensions
{
    class DialogService
    {
        Window playersModalWindow = null;
        Window rulesModalWindow = null;
        Window treasuresModalWindow = null;
        Window scoresWindow = null;


        public DialogService()
        {

        }

        public void ShowPlayers()
        {
            playersModalWindow = new PlayersModalWindow();
            playersModalWindow.ShowDialog();
        }

        public void ClosePlayers()
        {
            if (playersModalWindow != null) playersModalWindow.Close();
        }

        public void ShowRules()
        {
            rulesModalWindow = new RulesModalWindow();
            rulesModalWindow.ShowDialog();
        }

        public void CloseRules()
        {
            if (rulesModalWindow != null) rulesModalWindow.Close();
        }

        public void ShowTreasures()
        {
            treasuresModalWindow = new TreasuresModalWindow();
            treasuresModalWindow.ShowDialog();
        }

        public void CloseTreasures()
        {
            if (treasuresModalWindow != null) treasuresModalWindow.Close();
        }

        public void ShowScores()
        {
            scoresWindow = new ScoresWindow();
            scoresWindow.ShowDialog();
        }

        public void CloseScores()
        {
            if (scoresWindow != null) scoresWindow.Close();
        }

    }
}
