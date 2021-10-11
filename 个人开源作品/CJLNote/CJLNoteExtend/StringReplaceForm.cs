using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJLNoteExtend
{

    public partial class StringReplaceForm : Form
    {
        RichTextBox rtb = new RichTextBox();
        public StringReplaceForm(RichTextBox rtb)
        {
            this.rtb = rtb;
            InitializeComponent();
        }
        //替换
        private void btnReplace_Click(object sender, EventArgs e)
        {
            rtb.Text = Regex.Replace(rtb.Text, rtbQuery.Text, rtbReplace.Text);
        }
        //搜索
        private void btnSelect_Click(object sender, EventArgs e)
        {
            string result = "";
            var strs = Regex.Matches(rtb.Text, rtbSelect.Text);
            foreach (var item in strs)
            {
                result += item.ToString() + @"
";
            }
            rtb.Text = result;
        }
    }
}
