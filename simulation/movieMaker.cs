using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Drawing;
using System.IO;
namespace simulation
{
    //public class movieMaker
    //{

    //    string inputPath = @"C:\ścieżka\do\katalogu\z\plikami\PNG\";

    //    //ścieżka do pliku wyjściowego mp4

    //    string outputPath = @"C:\ścieżka\do\pliku\wyjściowego\animacja.mp4";

    //    //nazwa pliku iścieżka wejściowa
    //    string[] inputFileNames = new string[] { "frame1.png", "frame2.png", "frame3.png" };
    //    //MagickReadSettings settings;
    //    //public movieMaker(string inputPath, string outputPath, string[] inputfilenames, MagickReadSettings settings)
    //    //{

    //    //    this.inputPath = inputPath;
    //    //    this.outputPath = outputPath;
    //    //    this.inputFileNames = inputfilenames;
    //    //    this.settings = settings;


    //    //}
    //    public movieMaker(string inputPath, string outputPath, string[] inputfilenames)
    //    {

    //        this.inputPath = inputPath;
    //        this.outputPath = outputPath;
    //        this.inputFileNames = inputfilenames;
    //        //this.settings = settings;


    //    }
    //    public void makeGIF()
    //    {
    //        //string[] pngFiles = { "image1.png", "image2.png", "image3.png" };
    //        string outputVideo = "output.mp4";

    //        string tempFolder = Path.Combine(Path.GetTempPath(), "VideoFrames");
    //        Directory.CreateDirectory(tempFolder);

    //        foreach (string pngFile in inputFileNames)
    //        {
    //            string framePath = Path.Combine(tempFolder, $"{Guid.NewGuid()}.png");

    //            // Load and resize the PNG image if needed
    //            using (System.Drawing.Image image =  System.Drawing.Image.FromFile(inputPath + pngFile))
    //            {
    //                // Adjust the size as per your requirements
    //                System.Drawing.Size resizedSize = new System.Drawing.Size(1280, 720);
    //                using (Bitmap resizedImage = new Bitmap(image, resizedSize))
    //                {
    //                    resizedImage.Save(framePath);
    //                }
    //            }
    //        }

    //        // Encode the frames into an MP4 video
    //        using (var engine = new Engine())
    //        {
    //            //engine.GetMetadata( new MediaFile( tempFolder));
                
    //            var inputFile = new MediaFile { Filename = "png/0.png" };
    //            var outputFile = new MediaFile { Filename = outputVideo };
    //            //new MediaFile()
    //            var options = new ConversionOptions { CustomWidth = 1000, CustomHeight = 1000, VideoBitRate = 5000 };
    //            //var options = new ConversionOptions { };
    //            engine.Convert(inputFile, new MediaFile( "mp4/anim0.mp4"), options);
    //            engine.Dispose();
    //        }
           
    //        Console.WriteLine("Video creation successful!");
    //    }


    //    //static void Main()
    //    //{
    //    //    //string[] pngFiles = { "image1.png", "image2.png", "image3.png" };
    //    //    string outputVideo = "output.mp4";

    //    //    string tempFolder = Path.Combine(Path.GetTempPath(), "VideoFrames");
    //    //    Directory.CreateDirectory(tempFolder);

    //    //    foreach (string pngFile in pngFiles)
    //    //    {
    //    //        string framePath = Path.Combine(tempFolder, $"{Guid.NewGuid()}.png");

    //    //        // Load and resize the PNG image if needed
    //    //        using (Image image = Image.FromFile(pngFile))
    //    //        {
    //    //            // Adjust the size as per your requirements
    //    //            Size resizedSize = new Size(1280, 720);
    //    //            using (Bitmap resizedImage = new Bitmap(image, resizedSize))
    //    //            {
    //    //                resizedImage.Save(framePath);
    //    //            }
    //    //        }
    //    //    }

    //    //    // Encode the frames into an MP4 video
    //    //    using (var engine = new Engine())
    //    //    {
    //    //        engine.GetMetadata(tempFolder);
    //    //        var inputFile = new MediaFile { Filename = Path.Combine(tempFolder, "%d.png") };
    //    //        var outputFile = new MediaFile { Filename = outputVideo };

    //    //        var options = new ConversionOptions { CustomWidth = 1280, CustomHeight = 720, FrameRate = 30, VideoBitRate = 5000 };

    //    //        engine.Convert(inputFile, outputFile, options);
    //    //    }

    //    //    Console.WriteLine("Video creation successful!");
    //    //}











    //    //using (MagickImageCollection collection = new MagickImageCollection())
    //    //{
    //    //    foreach (string inputFileName in inputFileNames)
    //    //    {
    //    //        string inputFile = Path.Combine(inputPath, inputFileName);

    //    //            collection.Add(new MagickImage(inputFile));
    //    //    }
    //    //    //collection.Write()
    //    //    // Ustaw prędkość klatek
    //    //    collection.Coalesce();
    //    //    foreach (MagickImage image in collection)
    //    //    {
    //    //        image.AnimationDelay = 100 / 30;
    //    //    }
    //    //    collection.Write("mp4/anim0.mp4", MagickFormat.Mp4);
    //    //}
    //}



}
