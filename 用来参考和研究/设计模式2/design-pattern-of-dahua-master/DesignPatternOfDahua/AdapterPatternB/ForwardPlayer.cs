using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternB
{
    class ForwardPlayer : Player
    {
        public ForwardPlayer(string name) : base(name)
        {
        }

        public override void Attack()
        {
            Console.WriteLine("前锋{0}发起进攻", name);
        }

        public override void Defense()
        {
            Console.WriteLine("前锋{0}积极防守", name);
        }
    }
}
