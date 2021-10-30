using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    /// <summary>
    /// 终端解释器(表达式)
    /// </summary>
    public class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("终端解释器");
        }
    }
}
