using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa reprezentująca roślinożerce -> zwierzę które je tylko rośliny 
    /// </summary>
    public class Herbivore : Animal, Reproducable
    {

        static bool[,] map = new bool[10, 10];
        /// <summary>
        /// zmienia rozmiar mapy pól na których jest w statnie stanąć
        /// </summary>
        /// <param name="x">rozmiar x </param>
        /// <param name="y">rozmiar y </param>
        public static void resizeMap(int x, int y)
        {
            map = new bool[x, y];
        }
        /// <summary>
        /// ustawia konkretne pole na mapie na możliwe lub niemożliwe do przejścia 
        /// </summary>
        /// <param name="x">pozycja x </param>
        /// <param name="y">pozycja y </param>
        /// <param name="c">wartość</param>
        public static void SetMap(int x, int y, bool c)
        {
            map[x, y] = c;
        }
        /// <summary>
        /// ustawia wszystkie pola na mapie na możliwe do przejścia 
        /// </summary>
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
            stats.chanceTOMultiply = 0.01f;
            this.stats.sight = 10;
            //stats.chanceTOMultiply = chanceTOMultiply;
            //stats.sight = sight;
        }
        public Herbivore() : base() {
            stats.hungerperaction = 2;
            stats.chanceTOMultiply = 0.01f;
            this.stats.sight = 10;
            //stats.chanceTOMultiply = chanceTOMultiply;
            //stats.sight = sight;
        }
        public Herbivore(coords c, Board b,stats s) : base(c, b,s) {
            stats.chanceTOMultiply = 0.01f;
            this.stats.sight = 10;
            //stats.chanceTOMultiply = chanceTOMultiply;
            //stats.sight = sight;
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


        /// <summary>
        /// sprawdza drogę do najbliższego jedzenia i uzdatnia ją 
        /// </summary>
        /// <param name="endCoordsOrganism">najbliższe jedzenie </param>
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
        /// <summary>
        /// logika poruszania się miesożercy 
        /// </summary>
        /// <param name="herbivores">roślinożercy </param>
        /// <returns>akcję mięsożercy </returns>
        /// <exception cref="Exception">błąd w sprawdzaniu </exception>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns>reprezectację roślinożercy jako string </returns>
        public virtual string presentation()
        {
            return base.presentation() + " roślinożerca";
        }
        public override string toString()
        {
            return "H" + base.toString();
        }


    }
}
