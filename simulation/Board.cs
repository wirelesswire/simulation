using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class  statList<T>:List<T>
    {
        public int  allAddedEver = 0 ;
        public new  void  Add(T a )
        {
            base.Add(a);
            allAddedEver++; 
        }
        public new void AddRange(IEnumerable<T> a )
        {
            base.AddRange(a);
            allAddedEver += a.Count();
        }

    }

    public class Board 
    {
        public  statList<Plant> plants;
        public  statList<Herbivore> herbivores;
        public  statList<Carnivore> carnivores;
        public  statList<Mountain> mountains;
        public  statList<Lake> lakes;
        public  statList<Corpse> corpses;
        public int allObjectsEverCount = 0;


        private ObjectOnMap[,] organisms;
        private int size;
        public int[] getMaxEverUnitNumbers()
        {
            return new int[] {plants.Count,herbivores.Count,carnivores.Count,mountains.Count,lakes.Count,corpses.Count };
        }
        //private Board(
        //  statList<Plant> plants, statList<Herbivore> herbivores, statList<Carnivore> carnivores, statList<Mountain> mountains, statList<Lake> lakes, statList<Corpse> corpses,int allObjectsEverCount , ObjectOnMap[,] organisms,int size)
        //{
        //    this.plants = plants;
        //    this.herbivores = herbivores;
        //    this.carnivores = carnivores;
        //    this.mountains = mountains;
        //    this.lakes = lakes;
        //    this.corpses = corpses;
        //    this.organisms = organisms;
        //    this.size = size;
        //    this.allObjectsEverCount = allObjectsEverCount; 
        //}
        public Board(int size)
        {
            plants = new statList<Plant>();
            herbivores = new statList<Herbivore>();
            carnivores = new statList<Carnivore>();
            mountains = new statList<Mountain>();
            lakes = new statList<Lake>();
            corpses = new  statList<Corpse>();
            this.size = size;
            organisms = new ObjectOnMap[size, size];
        }
        public Board(string[,] objectLayout)
        {
            if (objectLayout.GetLength(0) != objectLayout.GetLength(1))
            {
                throw new Exception("nieprawidłowa mapa ");
            }
            plants = new statList<Plant>();
            herbivores = new statList<Herbivore>();
            carnivores = new statList<Carnivore>();
            mountains = new statList<Mountain>();
            lakes = new statList<Lake>();
            corpses = new statList<Corpse>();
            this.size = objectLayout.GetLength(0);
            organisms = new ObjectOnMap[this.size, this.size];

            for (int i = 0; i < objectLayout.GetLength(0); i++)
            {
                for (int j = 0; j < objectLayout.GetLength(1); j++)
                {
                    
                    switch (objectLayout[i,j])
                    {
                        case "M":
                            makeTAtCoords_notApperance<Mountain>(i, j);
                            break;
                        case "L":
                            makeTAtCoords_notApperance<Lake>(i, j);
                            break;
                        case "C":
                            makeTAtCoords_notApperance<Carnivore>(i, j);
                            break;
                        case "H":
                            makeTAtCoords_notApperance<Herbivore>(i, j);
                            break;
                        case "P":
                            makeTAtCoords_notApperance<Plant>(i, j);
                            break;
                        case "_":
                            //makeTAtCoords<Plant>(i, j);
                            break;
                        default:
                            throw new Exception("złe dane wejściowe ");
                            break;
                    }
                }
            }

        }


        public void AddObject(ObjectOnMap obj)
        {
            organisms[obj.GetX(), obj.GetY()] = obj;
            if (obj is Carnivore carnivore)
            {
                carnivores.Add(carnivore);//// xd że to tak działa xdddddd
            }
            if (obj is Herbivore herbivore)
            {
                herbivores.Add(herbivore);
            }
            if (obj is Plant plant)
            {
                plants.Add(plant);
            }
            if (obj is Mountain mountain)
            {
                mountains.Add(mountain);
            }
            if (obj is Lake lake)
            {
                lakes.Add(lake);
            }
            if (obj is Corpse corpse)
            {
                corpses.Add(corpse);
            }
            allObjectsEverCount++;  

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
        public int countOFTypeinRange<T>(int squareRange, coords a)
        {
            int xRangeMin = (a.x - squareRange >= 0 ? a.x - squareRange : 0);
            int xRangeMax = (a.x + squareRange < size ? a.x + squareRange : size);
            int yRangeMin = (a.y - squareRange >= 0 ? a.y - squareRange : 0);
            int yRangeMax = (a.y + squareRange < size ? a.y + squareRange : size);
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



        public void MakeActHappen(epochPass e ,bool forward)
        {
            foreach (var item in returnAllOrganisms())
            {
                item.epochPass(forward);
            }
        }

        public void MakeActHappen(Move a,bool forward = true)
        {
            if (forward)
            {
                if (!(GetObjectOnMap(a.to) == null))
                {
                    throw new Exception("pole na które stajesz musi  być puste ");
                }
                if (!( GetObjectOnMap(a.from) == a.who ))
                {
                    throw new Exception("no musi stać tu skąd się porusza ");
                }
                MoveOrganism(a,forward);

                if (!(GetObjectOnMap(a.to) == a.who))
                {
                    throw new Exception(" musi go przenieść ");
                }
                if  (  !(GetObjectOnMap(a.from) == null))
                {
                    throw new Exception("tam skąd przyszedł powinien być   null ");
                }

                //return;// brak symetrii z movecost bo już został pobrany 
            }
            else
            {
                if (!(GetObjectOnMap(a.to) == a.who))
                {
                    throw new Exception("tam gdzie poszedł musi być  ");
                }
                if (!(GetObjectOnMap(a.from) == null))
                {
                    throw new Exception("tam skąd przyszedł musi być puste  ");
                }
                MoveOrganism(a,forward);


                if (!(GetObjectOnMap(a.to) == null))
                {
                    throw new Exception("musi go przenieść");
                }
                if (!(GetObjectOnMap(a.from) == a.who))
                {
                    throw new Exception("no musi stać tu skąd się porusza ");
                }



            }
            a.who.MoveCost(forward);

        }
        public void MakeActHappen(Die a, bool forwards)
        {
            if (forwards)
            {
                if (!(GetObjectOnMap(a.from) == a.who))
                {
                    throw new Exception("no musi stać tu dokąd umiera ");
                }

                removeAt(a.from);

                AddObject(a.affected);

                if (GetObjectOnMap(a.from) is not Corpse)
                {
                    throw new Exception("musi się zamienić w ciało  ");
                }
            }
            else
            {
                removeAt(a.affected.coords);
                AddObject(a.who);


            }
            
        }

        public void MakeActHappen(Eat a, bool forwards)
        {
            if (forwards)
            {
                //zjedz 
                Organism org = (Organism)GetObjectOnMap(a.to);
                a.who.Eat(org,forwards);
                removeAt(a.to);

                // porusz sie 
                MoveOrganism(a,forwards);
            }
            else
            {
                MoveOrganism(a, forwards);
                AddObject(a.dead);


                a.who.Eat(a.dead,forwards);



            }
            a.who.MoveCost(forwards);



        }

        public void MakeActHappen(selfDestruct a , bool forwards)
        {
            if (forwards)
            {
                if (a.affected == null)
                {
                    throw new Exception();
                }
                if (GetObjectOnMap(a.from) == null)
                {
                    throw new Exception();
                }
                removeAt(a.to);
            }
            else
            {
                if (a.affected == null)
                {
                    throw new Exception();
                }
                if (GetObjectOnMap(a.from) == null)
                {
                    throw new Exception();
                }
                AddObject(a.affected);  
            }
           
        }
        public void MakeActHappen(DraxStanding a, bool forwards)
        {
            if (forwards)
            {

            }
            else
            {

            }
            // so incredibly still 
        }

        public void MakeActHappen(Apeerance a ,bool forwards )
        {
            if (forwards)
            {
                if (!(GetObjectOnMap(a.what.coords) == null))
                {
                    throw new Exception();
                }
                AddObject(a.what);
                if (!(GetObjectOnMap(a.what.coords) != null))
                {
                    throw new Exception();
                }


            }
            else
            {
                if (!(GetObjectOnMap(a.what.coords) != null))
                {
                    throw new Exception();
                }
                removeAt(a.what.coords);

                if (!(GetObjectOnMap(a.what.coords) == null))
                {
                    throw new Exception();
                }
            }
        }




        public void MakeActHappen(Act a,bool forwards  )
        {


            if (a is Move b )
            {
                MakeActHappen((Move)b,forwards);
            }
            else if (a is DraxStanding d )
            {
                MakeActHappen(d,forwards);
            }
            else if(a is Eat e)
            {
                MakeActHappen(e,forwards);
            }
            else if (a is selfDestruct f)
            {
                MakeActHappen(f, forwards);
            }
            else if (a is Die g)
            {
                MakeActHappen(g,forwards);
            }
            else if(a is epochPass h)
            {
                MakeActHappen(h,forwards);
            }
        }
        public bool makeTAtCoords<T>(int x, int y , out Apeerance z) where T : ObjectOnMap, new()
        {
            z = null;
            if (IsEmpty(x, y))
            {
                T a = new T();
                a.SetXY(x, y);
                a.SetBoard(this);
                z = new Apeerance(a);
                return true;
            }
            return false;
        }
        public T makeTAtCoords_notApperance<T>(int x, int y) where T : ObjectOnMap, new()
        {
            if (IsEmpty(x, y))
            {
                T a = new T();
                a.SetXY(x, y);
                a.SetBoard(this);
                this.AddObject(a);
                return a;
            }
            return null;
        }
        public List<Animal> returnAllAnimals()
        {
            List<Animal> animals = new List<Animal>();
            animals.AddRange(carnivores);
            animals.AddRange(herbivores);
            //animals.Shuffle();
            return animals;
        }
        public List<Organism> returnAllOrganisms()
        {
            List<Organism> organisms = new List<Organism>();
            organisms.AddRange(carnivores);
            organisms.AddRange(herbivores);
            organisms.AddRange(plants);
            organisms.AddRange(corpses);
            return organisms;
        }
        public List<ObjectOnMap> returnAllObjectOnMap()
        {
            List<ObjectOnMap> objects = new List<ObjectOnMap>();
            objects.AddRange(carnivores);
            objects.AddRange(herbivores);
            objects.AddRange(plants);
            objects.AddRange(corpses);
            objects.AddRange(mountains);
            objects.AddRange(lakes);
            return objects;
        }
        public List<T> returnAllT<T>() where T : ObjectOnMap
        {


            List<T> ret  = new List<T>();
            if(plants is List<T> a )
            {
                ret.AddRange(a);
            }
            if (lakes is List<T> b)
            {
                ret.AddRange(b);
            }
            if (carnivores is List<T> c)
            {
                ret.AddRange(c);
            }
            if (herbivores is List<T> d)
            {
                ret.AddRange(d);
            }
            if (mountains is List<T> e)
            {
                ret.AddRange(e);
            }
            if (corpses is List<T> f)
            {
                ret.AddRange(f);
            }
            return ret;
        }

        public void removeAt(coords a)
        {
            ObjectOnMap obj  =  organisms[a.x, a.y] ;
            if(obj == null)
            {
                throw new Exception("tu już nie powinno być takich błędów ");
            }
            if (obj is Carnivore carnivore)
            {
                carnivores.Remove(carnivore);//// xd że to tak działa xdddddd
            }
            if (obj is Herbivore herbivore)
            {
                herbivores.Remove(herbivore);
            }
            if (obj is Plant plant)
            {
                plants.Remove(plant);
            }
            if (obj is Mountain mountain)
            {
                mountains.Remove(mountain);
            }
            if (obj is Lake lake)
            {
                lakes.Remove(lake);
            }
            if(obj is Corpse corpse)
            {
                corpses.Remove(corpse); 
            }

            organisms[a.x, a.y] = null;
        }


        public ObjectOnMap GetObjectOnMap(int x, int y)
        {
            return GetObjectOnMap(new coords(x,y));
        }
        public ObjectOnMap GetObjectOnMap(int[]a  )
        {
            return GetObjectOnMap( new coords(a[0],a[1]));
        }
        public ObjectOnMap GetObjectOnMap(coords a)
        {
            return organisms[a.x, a.y];
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
        public void MoveOrganism(Act a,bool forwards  )
        {
            if (forwards)
            {
                MoveOrganism(a.who, a.to.x, a.to.y);

            }
            else
            {
                MoveOrganism(a.who,a.from.x,a.from.y);
            }
        }
        public bool IsEmpty(int x, int y)
        {
            return organisms[x, y] == null;
        }
        public bool IsEmpty(int[] a)
        {
            return organisms[a[0], a[1]] == null;
        }
        public bool IsEmpty(coords a )
        {
            return organisms[a.x, a.y] == null;
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

       // public object Clone()
       // {
       //     return new Board(
       //  plants,
       //  herbivores,
       //  carnivores,
       //  mountains,
       //  lakes,
       //  corpses,
       // allObjectsEverCount = 0,
       //organisms,
       // size
       //     );
       // }
    }

}
