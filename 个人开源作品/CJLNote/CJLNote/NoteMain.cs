using CJLNoteInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJLNote
{
    public partial class NoteMain : Form
    {
        private List<IMenuTool> menuTools = new List<IMenuTool>();
        public NoteMain()
        {
            InitializeComponent();
            MenuToolInit();
        }
        /// <summary>
        /// 菜单工具栏初始化
        /// </summary>
        private void MenuToolInit()
        {
            //获取程序集所在路径
            var path = Path.Combine(Path.GetFullPath("."), "Extend");
            //判断是否
            if (Directory.Exists(path))
            {
                //匹配所有dll
                string[] files = Directory.GetFiles(path, "*.dll");
                if (files.Length == 0)
                {
                    MessageBox.Show("无任何扩展项");
                    return;
                }
                //获取dll程序集
                foreach (var item in files)
                {
                    Assembly assembly = Assembly.LoadFrom(item);
                    //获取其中的类
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        //判断是否实现了工具类接口
                        if (typeof(IMenuTool).IsAssignableFrom(type))
                        {
                            //实例化并保存
                            menuTools.Add((IMenuTool)Activator.CreateInstance(type));
                        }
                    }
                }
                //初始化菜单栏
                List<MenuTools> menuToolList = menuTools.GroupBy(t => t.FatherText).Select(t => new MenuTools()
                {
                    Name = t.Key,
                    MenuToolList = menuTools.Where(tt => tt.FatherText == t.Key).ToList()
                }).ToList();
                //构造界面
                List<ToolStripMenuItem> toolStripMenuItemList = new List<ToolStripMenuItem>();
                foreach (var item in menuToolList)
                {
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
                    {
                        Text = item.Name
                    };
                    List<ToolStripItem> toolStripItems = new List<ToolStripItem>();
                    foreach (var sub in item.MenuToolList)
                    {
                        ToolStripItem toolStripItem = new ToolStripMenuItem()
                        {
                            Text = sub.Text,
                            Tag = sub
                        };
                        toolStripItem.Click += ToolStripMenuItem_Click;
                        toolStripItems.Add(toolStripItem);
                    }
                    toolStripMenuItem.DropDownItems.AddRange(toolStripItems.ToArray());
                    toolStripMenuItemList.Add(toolStripMenuItem);
                }
                this.menuStrip1.Items.AddRange(toolStripMenuItemList.ToArray());
            }
            else
            {
                MessageBox.Show("运行路径下不存在Extend扩展文件夹");
            }
        }
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            //执行相应操作
            ((IMenuTool)toolStripMenuItem.Tag).Operation(richTextBox1);
        }
    }
    class MenuTools
    {
        public string Name { get; set; }
        public List<IMenuTool> MenuToolList { get; set; }
    }
}
