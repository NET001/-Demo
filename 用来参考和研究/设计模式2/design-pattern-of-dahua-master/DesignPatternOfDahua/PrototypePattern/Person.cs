using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }

        public Person(string name, string position, int age)
        {
            this.Age = age;
            this.Name = name;
            this.Position = position;
        }

        public void Show()
        {
            Console.WriteLine("{0}{1}{2}",this.Name, this.Position, this.Age);
        }

        public Person ClonePersion()
        {
            return (Person)this.MemberwiseClone();
        }
    }
}
