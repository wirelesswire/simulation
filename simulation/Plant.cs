using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Plant : Organism
    {
        int nutritiousness = 0;
        int maxNutritiousness = 100;
        public Plant(int x, int y) : base(x, y) { }
        public Plant() : base() {
       
        }


        public void iteration()
        {
            nutritiousness++;
            if (nutritiousness > 100)
            {
                nutritiousness = 100;
            }
        }
        public override string toString()
        {
            return "P" + base.toString();
        }


    }

}
