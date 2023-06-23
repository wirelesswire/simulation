
////using ImageMagick;
using simulation;

ConsoleInitializer init;
while (true)
{
    string cmd = "";
    Console.WriteLine("wpisz numer rodzaju symulacjii ");
    Console.WriteLine("typy symulacji : \n 1 - wartości predefiniowane  \n 2 -losowa (samemu wpisujesz wartości startowe losowej symulacji  )\n 3 - wybór  predefiniowanej mapy ");
    cmd = Console.ReadLine();
    if (cmd == "1")
    {
        init = new ConsoleInitializer(true);
    }
    else if (cmd == "2")
    {
        init = new ConsoleInitializer();
    }
    else if (cmd == "3")
    {
        init = new ConsoleInitializer(false);
    }
    else if (cmd == "4")
    {
        init = new ConsoleInitializer(4);
    }

    else
    {
        Console.WriteLine("nieprawidłowy numer komendy ");
        continue;
    }
    break;

}

init.consoleUI();
//init.simulate();

/// <summary>
/// klasa zarządzająca konsolowym interface-em użytkownika 
/// </summary>
public class ConsoleInitializer
{

    //sekcja inicjalizacji//

    int resolutionOfImage = 1000;
    int sizeOfSimulaton = 30;
    int startRoślino = 20;
    int startMieso = 1;
    int startGory = 10;
    int startJeziora = 50;
    int EpochDelayMs = 100;//cała epoka 
    int actionDelayMs = 50;//każdy jeden ruch  nawet jeżeli w trakcie epoki jest więcej niż jeden zostanie pokazany 
                          //sekcja inicjalizacji//


    //sekcja tworzenia gifów//
    //Rgba32 emptyColor = Rgba32.ParseHex("000");
    //Rgba32 plantColor = Rgba32.ParseHex("0f0");
    //Rgba32 herbivoreColor = Rgba32.ParseHex("fa00bc");
    //Rgba32 carnivoreColor = Rgba32.ParseHex("ff9910");
    //Rgba32 mountainColor = Rgba32.ParseHex("fafafa");
    //Rgba32 lakeColor = Rgba32.ParseHex("0000ff");
    //int iter = 0;
    //int smallGIFiter = 0;
    //int fullGIFiter = 0;
    //string videofDirectioryName = "mp4";
    //string pngDirectoryName = "png";
    //string gifName = "anim";
    //string longGifName = "final";
    //int smallGIFLength = 10;
    //int numberOfshortGifsMergedIntoLongOne = 5;
    //sekcja tworzenia gifów//



    Simulation simulation;
    //pngmaker pngmaker;
    /// <summary>
    /// zwraca jedną z przygotowanych map 
    /// </summary>
    /// <param name="num">numer mapy </param>
    /// <returns>mapa w formie string[,]</returns>
    public string[,] makeMap(int num = 0)
    {

        string[,] a = new string[5, 5] {
             { "_","_","_","_","_"},
             { "_","_","_","_","_"},
             { "_","H","_","_","_"},
             { "_","_","_","_","_"},
             { "_","_","_","_","_"},

            };

        if (num == 1)
        {
            a = new string[10, 10] {
             { "_","_","_","L","_","L","_","_","_","_"},
             { "_","C","_","L","_","L","_","_","C","_"},
             { "_","_","_","L","_","L","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "M","M","M","M","_","M","M","M","M","M"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","L","_","L","_","_","_","_"},
             { "_","_","_","L","_","L","_","_","_","_"},
             { "_","_","_","L","_","L","_","_","_","_"},
             { "H","_","_","L","_","L","_","_","_","H"},
            };
        }
        else if (num == 2)
        {
            a = new string[30, 30] {
            { "_","_","_","_","_","_","_","M","_","_","C","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","_","M","_","_","_","M","_","_","_","_","_","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","L","L","L","L","L","L","_"},
            { "L","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","_","M","_","_","_","_","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","_","_","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","_","_","_","_","_","_","L","L","L","L","L","L","_"},
            { "L","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","L","L","L","L","L","L","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","_","_","_","_","_","_","_","_","_"},
            { "_","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","H","_","M","_","_","_","M","_","_","C","_","M","_","_","_","_","_","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","_","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","_","_","_","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","_","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","_","_","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","L","L","L","L","L","L","_"},
            { "_","_","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "_","_","L","M","_","_","_","M","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},
            { "L","L","L","M","_","_","_","_","_","_","_","_","M","_","_","_","_","M","_","_","_","M","_","_","_","_","_","_","_","_"},



            };
        }
        else if(num ==4)
        {
            a = new string[10,10]{
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","C","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
             { "_","_","_","_","_","_","_","_","_","_"},
            };
        }



        return a;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">mapa</param>
    /// <returns>zwraca mapę w formie stringa do wypisania w konsoli </returns>
    public string mapStringify(string[,] a)
    {
        string ret = "";
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                ret += a[i, j] + " ";
            }
            ret += "\n";

        }




        return ret;
    }

    /// <summary>
    /// na podstawie podanych danych tworzy nową symulację 
    /// </summary>
    /// <param name="resolutionOfImage">rozdzeilczość generowanego obrazu </param>
    /// <param name="sizeOfSimulaton">wielkość symulacji </param>
    /// <param name="startRoślino">ilość roślin na start</param>
    /// <param name="startMieso">ilość mięsożerców na start</param>
    /// <param name="startGory">ilość gór na start</param>
    /// <param name="startJeziora">ilość gór na start </param>
    /// <param name="FrameDelayMs">długość przerwy pomiędzy epokami w ms </param>
    /// <param name="actionDelayMs">dłougość przerwy pomiędzy akcjami w ms </param>
    public ConsoleInitializer(int resolutionOfImage, int sizeOfSimulaton, int startRoślino, int startMieso, int startGory, int startJeziora, int FrameDelayMs, int actionDelayMs)
    {
        this.resolutionOfImage = resolutionOfImage;
        this.sizeOfSimulaton = sizeOfSimulaton;
        this.startRoślino = startRoślino;
        this.startMieso = startMieso;
        this.startGory = startGory;
        this.startJeziora = startJeziora;
        this.EpochDelayMs = FrameDelayMs;
        this.actionDelayMs = actionDelayMs;


        makeSimulation(false);

        //gifInit();


    }
    /// <summary>
    /// tworzy nową symulację,  na podstawie odpowiedzi użytkownika  decyduje czy bierze pod uwagę losowo ułożone  wartości podstawowe czy predefinowaną mapę 
    /// </summary>
    /// <param name="wartoscistartowe"></param>
    public ConsoleInitializer(bool losowoscVSpredefinowana = true)
    {
        if (losowoscVSpredefinowana == true)//losowa
        {
            makeSimulation(false);

        }
        else
        {
            string[][] a;
            Console.WriteLine("wybierz jedną z podanych map ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("mapa nr : " + (i +1));
                Console.WriteLine(mapStringify(makeMap(i)));

            }
            int z;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int b))
                {
                    z = b; break;
                }


                Console.WriteLine("nieprawidłowa wartość");

            }
            makeSimulation(makeMap(z-1));





        }


        //gifInit();

    }
    public ConsoleInitializer(int mapNumber) {

        makeSimulation(makeMap( mapNumber));

        //gifInit();
    }


    /// <summary>
    /// pobiera wartości od użytkownika i tworzy nową symulację 
    /// </summary>
    public ConsoleInitializer()
    {
        string[] nazwy = new string[] { /*"rozdzielczość obrazu",*/ "wielkość symulacji", "ilość roślinożerców", "ilość mięsożerców", "ilość gór", "ilość jezior", "czas epoki ", "czas klatki" };
        int[][] przedzialy = new int[][] {
        //new int []{200,10000},//"rozdzielczość obrazu" 
        new int []{5,10000},//"wielkość symulacji"
        new int []{0,10000},//"ilość roślinożerców"
        new int []{0,10000},//"ilość mięsożerców"
        new int []{0,10000},//"ilość gór"
        new int []{0,10000},//"ilość jezior"
        new int []{5,10000},//"czas epoki "
        new int []{0,1000},//"czas klatki"


        };
        int[] wartosci = new int[nazwy.Length];

        wartosci = (int[])helper.getSomeValuesOfType("wpisz wielkość odpowiadającej wartości ( ", " ) w symulacji  w przedziale ", nazwy, przedzialy).Select(d => (int)d).ToArray();


        //this.resolutionOfImage = wartosci[0];
        this.sizeOfSimulaton = wartosci[0];
        this.startRoślino = wartosci[1];
        this.startMieso = wartosci[2];
        this.startGory = wartosci[3];
        this.startJeziora = wartosci[4];
        this.EpochDelayMs = wartosci[5];
        this.actionDelayMs = wartosci[6];


        makeSimulation(false);


        //gifInit();

    }
    /// <summary>
    /// tworzy obiekt symulacji na podstawie parametrów przekształconych przez konstruktor 
    /// </summary>
    /// <param name="rand"> czy symulacjia jest losowa czy też jest przygotowana specjalnie </param>
    public void makeSimulation(bool rand)
    {
        simulation = new Simulation(sizeOfSimulaton, startRoślino, startMieso, startGory, startJeziora, actionDelayMs);
    }
    /// <summary>
    /// tworzy nawoą symulację na podstawie przygotwanej wcześniej mapy 
    /// </summary>
    /// <param name="a">mapa </param>
    public void makeSimulation(string[,] a)
    {
        simulation = new Simulation(a, actionDelayMs);
    }
    /// <summary>
    /// wyświetla mapę i pozwala użytkownikowi na interakcję z symulacją 
    /// </summary>
    public void consoleUI()
    {
        simulation.PrintBoard();
        string cmd;
        while (true){
            Console.WriteLine("wybierz komendę :\n1-> 10 do przodu \n2-> 10 do tyłu \n3-> 1 do przodu \n4-> 1 do tyłu\n5-> 100 do przodu \n6-> 100 do tyłu");
        cmd = Console.ReadLine();
            if(int.TryParse (cmd , out int a ))
            {
                Console.Clear();

                switch (a)
                {
                    case 1:
                        for (int i = 0; i < 10; i++)
                        {
                            simulation.showAct(true);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 10; i++)
                        {
                            simulation.showAct(false);
                        }
                        //simulation.showAct(false);
                        break;
                    case 3:
                        simulation.showAct(true);
                        break; 
                    case 4:
                        simulation.showAct(false);
                        break;
                    case 5:
                        for (int i = 0; i < 100; i++)
                        {
                            simulation.showAct(true);
                        }
                        break;
                    case 6:
                        for (int i = 0; i < 100; i++)
                        {
                            simulation.showAct(false);
                        }
                        //simulation.showAct(false);
                        break;
                    default:
                        for(int i = 0;i < Math.Abs( a); i++)
                        {
                            simulation.showAct(a > 0 ? true : false);
                        }
                        Console.WriteLine("nieprwidłowy numer komendy ");
                        break;
                }
                simulation.PrintBoard();


                //simulation.showEpoch();
            }
            else
            {

            }

        
        }
    }

    /// <summary>
    /// rozpoczyna symulację i odpowiada za jej wyświetlanie 
    /// </summary>
    public void simulate()
    {
 


        bool a = true;
        int b = 0;
        while (true)
        {
            simulation.RunStep();
            Console.Clear();
            simulation.PrintBoard();

            //simulation.showAct(a);
            //if (b % 10 == 0)
            //{
            //    a = !a;
            //}
            //b++;



            ////simulation.PrintBoard();
            Thread.Sleep(EpochDelayMs);
            //////makePNG();
            //iter++;
            //Console.Clear();

        }
        // sekcja symulacji // 
    }
    /// <summary>
    /// inicjalizuje sekcję odpowiedzialną za tworzeni gifów z symulacjii -> tworzy potrzebne foldery i ustawia potrzebne wartości 
    /// </summary>
    //void gifInit()
    //{
    //    Directory.CreateDirectory(pngDirectoryName);
    //    Directory.CreateDirectory(videofDirectioryName);
    //    Directory.CreateDirectory("finals");
    //    //MagickReadSettings gifSettings = new MagickReadSettings
    //    //{
    //    //    Format = MagickFormat.Png,
    //    //    Height = resolutionOfImage,
    //    //    Width = resolutionOfImage
    //    //};
    //    pngmaker = new pngmaker(resolutionOfImage / sizeOfSimulaton);
    //}
    ///// <summary>
    ///// tworzy przedstaweinie graficzne obecnego stanu symulacjii w postaci obrazu png 
    ///// </summary>
    //void makePNG()
    //{


    //    pngmaker.GeneratePNG(simulation.getOrganismsBoard(), pngDirectoryName + "/" + iter.ToString() + ".png", emptyColor, plantColor, herbivoreColor, carnivoreColor, mountainColor, lakeColor);

    //    if (iter >= 1 && iter % smallGIFLength == 0)
    //    {
    //        makegif();
    //    }


    //}

    /// <summary>
    /// tworzy przedstawienie części symulacji w formie gifa 
    /// </summary>
    //void makegif()
    //{
    //    List<string> list = new List<string>();
    //    for (int i = iter - smallGIFLength; i < iter; i++)
    //    {
    //        list.Add(i + ".png");
    //    }
    //    Console.WriteLine("From : " + (iter - smallGIFLength) + " to " + iter);

    //    movieMaker a = new movieMaker(pngDirectoryName + "/", videofDirectioryName + "/" + gifName + smallGIFiter + ".mp4", list.ToArray());
    //    a.makeGIF();
    //    smallGIFiter++;
    //    if (smallGIFiter % numberOfshortGifsMergedIntoLongOne == 0)
    //    {
    //        //mergeGif();
    //    }

    //}
}


///<summary>
///łączy częściowe gify  w jeden długi 
/// 
/// </summary>
//void mergeGif()
//{
//    movieMerger merger = new movieMerger();
//    List<string> shortGifNames = new List<string>();
//    for (int i = 0; i < smallGIFiter; i++)
//    {
//        Console.WriteLine("From : " + 0 + " to " + (iter / (smallGIFLength * numberOfshortGifsMergedIntoLongOne)));
//        shortGifNames.Add(videofDirectioryName + "/" + gifName + i + ".mp4");
//    }

//    merger.Merge(shortGifNames.ToArray(), "finals/" + longGifName + fullGIFiter + ".mp4");
//    fullGIFiter++;
//}





