using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SimpleBoardingBoard
{
    public class viewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void applyChange(String property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
