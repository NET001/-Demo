using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern
{
    public class Template
    {
        public void Question1()
        {
            Console.WriteLine("问题1：111");
            Console.WriteLine("答案1：{0}", Answer1());
        }

        public virtual string Answer1()
        {
            return "";
        }
        public void Question2()
        {
            Console.WriteLine("问题2：222");
            Console.WriteLine("答案2：{0}", Answer2());
        }

        public virtual string Answer2()
        {
            return "";
        }
    }
}
