using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationPattern
{
    /// <summary>
    /// 组合模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteComponent component = new ConcreteComponent("总公司");
            ConcreteComponent component1 = new ConcreteComponent("子公司");
            Leaf leaf = new Leaf("总公司-部门1");
            Leaf leaf1 = new Leaf("总公司-部门2");
            Leaf leaf2 = new Leaf("子公司-部门1");
            Leaf leaf3 = new Leaf("子公司-部门2");
            component1.Add(leaf2);
            component1.Add(leaf3);
            component.Add(component1);
            component.Add(leaf);
            component.Add(leaf1);
            component.Show(1);
            Console.ReadKey();
        }
    }
}
