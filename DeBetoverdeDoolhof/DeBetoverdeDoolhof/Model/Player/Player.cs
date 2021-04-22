using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class Player: BaseModel
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private int score;
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                NotifyPropertyChanged();
            }
        }

        private string position;
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                NotifyPropertyChanged();
            }
        }

        private bool isWinner;
        public bool IsWinner
        {
            get
            {
                return isWinner;
            }
            set
            {
                isWinner = value;
                NotifyPropertyChanged();
            }
        }

        private int wizardID;
        public int WizardID
        {
            get
            {
                return wizardID;
            }
            set
            {
                wizardID = value;
                NotifyPropertyChanged();
            }
        }

        public Player()
        {

        }

        public Player(string name, int score, string position, bool isWinner, int wizardID)
        {
            Name = name;
            Score = score;
            Position = position;
            IsWinner = isWinner;
            WizardID = wizardID;
        }
    }
}
