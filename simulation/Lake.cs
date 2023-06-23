using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// klasa opisująca jezioro na mapie 
    /// </summary>
    public class Lake : ObjectOnMap
    {
        public Lake(int x, int y, Board b) : base(x, y,b) { }
        public Lake() : base() { }
        public Lake(coords c , Board b) : base(c, b) { }
        public override string toString()
        {
            
            return "L" + base.toString();
        }
        /// <summary>
        /// jezior nie zmienia się w czasie 
        /// </summary>
        /// <param name="forwards"></param>
        public override void epochPass(bool forwards )
        {
            //throw new NotImplementedException();
        }
        public virtual string presentation()
        {
            return base.presentation() +" jezioro";
        }
    }


}
