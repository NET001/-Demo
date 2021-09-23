using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Product
    {
        public IList<string> list = new List<string>();

        public void Add(string part)
        {
            list.Add(part);
        }

        public void Show()
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
