using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternB
{
    class ConcreteObserverA : Observer
    {
        public string Name { get; set; }
        public INotifier Notifier { get; set; }
        public ConcreteObserverA(string name = null, INotifier notifier = null)
        {
            Name = name;
            Notifier = notifier;
        }

        public void UpdateConcreteObserverB()
        {
            Console.WriteLine("{0},别聊了，{1}", Name, Notifier.NotifyState);
        }
    }
}
