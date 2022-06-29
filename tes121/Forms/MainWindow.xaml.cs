
using FastReport;
using FastReport.Data;
using FastReport.Export.Html;
using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tes121
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Sotrudnik Sotrudnik { get; private set; }

        static string TEMPDIR = System.IO.Path.GetTempPath();
        public MainWindow(Sotrudnik s)
        {
           
            InitializeComponent();
            Sotrudnik = s;

            this.DataContext = Sotrudnik;

            // tblOrdersDataGrid.ItemsSource = dbDataSetTableAdapters.Tables.OfType<DataTable>().Select(dt => dt.TableName);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnPIN_Click(object sender, RoutedEventArgs e)
        {
            Forms.GeneratePIN generatePIN = new Forms.GeneratePIN();
            generatePIN.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateReport("Orders");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка" + e.ToString(), "Я упал извините 😭");
            }
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {


        }
        /// <summary>
        /// REgion TAB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_Initialized(object sender, EventArgs e)
        {

        }

        private void TabItem_Initialized_1(object sender, EventArgs e)
        {

        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }




        public static void GenerateReport(string FileName)
        {
           RegisteredObjects.AddConnection(typeof(SQLiteDataConnection));
            // Create new Report 
            Report report = new Report();
            string GetDir = Directory.GetCurrentDirectory();
            // Load report from file
            report.Load($"{GetDir}//Report//{FileName}.frx");
            // Set the parameter
            report.SetParameterValue("MYPARAMETER", 1024);
            // Prepare the report
            report.Prepare();
            // Export in Jpeg
            //ImageExport image = new ImageExport();
            //image.ImageFormat = ImageExportFormat.Jpeg;
            //// Set up the quality
            //image.JpegQuality = 90;
            //// Decrease a resolution
            //image.Resolution = 72;
            //// We need all pages in one big single file
            //image.SeparateFiles = false;
            //report.Export(image, "report.jpg");


            HTMLExport html = new HTMLExport();
            // We need embedded pictures inside html
            html.EmbedPictures = true;
            // Enable all report pages in one html file
            html.SinglePage = true;
            // We don't need a subfolder for pictures and additional files
            html.SubFolder = false;
            // Enable layered HTML
            html.Layers = true;
            // Turn off the toolbar with navigation
            html.Navigator = false;
            // Save the report in html
            report.Export(html, $"{TEMPDIR}//report.html");
            if (!File.Exists($"{TEMPDIR}//report.html")) return;
            Forms.Print print = new Forms.Print();
            print.ShowDialog();

        }



        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try {
                GenerateReport("Positions");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка"+ e.ToString());
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Forms.About about = new Forms.About();
            about.Show();
        }

    }
}
