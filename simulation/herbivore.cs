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
        //public int sight = 10;
        public Herbivore(int x, int y) : base(x, y) { 
            chanceTOMultiply = 0.3f;
            sight = 10;
        }
        public Herbivore() : base() { 
            chanceTOMultiply = 0.3f;
            sight = 10;
        }




        public override void Eat(Organism o)
        {
            if (o == null)
            {
                throw new Exception("no nie może być null ");
            }

            // logika jedzenia mięsożercy
        }



        private void getPathToNearestFood(int endX, int endY)
        {
            pathToFood = FindPath(map, x, y, endX, endY);
            if (pathToFood != null)
            {
                if (pathToFood[0].x == GetX() && pathToFood[0].y == GetY())
                {
                    pathToFood.RemoveAt(0);
                }
            }
        }

        public override Act MoveSpecific(List<Organism> plants, int size, Board b)
        {


            Organism nearestFood = GetNearestFood(plants);
            if (possibleStepableCells(b/*tu delta nie ma znaczenia */) == null)
            {
                return new Act(0, 0, Act.actionTaken.nothing, 0);
            }
            else if (nearestFood == null)
            {
                // if there is no food nearby, move randomly
                Act a = moveRandomly(size, b);
                a.setAction(Act.actionTaken.move);
                return a;
                //return;
            }
            else
            {

                getPathToNearestFood(nearestFood.GetX(), nearestFood.GetY());
                if (pathToFood == null)
                {
                    return moveRandomly(size, b);
                }
                if (pathToFood.Count == 0)
                {
                    throw new Exception("niemożliwość");
                }
                if (pathToFood.Count == 1)
                {

                    int[] poleDocelowe = new int[] { pathToFood[0].x - GetX(), pathToFood[0].y - GetY() };

                    return new Act(poleDocelowe[0], poleDocelowe[1], Act.actionTaken.eat, actionsLeft);
                }

                if (b.IsEmpty(pathToFood[0].toarr()))
                {
                    Node tmp = pathToFood[0];
                    pathToFood.RemoveAt(0);
                    int[] poleDocelowe = new int[] { tmp.x - GetX(), tmp.y - GetY() };
                    return new Act(poleDocelowe[0], poleDocelowe[1], Act.actionTaken.move, actionsLeft);
                }
                else
                {
                    throw new Exception("nimożność");
                    // UPDATE CLASS BOARDS tutaj jeszcze dopisać
                    return MoveSpecific(plants, size, b);
                }



            }


        }
        public override string toString()
        {
            return "H" + base.toString();
        }


    }
}
