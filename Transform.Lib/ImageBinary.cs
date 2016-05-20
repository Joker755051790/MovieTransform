using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Transform.Lib
{
    public static class ImageBinary
    {
        /// <summary>
        /// 将图片转二值化为bit array data
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitArray GetBitData(Bitmap image)
        {
            BitArray result = new BitArray(image.Height * image.Width);

            Rectangle area = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bitData = image.LockBits(area, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr rgbDataPtr = bitData.Scan0;
            int length = image.Width * image.Height * 3;
            byte[] rgbData = new byte[length];
            Marshal.Copy(rgbDataPtr, rgbData, 0, length);
            image.UnlockBits(bitData);

            for (var i = 0; i < rgbData.Length; i += 3)
            {
                double average = (rgbData[i] + rgbData[i + 1] + rgbData[i + 2]) / 3.0;
                result[i / 3] = average > 255 / 2.0;
            }

            return result;
        }
    }
}
