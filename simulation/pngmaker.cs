using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;
namespace simulation
{
//    public  class pngmaker
//    {
        
//         int scale = 20;
//        public pngmaker(int scale)
//        {
//            this.scale = scale;
//        }

//// funkcja generująca obraz PNG
//public  void GeneratePNG(ObjectOnMap[,] board, string fileName, Rgba32 emptyColor, Rgba32 plantColor, Rgba32 herbivoreColor, Rgba32 carnivoreColor,Rgba32 mountainColor,Rgba32 lakeColor)
//    {
//        int width = board.GetLength(0);
//        int height = board.GetLength(1);

//        // Tworzenie nowego obrazu
//        using (var image = new Image<Rgba32>(width *scale, height*scale))
//        {
//            // Wypełnianie obrazu zgodnie z zawartością planszy
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    Rgba32 color;
//                        ObjectOnMap objectColored = board[x, y];
//                        if (objectColored is Carnivore)
//                        {
//                            color = carnivoreColor;

//                        }
//                        else if( objectColored is Herbivore)
//                        {
//                            color = herbivoreColor;
//                        }
//                        else if(objectColored is Plant)
//                        {
//                            color = plantColor;
//                        }
//                        else if (objectColored is Mountain)
//                        {
//                            color = mountainColor;
//                        }
//                        else if (objectColored is Lake)
//                        {
//                            color = lakeColor;
//                        }
//                        else
//                        {
//                            color = emptyColor;
//                        }


                    
//                        for (int i = 0; i < scale; i++)
//                        {
//                            for (int j = 0; j < scale; j++)
//                            {
//                                image[x*scale +i, y*scale+j] = color;
//                            }
//                        }
                    
//                }
//            }

//            // Zapisywanie obrazu w formacie PNG
//            using (var stream = new FileStream(fileName, FileMode.Create))
//            {
//                image.Save(stream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
//            }
//        }
//    }

//}
}
