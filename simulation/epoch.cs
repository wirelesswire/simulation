using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    /// <summary>
    /// epoka zawiera wszystkie akcje które wydarzyły się w jej trakcie 
    /// </summary>
    public class epoch
    {
        public List<Act> acts {  get ;  private  set; }

        public epoch() { acts = new List<Act>(); }
        
        /// <summary>
        /// dodaje akcje do listy 
        /// </summary>
        /// <param name="a"></param>
        public void  addAct(Act a )
        {
            acts.Add(a);
        }


    }

}
