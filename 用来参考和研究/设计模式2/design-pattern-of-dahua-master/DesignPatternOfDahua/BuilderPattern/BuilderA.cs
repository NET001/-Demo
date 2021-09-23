using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class BuilderA : BuilderRepository
    {
        public override void SetPartsA()
        {
            product.Add("我是部件A");
        }

        public override void SetPartsB()
        {
            product.Add("我是部件B");
        }
    }
}
