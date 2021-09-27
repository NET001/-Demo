using CJLNoteInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJLNoteExtend
{
    public class SelectUpperMenuTool : IMenuTool
    {
        public int Index => 2;
        public string FatherText => "工具";
        public string Text => "选定部分转大写";
        public void Operation(RichTextBox rtb)
        {
            rtb.SelectedText = rtb.SelectedText.ToUpper();
        }
    }
}