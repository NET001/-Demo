using System;

namespace CompositePattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Department root = new Department(1);
            root.AddSubNode(new Employee(11, 123.4));
            root.AddSubNode(new Employee(12, 123.5));
            root.AddSubNode(new Employee(13, 123.5));
            root.AddSubNode(new Employee(14, 123.5));
            root.AddSubNode(new Func<Department>(() =>
            {
                Department root_sub = new Department(15);
                root_sub.AddSubNode(new Employee(151, 123));
                root_sub.AddSubNode(new Employee(152, 123));
                root_sub.AddSubNode(new Employee(153, 123));
                return root_sub;
            })());
            root.AddSubNode(new Func<Department>(() =>
            {
                Department root_sub = new Department(16);
                root_sub.AddSubNode(new Employee(161, 123));
                root_sub.AddSubNode(new Employee(162, 123));
                root_sub.AddSubNode(new Employee(163, 123));
                return root_sub;
            })());
            //计算出所有值
            Console.WriteLine(root.CalculateSalary());
            Console.Read();
        }
        //构建树并展示
        static void Demo1()
        {
            Composite root = new Composite("root");
            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));
            Composite comp = new Composite("Composite X");
            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));
            root.Add(comp);
            Composite comp2 = new Composite("Composite XY");
            comp2.Add(new Leaf("Leaf XYA"));
            comp2.Add(new Leaf("Leaf XYB"));
            comp.Add(comp2);
            root.Add(new Leaf("Leaf C"));
            Leaf leaf = new Leaf("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);
            root.Display(1);
        }
        //构建公司组织架构并计算全公司薪酬
        static void Demo2()
        {
            Department root = new Department(1);
            root.AddSubNode(new Employee(11, 123.4));
            root.AddSubNode(new Employee(12, 123.5));
            root.AddSubNode(new Employee(13, 123.5));
            root.AddSubNode(new Employee(14, 123.5));
            root.AddSubNode(new Func<Department>(() =>
            {
                Department root_sub = new Department(15);
                root_sub.AddSubNode(new Employee(151, 123));
                root_sub.AddSubNode(new Employee(152, 123));
                root_sub.AddSubNode(new Employee(153, 123));
                return root_sub;
            })());
            root.AddSubNode(new Func<Department>(() =>
            {
                Department root_sub = new Department(16);
                root_sub.AddSubNode(new Employee(161, 123));
                root_sub.AddSubNode(new Employee(162, 123));
                root_sub.AddSubNode(new Employee(163, 123));
                return root_sub;
            })());
            //计算出所有值
            Console.WriteLine(root.CalculateSalary());
        }
    }
}