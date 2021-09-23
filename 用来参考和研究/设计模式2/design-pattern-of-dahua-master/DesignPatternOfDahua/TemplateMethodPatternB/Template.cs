using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPatternB
{
    public abstract class Template
    {
        public abstract void TemplateMethod1();
        public abstract void TemplateMethod2();

        public void Show()
        {
            this.TemplateMethod1();
            this.TemplateMethod2();
        }
    }
}
