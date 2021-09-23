using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    class CommandA : Command
    {
        public CommandA(Worker worker) : base(worker)
        {
        }

        public override void Excute()
        {
            worker.BraisedChickenWings();
        }
    }
}
