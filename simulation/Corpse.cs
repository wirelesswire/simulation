using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa opisująca ciało organizmu po jego śmierci 
    /// </summary>
    public  class Corpse:Organism
    {
        /// <summary>
        /// dodaje efekty upływu czasu do rośliny zgodnie z kierunkiem jego upływu -> zmniejsza pożywienie które może dostarczyć po spożyciu (rozkłada się )
        /// </summary>
        /// <param name="forward">kierunek upływu czasu</param>
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

        //public override double getNutritionalValue()
        //{
        //    return this.nutritiousness;
        //}
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
