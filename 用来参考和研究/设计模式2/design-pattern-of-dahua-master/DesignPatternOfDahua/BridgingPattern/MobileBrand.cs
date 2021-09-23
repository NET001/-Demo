using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingPattern
{
    abstract class MobileBrand
    {
        protected MobileSoft mobileSoft;

        public void SetSoft(MobileSoft mobileSoft)
        {
            this.mobileSoft = mobileSoft;
        }

        public abstract void Run();
    }
}
