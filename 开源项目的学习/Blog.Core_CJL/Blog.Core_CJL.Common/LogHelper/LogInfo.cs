using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Common.LogHelper
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        public DateTime Datetime { get; set; }
        public string Content { get; set; }
        public string IP { get; set; }
        public string LogColor { get; set; }
        public int Import { get; set; } = 0;
    }
}
