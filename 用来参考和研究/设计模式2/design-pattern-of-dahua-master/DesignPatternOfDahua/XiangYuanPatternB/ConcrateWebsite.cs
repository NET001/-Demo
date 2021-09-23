using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPatternB
{
    class ConcrateWebsite : Website
    {
        public string name;
        public ConcrateWebsite(string name)
        {
            this.name = name;
        }
        public override void Use(User user)
        {
            Console.WriteLine("网站分类：{0}--用户：{1}", name, user.Name);
        }
    }
}
