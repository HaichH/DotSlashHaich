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
        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }

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

        public List<Bitmap> breakImageDown(int width, int height)
        {
            List<Bitmap> blocks = new List<Bitmap>();
            Bitmap imageRaw = new Bitmap(ImageLocation);
            OriginalWidth = imageRaw.Width;
            OriginalHeight = imageRaw.Height;
            int partWidth = imageRaw.Width / width;
            int partHeight = imageRaw.Height / height;
            int totalBlocksInWidth = imageRaw.Width / partWidth;
            OriginalWidth = totalBlocksInWidth * partWidth;
            int totalBlocksInHeight = imageRaw.Height / partHeight;
            OriginalHeight = totalBlocksInHeight * partHeight;
            int totalBlocks = totalBlocksInWidth * totalBlocksInHeight;
            int x = 0;
            int y = 0;
            for (int X = 0; X < totalBlocksInWidth* partWidth; X++)
            {
                if (x>=totalBlocksInWidth*partWidth)
                {
                    x = 0;
                    y += partHeight;
                }

                Rectangle rct = new Rectangle(x, y, partWidth, partHeight);
                Bitmap mp = imageRaw.Clone(rct, System.Drawing.Imaging.PixelFormat.DontCare); 
                blocks.Add(imageRaw.Clone(rct, System.Drawing.Imaging.PixelFormat.DontCare));
                x += partWidth;
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

        public Bitmap DrawMosaic(List<Bitmap> s, Image self)
        {
            
            Bitmap bitmap = new Bitmap(self.OriginalWidth, self.OriginalHeight);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int x = 0;
                int y = 0;
                foreach (Bitmap item in s)
                {
                    if (x>= bitmap.Width)
                    {
                        x = 0;
                        y += item.Height;
                    }
                    g.DrawImage(item, x, y);
                    x += item.Width;
                }
            }

            return bitmap;
        }
    }
}
