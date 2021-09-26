using ReflectInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectExtend_CJL
{
    /// <summary>
    /// 将指定选择内容转换为消息
    /// </summary>
    public class SelectLowerMenuTool : IMenuTool
    {
        public string FatherText => "工具";

        public string Text => "选定部分转小写";

        public void Operation(RichTextBox rtb)
        {
            rtb.SelectedText = rtb.SelectedText.ToLower();
            rtb.SelectedText = rtb.SelectedText.ToUpper();
        }
    }

}
