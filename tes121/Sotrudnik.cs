using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace tes121
{
    public class Sotrudnik : INotifyPropertyChanged
    {
        [Key]
        public int sotrudnik_id { get; set; }
        private string login { get; set; }
        public string password { get; set; }
      //  private int dolzh_id { get; set; }
        private string fam { get; set; }
        private string names { get; set; }
        private string subname { get; set; }
        public string phones { get; set; }
        public DateTime birthday { get; set; }
        private string fact_addr { get; set; }
        public char TRIAL963 { get; set; }


        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }      
        
        public string Names
        {
            get { return names; }
            set
            {
                names = value;
                OnPropertyChanged("Names");
            }
        }       
        
        public string Fam
        {
            get { return fam; }
            set
            {
                fam = value;
                OnPropertyChanged("Fam");
            }
        }        
        
        public string Subname
        {
            get { return subname; }
            set
            {
                subname = value;
                OnPropertyChanged("Subname");
            }
        }
                
        public string Phones
        {
            get { return phones; }
            set
            {
                phones = value;
                OnPropertyChanged("Phones");
            }
        }      
        public string Fact_addr
        {
            get { return fact_addr; }
            set
            {
                fact_addr = value;
                OnPropertyChanged("Fact_addr");
            }
        }

        private int dolzh_id { get; set; }
        public int Dolzh_id
        {
            get { return dolzh_id; }
            set
            {
                dolzh_id = value;
                OnPropertyChanged("Dolzh_id");
            }
        }

        public virtual Dolzh Dolzh { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
