using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DotSlashHaich.TypeLibrary
{
    class Tile
    {
        public string FolderLocation { get; set; }
        public List<double> red = new List<double>();
        public List<double> green = new List<double>();
        public List<double> blue = new List<double>();
        public double width;
        public double height;
        public string imageName;
        public double averageRed;
        public double averageGreen;
        public double averageBlue;
        public double distance;

        public bool getFolderSource(string folderLocation)
        {
            if (Directory.Exists(@folderLocation))
            {
                FolderLocation = folderLocation;
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Tile> getTiles()
        {
            List<Tile> folderTileImages = new List<Tile>();
            string[] tilesInFolder = Directory.GetFiles(FolderLocation);
            foreach (string tileLocation in tilesInFolder)
            {
                Tile tile = new Tile();
                ProccessTileParameters(tile, tileLocation);
                folderTileImages.Add(tile);
            }
            
            return folderTileImages;
        }

        public void ProccessTileParameters(Tile t, string tileLocation)
        {
            try
            {
                //bring file into program for manipulation
                Bitmap imageRaw = new Bitmap(tileLocation, true);
                t.width = imageRaw.Width;
                t.height = imageRaw.Height;
                string[] loc = tileLocation.Split('\\');
                t.imageName = loc[loc.Length-1];

                //process to extract values by looping through every pixel and getting the color
                for (int x =0; x<imageRaw.Width; x++)
                {
                    for (int y = 0; y < imageRaw.Height; y++)
                    {
                        //set the rgb values for that specific pixel
                        Color col = imageRaw.GetPixel(x, y);
                        t.red.Add(col.R);
                        t.green.Add(col.G);
                        t.blue.Add(col.B);
                       
                    }
                }

            }
            catch (Exception)
            {
                //file probably isn't an image, just skip it because showing endless error messages isn't worth it
                //=> also would be out of scope, giving an error report
            }
        }

        public List<Tile> getAverageRGB(List<Tile> t)
        {
            List<Tile> tileAvg = new List<Tile>();
            foreach (Tile item in t)
            {
                Tile tObj = new Tile();
                tObj.imageName = item.imageName;
                tObj.averageRed = (int)item.red.Average();
                tObj.averageGreen = (int)item.green.Average();
                tObj.averageBlue = (int)item.blue.Average();
                tileAvg.Add(tObj);
            }
            return tileAvg;
        }


    }
}
