using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Carnivore : Animal
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
       
        public Carnivore(int x, int y, Board b,stats s ) : base(x, y, b,s)
        {
            


            chanceTOMultiply = 0.01f;
            sight = 5;
            stats.chanceTOMultiply = chanceTOMultiply;
            stats.sight = sight;

        }
        public Carnivore() : base()
        {
            chanceTOMultiply = 0.01f;
            sight = 2;
            stats.chanceTOMultiply = chanceTOMultiply;
            stats.sight = sight;
        }
        public Carnivore(coords c, Board b,stats s ) : base(c, b, s) {
            
            chanceTOMultiply = 0.01f;
            sight = 2;
            stats.chanceTOMultiply = chanceTOMultiply;
            stats.sight = sight;
        }



        public override   bool doIEatIt(Organism o)
        {
            return o is Herbivore;
        }
        public override bool doIEatIt(ObjectOnMap o)
        {
            return o is Herbivore;

        }


        public override void Eat(Organism o)
        {
            base.Eat(o);
            if (o == null)
            {
                throw new Exception("no nie może być null ");
            }
            o.Die();
        }

        private void getPathToNearestFood(Organism endCoordsOrganism)
        {
            pathToFood = FindPath(map, coords , endCoordsOrganism.coords);
            if (pathToFood != null)
            {
                if (pathToFood[0].x == GetX() && pathToFood[0].y == GetY())
                {
                    pathToFood.RemoveAt(0);
                }
            }
        }

        public override Act MoveSpecific(List<Organism> herbivores)
        {


            Organism nearestFood = GetNearestFood(herbivores);//patrzy czy w okolicy jest jedzenie i wybiera najbliższe


            
            if (nearestFood == null)// jeżeli nie ma jedzenia wokół-> poruszanie losowe 
            {
                if (emptyCellsAroundRectangle(new coords(1, 1)) == null)// jeżeli jesteś zablokowany i żadnego z blokujących nie możesz zjeść 
                {
                    return new Act(this, new coords(0, 0), 0, Act.actionTaken.nothing);
                }
                return moveRandomly();
            }            
            else//jeżeli jest 
            {

                getPathToNearestFood(nearestFood);// szukaj drogi do niego 

                if (pathToFood == null)// jeżeli nie istnieje -> poruszanie losowe 
                {
                    if (emptyCellsAroundRectangle(new coords(1, 1)) == null)// jeżeli jesteś zablokowany i żadnego z blokujących nie możesz zjeść 
                    {
                        return new Act(this, new coords(0, 0), 0, Act.actionTaken.nothing);
                    }
                    return moveRandomly();
                }
                if (pathToFood.Count == 0)//musiałbyś stać na jedzeniu (niemożliwe)
                {
                    throw new Exception("niemożliwość");
                }
                if (pathToFood.Count == 1)//jesteś krok od jedzenia i wystarczy zjeść 
                {                    
                    coords poleDocelowe = new coords (  pathToFood[0]);

                    return new Act(this,board.GetObjectOnMap(poleDocelowe),coords, poleDocelowe, Act.actionTaken.eat, actionsLeft);
                }

                if (board.IsEmpty(pathToFood[0].toCoords()))//jeżeli na pewno możesz stanąć na polu kierującym cie w stronę najbliższego jedzenia -> stań tam 
                {
                    Node tmp = pathToFood[0];
                    coords  poleDocelowe = new coords(tmp);
                    return new Act(this, coords, poleDocelowe, Act.actionTaken.move, actionsLeft);
                }
                else
                {
                    throw new Exception("nimożność");
                }



            }


        }

        public override string toString()
        {
            return "C" + base.toString();
        }



    }
   
}
