using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_CJL
{
    public interface IHandler
    {
        bool HandleRequest(int request);
    }
    public class ConcreteHandlerChain1 : IHandler
    {
        public bool HandleRequest(int request)
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
    public class ConcreteHandlerChain2 : IHandler
    {
        public bool HandleRequest(int request)
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
    public class ConcreteHandlerChain3 : IHandler
    {
        public bool HandleRequest(int request)
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
    public class HandlerChain
    {
        private List<IHandler> handlers = new List<IHandler>();
        public void AddSuccessor(IHandler successor)
        {
            handlers.Add(successor);
        }
        public void HandleRequest(int request)
        {
            foreach (var item in handlers)
            {
                if (item.HandleRequest(request))
                {
                    break;
                }
            }
        }
    }
}
