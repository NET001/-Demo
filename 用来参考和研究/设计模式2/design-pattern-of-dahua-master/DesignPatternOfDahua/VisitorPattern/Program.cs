using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    /// <summary>
    /// 访问者模式（分两派）
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ObjectStructrue structrue = new ObjectStructrue();
            structrue.Add(new FactionA());
            structrue.Add(new FactionB());

            State state;
            state = new StateA();
            structrue.Display(state);

            state = new StateB();
            structrue.Display(state);
            Console.ReadKey();
        }
    }
}