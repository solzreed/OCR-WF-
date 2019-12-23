using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace OCR_WF_
{
    class adb
    {
        public Bitmap screenshot()
        {
            var process = new Process();
            var start = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = @"adb.exe",
                Arguments = "shell screencap -p"
            };
            process.StartInfo = start;
            process.Start();
            var stream = process.StandardOutput.BaseStream;
            List<byte> data = new List<byte>(1024);

            int read = 0;
            bool isCR = false;
            do
            {
                byte[] buf = new byte[1024];
                read = stream.Read(buf, 0, buf.Length);

                for (int i = 0; i < read; i++) 
                {
                    if (isCR && buf[i] == 0x0A)
                    {
                        isCR = false;
                        data.RemoveAt(data.Count - 1);
                        data.Add(buf[i]);
                        continue;
                    }
                    isCR = buf[i] == 0x0D;
                    data.Add(buf[i]);
                }
            }
            while (read > 0);

            if (data.Count == 0)
            {
                Console.WriteLine("fail");
                return null;
            }

            Bitmap b = new System.Drawing.Bitmap(new MemoryStream(data.ToArray()));
            return b;
        }
    }
}
