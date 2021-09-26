using ReflectExtendCommon_CJL;
using ReflectInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectExtend_CJL
{  /// <summary>
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
