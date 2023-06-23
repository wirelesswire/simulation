using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static simulation.Act;

namespace simulation
{
    /// <summary>
    /// akcja wykonywana na mapie 
    /// </summary>
    public class Act
    {
        public coords from;//skąd 
        public coords to;//dokąd 
        public Animal who;//kto wykonał
        public Organism affected;//kto zjedzony 
        public Organism dead; // kto martwy 
        protected bool moreActions = false;
        /// <summary>
        /// ustawia czy może się jeszcze poruszyć 
        /// </summary>
        /// <param name="a">czy ma więcej ruchów </param>
        public void setMoves(bool a)
        {
            this.moreActions = a;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>czy może się jeszcze poruszyć</returns>
        public bool gotMoreMoves()
        {
            return this.moreActions;
        }
    }
    /// <summary>
    /// akcja przemieszczenia się z miejsca na miejsce 
    /// </summary>
    public class Move : Act
    {

        public Move(Animal who, coords from, coords to)//move
        {
            if (from.XandYequal(to))
            {
                throw new Exception("to nie ten konstruktor ");
            }

            this.who = who;
            this.from = from;
            this.to = to;
            //this.acted = action;
            affected = null;
            dead = null;
            //this.moreActions = actionsLeft > 0;
        }
        public void reverse()
        {

        }
    }
    /// <summary>
    /// akcja pojawienia się na mapie 
    /// </summary>
    public class Apeerance:Act
    {
        public ObjectOnMap what;//co się pojawiło 
        public Apeerance(ObjectOnMap what )
        {
            this.what = what;
            if(what is Animal a)
            {
                this.who = a;

            }
        }

    }
    /// <summary>
    /// akcja upływu czasu 
    /// </summary>
    public class epochPass:Act
    {
        public epochPass() { }
    }

    /// <summary>
    /// akcja zjedzenia innego organizmu 
    /// </summary>
    public class Eat : Act
    {
       

        public Eat(Animal fromObj, ObjectOnMap toObj, coords from, coords to)//eat
        {
            if (from.XandYequal(to))
            {
                throw new Exception("to nie ten konstruktor ");
            }
            if (toObj is not Organism org)
            {
                throw new Exception("tu ");
            }
            this.from = from;
            this.to = to;
            this.who = fromObj;
            this.affected = org;
            this.dead = org;
        }
    }
    /// <summary>
    /// akcja braku poruszenia się 
    /// </summary>
    public class DraxStanding : Act
    {
        //public bool moreActions;

        public DraxStanding(Animal who, coords fromto)//nic
        {
            this.who = who;
            this.from = fromto;
            this.to = fromto;
            affected = null;
            dead = null;
            this.moreActions = false;// jak raz sie nie poruszył to więcejj też się nie poruszy 
        }
    }
    /// <summary>
    /// akcja śmierci i zamienienia się w ciało 
    /// </summary>
    public class Die : Act
    {
        //public override bool 
        public Die(Animal fromObj, Corpse toObj, coords from, coords to)//die 
        {
            if (!from.XandYequal(to))
            {
                throw new Exception("to nie ten konstruktor ");
            }
            if (!from.XandYequal(to))
            {
                throw new Exception();
            }
            this.from = from;
            this.to = to;
            this.who = fromObj;
            this.affected = toObj; // tutaj jako to co popwstanie nazwy trochę mylące więc do poprawek
            this.dead = fromObj;
            this.moreActions = false;

        }
    }
    /// <summary>
    /// akcja samounicestwienia się ciała
    /// </summary>
    public class selfDestruct : Act
    {
        public selfDestruct(Corpse fromObj)//selfdestruction
        {

            this.from = fromObj.coords;
            this.to = fromObj.coords;
            this.who = null;// nikt tego nie zrobił , samo się zrobiło 
            this.affected = fromObj;
            this.dead = null; // ciało nie może umrzeć 
        }
    }
}
