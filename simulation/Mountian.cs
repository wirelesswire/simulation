using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class Mountain : ObjectOnMap
    {
        public Mountain(int x, int y,Board b ) : base(x, y,b) { }
        public Mountain() : base() { }
        public Mountain(coords c , Board b ) : base(c,b) { }
        public override string toString()
        {
            return "^" +base.toString();
        }
    

    }
}
