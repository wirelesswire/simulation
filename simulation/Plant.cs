using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa opisująca roślinę i jej właściwości
    /// </summary>
    public class Plant : Organism
    {

        int maxNutritiousness { get; set; } = 100;
        int nutritiousnessperEpoch = 2;
        public Plant(int x, int y,Board b ) : base(x, y,b) { }
        public Plant() : base() {
            nutritiousness = 1;
        }
        public Plant(coords c, Board b) : base(c, b) { }


        /// <summary>
        /// dodaje efekty upływu czasu do rośliny zgodnie z kierunkiem jego upływu -> zwiększa pożywienie które roślina może dostarczyć po spożyciu 
        /// </summary>
        /// <param name="forward">kierunek upływu czasu </param>
        public override  void epochPass(bool forward)
        {
            if (forward)
            {
                nutritiousness+=nutritiousnessperEpoch;
                if (nutritiousness > maxNutritiousness)
                {
                    nutritiousness = maxNutritiousness;
                }

            }
            else
            {
                nutritiousness-= nutritiousnessperEpoch;   
                
            }
            base.epochPass(forward);// zmienia wiek 


        }

        //public override double getNutritionalValue()
        //{
        //    return nutritiousness;
        //}

        public override string toString()
        {
            return "P" + base.toString();
        }


    }

}
