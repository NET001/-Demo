using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    public class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("终端解释器");
        }
    }
}
