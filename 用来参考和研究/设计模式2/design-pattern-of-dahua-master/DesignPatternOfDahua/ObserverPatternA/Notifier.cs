using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternA
{
    public interface Notifier
    {
        string NotificationStatus { get; set; }

        void Add(Observer observer);

        void Delete(Observer observer);

        void Notify();
    }
}
