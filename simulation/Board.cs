using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    class HelperDictionry<T> where T : ObjectOnMap
    {
        public  IDictionary<T, List<T>> _dataOfType;

        public HelperDictionry(IDictionary<T, List<T>> dataOfType)
        {
            this._dataOfType = dataOfType;
            
        }
        public void Add(T t , T wartosc)
        {
            if (!_dataOfType.ContainsKey(t))
            {
                _dataOfType.Add(t,new List<T>());
            }
            if(_dataOfType.TryGetValue(t, out List<T> lista)){
                lista.Add(wartosc);
            }
        }
        public void initializeWithType(T t)
        {
            _dataOfType = null;
            _dataOfType = new Dictionary<T, List<T>>();
            
        }


    }

    public class Board
    {
        public  List<Plant> plants;
        public  List<Herbivore> herbivores;
        public  List<Carnivore> carnivores;
        public  List<Mountain> mountains;
        public  List<Lake> lakes;


        private ObjectOnMap[,] organisms;
        private int size;
        private HelperDictionry<ObjectOnMap> a = new HelperDictionry<ObjectOnMap>(new Dictionary<ObjectOnMap,List<ObjectOnMap>>()  );


        public Board(int size)
        {
            plants = new List<Plant>();
            herbivores = new List<Herbivore>();
            carnivores = new List<Carnivore>();
            mountains = new List<Mountain>();
            lakes = new List<Lake>();
            this.size = size;
            organisms = new ObjectOnMap[size, size];
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

        public void makeActHappen(Act a)
        {
            if (a.moves())
            {
                if(! (GetObjectOnMap(a.to) == null) )
                {
                    throw new Exception("pole na które stajesz musi być puste ");
                }
                if(! (GetObjectOnMap(a.from) == a.who) ) {
                    throw new Exception("no musi stać tu skąd się porusza ");
                }
                MoveOrganism(a);

                if (! (GetObjectOnMap(a.to) == a.who))
                {
                    throw new Exception(" musi go przenieść ");
                }
                if (GetObjectOnMap(a.from) == null && a.GetAction() !=  Act.actionTaken.nothing ){
                    throw new Exception("tam skąd przyszedł powinien być   null ");
                }
            }
            else if (a.eats())
            {
                //zjedz 
                Organism org = (Organism)GetObjectOnMap(a.to);
                a.who.Eat(org);
                removeAt(a.to);

                // porusz sie 
                MoveOrganism(a);
            }
        }

        public void removeAt(coords a)
        {
            Object obj  =  organisms[a.x, a.y] ;
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
        public void MoveOrganism(Act a )
        {
            MoveOrganism(a.who,a.to.x,a.to.y);
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
    }

}
