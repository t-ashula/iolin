namespace OperaLink
{
  partial class MainForm
  {
    /// <summary>
    /// 必要なデザイナ変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナで生成されたコード

    /// <summary>
    /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディタで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.configCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.syncSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.quitQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.helpHToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(639, 26);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileFToolStripMenuItem
      // 
      this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configCToolStripMenuItem,
            this.syncSToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitQToolStripMenuItem});
      this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
      this.fileFToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
      this.fileFToolStripMenuItem.Text = "File(&F)";
      // 
      // configCToolStripMenuItem
      // 
      this.configCToolStripMenuItem.Name = "configCToolStripMenuItem";
      this.configCToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
      this.configCToolStripMenuItem.Text = "Config(&C)";
      this.configCToolStripMenuItem.Click += new System.EventHandler(this.configCToolStripMenuItem_Click);
      // 
      // syncSToolStripMenuItem
      // 
      this.syncSToolStripMenuItem.Name = "syncSToolStripMenuItem";
      this.syncSToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
      this.syncSToolStripMenuItem.Text = "Sync(&S)";
      this.syncSToolStripMenuItem.Click += new System.EventHandler(this.syncSToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
      // 
      // quitQToolStripMenuItem
      // 
      this.quitQToolStripMenuItem.Name = "quitQToolStripMenuItem";
      this.quitQToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
      this.quitQToolStripMenuItem.Text = "Quit(&Q)";
      this.quitQToolStripMenuItem.Click += new System.EventHandler(this.quitQToolStripMenuItem_Click);
      // 
      // helpHToolStripMenuItem
      // 
      this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutAToolStripMenuItem});
      this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
      this.helpHToolStripMenuItem.Size = new System.Drawing.Size(65, 22);
      this.helpHToolStripMenuItem.Text = "Help(&H)";
      // 
      // aboutAToolStripMenuItem
      // 
      this.aboutAToolStripMenuItem.Name = "aboutAToolStripMenuItem";
      this.aboutAToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
      this.aboutAToolStripMenuItem.Text = "About(&A)";
      this.aboutAToolStripMenuItem.Click += new System.EventHandler(this.aboutAToolStripMenuItem_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 460);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(639, 22);
      this.statusStrip1.TabIndex = 1;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
      // 
      // tabControl1
      // 
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 26);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(639, 434);
      this.tabControl1.TabIndex = 2;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(639, 482);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Name = "MainForm";
      this.Text = "iolin";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem configCToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem quitQToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutAToolStripMenuItem;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.ToolStripMenuItem syncSToolStripMenuItem;


  }
}

