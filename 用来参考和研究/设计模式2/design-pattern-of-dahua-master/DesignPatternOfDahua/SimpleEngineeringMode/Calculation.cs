using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryPattern
{
    /// <summary>
    /// 可不用abstract类 用虚方法代替抽象方法
    /// </summary>
    public abstract class Calculation
    {
        protected string NumberA { get; set; }
        protected string NumberB { get; set; }

        public abstract string GetResult(string NumberA, string NumberB);
    }
}
