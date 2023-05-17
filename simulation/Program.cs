
using ImageMagick;
using simulation;


//sekcja inicjalizacji//

int resolutionOfImage = 1000;
int sizeOfSimulaton = 20;
int startRoślino = 20;
int startMieso = 1;
int startGory = 20;
int startJeziora = 100;
int FrameDelayMs = 250;
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
string gifDirectioryName = "gif";
string pngDirectoryName = "png";
string gifName = "anim";
string longGifName = "final";
int smallGIFLength = 10;
int numberOfshortGifsMergedIntoLongOne = 5;
Directory.CreateDirectory(pngDirectoryName);
Directory.CreateDirectory(gifDirectioryName);
Directory.CreateDirectory("finals");
MagickReadSettings gifSettings = new MagickReadSettings
{
    Format = MagickFormat.Png,
    Height = resolutionOfImage,
    Width = resolutionOfImage
};
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

    gifmaker a = new gifmaker(pngDirectoryName + "/", gifDirectioryName + "/" + gifName + smallGIFiter + ".gif", list.ToArray(), gifSettings);
    a.makeGIF();
    smallGIFiter++;
    if (smallGIFiter % numberOfshortGifsMergedIntoLongOne == 0)
    {
        mergeGif();
    }

}
void mergeGif()
{
    gifMerger merger = new gifMerger();
    List<string> shortGifNames = new List<string>();
    for (int i = 0; i < smallGIFiter; i++)
    {
        Console.WriteLine("From : " + 0 + " to " + (iter / (smallGIFLength * numberOfshortGifsMergedIntoLongOne)));
        shortGifNames.Add(gifDirectioryName + "/" + gifName + i + ".gif");
    }

    merger.Merge(shortGifNames.ToArray(), "finals/" + longGifName + fullGIFiter + ".gif");
    fullGIFiter++;
}

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

}
// sekcja symulacji // 




public class Board
{
    private ObjectOnMap[,] organisms;
    private int size;


    public Board(int size)
    {
        this.size = size;
        organisms = new ObjectOnMap[size, size];
    }

    public void AddObject(ObjectOnMap organism)
    {
        organisms[organism.GetX(), organism.GetY()] = organism;
    }
    public int countOFTypeinRange<T>(int squareRange, int x, int y)
    {
        int xRangeMin = (x - squareRange >= 0 ? x - squareRange : 0);
        int xRangeMax = (x + squareRange < size ? x + squareRange : size);
        int yRangeMin = (y - squareRange >= 0 ? y - squareRange : 0);
        int yRangeMax = (y + squareRange < size ? y + squareRange : size);
        int count = 0;
        for (int i = xRangeMin; i < xRangeMax; i++)
        {
            for (int j = yRangeMin; j < yRangeMax; j++)
            {


                if (GetObjectOnMap(i, j) is T) { count++; }
            }
        }
        return count;

    }



    public void RemoveObject(ObjectOnMap organism)
    {
        organisms[organism.GetX(), organism.GetY()] = null;

    }

    public ObjectOnMap GetObjectOnMap(int x, int y)
    {
        return organisms[x, y];
    }
    public ObjectOnMap[,] getAllOrganisms()
    {
        return organisms;
    }

    public void MoveOrganism(Organism organism, int newX, int newY)
    {
        organisms[newX, newY] = organism;
        organisms[organism.GetX(), organism.GetY()] = null;
        organism.MoveTo(newX, newY);
    }
    public bool IsEmpty(int x, int y)
    {
        return organisms[x, y] == null;
    }
    public bool IsEmpty(int[] a)
    {
        return organisms[a[0], a[1]] == null;
    }

    public int GetSize()
    {
        return size;
    }
    public void PrintBoard()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                ObjectOnMap organism = organisms[i, j];
                if (organism == null)
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(organism.toString());
                }

            }
            Console.WriteLine();
        }
    }
}


public class Simulation
{
    private Board board;
    private List<Plant> plants;
    private List<Herbivore> herbivores;
    private List<Carnivore> carnivores;
    private List<Mountain> mountains;
    private List<Lake> lakes;
    public static Random random = new Random();
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
        plants = new List<Plant>();
        herbivores = new List<Herbivore>();
        carnivores = new List<Carnivore>();
        mountains = new List<Mountain>();
        lakes = new List<Lake>();

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
    public void removeObjectFromMap(ObjectOnMap org)
    {
        if (org == null)
        {
            throw new Exception("null jest czemus nie wiem czemu ");
        }
        board.RemoveObject(org);

        if (org is Carnivore carnivore)
        {
            carnivores.Remove(carnivore);
        }
        if (org is Herbivore herbivore)
        {
            herbivores.Remove(herbivore);
        }
        if (org is Plant plant)
        {
            plants.Remove(plant);
        }
        if (org is Mountain mountain)
        {
            mountains.Remove(mountain);
        }
        if (org is Lake lake)
        {
            lakes.Remove(lake);
        }
    }
    public void InitializeSpecially()
    {
        plants = new List<Plant>();
        herbivores = new List<Herbivore>();
        carnivores = new List<Carnivore>();
        mountains = new List<Mountain>();
        lakes = new List<Lake>();
        for (int i = 0; i < board.GetSize(); i++)
        {

            if (i != 5)
            {
                makeTAtCoords<Mountain>(i, i);
            }
            //else
            //{

            //}
        }
        makeTAtCoords<Herbivore>(5, 8);
        makeTAtCoords<Lake>(2, 1);
        makeTAtCoords<Lake>(1, 2);


        makeTAtCoords<Carnivore>(19, 10);
    }
    public T makeTAtCoords<T>(int x, int y) where T : ObjectOnMap, new()
    {
        if (board.IsEmpty(x, y))
        {
            T a = new T();
            a.SetXY(x, y);
            board.AddObject(a);

            if (a is Carnivore carnivore)
            {
                carnivores.Add(carnivore);//// xd że to tak działa xdddddd
            }
            if (a is Herbivore herbivore)
            {
                herbivores.Add(herbivore);
            }
            if (a is Plant plant)
            {
                plants.Add(plant);
            }
            if (a is Mountain mountain)
            {
                mountains.Add(mountain);
            }
            if (a is Lake lake)
            {
                lakes.Add(lake);
            }
            return a;
        }
        return null;
    }

    public void initializeRandomly()
    {
        //// Tworzenie roślin na planszy
        //for (int i = 0; i < board.GetSize(); i++)
        //{
        //    for (int j = 0; j < board.GetSize(); j++)
        //    {
        //        if (random.NextDouble() < 0.2) // losowe tworzenie roślin z 20% szansą
        //        {
        //            makeTAtCoords<Plant>(i, j);
        //        }
        //    }
        //}


        // Tworzenie  roślinożerców i mięsożerców na planszy
        for (int i = 0; i < startingHerbivores; i++)
        {

            makeTAtCoords<Herbivore>( random.Next(board.GetSize()), random.Next(board.GetSize()));
        }

        for (int i = 0; i < startingCarnivores; i++)
        {

            makeTAtCoords<Carnivore>(random.Next(board.GetSize()), random.Next(board.GetSize()));
        }

        for (int i = 0; i < startingMountains; i++)
        {

            makeTAtCoords<Mountain>(random.Next(board.GetSize()), random.Next(board.GetSize()));

        }

        for (int i = 0; i < startingLakes; i++)
        {

            makeTAtCoords<Lake>(random.Next(board.GetSize()), random.Next(board.GetSize()));

        }
        // Tworzenie roślin na planszy
        for (int i = 0; i < board.GetSize(); i++)
        {
            for (int j = 0; j < board.GetSize(); j++)
            {
                if (random.NextDouble() < 0.2) // losowe tworzenie roślin z 20% szansą
                {
                    makeTAtCoords<Plant>(i, j);
                }
            }
        }

    }

    public void PrintDebug()
    {
        Console.WriteLine("mięsożerców " + carnivores.Count);
        Console.WriteLine("roślinożerców " + herbivores.Count);
        Console.WriteLine("roślin" + plants.Count);
        Console.WriteLine("użycie randoma od ostatniego razy " + (helper.nextCount - helper.lastShown));

        Console.WriteLine("uzycie randoma " +  helper.getNextCount());
    }
    public void PrintBoard()
    {
        board.PrintBoard();
        PrintDebug();


    }
    public void checkerIfOkOnBoard()
    {
        List<ObjectOnMap> allObjects = new List<ObjectOnMap>();
        allObjects.AddRange(carnivores);
        allObjects.AddRange(herbivores);
        allObjects.AddRange(plants);
        allObjects.AddRange(mountains);
        allObjects.AddRange(lakes);

        foreach (var item in allObjects)
        {
            if (board.GetObjectOnMap(item.GetX(), item.GetY()) == item)
            {

            }
            else
            {
                Console.WriteLine("o tui nie działa ");
                throw new Exception("asdasd");
            }
        }

    }
    public void RunStep()
    {
        checkerIfOkOnBoard();
        spawningOfFood();
        updateClassBoards();
        movingOfCreatures();
        multiplicationOfCreatures();

    }


    public void movingOfCreatures()
    {

        List<Animal> animals = new List<Animal>();
        animals.AddRange(carnivores);
        animals.AddRange(herbivores);
        animals.Shuffle();

        for (int i = 0; i < animals.Count; i++)
        {
            Animal ani = animals[i];
            if (ani != null && carnivores.Contains(ani) || herbivores.Contains(ani))// gdyby zostało zjedzone nie powinno móc się ruszyć 
            {
                //trochę do while wyszedł ale niech bedzie 
                Act a = ani.Move((ani is Carnivore) ? herbivores.Cast<Organism>().ToList() : plants.Cast<Organism>().ToList(), board.GetSize(), board);

                while (true)
                {
                    if (a.moves())
                    {
                        board.MoveOrganism(ani, ani.GetX() + a.getdX(), ani.GetY() + a.getdY());
                    }
                    else if (a.eats())
                    {
                        //zjedz 
                        Organism org = (Organism)board.GetObjectOnMap(ani.GetX() + a.getdX(), ani.GetY() + a.getdY());
                        ani.Eat(org);
                        removeObjectFromMap(org);

                        // porusz sie 
                        board.MoveOrganism(ani, ani.GetX() + a.getdX(), ani.GetY() + a.getdY());
                    }
                    if (a.gotMoreMoves())
                    {
                        a = ani.Move((ani is Carnivore) ? herbivores.Cast<Organism>().ToList() : plants.Cast<Organism>().ToList(), board.GetSize(), board);
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
        animals.AddRange(carnivores);
        animals.AddRange(herbivores);
        animals.Shuffle();
        foreach (Animal item in animals)
        {
            if (item.CanReproduce(board)) // losowe rozmnażanie z 20% szansą
            {
                if (item is Herbivore)
                {
                    Herbivore child = (Herbivore)item.Reproduce(item, board);
                    board.AddObject(child);
                    herbivores.Add(child);
                    continue;
                }
                if (item is Carnivore)
                {
                    Carnivore child = (Carnivore)item.Reproduce(item, board);
                    board.AddObject(child);
                    carnivores.Add(child);
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
        allObjects.AddRange(carnivores);
        allObjects.AddRange(herbivores);
        allObjects.AddRange(plants);
        allObjects.AddRange(mountains);
        allObjects.AddRange(lakes);
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