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

        protected bool isDead = false;


        //public override void addToInstances()
        //{
        //    if (Organism.objectInstances is null)
        //    {
        //        Organism.objectInstances = new List<ObjectOnMap>();
        //    }
        //    Organism.objectInstances.Add(this);
        //}


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
        Organism Reproduce(Organism parent,bool asdelta);
        bool CanReproduce( );
    }


}
