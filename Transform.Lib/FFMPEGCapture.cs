using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Transform.Lib
{
    public class FFMPEGCapture
    {
        public FFMPEGCapture(string toolPath, string imageDirectory)
        {
            if (!File.Exists(toolPath))
            {
                throw new FileNotFoundException(toolPath);
            }

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            this.ToolPath = toolPath;
            this.ImageDirectory = imageDirectory;
        }

        public string ToolPath { get; set; }

        public string ImageDirectory { get; set; }

        public void Capture(string moviePath, int width, int height, int fps)
        {
            string imagePath = Path.Combine(this.ImageDirectory, "%4d.jpg");

            ProcessStartInfo ffmpeg = new ProcessStartInfo(this.ToolPath);
            ffmpeg.UseShellExecute = false;
            ffmpeg.RedirectStandardOutput = true;
            ffmpeg.WindowStyle = ProcessWindowStyle.Hidden;
            ffmpeg.Arguments = string.Format("-i \"{0}\" -r {4} -s {2}*{3} \"{1}\"",
                moviePath,
                imagePath,
                width,
                height,
                fps);

            Process process = Process.Start(ffmpeg);
            process.WaitForExit();
        }
    }
}
