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
using System.Windows.Shapes;

namespace tes121.Forms
{
    /// <summary>
    /// Interaction logic for GeneratePIN.xaml
    /// </summary>
    public partial class GeneratePIN : Window
    {
        public GeneratePIN()
        {
            InitializeComponent();
            GenPIN();
        }

        private void BtnGenPIN_Click(object sender, RoutedEventArgs e)
        {
            GenPIN();
        }
        private void GenPIN()
        {
            txtPIN.Text = RandomDigits(15);

        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        private void BtnCopyPIN_Click(object sender, RoutedEventArgs e)
        {

            Clipboard.SetText(txtPIN.Text);
        }
    }
}
