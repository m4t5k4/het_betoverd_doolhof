using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Deze methode wordt opgeroepen in de setter van elke property.  
        // [CallerMemberName Attribute]  is nieuw in .NET Framework 4.5.   
        // Dit attribuut zorgt automatisch voor bepalen van de calling propertyName! 
        // Laat toe om bij de properties NotifyPropertyChanged() op te roepen ipv
        // OnPropertyChanged("naam property")

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
