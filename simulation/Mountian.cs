using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa opisująca góre na mapie 
    /// </summary>
    public class Mountain : ObjectOnMap
    {
        public Mountain(int x, int y,Board b ) : base(x, y,b) { }
        public Mountain() : base() { }
        public Mountain(coords c , Board b ) : base(c,b) { }
        public override string toString()
        {
            return "^" +base.toString();
        }
        /// <summary>
        /// góra nie zmienia się w czasie 
        /// </summary>
        /// <param name="forwards"></param>
        public override void epochPass(bool forwards)
        {
            //throw new NotImplementedException();
        }
        public virtual string presentation()
        {
            return base.presentation() + " góra";
        }

    }
}
