
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlPowerTools;
using QRCoder;
using QRCoder.Xaml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;


namespace tes121.Forms
{
    /// <summary>
    /// Interaction logic for BirthDoc.xaml
    /// </summary>
    public partial class BirthDoc : Window
    {
        public BirthDoc()
        {
            AppDbContext db;
            InitializeComponent();
            db = new AppDbContext();
            db.Sotrudniks.Load();
            var list = db.Sotrudniks.Where(i => i.Dolzh_id == 1).Select(i => i.Names + " " + i.Fam + " " + i.Subname).ToList();

            ZagsDirector.ItemsSource = list;
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public byte[] ImageToByteArray1(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           SearchAndReplace("forms//dtb_kgz_bak.docx");
           
            var LIST = new string[] { Citizen.Text, IIN.Text, BirthRegion.Text, RegNumber.Text };
            var LIST1 = new string[] { Citizen.Text, };
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

            AddQR("qr_a", ImageToByteArray1(qrCodeImage1));
            AddQR("qr_b", ImageToByteArray1(qrCodeImage));
            string CurrentDir = Directory.GetCurrentDirectory()+"\\forms\\svidetelstvo_orozhdeni.docx";

            PrintDocx(CurrentDir);
        }



  
        private void GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.H);

            QRCode qrCodeDataBmp = new QRCode(qrCodeData);

            XamlQRCode qrCode = new XamlQRCode(qrCodeData);

            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20);

            QrAllInf.Source = qrCodeAsXaml;


            Bitmap qrCodeImage = qrCodeDataBmp.GetGraphic(20);



        }

        //public void ReplaceInternalImage(WordprocessingDocument document, string oldImagesPlaceholderText, byte[] newImageBytes)
        //    {
        //        var imagesToRemove = new List<DocumentFormat.OpenXml.Office.Drawing.Drawing>();

        //        IEnumerable<DocumentFormat.OpenXml.Office.Drawing.Drawing> drawings = document.MainDocumentPart.Document.Descendants<DocumentFormat.OpenXml.Office.Drawing.Drawing>().ToList();
        //        foreach (DocumentFormat.OpenXml.Office.Drawing.Drawing drawing in drawings)
        //        {
        //            DocProperties dpr = drawing.Descendants<DocProperties>().FirstOrDefault();
        //            if (dpr != null && dpr.Name == oldImagesPlaceholderText)
        //            {
        //                foreach (Blip b in drawing.Descendants<Blip>().ToList())
        //                {
        //                    OpenXmlPart imagePart = document.MainDocumentPart.GetPartById(b.Embed);

        //                    if (newImageBytes == null)
        //                    {
        //                        imagesToRemove.Add(drawing);
        //                    }
        //                    else
        //                    {
        //                        using (var writer = new BinaryWriter(imagePart.GetStream()))
        //                        {
        //                            writer.Write(newImageBytes);
        //                        }
        //                    }
        //                }
        //            }

        //            foreach (var image in imagesToRemove)
        //            {
        //                image.Remove();
        //            }
        //        }
        //    }


        public  void SearchAndReplace(string document)
        {

            File.Copy(document, "forms//svidetelstvo_orozhdeni.docx", true);

            document = "forms//svidetelstvo_orozhdeni.docx";
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                
               
                Regex regexText1 = new Regex("_USER_GRAZHDAN");
                docText = regexText1.Replace(docText, Citizen.Text);

                Regex regexText2 = new Regex("_USER_IIN");
                docText = regexText2.Replace(docText,IIN.Text);   
                
                Regex regexText3 = new Regex("_USER_DTB");
                docText = regexText3.Replace(docText,Dtb.Text.Replace("/","."));

                Regex regexText4 = new Regex("_USER_BC");
                docText = regexText4.Replace(docText,BirthRegion.Text);

                Regex regexText5 = new Regex("_USER_REGDATE");
                docText = regexText5.Replace(docText, Dtb.Text.Replace("/", "."));

                Regex regexText6 = new Regex("_USER_REGNUM");
                docText = regexText6.Replace(docText, RegNumber.Text);

               Regex regexText7 = new Regex("_USER_FATHER_NAME");
               docText = regexText7.Replace(docText, Father.Text);


               Regex regexText8 = new Regex("_USER_FATHER_PIN");
               docText = regexText8.Replace(docText, FatherPIN.Text);

               Regex regexText9 = new Regex("_USER_FATHER_CITIZEN");
               docText = regexText9.Replace(docText, FatherCitizen.Text);

               Regex regexText10 = new Regex("_USER_FATHER_NATIONALITY");
               docText = regexText10.Replace(docText, FatherNationality.Text);



                //mother
               Regex regexText11= new Regex("_USER_MOTHER_PIN");
               docText = regexText11.Replace(docText, MotherPIN.Text);

               Regex regexText12= new Regex("_USER_MOTHER");
               docText = regexText12.Replace(docText, Mother.Text);

               Regex regexText13= new Regex("_MOTHERCITIZEN");
               docText = regexText13.Replace(docText, MotherCitizen.Text);      
                
                Regex regexText14= new Regex("_MOTHER_NATIONALITY");
               docText = regexText14.Replace(docText, MotherNationality.Text); 
                
                Regex regexText15= new Regex("_MOTHERREGLOCALE");
               docText = regexText15.Replace(docText, RegRegion.Text);


               Regex regexText16= new Regex("_USER_DELIVERY_DATE");
               docText = regexText16.Replace(docText, DeliveryDate.Text.Replace("/", "."));

                
               Regex regexText17= new Regex("_ZAGS_DIRECTOR");
               docText = regexText17.Replace(docText, ZagsDirector.Text);


                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
        }

        private void Dtb_Initialized(object sender, EventArgs e)
        {
            
        }

        private void Dtb_CalendarClosed(object sender, RoutedEventArgs e)
        {
           
        }



        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        private void RegNumber_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IIN.Text = RandomDigits(2);
        }

        private void RegNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            var LIST = new string[] { Citizen.Text, IIN.Text, BirthRegion.Text, RegNumber.Text };
            var LIST1 = new string[] { Citizen.Text, };
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
            changePIN();
        }

        private void changePIN()
        {
            var i =new Random();
            int dice = i.Next(1, 7);
            var date = Dtb.Text.Replace("/",String.Empty);

            string pin = String.Join("", dice + date + RandomDigits(7));
            IIN.Text = pin;
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
            try
            {
                printDoc.PrintPage += new PrintPageEventHandler((sender, args) =>
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(imageFilePath);
                    int printHeight = (int)printDoc.DefaultPageSettings.PrintableArea.Height;
                    int printWidth = (int)printDoc.DefaultPageSettings.PrintableArea.Width;
                    int leftMargin = 0;
                    int rightMargin = 0;

                    args.Graphics.DrawImage(img, new System.Drawing.Rectangle(leftMargin, rightMargin, printWidth, printHeight));
                });
            }
            catch { }
            printDoc.Print();
            printDoc.Dispose();


        }

        private void AddQR(string ElemName ,byte[] QrData)
        {
            //---------------/NOT WORKING WTF -------------------------------

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(@"C:\Users\zoe\source\repos\tes121\tes121\bin\Debug\forms\svidetelstvo_orozhdeni.docx", true))
            {
                ReplaceImage replaceImage = new ReplaceImage();
                replaceImage.ReplaceInternalImage(wordDoc, ElemName, QrData);
            }
            //----------------------------------------------------------------
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

           
        }

        private void ExportToDoc(object sender, RoutedEventArgs e)
        {
            SearchAndReplace("forms//dtb_kgz_bak.docx");

            var LIST = new string[] { Citizen.Text, IIN.Text, BirthRegion.Text, RegNumber.Text };
            var LIST1 = new string[] { Citizen.Text, };
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

            AddQR("qr_a", ImageToByteArray1(qrCodeImage1));
            AddQR("qr_b", ImageToByteArray1(qrCodeImage));
            string DocDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = DocDir;      
            saveFileDialog1.Title = "Экспорт файла";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;

            saveFileDialog1.DefaultExt = "docx";
            saveFileDialog1.Filter = "Word File (.docx ,.doc)|*.docx;*.doc|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;


            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string newDirectory = saveFileDialog1.FileName;
               string CurrentDir= Directory.GetCurrentDirectory();
                LblStatus.Content ="Файл сохранен :"+ saveFileDialog1.FileName;
                File.Copy(CurrentDir+"\\forms\\svidetelstvo_orozhdeni.docx", newDirectory);
            }
        }
    }
}
