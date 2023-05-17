using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class Lake : ObjectOnMap
    {
        public Lake(int x, int y) : base(x, y) { }
        public Lake() : base() { }
        public override string toString()
        {
            return "#" + base.toString();
        }


    }


}
