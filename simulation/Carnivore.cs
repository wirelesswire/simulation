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
        public Carnivore(int x, int y) : base(x, y)
        {
            chanceTOMultiply = 0.1f;
            sight = 5;
        }
        public Carnivore() : base()
        {
            chanceTOMultiply = 0.1f;
            sight = 5;
        }

        public override void Eat(Organism o)
        {
            if (o == null)
            {
                throw new Exception("no nie może być null ");
            }
            o.wasEaten = true;
        }

        private void getPathToNearestFood(int endX, int endY, Board b)
        {
            //if(possibleStepableCells(b).Count == 0) // tutaj delta nie ma znaczenia 
            //{
            //    pathToFood =  null;
            //    return;
            //}
            pathToFood = FindPath(map, x, y, endX, endY);
            if (pathToFood != null)
            {
                if (pathToFood[0].x == GetX() && pathToFood[0].y == GetY())
                {
                    pathToFood.RemoveAt(0);
                }
            }
        }

        public override Act MoveSpecific(List<Organism> organisms, int size, Board b)
        {


            Organism nearestFood = GetNearestFood(organisms);


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

                getPathToNearestFood(nearestFood.GetX(), nearestFood.GetY(), b);

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

                    return MoveSpecific(organisms, size, b);
                }



            }


        }

        public override string toString()
        {
            return "C" + base.toString();
        }



    }
    public class Act
    {
        int xFrom;
        int yFrom;
        int xTo;   
        int yTo;


        int xcoorddelta;
        int ycoorddelta;
        actionTaken acted;
        bool moreActions;
        public int getdX()
        {
            return xcoorddelta;

        }
        public int getdY()
        {
            return ycoorddelta;
        }
        public void setAction(actionTaken action)
        {
            this.acted = action;
        }

        public Act(int dx, int dy, actionTaken act, int actionsLeft)
        {
            xcoorddelta = dx;
            ycoorddelta = dy;
            acted = act;
            moreActions = actionsLeft > 0;
        }
        public Act(int dx, int dy, int actionsLeft)
        {
            xcoorddelta = dx;
            ycoorddelta = dy;
            moreActions = actionsLeft > 0;
            acted = actionTaken.move;
        }
        public bool gotMoreMoves()
        {
            return moreActions;
        }
        public enum actionTaken
        {
            move,// przesuwa sie na podane pole 
            eat,// przesuwa sie na podane pole i zjada to co sie na nim znajduje 
            nothing // jest przyblokowany albo z innego powodu sie nie porusza 

        }
        public bool eats()
        {
            return acted == actionTaken.eat ? true : false;
        }
        public bool moves()
        {
            return acted == actionTaken.move ? true : false;
        }

    }
}
