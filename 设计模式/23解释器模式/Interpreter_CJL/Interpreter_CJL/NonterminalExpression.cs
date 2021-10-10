using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    public class NonterminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("非终端解释器");
        }
    }
}
