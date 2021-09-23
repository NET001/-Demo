using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterPattern
{
    class ConcrateExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("解释器A");
        }
    }
}
