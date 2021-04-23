using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class Square : BaseModel
    {
        private int id;
        private int row;
        private int column;
        private List<Player> playersOnSquare;

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

        private int rotation;

        public int Rotation
        {
            get { return rotation; }
            set { rotation = value; NotifyPropertyChanged(); }
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

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        public int Row
        {
            get { return row; }
            set
            {
                row = value;
                NotifyPropertyChanged();
            }
        }

        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                NotifyPropertyChanged();
            }
        }
        public List<Player> PlayersOnSquare
        {
            get { return playersOnSquare; }
            set
            {
                playersOnSquare = value;
                NotifyPropertyChanged();
            }
        }

        public Square(int id, int row, int column, string name, string image, int rotation)
        {
            Id = id;
            Row = row;
            Column = column;
            Name = name;
            Image = image;
            Rotation = rotation;
            playersOnSquare = new List<Player>();
        }
    }
}
