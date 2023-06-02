using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Simulation
    {

        List<epoch> epochs = new List<epoch>();
        //int currentEpochSimulated = 0;
        int currentEpoch_shown = 0;
        int currentAct_shown = 0;
        private Board board;
        private int startingHerbivores = 1;
        private int startingCarnivores = 1;
        private int startingMountains = 1;
        private int startingLakes = 1;
        int timebetweensupdatesOfOneCreature = 20;
        int rangeOfEffenctiveFoodWatering = 5;

        public epoch currentEpoch = null;



        public void showEpoch(bool forwards = true)
        {
            if (currentEpoch_shown >= epochs.Count)
            {
                return;
            }
            for (int i = currentAct_shown; i < epochs[currentEpoch_shown].acts.Count; i++)
            {
                board.MakeActHappen(epochs[currentEpoch_shown].acts[i], forwards);
            }




            if (forwards)
            {
                currentEpoch_shown++;
                currentAct_shown++;

            }
            else
            {
                try
                {
                    currentAct_shown = epochs[currentEpoch_shown - 1].acts.Count - 1;

                }
                catch
                {
                    currentAct_shown = 0;
                }
                currentEpoch_shown--;
            }
            board.MakeActHappen(epochs[currentAct_shown].acts[currentAct_shown], forwards);

        }
        public void showAct(bool forwards = true)
        {
            if (forwards)
            {
                if (currentEpoch_shown >= epochs.Count - 1)
                {
                    //return;
                    RunStep();
                }

                if (currentAct_shown >= epochs[currentEpoch_shown].acts.Count)
                {
                    currentAct_shown = 0;
                    currentEpoch_shown++;
                }
                if (currentEpoch_shown == 0 && currentAct_shown == -1)
                {
                    currentAct_shown = 0;
                }

                Console.WriteLine("wywołane  epoka " + currentEpoch_shown + " act " + currentAct_shown);

                board.MakeActHappen(epochs[currentEpoch_shown].acts[currentAct_shown], forwards);
                currentAct_shown++;
            }
            else
            {
                if (currentAct_shown == 0 && currentEpoch_shown == 0)
                {
                }
                else if (currentEpoch_shown == 0 && currentAct_shown == -1)
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
                        currentEpoch_shown = 0; currentAct_shown = -1;
                        return;
                    }
                    currentAct_shown = epochs[currentEpoch_shown].acts.Count - 1;
                }
                if (currentAct_shown >= epochs[currentEpoch_shown].acts.Count)
                {
                    currentAct_shown = epochs[currentEpoch_shown].acts.Count - 1;
                }


                Console.WriteLine("wywołane  epoka " + currentEpoch_shown + " act " + currentAct_shown);

                board.MakeActHappen(epochs[currentEpoch_shown].acts[currentAct_shown], forwards);



                currentAct_shown--;


            }

        }


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
            Initialize(false);
        }

        private void Initialize(bool special)
        {


            Carnivore.resizeMap(board.GetSize(), board.GetSize());
            Herbivore.resizeMap(board.GetSize(), board.GetSize());

            if (special) { InitializeSpecially(); }
            else { initializeRandomly(); }
            //boardShown =(Board) board.Clone();
            //initializeRandomly();
            //InitializeSpecially();


            updateClassBoards();

        }


        public void InitializeSpecially()
        {

            //board.makeTAtCoords<Herbivore>(5, 8);
        }
        public void InitializeSpecially(bool a = false)
        {
            //for (int i = 0; i < board.GetSize(); i++)
            //{
            //    //makeTAtCoords<Herbivore>(0, i);
            //    for (int j = 0; j < board.GetSize(); j++)
            //    {
            //        board.makeTAtCoords<Herbivore>(i, j);
            //    }
            //}
            //board.removeAt(new coords(10, 10));
            //board.makeTAtCoords<Carnivore>(10, 10);

        }

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

        }

        public void PrintDebug()
        {
            Console.WriteLine("mięsożerców " + board.carnivores.Count);
            Console.WriteLine("roślinożerców " + board.herbivores.Count);
            Console.WriteLine("roślin " + board.plants.Count);
            Console.WriteLine("iterecja " +epochs.Count());
            Console.WriteLine("wszystkich zdarzeń " + epochs.CountAll());
            Console.WriteLine("użycie randoma od ostatniego razy " + (helper.nextCount - helper.lastShown));

            Console.WriteLine("uzycie randoma " + helper.getNextCount());

            Console.Write(helper.avgStatsAsString(board.carnivores.Cast<Animal>().ToList(), "mięsożercy ") + "\n");

            Console.Write(helper.avgStatsAsString(board.herbivores.Cast<Animal>().ToList(), "roślinożercy ") + "\n");
        }
        public void PrintBoard()
        {
            board.PrintBoard();
            //boardShown.PrintBoard();
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
                            if (board.makeTAtCoords<Plant>(i, j, out Apeerance a))
                            {
                                board.MakeActHappen(a, true);
                                currentEpoch.addAct(a);
                            }

                        }
                    }
                }
            }
        }

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

        public void updateClassBoards()
        {
            Herbivore.resetMap();
            Carnivore.resetMap();
            //List<ObjectOnMap> allObjects = new List<ObjectOnMap>();
            //allObjects.AddRange(board.carnivores);
            //allObjects.AddRange(board.herbivores);
            //allObjects.AddRange(board.plants);
            //allObjects.AddRange(board.mountains);
            //allObjects.AddRange(board.lakes);
            //allObjects.AddRange(board.corpses);
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
