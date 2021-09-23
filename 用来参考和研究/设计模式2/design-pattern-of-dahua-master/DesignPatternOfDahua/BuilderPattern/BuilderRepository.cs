using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public abstract class BuilderRepository
    {
        protected Product product = new Product();
        public abstract void SetPartsA();

        public abstract void SetPartsB();

        public Product GetResult()
        {
            return product;
        }
    }
}
