using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternB
{
    public interface INotifier
    {
        string NotifyState { get; set; }

        void Notify();
    }
}
