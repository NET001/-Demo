using CJLNoteInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJLNoteExtend
{
    public class StringReplaceMenuTool : IMenuTool
    {
        public int Index => 2;
        public string FatherText => "工具";

        public string Text => "字符串替换";
        public void Operation(RichTextBox rtb)
        {
            new StringReplaceForm(rtb).Show();
        }
    }
}