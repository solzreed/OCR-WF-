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
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;




namespace OCR_WF_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

       private void Run(adb a,TesseractService service, OpenCV Convert)
        {
                //이미지 전처리 작업
                IplImage source = new IplImage();
                Bitmap src = new Bitmap(a.screenshot());
                source = Convert.Binary(Convert.Crop(src).ToIplImage());
                pictureBoxIpl1.ImageIpl = source;
                Bitmap savesrc = new Bitmap(source.ToBitmap());
                string temppath = Thread.GetDomain().BaseDirectory + "temp.jpg";
                savesrc.Save(temppath);
                //여기까지 이미지 전처리


                //테서렉트5.0 가동
                var stream = File.OpenRead(temppath);
                string text = textedit.Edit(service.GetText(stream));
                MessageBox.Show(text);
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string program = Thread.GetDomain().BaseDirectory + "Tesseract-OCR";
            OpenCV Convert = new OpenCV();
            adb a = new adb();
            var service = new TesseractService(program, "kor", program + @"\tessdata");

            while (true)
             {
                 Run(a, service,Convert);
                 Thread.Sleep(10000);
             }
            
        }

    }
}
