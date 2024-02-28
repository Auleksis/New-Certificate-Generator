using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ShowTitle: INotifyPropertyChanged
    {
        private string title;
        public string Title { 
            get { return title; } 
            set {
                if (title != value)
                {
                    title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }  
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
