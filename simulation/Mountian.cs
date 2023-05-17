using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class Mountain : ObjectOnMap
    {
        public Mountain(int x, int y) : base(x, y) { }
        public Mountain() : base() { }
        public override string toString()
        {
            return "^" +base.toString();
        }

    }
}
