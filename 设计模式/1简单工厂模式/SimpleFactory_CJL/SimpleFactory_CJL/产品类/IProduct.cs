using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    /// <summary>
    /// 产品抽象类,还可以是接口或基类
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        ///执行操作
        /// </summary>
        void Operation();
    }
}
