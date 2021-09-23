using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationPattern
{
    public class ConcreteComponent : Component
    {
        IList<Component> list = new List<Component>();
        public ConcreteComponent(string name) : base(name)
        {

        }

        public override void Add(Component component)
        {
            list.Add(component);
        }

        public override void Delete(Component component)
        {
            list.Remove(component);
        }

        public override void Show(int level)
        {
            Console.WriteLine(new string('-',level) + Name);
            foreach (var item in list)
            {
                item.Show(level + 2);
            }
        }
    }
}
