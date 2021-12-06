using System;
using System.Collections.Generic;
using System.Text;

namespace StatePattern_CJL
{
    public interface State2
    {
        public int State { get; }
        public void doAction(Context2 context);
    }
    public class StartState : State2
    {
        public int State => 1;
        public void doAction(Context2 context)
        {
            Console.WriteLine("打开状态");
            context.setState(this);
        }
    }
    public class StopState : State2
    {
        public int State => 0;

        public void doAction(Context2 context)
        {
            Console.WriteLine("关闭状态");
            context.setState(this);
        }
    }
    public class Context2
    {
        private State2 state;
        public Context2()
        {
            state = null;
        }
        public void setState(State2 state)
        {
            this.state = state;
        }
        public State2 getState()
        {
            return state;
        }
    }


}
