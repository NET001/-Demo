using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern
{
    public class Student2:Template
    {
        public override string Answer1()
        {
            return "学生2答案-B";
        }

        public override string Answer2()
        {
            return "学生2答案-B";
        }
    }
}
