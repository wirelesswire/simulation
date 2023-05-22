
//using ImageMagick;
using simulation;


//sekcja inicjalizacji//

int resolutionOfImage = 1000;
int sizeOfSimulaton = 10;
int startRoślino = 20;
int startMieso = 1;
int startGory = 10;
int startJeziora =50;
int FrameDelayMs = 20;
int actionDelayMs = 4;

Simulation simulation = new Simulation(sizeOfSimulaton, startRoślino, startMieso, startGory, startJeziora, actionDelayMs);

//sekcja inicjalizacji//


//sekcja tworzenia gifów//
Rgba32 emptyColor = Rgba32.ParseHex("000");
Rgba32 plantColor = Rgba32.ParseHex("0f0");
Rgba32 herbivoreColor = Rgba32.ParseHex("fa00bc");
Rgba32 carnivoreColor = Rgba32.ParseHex("ff9910");
Rgba32 mountainColor = Rgba32.ParseHex("fafafa");
Rgba32 lakeColor = Rgba32.ParseHex("0000ff");
int iter = 0;
int smallGIFiter = 0;
int fullGIFiter = 0;
string videofDirectioryName = "mp4";
string pngDirectoryName = "png";
string gifName = "anim";
string longGifName = "final";
int smallGIFLength = 10;
int numberOfshortGifsMergedIntoLongOne = 5;
Directory.CreateDirectory(pngDirectoryName);
Directory.CreateDirectory(videofDirectioryName);
Directory.CreateDirectory("finals");
//MagickReadSettings gifSettings = new MagickReadSettings
//{
//    Format = MagickFormat.Png,
//    Height = resolutionOfImage,
//    Width = resolutionOfImage
//};
pngmaker pngmaker = new pngmaker(resolutionOfImage / sizeOfSimulaton);
void makePNG()
{


    pngmaker.GeneratePNG(simulation.getOrganismsBoard(), pngDirectoryName + "/" + iter.ToString() + ".png", emptyColor, plantColor, herbivoreColor, carnivoreColor, mountainColor, lakeColor);

    if (iter >= 1 && iter % smallGIFLength == 0)
    {
        makegif();
    }


}
void makegif()
{
    List<string> list = new List<string>();
    for (int i = iter - smallGIFLength; i < iter; i++)
    {
        list.Add(i + ".png");
    }
    Console.WriteLine("From : " + (iter - smallGIFLength) + " to " + iter);

    movieMaker a = new movieMaker( pngDirectoryName + "/", videofDirectioryName + "/" + gifName + smallGIFiter + ".mp4",  list.ToArray());
    a.makeGIF();
    smallGIFiter++;
    if (smallGIFiter % numberOfshortGifsMergedIntoLongOne == 0)
    {
        //mergeGif();
    }

}
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

//sekcja tworzenia gifów//



// sekcja symulacji // 
while (true)
{
    Console.Clear();
    simulation.RunStep();


    simulation.PrintBoard();
    Thread.Sleep(FrameDelayMs);
    //makePNG();
    iter++;
    //Console.Clear();

}
// sekcja symulacji // 

public class epoch
{
    List<Act> acts = new List<Act>();
}

public class Simulation
{
    List <epoch > epochs = new List<epoch>();
    private Board board;
    //public static Random random = new Random();
    private int startingHerbivores = 1;
    private int startingCarnivores = 1;
    private int startingMountains = 1;
    private int startingLakes = 1;
    int timebetweensupdatesOfOneCreature = 20;
    int rangeOfEffenctiveFoodWatering = 5;


    public ObjectOnMap[,] getOrganismsBoard()
    {
        return board.getAllOrganisms();
    }


    public Simulation(int size, int startingHerbivores, int startingCarnivores, int startingMountains, int startingLakes, int timeBetweenmovesofcreature)
    {
        board = new Board(size);
        this.startingCarnivores = startingCarnivores;
        this.startingHerbivores = startingHerbivores;
        this.startingMountains = startingMountains;
        this.startingLakes = startingLakes;
        timebetweensupdatesOfOneCreature = timeBetweenmovesofcreature;

        Initialize(size);
    }

    private void Initialize(int size)
    {   


        Carnivore.resizeMap(size, size);
        Herbivore.resizeMap(size, size);

        //initializeRandomly();
        InitializeSpecially();


        updateClassBoards();

    }

    
    public void InitializeSpecially()
    {
      
        //for (int i = 0; i < board.GetSize(); i++)
        //{

        //    if (i != 5)
        //    {
        //        makeTAtCoords<Mountain>(i, i);
        //    }
        //    //else
        //    //{

        //    //}
        //}
        makeTAtCoords<Herbivore>(5, 8);
        //makeTAtCoords<Lake>(2, 1);
        //makeTAtCoords<Lake>(1, 2);


        //makeTAtCoords<Carnivore>(19, 10);
    }
    public void InitializeSpecially(bool a  = false )
    {
        for (int i = 0; i < board.GetSize(); i++)
        {
            //makeTAtCoords<Herbivore>(0, i);
            for (int j = 0; j < board.GetSize(); j++)
            {
                makeTAtCoords<Herbivore>(i, j);
            }
        }
        //makeTAtCoords<Herbivore>(1, 1);
        //makeTAtCoords<Herbivore>(2, 1);
        //makeTAtCoords<Herbivore>(3, 1);
        //makeTAtCoords<Carnivore>(19, 19);
        //makeTAtCoords<Carnivore>(19, 19);

        //board.removeAt(10, 10);
        board.removeAt(new coords(10,10));
        makeTAtCoords<Carnivore>(10, 10);

    }

    public T makeTAtCoords<T>(int x, int y) where T : ObjectOnMap, new()
    {
        if (board.IsEmpty(x, y))
        {
            T a = new T();
            a.SetXY(x, y);
            a.SetBoard(board);
            board.AddObject(a);
            return a;
        }
        return null;
    }

    public void initializeRandomly()
    {

        for (int i = 0; i < startingHerbivores; i++)
        {

            makeTAtCoords<Herbivore>( helper.Next(board.GetSize()), helper.Next(board.GetSize()));
        }

        for (int i = 0; i < startingCarnivores; i++)
        {

            makeTAtCoords<Carnivore>(helper.Next(board.GetSize()), helper.Next(board.GetSize()));
        }

        for (int i = 0; i < startingMountains; i++)
        {

            makeTAtCoords<Mountain>(helper.Next(board.GetSize()), helper.Next(board.GetSize()));

        }

        for (int i = 0; i < startingLakes; i++)
        {

            makeTAtCoords<Lake>(helper.Next(board.GetSize()), helper.Next(board.GetSize()));

        }
        // Tworzenie roślin na planszy
        for (int i = 0; i < board.GetSize(); i++)
        {
            for (int j = 0; j < board.GetSize(); j++)
            {
                if (helper.NextDouble() < 0.2) // losowe tworzenie roślin z 20% szansą
                {
                    makeTAtCoords<Plant>(i, j);
                }
            }
        }

    }

    public void PrintDebug()
    {
        Console.WriteLine("mięsożerców " + board.carnivores.Count);
        Console.WriteLine("roślinożerców " + board.herbivores.Count);
        Console.WriteLine("roślin" + board.plants.Count);
        Console.WriteLine("użycie randoma od ostatniego razy " + (helper.nextCount - helper.lastShown));

        Console.WriteLine("uzycie randoma " +  helper.getNextCount());

        Console.Write(helper.avgStatsAsString(board.carnivores.Cast<Animal>().ToList(), "mięsożercy "));
        Console.Write(helper.avgStatsAsString(board.herbivores.Cast<Animal>().ToList(), "roślinożercy "));
    }
    public void PrintBoard()
    {
        board.PrintBoard();
        PrintDebug();


    }
    public void checkerIfOkOnBoard()
    {
        List<ObjectOnMap> allObjects = new List<ObjectOnMap>();
        allObjects.AddRange(board.carnivores);
        allObjects.AddRange(board.herbivores);
        allObjects.AddRange(board.plants);
        allObjects.AddRange(board.mountains);
        allObjects.AddRange(board.lakes);

        foreach (var item in allObjects)
        {
            if (board.GetObjectOnMap(item.GetX(), item.GetY()) == item)
            {

            }
            else
            {
                if (item is Organism o)
                {
                    if (o.IsDead())
                    {
                        throw new Exception("jest martwy ale na mapie ");
                    }
                    throw new Exception("to organizm ");
                }
                Console.WriteLine("o tui nie działa ");
                throw new Exception("asdasd");
            }
        }

    }
    public void RunStep()
    {
        epoch a = new epoch();
        checkerIfOkOnBoard();
        //spawningOfFood();
        updateClassBoards();
        movingOfCreatures();
        //multiplicationOfCreatures();
        plantsAndCorpsesEpoch();
        epochs.Add(a);
    }


    public void movingOfCreatures()
    {

        List<Animal> animals = new List<Animal>();
        animals.AddRange(board.carnivores);
        animals.AddRange(board.herbivores);
        animals.Shuffle();

        for (int i = 0; i < animals.Count; i++)
        {
            Animal ani = animals[i];
            if (ani != null && board.carnivores.Contains(ani) || board.herbivores.Contains(ani))// gdyby zostało zjedzone nie powinno móc się ruszyć 
            {
                //trochę do while wyszedł ale niech bedzie 
                Act a = ani.Move((ani is Carnivore) ? board.herbivores.Cast<Organism>().ToList() : board.plants.Cast<Organism>().ToList());

                while (true)
                {
                    board.makeActHappen(a);
                    
                    if (a.gotMoreMoves())
                    {
                        a = ani.Move((ani is Carnivore) ? board.herbivores.Cast<Organism>().ToList() : board.plants.Cast<Organism>().ToList());
                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(timebetweensupdatesOfOneCreature);
                    //updateClassBoards();
                }
            }
            updateClassBoards(); //po skończonym ruchu każdego zwierzęcia bedzie odświezone bo zwierze samo sobie nie przeszkadza 
        }

    }
    public void plantsAndCorpsesEpoch()
    {
        List<Organism> org = new List<Organism>();
        org.AddRange(board.plants);
        org.AddRange(board.corpses);
        foreach (Organism a in org)
        {
            a.epochPass();
            if(a is Corpse c)
            {
                if(c.getNutritionalValue() <= 0)
                {
                    board.makeActHappen(new Act(c, Act.actionTaken.selfdestruction));
                    //dla wszystkich wypadałoby zrobić restytucję akcji 
                }
            }
        }
    }
    public void spawningOfFood()
    {
        for (int i = 0; i < board.GetSize(); i++)
        {
            for (int j = 0; j < board.GetSize(); j++)
            {
                if (board.IsEmpty(i, j))
                {
                    int lakesNearby = board.countOFTypeinRange<Lake>(rangeOfEffenctiveFoodWatering, i, j);
                    int chance = 2 + lakesNearby * 3;

                    if (chance > helper.Next(1000))
                    {
                        makeTAtCoords<Plant>(i, j);
                    }
                }
            }
        }
    }

    public void multiplicationOfCreatures()
    {
        List<Animal> animals = new List<Animal>();
        animals.AddRange(board.carnivores);
        animals.AddRange(board.herbivores);
        animals.Shuffle();
        foreach (Animal item in animals)
        {
            if (item.CanReproduce()) // losowe rozmnażanie z 20% szansą
            {
                //makeTAtCoords<typeof(item.GetType())>();


                if (item is Herbivore)
                {
                    stats c =   item.Reproduce( out coords a);

                    makeTAtCoords<Herbivore>(a.x,a.y).setStats(c);
                    //Herbivore child = (Herbivore)item.Reproduce(item);
                    //board.AddObject(child);
                    //board.herbivores.Add(child);
                    continue;
                }
                if (item is Carnivore)
                {
                    stats c = item.Reproduce(out coords a);

                    makeTAtCoords<Carnivore>(a.x, a.y).setStats(c);


                    //Carnivore child = (Carnivore)item.Reproduce(item);
                    ////board.AddObject(child);
                    //board.carnivores.Add(child);
                    continue;
                }

            }
        }
    }
    
    public void updateClassBoards()
    {
        Herbivore.resetMap();
        Carnivore.resetMap();
        List<ObjectOnMap> allObjects = new List<ObjectOnMap>();
        allObjects.AddRange(board.carnivores);
        allObjects.AddRange(board.herbivores);
        allObjects.AddRange(board.plants);
        allObjects.AddRange(board.mountains);
        allObjects.AddRange(board.lakes);
        foreach (var item in allObjects)
        {
            if (item is Carnivore)// nikt nie je mięsożerców 
            {
                Carnivore.SetMap(item.GetX(), item.GetY(), false);
                Herbivore.SetMap(item.GetX(), item.GetY(), false);
            }
            else if (item is Herbivore)// roślinożercy nie jedzą roślinożerców 
            {
                Herbivore.SetMap(item.GetX(), item.GetY(), false);

            }
            else if (item is Plant)//mięsożercy nie jedzą roślin 
            {
                Carnivore.SetMap(item.GetX(), item.GetY(), false);
            }
            else// nikt nie je gór i jezior 
            {
                Carnivore.SetMap(item.GetX(), item.GetY(), false);
                Herbivore.SetMap(item.GetX(), item.GetY(), false);

            }



        }
    }


}