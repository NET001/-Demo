using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern
{
    /// <summary>
    /// 模板方法模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Template template;
            template = new Studen1();
            template.Question1();
            template.Question2();

            template = new Student2();
            template.Question1();
            template.Question2();
            Console.ReadKey();
        }
    }
}
