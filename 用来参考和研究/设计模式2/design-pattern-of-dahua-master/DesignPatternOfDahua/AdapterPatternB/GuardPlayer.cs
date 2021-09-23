using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternB
{
    public class GuardPlayer : Player
    {
        public GuardPlayer(string name) : base(name)
        {
        }

        public override void Attack()
        {
            Console.WriteLine("后卫{0}开始进攻", name);
        }

        public override void Defense()
        {
            Console.WriteLine("后卫{0}开始防守", name);
        }
    }
}
