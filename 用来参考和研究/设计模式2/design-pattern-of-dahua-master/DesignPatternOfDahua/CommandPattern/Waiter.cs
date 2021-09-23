using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    class Waiter
    {
        public IList<Command> commands = new List<Command>();

        public void Add(Command command)
        {
            commands.Add(command);
        }

        public void Remove(Command command)
        {
            commands.Remove(command);
        }

        public void Notify()
        {
            foreach (var item in commands)
            {
                item.Excute();
            }
        }
    }
}
