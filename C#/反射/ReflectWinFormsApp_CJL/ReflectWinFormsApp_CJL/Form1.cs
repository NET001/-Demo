using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectWinFormsApp_CJL
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
            {
                Text = "工具"
            };
            toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                new ToolStripMenuItem(){
                Text="转小写"
                }
            });
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem });
            this.menuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
}
