using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_CJL
{
    /// <summary>
    /// 具体处理者2
    /// </summary>
    public class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Console.WriteLine("{0}  处理请求  {1}",
                  this.GetType().Name, request);
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }
}
