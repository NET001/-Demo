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
    /// 全部转小写
    /// </summary>
    public class LowerMenuTool : IMenuTool
    {
        public string FatherText => "工具";

        public string Text => "全部转小写";

        public void Operation(RichTextBox rtb)
        {
            rtb.Text = rtb.Text.ToLower();
        }
    }
    /// <summary>
    /// 全部转大写
    /// </summary>
    public class SelectLowerMenuTool : IMenuTool
    {
        public string FatherText => "工具";

        public string Text => "选定部分转小写";

        public void Operation(RichTextBox rtb)
        {
            rtb.SelectedText = rtb.SelectedText.ToLower();
        }
    }

    public class ReplaceAllMenuTool : IMenuTool
    {
        public string FatherText => "查询";

        public string Text => "查询和全部替换";

        public void Operation(RichTextBox rtb)
        {
            new Form1().Show();
        }
    }
    /// <summary>
    /// 目录结构生成
    /// </summary>
    public class DirectoryStructureCreateMenuTool : IMenuTool
    {
        public string FatherText => "工具";

        public string Text => "生成目录结构";

        public void Operation(RichTextBox rtb)
        {
            new OpenFileDialog().ShowDialog();
        }
    }
}
