using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class Act
    {
        public coords from;
        public coords to;
        public Animal who;
        public Organism eaten;
        public Animal dead;


        int xcoorddelta;
        int ycoorddelta;
        actionTaken acted;
        bool moreActions;
        public actionTaken GetAction()
        {
            return acted;
        }
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
        public Act(Animal who, ObjectOnMap eaten,coords from , coords to , actionTaken action , int actionsLeft)//eat
        {
            if (from.XandYequal( to))
            {
                throw new Exception("to nie ten konstruktor ");
            }
            if (eaten is not Organism org )
            {
                throw new Exception("tu ");
            }
            if(action != actionTaken.eat)
            {
                throw new Exception();
            }
            this.who = who;
            this.eaten = org;
            this.from = from;
            this.to = to;
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
