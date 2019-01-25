using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSlashHaich.TypeLibrary;
using System.Drawing;

namespace DotSlashHaich
{

    /// <summary>
    /// Hi there, this is my attempt of the task given to me by Attie Gelderblom, the requested functionality is:
    /// 1.       provision input for an image - and a folder containing tile images
    /// 2.       Calculate avg RGB for each tile image (Avg R, Avg G, Avg B) 
    /// 3.       Divide our input image in 20x20 parts.
    /// 4.       Calculate the avg RGB for each of the 400 parts in our input image.
    /// 5.       Calculate the distance between every tile (AVG RGB) and every part of our image(AVG RGB)
    /// 6.       Choose the tiles with the smallest distance, resize them and replace that image part with the tile
    /// 7.       Save output image
    /// My approach is to segregate the input and thus dealing with them as seperate enteties and using this class as an meeting point
    /// => for these two different classes so you'd find most of the code within those classes. 
    /// Author: Healing Haich Legodi - healing.legodi@gmail.com
    /// </summary>
    class Program
    {
        string defaultImageLocation = "";
        string defaultFolderLocation = "";
       static TypeLibrary.Image img = new TypeLibrary.Image();
       static Tile tile = new Tile();

        static void Main(string[] args)
        {
            /*1: Get input*/
            promptImageLocation();
            promptTileFolderLocation();

            /*2: calculate average RGB for */
            List<Tile> tileImages = tile.getTiles();
            List<Tile> averages = tile.getAverageRGB(tileImages);
            Console.WriteLine(new string('=', 25));
            Console.WriteLine("Average RGB per Tile: ");
            foreach (Tile item in averages)
            {
                Console.WriteLine("Name: {0} => Average Red: {1} => Average Green: {2} => Average Blue: {3}", item.imageName, item.averageRed, item.averageGreen, item.averageBlue);
            }
            Console.WriteLine(new string('=', 7)+"End of snippet"+ new string('=', 6));

            /*3 & 4: Divide image into 20*20 parts and calculate average RGB for each parts*/
            
            List<Bitmap> imageBlocks = img.breakImageDown(20,20);
            List<TypeLibrary.Image> blockListRGB = img.getBlockRGBAvg(imageBlocks);
            
            Console.WriteLine(new string('=', 25));
            Console.WriteLine("Average RGB from all blocks: ");
            int i = 0;
            foreach (TypeLibrary.Image item in blockListRGB)
            {
                Console.WriteLine("Block#: {0} => Average Red: {1} => Average Green: {2} => Average Blue: {3}",++i , item.AverageRed, item.AverageGreen, item.AverageBlue);
            }
            Console.WriteLine(new string('=', 7) + "End of snippet" + new string('=', 6));
            
            /*5 */
            

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void promptImageLocation()
        {
            Console.WriteLine("Hi "+Environment.UserName+" , Please provide the location of the image please:");
            string userImageLocation = Console.ReadLine();

            while (!img.getImageSource(userImageLocation))
            {
                Console.WriteLine("Error! "+userImageLocation+" doesn't exist. Try again please....");
                Console.Write(@"Enter such a format 'c:\\blah\blah\blah.jpg'");
                userImageLocation = Console.ReadLine();
            }
        }

        private static void promptTileFolderLocation()
        {
            Console.WriteLine();
            Console.WriteLine("Okay! "+ Environment.UserName+" now enter the folder directory:");
            string userFolderLocation = Console.ReadLine();

            while (!tile.getFolderSource(userFolderLocation))
            {
                Console.WriteLine("Error! The Tile Folder Location doesn't exist");
                Console.Write("Enter such a format 'c:\\blah\blah\'");
                userFolderLocation = Console.ReadLine();
            }
        }
    }
}
