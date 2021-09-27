
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CJLNoteInterface;

namespace CJLNoteExtend
{
    /// <summary>
    /// 目录结构生成
    /// </summary>
    public class DirectoryStructureCreateMenuTool : IMenuTool
    {
        public int Index => 0;
        //定义一个list集合
        public string FatherText => "工具";

        public string Text => "生成目录结构";

        public void Operation(RichTextBox rtb)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            //dialog.RootFolder = Environment.SpecialFolder.Programs;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                rtb.Text = "";
                Task.Run(() =>
                {
                    Control.CheckForIllegalCrossThreadCalls = false;
                    director(foldPath, rtb, 0);
                    Control.CheckForIllegalCrossThreadCalls = true;
                });
            }
        }
        public void director(string dirs, RichTextBox rtb, int tier)
        {
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                string str = "";
                string type = "";
                for (int i = 0; i < tier; i++)
                {
                    str += "  ";
                }
                if (fsinfo is DirectoryInfo)
                {
                    type = "├─";
                }
                else
                {
                    type = "│";
                }
                rtb.Text += @"
" + str + type + fsinfo.Name;
                //判断是否为空文件夹　　
                if (fsinfo is DirectoryInfo)
                {
                    //递归调用
                    director(fsinfo.FullName, rtb, tier + 1);
                }
            }
        }
    }
}
