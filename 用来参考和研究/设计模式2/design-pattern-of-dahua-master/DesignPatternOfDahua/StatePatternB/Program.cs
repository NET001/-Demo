using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternB
{
    class Program
    {
        static void Main(string[] args)
        {
            Work work = new Work(new ConcreteStateA());
            //work.Hour = 9;
            //work.Show();
            //work.Hour = 15;
            //work.Show();
            work.Hour = 19;
            work.Show();
            Console.ReadKey();
        }
    }
}
