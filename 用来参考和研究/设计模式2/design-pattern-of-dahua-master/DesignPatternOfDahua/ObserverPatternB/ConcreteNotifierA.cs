using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternB
{
    public delegate void Update();
    public class ConcreteNotifierA : INotifier
    {
        public Update update;
        private string Param { get; set; }
        public string NotifyState { get => Param; set => Param = value; }

        public void Notify()
        {
            update();
        }
    }
}
