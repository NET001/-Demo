using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectWindowsFormsApp_CJL
{
    /// <summary>
    /// 菜单插件接口
    /// </summary>
    public interface IMenuTool
    {
        /// <summary>
        /// 父菜单
        /// </summary>
        string FatherText { get;}
        /// <summary>
        /// 工具菜单名称
        /// </summary>
        string Text { get;  }
        /// <summary>
        /// 对文本进行处理
        /// </summary>
        void Operation(RichTextBox rtb);
    }
}
