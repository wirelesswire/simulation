using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Herbivore : Animal, Reproducable
    {

        static bool[,] map = new bool[10, 10];

        public static void resizeMap(int x, int y)
        {
            map = new bool[x, y];
        }
        public static void SetMap(int x, int y, bool c)
        {
            map[x, y] = c;
        }
        public static void resetMap()
        {
            for (int i = 0; i <= map.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= map.GetUpperBound(1); j++)
                {
                    SetMap(i, j, true);
                }
            }
        }
        public Herbivore(int x, int y,Board b ,stats s ) : base(x, y,  b,s) { 
            chanceTOMultiply = 0.01f;
            sight = 10;
            stats.chanceTOMultiply = chanceTOMultiply;
            stats.sight = sight;
        }
        public Herbivore() : base() {
            hungerperaction = 2;
            chanceTOMultiply = 0.01f;
            sight = 10;
            stats.chanceTOMultiply = chanceTOMultiply;
            stats.sight = sight;
        }
        public Herbivore(coords c, Board b,stats s) : base(c, b,s) {
            chanceTOMultiply = 0.01f;
            sight = 10;
            stats.chanceTOMultiply = chanceTOMultiply;
            stats.sight = sight;
        }
        public override bool doIEatIt(Organism o)
        {
            return o is Plant;
        }
        public override bool doIEatIt(ObjectOnMap o)
        {
            return o is Plant;

        }



        public override void Eat(Organism o, bool forwards)
        {
            base.Eat(o, forwards);
            if (o == null)
            {
                throw new Exception("no nie może być null ");
            }
            o.Die(forwards);
        }



        private void getPathToNearestFood(Organism endCoordsOrganism)
        {

            pathToFood = FindPath(map, coords, endCoordsOrganism.coords);
            if (pathToFood != null)
            {
                if (pathToFood[0].x == GetX() && pathToFood[0].y == GetY())
                {
                    pathToFood.RemoveAt(0);
                }
            }
        }

        public override Act MoveSpecific(List<Organism> plants)
        {
            Organism nearestFood = GetNearestFood(plants);
            if (nearestFood == null)// jeżeli nie ma jedzenia wokół-> poruszanie losowe 
            {
                if (emptyCellsAroundRectangle(new coords(1, 1)) == null)// jeżeli jesteś zablokowany i żadnego z blokujących nie możesz zjeść 
                {
                    return new DraxStanding(this, new coords(0, 0));
                }
                return moveRandomly();
            }
            else
            {
                getPathToNearestFood(nearestFood);
                if (pathToFood == null)
                {
                    if (emptyCellsAroundRectangle(new coords(1, 1)) == null)// jeżeli jesteś zablokowany i żadnego z blokujących nie możesz zjeść 
                    {
                        return new DraxStanding(this, new coords(0, 0));
                    }
                    return moveRandomly();
                }
                if (pathToFood.Count == 0)
                {
                    throw new Exception("niemożliwość");
                }
                if (pathToFood.Count == 1)
                {

                    coords poleDocelowe = new coords(pathToFood[0]);
                    return new Eat(this,board.GetObjectOnMap(poleDocelowe), coords,poleDocelowe);
                }

                if (board.IsEmpty(pathToFood[0].toCoords()))
                {
                    return new Move(this,coords, pathToFood[0].toCoords());
                }
                else
                {
                    throw new Exception("nimożność");
                }
            }
        }
        public override string toString()
        {
            return "H" + base.toString();
        }


    }
}
