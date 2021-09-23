using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPattern
{
    public class FlyweightFactory
    {
        public Hashtable hashtable = new Hashtable();

        public FlyweightFactory()
        {
            hashtable.Add("X", new ShareConcrateFlyweight());
            hashtable.Add("Y", new ShareConcrateFlyweight());
            hashtable.Add("Z", new ShareConcrateFlyweight());
        }

        public Flyweight CreateFlyweight(string key)
        {
            return (Flyweight)hashtable[key];
        }
    }
}
