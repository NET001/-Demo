using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    public class Context
    {
        public State state { get; set; }

        public Context(State state)
        {
            this.state = state;
        }

        public void Request()
        {
            state.Show(this);
        }
    }
}
