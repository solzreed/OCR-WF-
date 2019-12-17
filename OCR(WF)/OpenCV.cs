using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

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

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 101, 255, ThresholdType.ToZero);
            return bin;
        }

        public void Dispose()
        {
            if (gray != null) Cv.ReleaseImage(gray);
        }
    }
}
