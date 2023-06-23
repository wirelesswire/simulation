using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa odpowiedzialna za działanie symulacji 
    /// </summary>
    public class Simulation
    {

        List<epoch> epochs = new List<epoch>();


        private int currentEpoch_shown = 0;
        private int currentAct_shown = 0;
        private Board board;
        private int startingHerbivores = 1;
        private int startingCarnivores = 1;
        private int startingMountains = 1;
        private int startingLakes = 1;
        private int timebetweensupdatesOfOneCreature = 20;
        private int rangeOfEffenctiveFoodWatering = 5;

         epoch currentEpoch = null;



     

        /// <summary>
        /// urzeczywistnia skutki następnej akcji 
        /// </summary>
        /// <param name="forwards">kierunek upływu czasu </param>
        public void showAct(bool forwards = true)
        {
            if (forwards)
            {
                currentAct_shown++;

                if (currentEpoch_shown == epochs.Count - 1 && currentAct_shown >= epochs[currentEpoch_shown].acts.Count-1)
                {
                    //return;
                    RunStep();
                    for (int i = 0; i < currentAct_shown; i++)
                    {
                        showAct(false);
                    }
                    //Console.WriteLine("epoch run ");
                    return;
                }

                if (currentAct_shown >= epochs[currentEpoch_shown].acts.Count)
                {
                    currentAct_shown = 0;
                    currentEpoch_shown++;
                }
                if (currentEpoch_shown == 0 && currentAct_shown == -1)//x
                {
                    currentAct_shown = 0;
                }


                board.MakeActHappen(epochs[currentEpoch_shown].acts[currentAct_shown], forwards);
                //Console.WriteLine("wywołane  epoka " + currentEpoch_shown + " act " + currentAct_shown);

            }
            else
            {
                if (currentAct_shown == 0 && currentEpoch_shown == 0)
                {
                }
                else if (currentEpoch_shown == 0 && currentAct_shown == -1)//x
                {
                    return;
                }
                else if (currentEpoch_shown < 0)
                {
                    currentEpoch_shown = 0; currentAct_shown = 0;
                    return;
                }
                if (currentAct_shown < 0)
                {
                    currentEpoch_shown--;
                    if (currentEpoch_shown < 0)
                    {
                        currentEpoch_shown = 0; currentAct_shown = -1;//x
                        return;
                    }
                    currentAct_shown = epochs[currentEpoch_shown].acts.Count - 1;
                }
                if (currentAct_shown >= epochs[currentEpoch_shown].acts.Count)
                {
                    currentAct_shown = epochs[currentEpoch_shown].acts.Count - 1;
                }



                board.MakeActHappen(epochs[currentEpoch_shown].acts[currentAct_shown], forwards);

                currentAct_shown--;
                //Console.WriteLine("wywołane  epoka " + currentEpoch_shown + " act " + currentAct_shown);



            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>zwraca mapę </returns>
        public ObjectOnMap[,] getOrganismsBoard()
        {
            return board.getAllOrganisms();
        }


        public Simulation(int size, int startingHerbivores, int startingCarnivores, int startingMountains, int startingLakes, int timeBetweenmovesofcreature)
        {
            board = new Board(size);
            //this.boardShown = new Board(size);
            this.startingCarnivores = startingCarnivores;
            this.startingHerbivores = startingHerbivores;
            this.startingMountains = startingMountains;
            this.startingLakes = startingLakes;
            this.timebetweensupdatesOfOneCreature = timeBetweenmovesofcreature;

            Initialize(false);

        }
        public Simulation(string[,] map, int timeBetweenmovesofcreature)
        {
            this.board = new Board(map);
            //this.boardShown = new Board(map);

            this.timebetweensupdatesOfOneCreature = timeBetweenmovesofcreature;
            Initialize(true);
        }
        /// <summary>
        /// ustawia wartości początkowe symulacji w zależności od wybranego trybu 
        /// </summary>
        /// <param name="special">tryb inicjalizacji </param>
        private void Initialize(bool special)
        {


            Carnivore.resizeMap(board.GetSize(), board.GetSize());
            Herbivore.resizeMap(board.GetSize(), board.GetSize());

            if (special) { InitializeSpecially(); }
            else { initializeRandomly(); }



            updateClassBoards();

        }

        /// <summary>
        /// ustawia wartości początkowe symulacji
        /// </summary>
        public void InitializeSpecially()
        {
            RunStep();
        }
        /// <summary>
        /// ustawia wartości początkowe symulacji
        /// </summary>
        public void InitializeSpecially(bool a = false)
        {
            RunStep();


        }
        /// <summary>
        /// inicjalizuje zgodnie z podanymi przez użytkownika wartościami 
        /// </summary>
        public void initializeRandomly()
        {
            currentEpoch = new epoch();

            int howmany_left = board.GetSize() * board.GetSize();
            int herbivo = 0;
            int carnivo = 0;
            int mountaino = 0;
            int lako = 0;
            int planto = 0;

            while (true)
            {
                bool shouldBreake = false;
                if (howmany_left - (herbivo + carnivo + mountaino + lako + planto) <= 0)
                {
                    shouldBreake = true;
                }
                if( herbivo < startingHerbivores)
                {
                    int a = helper.Next(board.GetSize());
                    int b = helper.Next(board.GetSize());
                    if (board.makeTAtCoords<Herbivore>(a, b, out Apeerance app))
                    {
                        currentEpoch.addAct(app);
                        board.MakeActHappen(app, true);
                        herbivo++;
                    }
                }
                
                if ( carnivo < startingCarnivores)
                {
                    int a = helper.Next(board.GetSize());
                    int b = helper.Next(board.GetSize());
                    if (board.makeTAtCoords<Carnivore>(a, b, out Apeerance app))
                    {
                        currentEpoch.addAct(app);
                        board.MakeActHappen(app, true);

                        carnivo++;
                    }
                }

                if ( mountaino < startingMountains)
                {
                    int a = helper.Next(board.GetSize());
                    int b = helper.Next(board.GetSize());
                    if (board.makeTAtCoords<Mountain>(a, b, out Apeerance app))
                    {
                        currentEpoch.addAct(app);
                        board.MakeActHappen(app, true);

                        mountaino++;
                    }
                }

                if ( lako < startingLakes)
                {
                    int a = helper.Next(board.GetSize());
                    int b = helper.Next(board.GetSize());
                    if (board.makeTAtCoords<Lake>(a, b, out Apeerance app))
                    {
                        currentEpoch.addAct(app);
                        board.MakeActHappen(app, true);

                        lako++;
                    }
                }        
                if(herbivo == startingHerbivores && carnivo == startingCarnivores && mountaino == startingMountains && lako == startingLakes)
                {
                    shouldBreake = true;
                }


                if(shouldBreake) { break; }
            }
            // Tworzenie roślin na planszy

            for (int i = 0; i < board.GetSize(); i++)
            {
                for (int j = 0; j < board.GetSize(); j++)
                {
                    if (helper.NextDouble() < 0.2 && board.IsEmpty(i, j)) // losowe tworzenie roślin z 20% szansą
                    {
                        if (board.makeTAtCoords<Plant>(i, j, out Apeerance app))
                        {
                            currentEpoch.addAct(app);
                            board.MakeActHappen(app, true);

                            planto++;
                        }
                    }
                }
            }
            epochs.Add(currentEpoch);
            //foreach (var item in currentEpoch.acts)
            //{
            //    board.MakeActHappen(item,true);
            //}
            currentEpoch_shown = epochs.Count - 1;
            currentAct_shown = currentEpoch.acts.Count - 1;
            RunStep();

        }
        /// <summary>
        /// zwraca informacje dodatkowe o symulacjii 
        /// </summary>
        public void PrintDebug()
        {
            string a = " ";

            a += "mięsożerców " + board.carnivores.Count + "\n"
                + "roślinożerców " + board.herbivores.Count + "\n"
                + "roślin " + board.plants.Count + "\n"
                + "iterecja " + epochs.Count() + "\n"
                + "wszystkich zdarzeń " + epochs.CountAllActs() + "\n"
                + "użycie randoma od ostatniego razu " + (helper.nextCount - helper.lastShown) + "\n"
                + "uzycie randoma " + helper.getNextCount() + "\n";

            a+=helper.avgStatsAsString(board.carnivores.Cast<Animal>().ToList(), "mięsożercy ") + "\n";

            a+=helper.avgStatsAsString(board.herbivores.Cast<Animal>().ToList(), "roślinożercy ") + "\n";

            Console.Write(a);

        }
        /// <summary>
        /// printuje mapę do konsoli 
        /// </summary>
        public void PrintBoard()
        {
            board.PrintBoard();
            PrintDebug();


        }
        /// <summary>
        /// sprawdza czy wszystko jest na swoim miejscu na planszy 
        /// </summary>
        /// <exception cref="Exception">błąd w umiejscowieniu na mapie </exception>
        public void checkerIfOkOnBoard()
        {
            List<ObjectOnMap> allObjects = new List<ObjectOnMap>();

            allObjects = board.returnAllObjectOnMap();
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

        /// <summary>
        /// symuluje jedną epokę symulacji 
        /// </summary>
        public void RunStep()
        {
            currentEpoch = new epoch();
            checkerIfOkOnBoard();
            spawningOfFood();
            updateClassBoards();
            movingOfCreatures();
            multiplicationOfCreatures();
            plantsAndCorpsesEpoch();
            board.MakeActHappen(new epochPass(),true);
            currentEpoch.addAct(new epochPass());
            epochs.Add(currentEpoch);
            currentEpoch_shown = epochs.Count - 1;
            currentAct_shown = currentEpoch.acts.Count - 1;
        }
        /// <summary>
        /// symuluje poruszanie się zwierząt 
        /// </summary>
        public void movingOfCreatures()
        {

            List<Animal> animals = board.returnAllAnimals();
            //animals.AddRange(board.carnivores);
            //animals.AddRange(board.herbivores);
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
                        board.MakeActHappen(a, true);
                        currentEpoch.addAct(a);


                        if (a.gotMoreMoves()&&  a is not DraxStanding)
                        {
                            a = ani.Move((ani is Carnivore) ? board.herbivores.Cast<Organism>().ToList() : board.plants.Cast<Organism>().ToList());
                        }
                        else
                        {
                            break;
                        }
                        //Thread.Sleep(timebetweensupdatesOfOneCreature);
                        //updateClassBoards();
                    }
                }
                updateClassBoards(); //po skończonym ruchu każdego zwierzęcia bedzie odświezone bo zwierze samo sobie nie przeszkadza 
            }

        }
        /// <summary>
        /// sprawia że na rośliny i ciała działa czas 
        /// </summary>
        public void plantsAndCorpsesEpoch()
        {
            List<Organism> org = new List<Organism>();
            org.AddRange(board.plants);
            org.AddRange(board.corpses);
            foreach (Organism a in org)
            {
                //a.epochPass(true);
                if (a is Corpse c)
                {
                    if (c.getNutritionalValue() <= 0)
                    {
                        selfDestruct d = new selfDestruct(c);
                        board.MakeActHappen(d, true);
                        currentEpoch.addAct(d);
                        //dla wszystkich wypadałoby zrobić restytucję akcji 
                    }
                }
            }
        }
        /// <summary>
        /// sprawia,że jedzenie pojawia się na planszy 
        /// </summary>
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
                            if (board.makeTAtCoords<Plant>(i, j, out Apeerance apper))
                            {
                                board.MakeActHappen(apper, true);
                                currentEpoch.addAct(apper);
                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// sprawia, że zwierzęta się rozmnażają 
        /// </summary>
        public void multiplicationOfCreatures()
        {
            List<Animal> animals = board.returnAllAnimals();

            animals.Shuffle();
            foreach (Animal item in animals)
            {
                if (item.CanReproduce())
                {
                    if (item is Herbivore)
                    {
                        stats c = item.Reproduce(out coords a);
                        board.makeTAtCoords<Herbivore>(a.x, a.y, out Apeerance app);

                        app.who.setStats(c);
                        app.who.SetBoard(board);
                        board.MakeActHappen(app, true);
                        currentEpoch.addAct(app);
                        continue;
                    }
                    if (item is Carnivore)
                    {
                        stats c = item.Reproduce(out coords a);
                        board.makeTAtCoords<Carnivore>(a.x, a.y, out Apeerance app);
                        app.who.SetBoard(board);
                        app.who.setStats(c);
                        board.MakeActHappen(app, true);
                        currentEpoch.addAct(app);
                        continue;
                    }

                }
            }
        }
        /// <summary>
        /// uaktualnia stan wiedzy o otoczeniu dla wszystkich klas zwierząt 
        /// </summary>
        public void updateClassBoards()
        {
            Herbivore.resetMap();
            Carnivore.resetMap();

            List<ObjectOnMap> allObjects = board.returnAllObjectOnMap();
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
                else if (item is Corpse)//nikt nie je trupów na razie 
                {
                    Carnivore.SetMap(item.GetX(), item.GetY(), false);
                    Herbivore.SetMap(item.GetX(), item.GetY(), false);
                }
                else if(item is Mountain || item is Lake)// nikt nie je gór i jezior 
                {
                    Carnivore.SetMap(item.GetX(), item.GetY(), false);
                    Herbivore.SetMap(item.GetX(), item.GetY(), false);

                }



            }
        }


    }
}
