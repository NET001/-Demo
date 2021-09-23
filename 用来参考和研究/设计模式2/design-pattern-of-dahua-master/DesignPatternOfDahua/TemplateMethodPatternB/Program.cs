using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPatternB
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Template template;
            template = new Realization1();
            template.Show();

            template = new Realization2();
            template.Show();
            Console.ReadKey();
        }
    }
}
