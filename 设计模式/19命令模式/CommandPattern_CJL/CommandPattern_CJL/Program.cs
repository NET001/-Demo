using System;

namespace CommandPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver rA = new ReceiverA();
            Command cA = new ConcreteCommand(rA);
            Receiver rB = new ReceiverB();
            Command cB = new ConcreteCommand(rB);
            Invoker i = new Invoker();
            i.SetCommand(cA);
            i.ExecuteCommand();
            i.SetCommand(cB);
            i.ExecuteCommand();

            Console.Read();
        }
    }
}
