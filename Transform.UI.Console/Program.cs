using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Transform.UI.Console
{
    class Program
    {
        static int imageWidth = 160;
        static int imageHeight = 120;
        static int fps = 30;

        static string ffmpegPath = @"D:\ys\GitHub\MovieTransform\tool\ffmpeg.exe";
        static string movieDirecotry = @"E:\Movie";
        static string movieName = "APPLE!![BAD+][1440x1080][x264_flac].mkv";
        static string imageFolderName = "Image";
        static string dataFolderName = "Data";

        static string moviePath = Path.Combine(movieDirecotry, movieName);
        static string movieFolder = Path.Combine(movieDirecotry, Path.GetFileNameWithoutExtension(movieName));
        static string imageDirectory = Path.Combine(movieFolder, imageFolderName);
        static string dataDirectory = Path.Combine(movieFolder, dataFolderName);

        static void Main(string[] args)
        {
            System.Console.Write("ready to show...");
            Thread.Sleep(2000);
            System.Console.Clear();
            System.Console.SetBufferSize(imageWidth, imageHeight);

            foreach (var file in Directory.GetFiles(dataDirectory))
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        System.Console.Write(sr.ReadToEnd());
                    }
                }
                Thread.Sleep(1000 / fps);
                System.Console.SetCursorPosition(0, 0);
            }
        }
    }
}
