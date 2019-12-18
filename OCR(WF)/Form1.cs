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
using System.Threading;




namespace OCR_WF_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenCV Convert = new OpenCV())
            {
                //이미지 전처리 작업
                IplImage source = new IplImage();
                string path = Thread.GetDomain().BaseDirectory + "src.jpg";
                Bitmap src = new Bitmap(@path);
                Bitmap src2 = Convert.Crop(src);
                source = Convert.Binary(src2.ToIplImage());
                pictureBoxIpl1.ImageIpl = source;
                Bitmap savesrc = new Bitmap(source.ToBitmap());
                string temppath = Thread.GetDomain().BaseDirectory + "temp.jpg";
                savesrc.Save(temppath);
                //여기까지 이미지 전처리
                //테서렉트5.0 가동
                string program = Thread.GetDomain().BaseDirectory + "Tesseract-OCR";
                
                var service = new TesseractService(program, "kor", program+ @"\tessdata");
                var stream = File.OpenRead(temppath);
                string text = textedit.Edit(service.GetText(stream));
                MessageBox.Show(text);

            }


        }

    }
}
