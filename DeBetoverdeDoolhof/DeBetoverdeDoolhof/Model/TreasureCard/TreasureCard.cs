using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class TreasureCard : BaseModel
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

        private int playerID;
        public int PlayerID
        {
            get
            {
                return playerID;
            }
            set
            {
                playerID = value;
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

        private bool isFound;
        public bool IsFound
        {
            get
            {
                return isFound;
            }
            set
            {
                isFound = value;
                NotifyPropertyChanged();
            }
        }

        private string image;
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                NotifyPropertyChanged();
            }
        }

        public TreasureCard()
        {

        }

        public TreasureCard(int playerID, string name, string image, bool isFound)
        {
            PlayerID = playerID;
            Name = name;
            Image = image;
            IsFound = IsFound;
        }
    }
}
