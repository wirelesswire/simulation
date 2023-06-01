using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Lake : ObjectOnMap
    {
        public Lake(int x, int y, Board b) : base(x, y,b) { }
        public Lake() : base() { }
        public Lake(coords c , Board b) : base(c, b) { }
        public override string toString()
        {
            return "#" + base.toString();
        }
        public override void epochPass(bool forwards )
        {
            //throw new NotImplementedException();
        }
    }


}
