using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
namespace simulation
{
    public  class gifmaker
    {
        //static void Main(string[] args)
        //{
        //    ////ścieżka do katalogu z plikami PNG
        //    //string inputPath = @"C:\ścieżka\do\katalogu\z\plikami\PNG\";

        //    ////ścieżka do pliku wyjściowego GIF
        //    //string outputPath = @"C:\ścieżka\do\pliku\wyjściowego\animacja.gif";

        //    ////nazwa pliku iścieżka wejściowa
        //    //string[] inputFileNames = new string[] { "frame1.png", "frame2.png", "frame3.png" };

        //    ////ustawienia animacji GIF
        //    //MagickReadSettings settings = new MagickReadSettings
        //    //{
        //    //    Format = MagickFormat.Png,
        //    //    Height = 600,
        //    //    Width = 800
        //    //};

        //    ////tworzenie animacji GIF
        //    //using (MagickImageCollection collection = new MagickImageCollection())
        //    //{
        //    //    foreach (string inputFileName in inputFileNames)
        //    //    {
        //    //        string inputFile = Path.Combine(inputPath, inputFileName);
        //    //        using (MagickImage image = new MagickImage(inputFile, settings))
        //    //        {
        //    //            collection.Add(image);
        //    //        }
        //    //    }

        //    //    collection.Write(outputPath, MagickFormat.Gif);
        //    //}
        //}

        string inputPath = @"C:\ścieżka\do\katalogu\z\plikami\PNG\";

        //ścieżka do pliku wyjściowego GIF
        string outputPath = @"C:\ścieżka\do\pliku\wyjściowego\animacja.gif";

        //nazwa pliku iścieżka wejściowa
        string[] inputFileNames = new string[] { "frame1.png", "frame2.png", "frame3.png" };
        MagickReadSettings settings;
        public gifmaker(string inputPath , string outputPath , string[] inputfilenames,MagickReadSettings settings  )
        {

            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.inputFileNames = inputfilenames;
            this.settings = settings;   

           
        }

        public void makeGIF()
        {
            using (MagickImageCollection collection = new MagickImageCollection())
            {
                foreach (string inputFileName in inputFileNames)
                {
                    string inputFile = Path.Combine(inputPath, inputFileName);
                    //using (MagickImage image = new MagickImage(inputFile, settings))
                    //{
                        collection.Add(new MagickImage(inputFile, settings));
                    //}
                }

                collection.Write(outputPath, MagickFormat.Gif);
            }
        }


    }
}
