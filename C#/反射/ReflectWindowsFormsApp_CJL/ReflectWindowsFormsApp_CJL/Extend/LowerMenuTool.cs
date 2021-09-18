using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectWindowsFormsApp_CJL.Extend
{
    public class LowerMenuTool : IMenuTool
    {
        public string FatherText => "工具";

        public string Text => "全部转小写";

        public void Operation(RichTextBox rtb)
        {
            rtb.Text = rtb.Text.ToLower();
        }
    }
    public class SelectLowerMenuTool : IMenuTool
    {
        public string FatherText => "工具";

        public string Text => "选定部分转小写";

        public void Operation(RichTextBox rtb)
        {
            rtb.SelectedText = rtb.SelectedText.ToLower();
        }
    }
}
