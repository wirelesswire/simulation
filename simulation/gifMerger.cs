using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
namespace simulation
{
    
        public class gifMerger
        {
            float delay = 0 ;
            public void Merge(string[] fileNames, string outputFileName)
            {
                using (var collection = new MagickImageCollection())
                {
                    
                    foreach (var fileName in fileNames)
                    {
                       MagickImageCollection a = new MagickImageCollection();
                       a.Read(fileName,MagickFormat.Gif);
                    //a.
                    //collection.Add(a);
                    foreach (var item in a)
                    {
                        collection.Add(item);
                    }

                    //collection.AddRange(a);
                        //var image = new MagickImage(fileName);
                        
                        //collection.Add(image);
                    }

                    // Ustawienie czasu trwania dla każdej klatki (5 fps)
                    //var delay = TimeSpan.FromSeconds(1.0 / 5);
                    foreach (var image in collection)
                    {
                        image.AnimationDelay =20;
                    }

                // Złączenie obrazów w jedną animację GIF
                collection.Write(outputFileName,MagickFormat.Gif);
                    //using (var output = collection.Merge())
                    //{
                    //    output.Write(outputFileName);
                    //}
                }
            }
        }

    
}
