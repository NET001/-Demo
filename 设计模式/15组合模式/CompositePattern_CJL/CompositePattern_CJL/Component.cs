using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    public abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }

}
