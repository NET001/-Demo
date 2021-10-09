using CJLNoteInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJLNoteExtend
{
    /// <summary>
    /// C# 提取字段
    /// </summary>
    public class ExtractFieldMenuTool : IMenuTool
    {
        public int Index => 3;
        public string FatherText => "工具";

        public string Text => "取出C#属性中的属性值";

        public void Operation(RichTextBox rtb)
        {
            string str = rtb.Text;
            //移除注释
            str = Regex.Replace(str, "/.*?\n", "");
            //移除 { get; set; }
            str = Regex.Replace(str, @" \{ get; set; \}", "");
            //移除public xxxxx 
            str = Regex.Replace(str, "public .+? ", "");
            //移除空白
            str = Regex.Replace(str, " ", "");
            //移除多余的换行
            str = Regex.Replace(str, "\n\n", "\n");
            rtb.Text = str;
        }
    }
}
