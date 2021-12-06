using System;

namespace Strategy_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context;
            context = new Context(new ConcreteStrategyA());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyB());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyC());
            context.ContextInterface();
            Console.Read();
        }
        /// <summary>
        /// 直接创建
        /// </summary>
        static void Demo1()
        {
            Context context;
            context = new Context(new ConcreteStrategyA());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyB());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyC());
            context.ContextInterface();
        }
        /// <summary>
        /// 通过策略工厂创建
        /// </summary>
        static void Demo2()
        {
            Context context;
            context = new Context(StrategyFactory.GetStrategy(typeof(ConcreteStrategyA)));
            context.ContextInterface();
            context = new Context(StrategyFactory.GetStrategy(typeof(ConcreteStrategyB)));
            context.ContextInterface();
            context = new Context(StrategyFactory.GetStrategy(typeof(ConcreteStrategyC)));
            context.ContextInterface();
        }
    }
}
