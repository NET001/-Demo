using System;

namespace StatePattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.Read();
        }
        static void Demo1()
        {
            Context c = new Context(new ConcreteStateA());
            c.Request();
            c.Request();
            c.Request();
            c.Request();
        }
        static void Demo2()
        {
            Context2 context = new Context2();
            StartState startState = new StartState();
            startState.doAction(context);
            Console.WriteLine(context.getState().State);
            StopState stopState = new StopState();
            stopState.doAction(context);
            Console.WriteLine(context.getState().State);
        }
    }
}
