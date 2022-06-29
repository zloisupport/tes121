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
using System.Windows;

namespace tes121
{

    public class ApplicationViewModel : INotifyPropertyChanged
    {
        AppDbContext db;
        RelayCommand addCommand;
        RelayCommand deleteCommand;
        RelayCommand editSotrudnikCommand;
        RelayCommand editCommand;

        private Region selectedRegion;
        private Sotrudnik selectedSotrudnik;

        IEnumerable<Region> regions;
        IEnumerable<Category> categories;
        IEnumerable<Orders> orders;
        IEnumerable<Sotrudnik> sotrudniks;
        IEnumerable<Dolzh> dolzhs;

        public IEnumerable<Region> Regions
        {
            get { return regions; }
            set
            {
                regions = value;
                OnPropertyChanged("Regions");
            }
        }      
        
        public IEnumerable<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }      
        
        public IEnumerable<Orders> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }  
        
        public IEnumerable<Sotrudnik> Sotrudniks
        {
            get { return sotrudniks; }
            set
            {
                sotrudniks = value;
                OnPropertyChanged("Sotrudniks");
            }
        }
        
        public IEnumerable<Dolzh> Dolzhs
        {
            get { return dolzhs; }
            set
            {
                dolzhs = value;
                OnPropertyChanged("Dolzhs");
            }
        }

        public ApplicationViewModel()
        {
            db = new AppDbContext();
            db.Regions.Load();
            db.Categories.Load();
            db.Orders.Load();
            db.Sotrudniks.Load();
            db.Dolzhs.Load();

            var list = db.Dolzhs
                .Include(a => a.Sotrudnik)
                .ToList();


            Regions = db.Regions.Local.ToBindingList();
            Dolzhs = list;
            Orders = db.Orders.Local.ToBindingList();
            Sotrudniks = db.Sotrudniks.Local.ToBindingList();
            Categories = db.Categories.Local.ToBindingList();

        }


        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Region phone = selectedItem as Region;
                        db.Regions.Remove(phone);
                        db.SaveChanges();
                    }));
            }
        }


        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  { if (SelectedRegion == null) return;
                      Region phone = SelectedRegion;
                      db.Regions.Add(phone);
                      db.SaveChanges();
                  }));
            }
        }

        public Region SelectedRegion
        {
            get { return selectedRegion; }
            set
            {
                selectedRegion = value;
               
                OnPropertyChanged("SelectedRegion");
            }
        }
        public Sotrudnik SelectedSotrudnik
        {
            get { return selectedSotrudnik; }
            set
            {
                selectedSotrudnik = value;
               
                OnPropertyChanged("SelectedSotrudnik");
            }
        }


        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;


                            Region phone = SelectedRegion;
                             // получаем измененный объект
  
                              db.Entry(phone).State = EntityState.Modified;
                              db.SaveChanges();
                          
                      
                  }));
            }
        }


        // команда редактирования
        public RelayCommand EditSotrudnikCommand
        {
            get
            {
                return editSotrudnikCommand ??
                  (editSotrudnikCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;


                      Sotrudnik sotrudnik = SelectedSotrudnik;
                      // получаем измененный объект

                      db.Entry(sotrudnik).State = EntityState.Modified;
                      db.SaveChanges();


                  }));
            }
        }




        private Region _SelectedProduct;
        public Region SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedProduct)));
                //ShowSelectedProductCommand.RaiseCanExecuteChanged(); //обновляем доступность кнопки
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
