using System;
using System.Collections.Generic;
using System.Text;

namespace StatePattern_CJL
{
    /// <summary>
    /// 抽象状态类
    /// </summary>
    public abstract class State
    {
        public abstract void Handle(Context context);
    }
}