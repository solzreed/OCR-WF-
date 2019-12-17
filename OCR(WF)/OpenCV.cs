using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Blob;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.UserInterface;
using OpenCvSharp.Utilities;

namespace OCR_WF_
{
    class OpenCV : IDisposable
    {
        IplImage gray;
        IplImage bin;
        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }
        public Bitmap Crop(Bitmap src)
        {
            Mat source = OpenCvSharp.Extensions.BitmapConverter.ToMat(src);
            Mat dst = source.SubMat(new Rect(2335, 350, 442, 745));
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);
        }

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 86, 255, ThresholdType.ToZero);
            return bin;
        }

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
        }
    }
}
