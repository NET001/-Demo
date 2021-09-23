using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    abstract class Command
    {
        public Worker worker;

        public Command(Worker worker)
        {
            this.worker = worker;
        }

        public abstract void Excute();
    }
}
