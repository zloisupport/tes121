using DocumentFormat.OpenXml.Packaging;
using QRCoder;
using QRCoder.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace tes121.Forms
{
    /// <summary>
    /// Interaction logic for MarrinageDoc.xaml
    /// </summary>
    public partial class MarrinageDoc : Window
    {
        public MarrinageDoc()
        {
            InitializeComponent();
        }

        private void ZagsDirector_MouseDown(object sender, MouseButtonEventArgs e)
        {

    
        }

        private void ZagsLocale_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ZagsLocale_KeyDown(object sender, KeyEventArgs e)
        {
            
            var LIST = new string[] { CroomName.Text, CroomDtb.Text, CroomBirthRegion.Text, CroomNationality.Text, WomenName.Text, WomenDtb.Text };
            var LIST1 = new string[] { RegData.Text, RegRegion.Text, DeliveryDate.Text, ZagsLocale.Text };
            var str = string.Join(" ", LIST);
            var str1 = string.Join(" ", LIST1);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.H);
            QRCodeData qrCodeData1 = qrGenerator.CreateQrCode(str1, QRCodeGenerator.ECCLevel.H);

            QRCode qrCodeDataBmp = new QRCode(qrCodeData);
            QRCode qrCodeDataBmp1 = new QRCode(qrCodeData1);

            XamlQRCode qrCode = new XamlQRCode(qrCodeData);
            XamlQRCode qrCode1 = new XamlQRCode(qrCodeData1);
            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20);
            DrawingImage qrCodeAsXaml1 = qrCode1.GetGraphic(20);
            QrAllInf.Source = qrCodeAsXaml;
            QrDocInf.Source = qrCodeAsXaml1;
           
            Bitmap qrCodeImage = qrCodeDataBmp.GetGraphic(20);
            Bitmap qrCodeImage1 = qrCodeDataBmp1.GetGraphic(20);
        }
        public void SearchAndReplace(string document)
        {

            File.Copy(document, "forms//merrinage_forms_current.docx", true);

            document = "forms//merrinage_forms_current.docx";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }


                //CROOM
                Regex regexText1 = new Regex("_CROOMCITIZEN_PIN");
                docText = regexText1.Replace(docText,CroomPIN.Text.ToUpper());

                Regex regexText2 = new Regex("_CROOMNAME");
                docText = regexText2.Replace(docText, CroomName.Text.ToUpper());

                Regex regexText3 = new Regex("_CROOM_DTB");
                docText = regexText3.Replace(docText, CroomDtb.Text.Replace("/", "."));

                Regex regexText4 = new Regex("_CROOM_BC");
                docText = regexText4.Replace(docText, CroomBirthRegion.Text);

                Regex regexText5 = new Regex("_CROOM_CITIZEN");
                docText = regexText5.Replace(docText, CroomCitizen.Text.ToUpper());

                Regex regexText6 = new Regex("_CROOMNATIONALITY_");
                docText = regexText6.Replace(docText, CroomNationality.Text);


                // BRIDE
                Regex regexText7 = new Regex("_BRIDEPIN_");
                docText = regexText7.Replace(docText, WomenPIN.Text);

                Regex regexText8 = new Regex("_BRIDENAME_");
                docText = regexText8.Replace(docText, WomenName.Text);

                Regex regexText9 = new Regex("_BRIDEDTB_");
                docText = regexText9.Replace(docText, WomenDtb.Text);

                Regex regexText10 = new Regex("_BRIDEBC_");
                docText = regexText10.Replace(docText, WomenBirthRegion.Text);

                Regex regexText11 = new Regex("_BRIDECITIZEN_");
                docText = regexText11.Replace(docText, WoomenCitizen.Text);

                Regex regexText12 = new Regex("_BRIDENATIONALITY_");
                docText = regexText12.Replace(docText, WomenNationality.Text);


                ///PAGE TWO 
                Regex regexText13 = new Regex("_MIRRINAGEDATE_");
                docText = regexText13.Replace(docText, RegData.Text.Replace("/", "."));

                Regex regexText14 = new Regex("_MIRRINAGENUM_");
                docText = regexText14.Replace(docText, WiteNum.Text);

                Regex regexText15 = new Regex("_CROOMFAMILY_");
                docText = regexText15.Replace(docText, NewFamMan.Text);

                Regex regexText16 = new Regex("_BRIDEFAMILY_");
                docText = regexText16.Replace(docText, NewFamWom.Text);

                Regex regexText17 = new Regex("_ZAGSLOCAL_");
                docText = regexText17.Replace(docText, ZagsLocale.Text);     
                
                Regex regexText18 = new Regex("_DELIVERYDATE_");
                docText = regexText18.Replace(docText, DeliveryDate.Text.Replace("/", "."));       
                
                Regex regexText19 = new Regex("_MIRRINAGEREGLOCAL_");
                docText = regexText19.Replace(docText, RegRegion.Text);


                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }










        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WiteNum.Text = RandomDigits(6);
            var LIST = new string[] { CroomName.Text, CroomDtb.Text, CroomBirthRegion.Text, CroomNationality.Text, WomenName.Text, WomenDtb.Text };
            var LIST1 = new string[] { RegData.Text, RegRegion.Text, DeliveryDate.Text, ZagsLocale.Text };
            var str = string.Join(" ", LIST);
            var str1 = string.Join(" ", LIST1);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.H);
            QRCodeData qrCodeData1 = qrGenerator.CreateQrCode(str1, QRCodeGenerator.ECCLevel.H);

            QRCode qrCodeDataBmp = new QRCode(qrCodeData);
            QRCode qrCodeDataBmp1 = new QRCode(qrCodeData1);

            XamlQRCode qrCode = new XamlQRCode(qrCodeData);
            XamlQRCode qrCode1 = new XamlQRCode(qrCodeData1);
            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20);
            DrawingImage qrCodeAsXaml1 = qrCode1.GetGraphic(20);
            QrAllInf.Source = qrCodeAsXaml;
            QrDocInf.Source = qrCodeAsXaml1;
           
            Bitmap qrCodeImage = qrCodeDataBmp.GetGraphic(20);
            Bitmap qrCodeImage1 = qrCodeDataBmp1.GetGraphic(20);
            SearchAndReplace("forms//merrinage_forms.docx");
            AddQR("qr_a", ImageToByteArray1(qrCodeImage1));
            AddQR("qr_b", ImageToByteArray1(qrCodeImage));
            string CurrentDir = Directory.GetCurrentDirectory() + "\\forms\\merrinage_forms_current.docx";

            PrintDocx(CurrentDir);
        }
        public byte[] ImageToByteArray1(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        private static void PrintDocx(string imageFilePath)
        {
            ProcessStartInfo info = new ProcessStartInfo(imageFilePath);

            info.Verb = "Print";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(info);
            PrinterSettings settings = new PrinterSettings();
            string PrinterName = settings.PrinterName;

            //set paper size
            PaperSize oPS = new PaperSize
            {
                RawKind = (int)PaperKind.A4
            };
            //choose the tray here
            System.Drawing.Printing.PaperSource oPSource = new System.Drawing.Printing.PaperSource
            {
                RawKind = (int)PaperSourceKind.Upper
            };
            PrintDocument printDoc = new PrintDocument
            {
                PrinterSettings = settings,
            };
            //set printer name here it's the default printer
            printDoc.PrinterSettings.PrinterName = PrinterName;
            printDoc.DefaultPageSettings.PaperSize = oPS;
            printDoc.DefaultPageSettings.PaperSource = oPSource;
            //TODO NEED FIX: Double print window
           
            //    printDoc.PrintPage += new PrintPageEventHandler((sender, args) =>
            //    {
            //        try
            //        {
            //        System.Drawing.Image img = System.Drawing.Image.FromFile(imageFilePath);
            //     catch { }
            //        int printHeight = (int)printDoc.DefaultPageSettings.PrintableArea.Height;
            //        int printWidth = (int)printDoc.DefaultPageSettings.PrintableArea.Width;
            //        int leftMargin = 0;
            //        int rightMargin = 0;

            //        args.Graphics.DrawImage(img, new System.Drawing.Rectangle(leftMargin, rightMargin, printWidth, printHeight));
            //    });
            //}
     
            printDoc.Print();
            //printDoc.Dispose();
            //printDoc.Close();


        }
        private void AddQR(string ElemName, byte[] QrData)
        {
            //---------------/NOT WORKING WTF -------------------------------

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(@"forms\\merrinage_forms_current.docx", true))
            {
                ReplaceImage replaceImage = new ReplaceImage();
                replaceImage.ReplaceInternalImage(wordDoc, ElemName, QrData);
            }
            //----------------------------------------------------------------
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            var LIST = new string[] { CroomName.Text, CroomDtb.Text, CroomBirthRegion.Text, CroomNationality.Text, WomenName.Text, WomenDtb.Text };
            var LIST1 = new string[] { RegData.Text, RegRegion.Text, DeliveryDate.Text, ZagsLocale.Text };
            var str = string.Join(" ", LIST);
            var str1 = string.Join(" ", LIST1);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.H);
            QRCodeData qrCodeData1 = qrGenerator.CreateQrCode(str1, QRCodeGenerator.ECCLevel.H);

            QRCode qrCodeDataBmp = new QRCode(qrCodeData);
            QRCode qrCodeDataBmp1 = new QRCode(qrCodeData1);

            XamlQRCode qrCode = new XamlQRCode(qrCodeData);
            XamlQRCode qrCode1 = new XamlQRCode(qrCodeData1);
            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20);
            DrawingImage qrCodeAsXaml1 = qrCode1.GetGraphic(20);
            QrAllInf.Source = qrCodeAsXaml;
            QrDocInf.Source = qrCodeAsXaml1;

            Bitmap qrCodeImage = qrCodeDataBmp.GetGraphic(20);
            Bitmap qrCodeImage1 = qrCodeDataBmp1.GetGraphic(20);
            SearchAndReplace("forms//merrinage_forms.docx");
            AddQR("qr_a", ImageToByteArray1(qrCodeImage1));
            AddQR("qr_b", ImageToByteArray1(qrCodeImage));
            string DocDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = DocDir;
            saveFileDialog1.Title = "Экспорт файла";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.FileName = "" + DateTime.Now.ToString("yy-MM-HH mm");
            saveFileDialog1.DefaultExt = "docx";
            saveFileDialog1.Filter = "Word File (.docx ,.doc)|*.docx;*.doc|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string newDirectory = saveFileDialog1.FileName;
                string CurrentDir = Directory.GetCurrentDirectory();
                LblStatus.Content = "Файл сохранен :" + saveFileDialog1.FileName;
                File.Copy(CurrentDir + "\\forms\\merrinage_forms_current.docx", newDirectory);
            }
        }
    }
}
