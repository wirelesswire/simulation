using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Plant : Organism
    {
        //public static List<ObjectOnMap> objectInstances = new List<ObjectOnMap>();

        //nutritiousness = 0;
        int maxNutritiousness = 100;
        public Plant(int x, int y,Board b ) : base(x, y,b) { }
        public Plant() : base() {
       
        }
        public Plant(coords c, Board b) : base(c, b) { }
        //public override void addToInstances()
        //{   
        //    if (Plant.objectInstances is null)
        //    {
        //        Plant.objectInstances = new List<ObjectOnMap>();
        //    }
        //    //throw new NotImplementedException();
        //    Plant.objectInstances.Add(this);
        //}
        public override  void epochPass()
        {
            nutritiousness++;
            if (nutritiousness > maxNutritiousness)
            {
                nutritiousness = maxNutritiousness;
            }
        }
        public override double getNutritionalValue()
        {
            return nutritiousness;
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
