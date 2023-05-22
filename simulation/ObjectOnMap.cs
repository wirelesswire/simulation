using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{


    public abstract class ObjectOnMap
    {
        public coords coords;
        protected Board board;

        //public static List<ObjectOnMap> objectInstances;
        public abstract void epochPass();
        public ObjectOnMap(int x, int y,Board b )
        {

            this.coords = new coords(x,y);
            this.board  = b;
            //addToInstances();
        }
        public ObjectOnMap(coords a , Board b)
        {

            this.coords = a;
            this.board = b;
            //addToInstances();
        }

        public ObjectOnMap()
        {
            //addToInstances();

        }
        //public abstract void addToInstances();
        public void SetX(int x )
        {
            coords.x = x;

        }
        public void SetY(int y )
        {
            coords.y = y;

        }
        public void SetBoard(Board b )
        {
            board = b;
        }
        public Board getBoard()
        {
            return board;
        }
        public void SetXY(int x , int y )
        {
            if (coords == null) { // setT
                coords = new coords(x, y);
                return; 
            }

            coords.x = x;
            coords.y=y;
        }
        public int GetX()
        {
            return coords.x;
        }

        public int GetY()
        {
            return coords.y;
        }
        public virtual string toString()
        {
            return " ";
        }

    }


  

}
