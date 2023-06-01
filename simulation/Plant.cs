using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Plant : Organism
    {

        int maxNutritiousness = 100;
        public Plant(int x, int y,Board b ) : base(x, y,b) { }
        public Plant() : base() {
            nutritiousness = 1;
        }
        public Plant(coords c, Board b) : base(c, b) { }

        public override  void epochPass(bool forward)
        {
            if (forward)
            {
                nutritiousness++;
                if (nutritiousness > maxNutritiousness)
                {
                    nutritiousness = maxNutritiousness;
                }

            }
            else
            {
                nutritiousness--;   
                
            }
            base.epochPass(forward);// zmienia wiek 


        }
        public override double getNutritionalValue()
        {
            return nutritiousness;
        }

        public override string toString()
        {
            return "P" + base.toString();
        }


    }

}
