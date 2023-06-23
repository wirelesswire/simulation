using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa pomocnicza używana przez algorytm wyszukiwania najkrótszej drogi do celu
    /// </summary>
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
    /// <summary>
    /// interface pozwalający określić stan danego organizmu (żywy lub martwy )
    /// </summary>
    public interface IDieable
    {
        public bool IsDead();
    }
    /// <summary>
    /// klasa opisująca organizm znajdujący się na mapie 
    /// </summary>
    public  class Organism : ObjectOnMap,IDieable
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns> zwraca wartość odżywczą osobnika </returns>
        public virtual double getNutritionalValue() { return  this.nutritiousness; }

        protected bool isDead = false;
        protected   int nutritiousness = 0; // jest to maxymalna ilość głodu jaką dane zwierze zebrało przez całą symulację  lub roślina której wartość tego się zwiększa co epokę lub ciało gdzie zmienjsza sie co epokę 
        protected int age = 0;

        /// <summary>
        /// episuje starzenie się obiektu 
        /// </summary>
        /// <param name="forward">kierunek upływu czasu </param>
        public override void epochPass(bool forward )
        {
            if (forward)
            {
                age++;

            }
            else 
            { 
                age--; 
            } 
        }

        public Organism(int x, int y, Board b) : base(x, y, b) { }
        public Organism(coords c , Board b) : base(c, b) { }

        public Organism() : base() { }

        //protected void DecreaseHunger()
        //{
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns>czy organizm jest martwy </returns>
        public bool IsDead()
        {
            return isDead;
        }

        /// <summary>
        /// organizm staje się martwy 
        /// </summary>
        /// <param name="forwards">kierunek upływu czasu </param>
        public  void Die(bool forwards)
        {
            isDead = forwards ;
        }
        /// <summary>
        /// przemieszcza organizm do podanej pozycji 
        /// </summary>
        /// <param name="x">nowa pozycja x</param>
        /// <param name="y">nowa pozycja y</param>
        public void MoveTo(int x, int y)
        {
            this.coords.x = x;
            this.coords.y = y;
        }


        //public void MoveBy(int a , int b)
        //{
        //    this.coords.x += a;
        //    this.coords.y += b;
        //}
      
    }
    /// <summary>
    /// interface opisujący organizmy które mogą mieć potomstwo 
    /// </summary>
    public interface Reproducable
    {

        stats Reproduce(out coords a );
        bool CanReproduce( );
    }


}
