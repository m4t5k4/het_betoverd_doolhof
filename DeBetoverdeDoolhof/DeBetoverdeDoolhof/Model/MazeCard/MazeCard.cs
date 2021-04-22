using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class MazeCard : BaseModel
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


        public MazeCard()
        {

        }

        public MazeCard(string position, string name, string image, int rotation)
        {
            Name = name;
            Position = position;
            Image = image;
            Rotation = rotation;
        }
    }
}
