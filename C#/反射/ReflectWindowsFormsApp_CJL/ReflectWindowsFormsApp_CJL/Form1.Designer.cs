
namespace ReflectWindowsFormsApp_CJL
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具栏1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具栏2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.栏位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.栏位ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.栏位ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 39);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 399);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具栏1ToolStripMenuItem,
            this.工具栏2ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // 工具栏1ToolStripMenuItem
            // 
            this.工具栏1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.栏位ToolStripMenuItem,
            this.栏位ToolStripMenuItem1});
            this.工具栏1ToolStripMenuItem.Name = "工具栏1ToolStripMenuItem";
            this.工具栏1ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.工具栏1ToolStripMenuItem.Text = "工具栏1";
            this.工具栏1ToolStripMenuItem.Click += new System.EventHandler(this.工具栏1ToolStripMenuItem_Click);
            // 
            // 工具栏2ToolStripMenuItem
            // 
            this.工具栏2ToolStripMenuItem.Name = "工具栏2ToolStripMenuItem";
            this.工具栏2ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.工具栏2ToolStripMenuItem.Text = "工具栏2";
            this.工具栏2ToolStripMenuItem.Click += new System.EventHandler(this.工具栏1ToolStripMenuItem_Click);
            // 
            // 栏位ToolStripMenuItem
            // 
            this.栏位ToolStripMenuItem.Name = "栏位ToolStripMenuItem";
            this.栏位ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.栏位ToolStripMenuItem.Text = "栏位";
            // 
            // 栏位ToolStripMenuItem1
            // 
            this.栏位ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.栏位ToolStripMenuItem2});
            this.栏位ToolStripMenuItem1.Name = "栏位ToolStripMenuItem1";
            this.栏位ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.栏位ToolStripMenuItem1.Text = "栏位";
            // 
            // 栏位ToolStripMenuItem2
            // 
            this.栏位ToolStripMenuItem2.Name = "栏位ToolStripMenuItem2";
            this.栏位ToolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.栏位ToolStripMenuItem2.Text = "栏位";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具栏1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具栏2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 栏位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 栏位ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 栏位ToolStripMenuItem2;
    }
}

