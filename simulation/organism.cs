using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class Node
    {
        public int x;
        public int y;
        public Node parent;

        public int[] toarr()
        {
            return new int[] { x, y };
        }
        public coords toCoords()
        {
            return new coords(x, y);
        }
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public interface IDieable
    {
        public bool IsDead();
    }
    public  class Organism : ObjectOnMap,IDieable
    {
        public virtual double getNutritionalValue() { return 1; }

        protected bool isDead = false;
        protected   int nutritiousness = 0; // jest to maxymalna ilość głodu jaką dane zwierze zebrało przez całą symulację  lub roślina której wartość tego się zwiększa co epokę lub ciało gdzie zmienjsza sie co epokę 





        public Organism(int x, int y, Board b) : base(x, y, b) { }
        public Organism(coords c , Board b) : base(c, b) { }

        public Organism() : base() { }
        protected void DecreaseHunger()
        {
        }
        public bool IsDead()
        {
            return isDead;
        }
        public  void Die()
        {
            isDead = true;
        }
        public void MoveTo(int x, int y)
        {
            this.coords.x = x;
            this.coords.y = y;
        }
        public void MoveBy(int a , int b)
        {
            this.coords.x += a;
            this.coords.y += b;
        }
      
    }
    public interface Reproducable
    {
        stats Reproduce(out coords a ,bool asdelta);
        bool CanReproduce( );
    }


}
