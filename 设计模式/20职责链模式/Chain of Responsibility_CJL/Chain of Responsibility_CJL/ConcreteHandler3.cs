﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_CJL
{
    public class ConcreteHandler3 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20 && request < 30)
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
