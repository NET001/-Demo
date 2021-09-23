using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternA
{
    public abstract class Observer
    {
        protected string Name { get; set; }
        public Notifier Notifier { get; set; }

        public Observer(string name, Notifier notifier)
        {
            this.Notifier = notifier;
            this.Name = name;
        }

        public abstract void UpdateState();
    }
}
