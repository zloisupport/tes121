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
        AppDbContext db;


        IEnumerable<Region> regions;

        public IEnumerable<Region> Regions
        {
            get { return regions; }
            set
            {
                regions = value;
                OnPropertyChanged("Regions");
            }
        }

        public ApplicationViewModel()
        {
            db = new AppDbContext();
            db.Regions.Load();
            Regions = db.Regions.Local.ToBindingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
