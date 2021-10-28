using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    /// <summary>
    /// 非终端解释器(表达式)
    /// </summary>
    public class NonterminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("非终端解释器");
        }
    }
}
