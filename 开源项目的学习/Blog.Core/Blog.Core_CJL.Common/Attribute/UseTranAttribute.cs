using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Common.Attribute
{
    /// <summary>
    /// 此特性用于标记,在事务AOP中处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class UseTranAttribute : System.Attribute
    {

    }
}
