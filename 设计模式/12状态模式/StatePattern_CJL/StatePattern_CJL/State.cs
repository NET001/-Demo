using System;
using System.Collections.Generic;
using System.Text;

namespace StatePattern_CJL
{
    public abstract class State
    {
        public abstract void Handle(Context context);
    }
}
