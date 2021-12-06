using System;

namespace Chain_of_Responsibility_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            HandlerChain handlerChain = new HandlerChain();
            IHandler h1 = new ConcreteHandlerChain1();
            IHandler h2 = new ConcreteHandlerChain2();
            IHandler h3 = new ConcreteHandlerChain3();
            handlerChain.AddSuccessor(h1);
            handlerChain.AddSuccessor(h2);
            handlerChain.AddSuccessor(h3);
            int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
            foreach (int request in requests)
            {
                handlerChain.HandleRequest(request);
            }
            Console.Read();
        }
        //基础职责链模式
        static void Demo1()
        {
            Handler h1 = new ConcreteHandler1();
            Handler h2 = new ConcreteHandler2();
            Handler h3 = new ConcreteHandler3();
            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
            int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
            foreach (int request in requests)
            {
                h1.HandleRequest(request);
            }
        }
        //将链调用逻辑放到抽象类中
        static void Demo2()
        {
            HandlerTemp h1 = new ConcreteHandlerTemp1();
            HandlerTemp h2 = new ConcreteHandlerTemp2();
            HandlerTemp h3 = new ConcreteHandlerTemp3();
            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
            int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
            foreach (int request in requests)
            {
                h1.HandleRequest(request);
            }
        }
        //基于集合的职责链
        static void Demo3()
        {
            HandlerChain handlerChain = new HandlerChain();
            IHandler h1 = new ConcreteHandlerChain1();
            IHandler h2 = new ConcreteHandlerChain2();
            IHandler h3 = new ConcreteHandlerChain3();
            handlerChain.AddSuccessor(h1);
            handlerChain.AddSuccessor(h2);
            handlerChain.AddSuccessor(h3);
            int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
            foreach (int request in requests)
            {
                handlerChain.HandleRequest(request);
            }
        }
    }
}
