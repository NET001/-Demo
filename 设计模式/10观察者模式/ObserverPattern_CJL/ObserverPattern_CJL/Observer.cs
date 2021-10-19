using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverPattern_CJL
{
    /// <summary>
    /// 抽象观察者
    /// </summary>
    public abstract class Observer
    {
        public abstract void Update();
    }
}
