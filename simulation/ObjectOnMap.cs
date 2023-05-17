using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class ObjectOnMap
    {
        protected int x;
        protected int y;
        
        public ObjectOnMap(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public ObjectOnMap()
        {
            
        }    
        public void SetX(int x )
        {
            this.x = x;

        }
        public void SetY(int y )
        {
            this.y= y;

        }
        public void SetXY(int x , int y )
        {
            this.x = x;
            this.y = y;
        }
        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
        public virtual string toString()
        {
            return " ";
        }

    }


  

}
