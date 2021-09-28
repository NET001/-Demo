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
        private void btnReplace_Click(object sender, EventArgs e)
        {
            rtb.SelectAll();
            rtb.AppendText(Regex.Replace(rtb.Text, rtbQuery.Text, rtbReplace.Text));
        }
    }
}
