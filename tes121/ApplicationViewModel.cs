using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace tes121
{

    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Region selectedRegion;

        public ObservableCollection<Region> Regions { get; set; }
        public Region SelectedRegion
        {
            get { return selectedRegion; }
            set
            {
                selectedRegion = value;
                OnPropertyChanged("SelectedRegion");
            }
        }
 

        public ApplicationViewModel()
        {
            Regions = new ObservableCollection<Region>
            {
               for (int i = 0; i < table.Rows.Count; ++i)
                Regions.Add(new Person
                {
                    rn_id = table.Rows[i][0].ToString(),
                    names = Convert.ToInt32(table.Rows[i][1]),
                    TRIAL963 = table.Rows[i][2].ToString(),
                });
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
