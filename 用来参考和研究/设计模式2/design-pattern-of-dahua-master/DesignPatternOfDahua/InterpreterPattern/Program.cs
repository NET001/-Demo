using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterPattern
{
    /// <summary>
    /// 解释器模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            IList<AbstractExpression> abstracts = new List<AbstractExpression>();
            abstracts.Add(new ConcrateExpression());
            abstracts.Add(new ConcrateExpression());
            abstracts.Add(new ConcrateExpressionB());
            foreach (var item in abstracts)
            {
                item.Interpret(context);
            }
            Console.WriteLine();
        }
    }
}
