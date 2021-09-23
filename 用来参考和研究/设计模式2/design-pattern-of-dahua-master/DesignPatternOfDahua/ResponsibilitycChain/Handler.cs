using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsibilitycChain
{
    public abstract class Handler
    {
        protected Handler handler { get; set; }

        public void setHandler(Handler handler)
        {
            this.handler = handler;
        }

        public abstract void HandlerExcute(int request);
    }
}
