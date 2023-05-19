using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class coords
    {
        public int x;
        public int y;
        public coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public coords(Node n)
        {
            this.x = n.x;
            this.y += n.y;
        }
        public coords asDelta(int x, int y, bool forwards = true)
        {
            if (forwards)
            {
                return new coords(this.x - x, this.y - y);
            }
            return new coords(x - this.x, y - this.y);
        }
        public coords asDelta(coords x, bool forwards = true)
        {
            if (forwards)
            {
                return new coords(this.x - x.x, this.y - x.y);
            }
            return new coords(x.x - this.x, x.y - this.y);

        }
        public coords asDelta(int[] x, bool forwards = true)
        {
            if (forwards)
            {
                return new coords(this.x - x[0], this.y - x[1]);
            }
            return new coords(x[0] - this.x, x[1] - this.y);

        }
        public coords add(coords c)
        {
            return new coords(this.x+c.x,this.y + c.y);
        }
        public bool XandYequal(coords c )
        {
            if(x == c.x && y== c.y)
            {
                return true;
            }
            return false;
        }
        public bool isInBounds (int boundX, int boundY)
        {
            if (x >= 0 && x  < boundX && y  >= 0 && y  < boundY)
            {
                return true;
            }
            return false;


        }
        public bool isInBounds(int boundX)
        {
            if (x >= 0 && x < boundX && y >= 0 && y < boundX)
            {
                return true;
            }
            return false;


        }
        public bool isInBounds(coords c, int boundX , int boundY)
        {
            return this.add(c).isInBounds(boundX, boundY) ;

           
        }
        public bool absXeqY()
        {
            return Math.Abs(this.x) == Math.Abs(this.y);
        }
        public int  absDistanceOnBoard(coords c )
        {
            return  Math.Abs(x - c.x) + Math.Abs(y - c.y);
        }
        public double  absDistance(coords c)
        {
            return Math.Sqrt(((x - c.x) * (x - c.x)) + ((y - c.y) * (y - c.y)));
        }

    }
}
