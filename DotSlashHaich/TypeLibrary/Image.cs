using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DotSlashHaich.TypeLibrary
{

    class Image
    {
        public string ImageLocation { get; set; }
        public string ImageName { get; set; }
        public double AverageRed { get; set; }
        public double AverageGreen { get; set; }
        public double AverageBlue { get; set; }
        public double OriginalWidth { get; set; }
        public double OriginalHeight { get; set; }

        /// <summary>
        /// Gets an image source 
        /// </summary>
        /// <param name="imageLocation"></param>
        /// <returns>true, if image exists, otherwise returns false if doesn't exist</returns>
       public bool getImageSource(string imageLocation)
        {
            if (File.Exists(imageLocation))
            {
                ImageLocation = imageLocation;
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Bitmap> breakImageDown(int x, int y)
        {
            List<Bitmap> blocks = new List<Bitmap>();
            Bitmap imageRaw = new Bitmap(ImageLocation);
            OriginalWidth = imageRaw.Width;
            OriginalHeight = imageRaw.Height;
            int partWidth = imageRaw.Width / x;
            int partHeight = imageRaw.Height / y;
            int totalBlocksInWidth = imageRaw.Width / partWidth;
            int totalBlocksInHeight = imageRaw.Height / partHeight;
            int totalBlocks = totalBlocksInWidth * totalBlocksInHeight;

            for (int X = 0; X < totalBlocksInWidth* partWidth; X+=partWidth)
            {

                for (int Y = 0; Y < totalBlocksInHeight*partHeight; Y+= partHeight)
                {
                    Rectangle rct = new Rectangle(X, Y, partWidth, partHeight);
                   // Bitmap mp = imageRaw.Clone(rct, System.Drawing.Imaging.PixelFormat.DontCare);
                   // mp.Save("snip\\output" + (X +"c"+ Y)+".jpg");
                    blocks.Add(imageRaw.Clone(rct, System.Drawing.Imaging.PixelFormat.DontCare));
                }
            }

            return blocks;
        }

        public List<Image> getBlockRGBAvg(List<Bitmap> c)
        {
            List<Image> l = new List<Image>();
            foreach (Bitmap item in c)
            {
                List<double> rC = new List<double>();
                List<double> gC = new List<double>();
                List<double> bC = new List<double>();
                for (int x = 0; x < item.Width; x++)
                {
                    for (int y = 0; y < item.Height; y++)
                    {
                        //set the rgb values for that specific pixel
                        Color clr = item.GetPixel(x, y);
                       rC.Add(clr.R);
                       gC.Add(clr.G);
                       bC.Add(clr.B);

                    }
                }
                Image i = new Image();
                i.AverageRed = (int)rC.Average();
                i.AverageGreen = (int)gC.Average();
                i.AverageBlue = (int)bC.Average();
                l.Add(i);
            }
            
            return l;
        }


    }
}
