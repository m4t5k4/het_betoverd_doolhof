using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    [Table("Wizard")]
    public class Wizard: BaseModel
    {
        
        private int id;

        [Key]
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

        private string color;
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                NotifyPropertyChanged();
            }
        }

        private string startPosition;
        public string StartPosition
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
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

        public Wizard()
        {
                
        }

        public Wizard(int ID, string color, string startPosition, string image)
        {
            Color = color;
            StartPosition = startPosition;
            Image = image;
        }
    }
}
