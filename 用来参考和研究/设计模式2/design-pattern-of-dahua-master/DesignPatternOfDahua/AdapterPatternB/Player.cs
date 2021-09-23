using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternB
{
    public abstract class Player
    {
        public string name { get; set; }

        public Player(string name)
        {
            this.name = name;
        }

        public abstract void Attack();

        public abstract void Defense();
    }
}
