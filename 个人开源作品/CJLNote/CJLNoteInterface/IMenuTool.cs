using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJLNoteInterface
{
    /// <summary>
    /// 菜单插件接口
    /// </summary>
    public interface IMenuTool
    {
        /// <summary>
        /// 下标按照升序排列
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 父菜单
        /// </summary>
        string FatherText { get; }
        /// <summary>
        /// 工具菜单名称
        /// </summary>
        string Text { get; }
        /// <summary>
        /// 对文本进行处理
        /// </summary>
        void Operation(RichTextBox rtb);
    }
}
