using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReplacAllWindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            //dialog.RootFolder = Environment.SpecialFolder.Programs;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
            }
        }



        void init(string[] args)
        {
            //获取当前程序所在的文件路径
            String rootPath = Directory.GetCurrentDirectory();
            string parentPath = Directory.GetParent(rootPath).FullName;//上级目录
            string topPath = Directory.GetParent(parentPath).FullName;//上上级目录
            StreamWriter sw = null;
            try
            {
                //创建输出流，将得到文件名子目录名保存到txt中
                sw = new StreamWriter(new FileStream("fileList.txt", FileMode.Append));
                sw.WriteLine("根目录：" + topPath);
                getDirectory(sw, topPath, 2);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    Console.WriteLine("完成");
                }
            }

        }

        /// <summary>
        /// 获得指定路径下所有文件名
        /// </summary>
        /// <param name="sw">文件写入流</param>
        /// <param name="path">文件写入流</param>
        /// <param name="indent">输出时的缩进量</param>
        public static void getFileName(StreamWriter sw, string path, int indent)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                for (int i = 0; i < indent; i++)
                {
                    sw.Write("  ");
                }
                sw.WriteLine(f.Name);
            }
        }

        /// <summary>
        /// 获得指定路径下所有子目录名
        /// </summary>
        /// <param name="sw">文件写入流</param>
        /// <param name="path">文件夹路径</param>
        /// <param name="indent">输出时的缩进量</param>
        public static void getDirectory(StreamWriter sw, string path, int indent)
        {
            getFileName(sw, path, indent);
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                for (int i = 0; i < indent; i++)
                {
                    sw.Write("  ");
                }
                sw.WriteLine("文件夹：" + d.Name);
                getDirectory(sw, d.FullName, indent + 2);
                sw.WriteLine();
            }
        }
    }
}
