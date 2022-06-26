using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tes121
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void checkBox_showPassword_Checked(object sender, RoutedEventArgs e)
        {
            // what to do here ?
        }

        private void checkBox_showPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // what to do here ?
        }

        private void ShowHidePwd(object sender, MouseButtonEventArgs e)
        {
            if (pwdBox.IsVisible) { 

                pwdTxtBox.Visibility = Visibility.Visible;
                pwdBox.Visibility = Visibility.Hidden;
                pwdTxtBox.Text = pwdBox.Password;
                BtnSHpwd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Hide;
            }
            else
            {
                pwdBox.Visibility = Visibility.Visible;
                pwdTxtBox.Visibility = Visibility.Hidden;
                pwdBox.Password = pwdTxtBox.Text;
                BtnSHpwd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Show;
            }
        
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Forms.PassportRegistry passportRegistry = new Forms.PassportRegistry();
            passportRegistry.ShowDialog();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            if (BtnChange.IsDefault) {
                Login.IsEnabled = false;
                pwdBox.IsEnabled = false;
                pwdTxtBox.IsEnabled = false;
                LastName.IsEnabled = false;
                SubName.IsEnabled = false;
                FistName.IsEnabled = false;
                Email.IsEnabled = false;
                Phone.IsEnabled = false;
                BtnSHpwd.IsEnabled = false;
                Address.IsEnabled = false;
                BtnChange.IsDefault = false;
                BtnChange.Content = "Изменить";
                BtnSave.IsEnabled = false;
            }
            else
            {
    
                Login.IsEnabled = true;
                pwdBox.IsEnabled = true;
                pwdTxtBox.IsEnabled = true;
                LastName.IsEnabled = true;
                SubName.IsEnabled = true;
                FistName.IsEnabled = true;
                Email.IsEnabled = true;
                Phone.IsEnabled = true;
                Address.IsEnabled = true;
                BtnChange.IsDefault = true;
                BtnSHpwd.IsEnabled = true;
                BtnSave.IsEnabled = true;
                BtnChange.Content = "Отмена";
            }
        }
    }
}
