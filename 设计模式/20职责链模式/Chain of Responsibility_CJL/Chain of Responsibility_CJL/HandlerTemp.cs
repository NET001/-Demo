using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_CJL
{
    public abstract class HandlerTemp
    {
        protected HandlerTemp successor = null;
        public void SetSuccessor(HandlerTemp successor)
        {
            this.successor = successor;
        }
        public void HandleRequest(int request)
        {
            bool handle = DoHandle(request);
            if (!handle && successor != null)
            {
                successor.HandleRequest(request);
            }
        }
        protected abstract bool DoHandle(int request);
    }

    public class ConcreteHandlerTemp1 : HandlerTemp
    {
        protected override bool DoHandle(int request)
        {
            if (request >= 0 && request < 10)
            {
                Console.WriteLine("{0}  处理请求  {1}",
                  this.GetType().Name, request);
                return true;
            }
            return false;
        }
    }

    public class ConcreteHandlerTemp2 : HandlerTemp
    {
        protected override bool DoHandle(int request)
        {
            if (request >= 10 && request < 20)
            {
                Console.WriteLine("{0}  处理请求  {1}",
                  this.GetType().Name, request);
                return true;
            }
            return false;
        }
    }

    public class ConcreteHandlerTemp3 : HandlerTemp
    {
        protected override bool DoHandle(int request)
        {
            if (request >= 20 && request < 30)
            {
                Console.WriteLine("{0}  处理请求  {1}",
                  this.GetType().Name, request);
                return true;
            }
            return false;
        }
    }
}
