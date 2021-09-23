using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    /// <summary>
    /// 建造者模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BuilderRepository repository1 = new BuilderA();
            Commander commander = new Commander();
            commander.Created(repository1);
            var product = repository1.GetResult();
            product.Show();
            BuilderRepository repository2 = new BuilderB();
            commander.Created(repository2);
            var product2 = repository2.GetResult();
            product.Show();
            product2.Show();
            Console.ReadKey();
        }
    }
}
