using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions
{
    /// <summary>
    /// 许可
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 用户或角色或其他凭证名称
        /// </summary>
        public virtual string Role { get; set; }
        /// <summary>
        /// 请求url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
