using System;
using System.Runtime;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;




namespace OCR_WF_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private async void Ocrun()
        {
            var language = new Language("ko");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }
            var stream = File.OpenRead(@"C:\wtf\aa.jpg");
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            var bitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(language);
            var ocrResult = await engine.RecognizeAsync(bitmap).AsTask();
            MessageBox.Show(ocrResult.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenCV Convert = new OpenCV())
            {
                IplImage source = new IplImage();
                Bitmap src = new Bitmap(@"C:\Users\Solzreed\source\repos\OCR(WF)\OCR(WF)\src.jpg");
                source = Convert.Binary(src.ToIplImage());
                pictureBoxIpl1.ImageIpl = source;
                Bitmap savesrc = new Bitmap(source.ToBitmap());
                savesrc.Save(@"C:\wtf\aa.jpg");

                //var ocr = new TesseractEngine("./tessdata", "kor", EngineMode.TesseractOnly);
                //var texts = ocr.Process(source.ToBitmap());
                //MessageBox.Show(texts.GetText());
                Ocrun();

            }


        }

    }
}
