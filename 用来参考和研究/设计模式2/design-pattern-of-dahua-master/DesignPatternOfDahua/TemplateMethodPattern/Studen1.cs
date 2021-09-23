using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern
{
    class Studen1:Template
    {
        public override string Answer1()
        {
            return "学生1答案-A";
        }

        public override string Answer2()
        {
            return "学生1答案-A";
        }
    }
}
