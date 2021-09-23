using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class BuilderB : BuilderRepository
    {
        public override void SetPartsA()
        {
            product.Add("我是部件C");
        }

        public override void SetPartsB()
        {
            product.Add("我是部件D");
        }
    }
}
