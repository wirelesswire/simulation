using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public  class Corpse:Organism
    {
        
        public void epochPass()
        {
            this.nutritiousness--;
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


    }
}
