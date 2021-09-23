using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternA
{
    class ConcreteComponent : Component
    {
        public override void Show()
        {
            Console.Write("小菜最帅...");
        }
    }
}
