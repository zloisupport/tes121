using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MessageBox = System.Windows.Forms.MessageBox;

namespace tes121.Forms
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        AppDbContext db;
        public Auth()
        {
            InitializeComponent();
            db = new AppDbContext();
            db.Sotrudniks.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(isValidUser(this.LoginBox.Text, this.PwdBox.Password))
            {
                this.Hide();

              
                MainWindow phoneWindow = new MainWindow(new Sotrudnik
                {
                    
                });


                phoneWindow.Show();
                this.Close();
            }
            else
            {
                WarningLabel.Foreground = new SolidColorBrush(Colors.Red);
                WarningLabel.Content = "Не верный логин или пароль";
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(10);
                WarningLabel.Visibility = Visibility.Visible;
                timer.Tick += (s, en) => {
                    WarningLabel.Visibility = Visibility.Collapsed;
                    timer.Stop(); // Stop the timer
                };
                timer.Start(); // Starts the timer. 
  
            }
        }
        private bool isValidUser(string name , string pwd)
        {
            bool userName = db.Sotrudniks.Where(i => i.Login == name && i.password == pwd || i.Login == name && i.password ==this.PwdTextBox.Text).Any();

            if (userName)
            {
                return true;
            }
            return false;
        }

        private void ShowHidePwd(object sender, MouseButtonEventArgs e)
        {


        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PwdBox.IsVisible)
            {

                PwdTextBox.Visibility = Visibility.Visible;
                PwdBox.Visibility = Visibility.Hidden;
                PwdTextBox.Text = PwdBox.Password;
                BtnSHpwd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Hide;
            }
            else
            {
                PwdBox.Visibility = Visibility.Visible;
                PwdTextBox.Visibility = Visibility.Hidden;
                PwdBox.Password = PwdTextBox.Text;
                BtnSHpwd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Show;
            }
        }
    }
}
