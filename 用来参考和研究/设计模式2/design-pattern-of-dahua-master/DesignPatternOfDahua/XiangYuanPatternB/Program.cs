using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPatternB
{
    /// <summary>
    /// 享元模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            WebsiteFactory factory = new WebsiteFactory();
            Website website1 = factory.CreateWebsite("博客");
            website1.Use(new User("张萨满"));
            Website website2 = factory.CreateWebsite("博客");
            website2.Use(new User("张管家"));
            Website website3 = factory.CreateWebsite("网站");
            website3.Use(new User("李时珍"));
            Console.WriteLine(factory.Count);
            Console.ReadKey();
        }
    }
}
