using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    public class Adapter:OdlInterface
    {
        Newinterface newinterface = new Newinterface();
        public override void Request()
        {
            newinterface.Request();
        }
    }
}
