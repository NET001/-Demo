using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    public class OdlInterface
    {
         public virtual void Request()
        {
            Console.WriteLine("旧方法");
        }
    }
}
