using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace tes121
{
    public class Category : INotifyPropertyChanged
    {
        public int categoryID { get; set; }
        private string names { get; set; }
        public char TRIAL957 { get; set; }


        public string Names
        {
            get { return names; }
            set
            {
                names = value;
                OnPropertyChanged("Names");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
