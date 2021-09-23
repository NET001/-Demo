using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsibilitycChain
{
    public class ConcreteHandler1 : Handler
    {
        public override void HandlerExcute(int request)
        {
            if (request >= 0 && request <= 10)
            {
                Console.WriteLine("{0}处理了这个请求{1}",this.GetType().Name, request);
            }
            else
            {
                if (handler != null)
                {
                    handler.HandlerExcute(request);
                }
                else
                {
                    Console.WriteLine("{0}很遗憾的通知您：没人能处理您的请求{1}", this.GetType().Name, request);
                }
            }
        }
    }
}
