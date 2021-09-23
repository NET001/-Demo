using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingPattern
{
    class MobileBrandA : MobileBrand
    {
        public override void Run()
        {
            mobileSoft.Run();
        }
    }
}
