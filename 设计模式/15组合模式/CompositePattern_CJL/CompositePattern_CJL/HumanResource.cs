using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    //员工和部门抽象类
    public abstract class HumanResource
    {
        protected long id;
        protected double salary;
        public HumanResource(long id)
        {
            this.id = id;
        }
        public long GetId()
        {
            return id;
        }
        public abstract double CalculateSalary();
    }
    //部门
    public class Department : HumanResource
    {
        List<HumanResource> subNodes = new List<HumanResource>();
        public Department(long id) : base(id)
        {
            this.id = id;
        }
        public void AddSubNode(HumanResource humanResource)
        {
            subNodes.Add(humanResource);
        }
        //计算出部门薪资
        public override double CalculateSalary()
        {
            double totalSalary = 0;
            foreach (var item in subNodes)
            {
                totalSalary += item.CalculateSalary();
            }
            return totalSalary;
        }
    }
    //员工
    public class Employee : HumanResource
    {
        public Employee(long id, double salary) : base(id)
        {
            this.salary = salary;
        }
        //返回员工薪资
        public override double CalculateSalary()
        {
            return salary;
        }
    }
}
