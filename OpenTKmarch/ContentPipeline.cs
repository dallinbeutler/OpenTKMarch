
using FFImageLoading;
using ImageProcessor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OpenTKmarch
{
    static class ContentPipeline
    {
        const string platform = "WINDOWS";

       static ContentPipeline()
        {
           
        }

        //public static byte[] loadImage(string fileName)
        //{
        //    //var img = FFImageLoading.ImageService.Instance.LoadFile("").Success(()=>return )

        //    //ImageService imageservice = new ImageService();
        //    //var foo = imageservice.LoadUrl("http://www.funchap.com/wp-content/uploads/2014/05/help-dog-picture.jpg")
        //    //.LoadingPlaceholder("loading.png")
        //    //.ErrorPlaceholder("error.png")
        //    //.Retry(3, 200)
        //    //Image.Load(@"C:\skin.jpg");

        //    //return Bitmap.FromFile(@"Content\" + fileName).;
        //    OpenTK.
        //}

        public static System.Drawing.Imaging.BitmapData LoadImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("File don't exist!");
            }
            Image image= Image.FromFile(fileName);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            
            Bitmap bitmap = (Bitmap)image;
            
            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);

            
            switch (image.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                case System.Drawing.Imaging.PixelFormat.PAlpha:
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
                    break;
                //case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Bgr, PixelType.UnsignedByte, bitmapData.Scan0);
                    break;
                default:
                    throw new Exception(image.PixelFormat.ToString() + " Unkown!");
            }
            Console.WriteLine(fileName + "  - " + image.PixelFormat + " loaded.");
            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, texData.Width, texData.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, texData.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return bitmapData;
        }



    }
}
