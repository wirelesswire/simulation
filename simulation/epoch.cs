using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{

    public class epoch
    {
        public List<Act> acts {  get ;  private  set; }

        public epoch() { acts = new List<Act>(); }
        

        public void  addAct(Act a )
        {
            acts.Add(a);
        }


    }

}
