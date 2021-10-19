using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    /// <summary>
    /// 工厂接口
    /// </summary>
    public interface Creator
    {
        Product CeateProduct();
    }
}
