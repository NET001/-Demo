using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPatternB
{
    class WebsiteFactory
    {
        public Hashtable hashtable = new Hashtable();

        public Website CreateWebsite(string key)
        {
            if (!hashtable.ContainsKey(key))
                hashtable.Add(key, new ConcrateWebsite(key));
            return (Website)hashtable[key];
        }

        public int Count
        {
            get => hashtable.Count;
        }
    }
}
