using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternA
{
    class ConcreteNotifierB : Notifier
    {
        public string Action { get; set; }
        public string NotificationStatus { get => Action; set => _ = Action; }

        IList<Observer> observers = new List<Observer>();

        public void Add(Observer observer)
        {
            observers.Add(observer);
        }

        public void Delete(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var item in observers)
            {
                item.UpdateState();
            }
        }
    }
}
