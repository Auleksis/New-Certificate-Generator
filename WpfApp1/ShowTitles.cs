using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ShowTitles: ObservableCollection<ShowTitle>
    {
        public void Add(string title)
        {
            this.Add(new ShowTitle { Title = title });
        }

        public static implicit operator ObservableCollection<object>(ShowTitles v)
        {
            throw new NotImplementedException();
        }
    }
}
