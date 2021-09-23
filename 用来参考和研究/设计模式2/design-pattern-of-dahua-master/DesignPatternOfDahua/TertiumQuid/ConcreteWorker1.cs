using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TertiumQuid
{
    public class ConcreteWorker1 : Worker
    {
        public ConcreteWorker1(Medium medium)
        {
            this.medium = medium;
        }
        public override void Send(string message)
        {
            medium.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine("同事1接到消息：{0}", message);
        }
    }
}
