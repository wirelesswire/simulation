using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public  class Corpse:Organism
    {
        
        public override void epochPass(bool forward)
        {
            if (forward)
            {

                this.nutritiousness -= this.age;
                base.epochPass(forward);// wiek zwiekszony 


            }
            else
            {
                base.epochPass(forward);    
                this.nutritiousness += this.age;    
            }
        }

        public override double getNutritionalValue()
        {
            return this.nutritiousness;
        }
        public Corpse() { }
        public Corpse(Animal a ):base(a.coords,a.getBoard()) // corpse można stworzyć tylko z animal nie można tworzyć bezpośrendnio 
        {
            this.nutritiousness = a.getNutritiousness();
        }

        public override string toString()
        {
            return "X"+ base.toString();
        }
    }
}
