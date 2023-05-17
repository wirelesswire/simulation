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
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }


    public  class Organism : ObjectOnMap
    {
        //protected int x;
        //protected int y;
        //protected int hunger;
        protected bool isDead = false;
        public bool wasEaten = false;

        public Organism(int x, int y) : base(x, y)
        {

            //this.x = x;
            //this.y = y;
            //this.hunger = 100;
            //pathToFood = null;

        }
        public Organism() : base() { }
        protected void DecreaseHunger()
        {
            //hunger--;
            //if (hunger <= 0)
            //{
            //    Die();
            //}
        }
        public bool IsDead()
        {
            return isDead;
        }
        protected void Die()
        {
            //grid.RemoveOrganism(x, y);
            isDead = true;
        }
        public void MoveTo(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void MoveBy(int a , int b)
        {
            this.x += a;
            this.y += b;
        }
      
    }
    public interface Reproducable
    {
        Organism Reproduce(Organism parent, Board board);
        bool CanReproduce(Board b );
    }


}
