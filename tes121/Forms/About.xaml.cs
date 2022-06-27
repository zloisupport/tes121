using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace tes121.Forms
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public class Data
        {
            public string Message { get; set; }//for storing message content's     
        }
        public About()
        {
            InitializeComponent();
            List<Data> ObjDataList = new List<Data>();//for list of Data items     
            List<Data> ObjUrlList = new List<Data>();//for list of Data items     

            ObjDataList.Add(new Data { Message = "zh" });

            ObjUrlList.Add(new Data { Message = "https://https://stackoverflow.com/" });
            ObjUrlList.Add(new Data { Message = "https://metanit.com/" });
            ObjUrlList.Add(new Data { Message = "https://www.fast-report.com/" });
            ObjUrlList.Add(new Data { Message = "https://entityframework.net/" });
            ObjUrlList.Add(new Data { Message = "https://dbeaver.io/" });
    
            ListBoxMessage.ItemsSource = ObjDataList;
            ListBoxURL.ItemsSource = ObjUrlList;
            ListBoxLicense.Text = @"This is free and unencumbered software released into the public domain.Anyone is free to copy, modify, publish, use, compile, sell, ordistribute this software, either in source code form or as a compiledbinary, for any purpose, commercial or non-commercial, and by anymeans.In jurisdictions that recognize copyright laws, the author or authorsof this software dedicate any and all copyright interest in thesoftware to the public domain. We make this dedication for the benefitof the public at large and to the detriment of our heirs andsuccessors. We intend this dedication to be an overt act ofrelinquishment in perpetuity of all present and future rights to thissoftware under copyright law.THE SOFTWARE IS PROVIDED `AS IS`, WITHOUT WARRANTY OF ANY KIND,EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OFMERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OROTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OROTHER DEALINGS IN THE SOFTWARE.For more information, please refer to http://unlicense.org/";
        }

 
    }
}
