using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternB
{
    public class Work
    {

        public int Hour { get; set; }
        public State state { get; set; }

        public Work(State state)
        {
            this.state = state;
        }

        public void setWork(State state)
        {
            this.state = state;
        }

        public void Show()
        {
            state.ShowState(this);
        }
    }
}
