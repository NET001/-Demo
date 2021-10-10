using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter_CJL
{
    public abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }

}
