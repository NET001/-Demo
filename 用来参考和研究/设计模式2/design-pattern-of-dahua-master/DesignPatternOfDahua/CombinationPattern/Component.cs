using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationPattern
{
    public abstract class Component
    {
        protected string Name { get; set; }

        public Component(string name)
        {
            Name = name;
        }
        public abstract void Add(Component component);
        public abstract void Delete(Component component);
        public abstract void Show(int level);
    }
}