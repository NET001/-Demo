using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternB
{
    public class Person
    {
        public Person()
        {

        }
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }

        public virtual void Show()
        {
            Console.Write("{0}最帅", Name);
        }
    }
}
