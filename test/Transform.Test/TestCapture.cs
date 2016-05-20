using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using Transform.Lib;
using System.Threading;
using System.Collections;

namespace Transform.Test
{
    [TestClass]
    public class TestCapture
    {
        static int imageWidth = 160;
        static int imageHeight = 120;
        static int fps = 30;

        static string ffmpegPath = @"D:\ys\GitHub\MovieTransform\tool\ffmpeg.exe";
        static string movieDirecotry = @"E:\Movie";
        static string movieName = "APPLE!![BAD+][1440x1080][x264_flac].mkv";
        static string imageFolderName = "Image";
        static string dataFolderName = "Data";

        static string moviePath = Path.Combine(movieDirecotry,movieName);
        static string movieFolder = Path.Combine(movieDirecotry, Path.GetFileNameWithoutExtension(movieName));
        static string imageDirectory = Path.Combine(movieFolder, imageFolderName);
        static string dataDirectory = Path.Combine(movieFolder, dataFolderName);

        [TestMethod]
        public void CaptureImage()
        {
            var cap = new FFMPEGCapture(ffmpegPath, imageDirectory);
            cap.Capture(moviePath, imageWidth, imageHeight, fps);
        }

        [TestMethod]
        public void CreateBinaryData()
        {
            Parallel.ForEach(Directory.GetFiles(imageDirectory), file =>
            {
                Bitmap image = new Bitmap(file);
                BitArray data = ImageBinary.GetBitData(image);
                StringBuilder sb = new StringBuilder();

                for (var i = 0; i < image.Height * image.Width; i++)
                {
                    sb.Append(data[i] ? "x" : " ");
                }

                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                using (FileStream fs = new FileStream(Path.Combine(dataDirectory, Path.GetFileNameWithoutExtension(file) + ".txt"), FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(sb.ToString());
                    }
                }
            });
        }
    }
}
