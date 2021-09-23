using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationPattern
{
    public class Leaf : Component
    {
        public Leaf(string name) : base(name)
        {

        }

        public override void Add(Component component)
        {
            
        }

        public override void Delete(Component component)
        {
            
        }

        public override void Show(int level)
        {
            Console.WriteLine(new string('-', level) + Name);
        }
    }
}