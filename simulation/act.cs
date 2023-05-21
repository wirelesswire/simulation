using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public interface IAct
    {




    }
    public class Act
    {
        public enum actionTaken
        {
            move,// przesuwa sie na podane pole 
            eat,// przesuwa sie na podane pole i zjada to co sie na nim znajduje 
            nothing,// jest przyblokowany albo z innego powodu sie nie porusza 
            die// umiera z głodu 

        }
        public coords from;
        public coords to;
        public Animal who;
        public Organism eaten;
        public Animal dead;


        actionTaken acted;
        bool moreActions;
        public actionTaken GetAction()
        {
            return acted;
        }

        public void setAction(actionTaken action)
        {
            this.acted = action;
        }
        public Act(Animal fromObj, ObjectOnMap toObj,coords from , coords to , actionTaken action , int actionsLeft)//eat
        {
            if (from.XandYequal( to))
            {
                throw new Exception("to nie ten konstruktor ");
            }
            if (toObj is not Organism org )
            {
                throw new Exception("tu ");
            }
            if(action != actionTaken.eat)
            {
                throw new Exception();
            }
            this.who = fromObj;
            this.eaten = org;
            this.from = from;
            this.to = to;
            this.acted = action;
            this.moreActions = actionsLeft > 0;
        }
        public Act(Animal fromObj, Corpse toObj, coords from, coords to, int actionsLeft,actionTaken action = actionTaken.die )//die 
        {
            if (from.XandYequal(to))
            {
                throw new Exception("to nie ten konstruktor ");
            }
            if (action != actionTaken.die)
            {
                throw new Exception();
            }
            if (!from.XandYequal(to))
            {
                throw new Exception() ;
            }
            this.who = fromObj;
            this.from = from;
            this.to = to;
            this.eaten = toObj; // tutaj jako to co popwstanie nazwy trochę mylące więc do poprawek
            this.acted = action;
            this.moreActions = actionsLeft > 0;
        }
        public Act(Animal who  , coords from, coords to, actionTaken action, int actionsLeft)//move
        {
            if (from.XandYequal(to))
            {
                throw new Exception("to nie ten konstruktor ");
            }
            if (action != actionTaken.move)
            {
                throw new Exception();
            }
            this.who = who ;
            this.from = from;
            this.to = to;
            this.acted = action;
            this.moreActions = actionsLeft > 0;
        }
        public Act(Animal who , coords fromto, int actionsLeft, actionTaken action = actionTaken.nothing )//nic
        {
            if(action != actionTaken.nothing)
            {
                throw new Exception("nie może tak być ");
            }
            this.who = who;
            this.from = fromto;
            this.to = fromto;
            acted = actionTaken.nothing;
            this.moreActions = false ;// jak raz sie nie poruszył to więcejj też się nie poruszy 
        }


        public bool gotMoreMoves()
        {
            return moreActions;
        }
   
        public bool eats()
        {
            return acted == actionTaken.eat ? true : false;
        }
        public bool moves()
        {
            return acted == actionTaken.move ? true : false;
        }
        public bool dies()
        {
            return acted == actionTaken.die;
        }

    }
}
