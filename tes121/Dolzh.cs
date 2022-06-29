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
    public class Dolzh : INotifyPropertyChanged
    {

        [Key]
        public int dolzh_id { get; set; }
        private string names { get; set; }
        public char TRIAL960 { get; set; }

        public string Names
        {
            get { return names; }
            set
            {
                names = value;
                OnPropertyChanged("Names");
            }
        }
        public virtual ICollection<Sotrudnik> Sotrudnik { get; set; }


        public override string ToString()
        {
            return Names;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
