using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterPattern
{
    abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }
}
