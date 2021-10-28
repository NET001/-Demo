using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    /// <summary>
    /// 抽象解释器(表达式)
    /// </summary>
    public abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }
}
