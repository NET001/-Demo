using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Model.MessageCore
{
    /// <summary>
    /// 通信分页类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前标
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; } = 6;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int dataCount { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> data { get; set; }
    }
}
