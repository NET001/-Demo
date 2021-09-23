using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternA
{
    public class ConcreteObserverA : Observer
    {
        public ConcreteObserverA(string name, Notifier notifier) :base(name, notifier)
        {

        }
        public override void UpdateState()
        {
            Console.WriteLine("{0}{1};糟糕,不能看球了，赶快工作！", Name, Notifier.NotificationStatus);
        }
    }
}
