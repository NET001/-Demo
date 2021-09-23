using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TertiumQuid
{
    public class ConcreteMedium1 : Medium
    {
        private ConcreteWorker1 concreteWorker1;
        public ConcreteWorker1 ConcreteWorker1 { set => concreteWorker1 = value; }

        private ConcreteWorker2 concreteWorker2;
        public ConcreteWorker2 ConcreteWorker2 { set => concreteWorker2 = value; }

        public override void Send(string message, Worker worker)
        {
            if (worker == concreteWorker1)
            {
                concreteWorker2.Notify(message);
            }
            else
            {
                concreteWorker1.Notify(message);
            }
        }
    }
}
