namespace OperaLink.Forms
{
  partial class SpeedDialDial
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

    #region コンポーネント デザイナで生成されたコード

    /// <summary> 
    /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
    /// コード エディタで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.label1 = new System.Windows.Forms.Label();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.reloadRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reloadEverylToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.neverNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.label1);
      this.splitContainer1.Size = new System.Drawing.Size(150, 150);
      this.splitContainer1.SplitterDistance = 103;
      this.splitContainer1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.label1.Location = new System.Drawing.Point(0, 31);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(150, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteDToolStripMenuItem,
            this.toolStripSeparator1,
            this.reloadRToolStripMenuItem,
            this.reloadEverylToolStripMenuItem,
            this.clearCToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(166, 120);
      // 
      // deleteDToolStripMenuItem
      // 
      this.deleteDToolStripMenuItem.Name = "deleteDToolStripMenuItem";
      this.deleteDToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.deleteDToolStripMenuItem.Text = "Edit(&E)";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
      // 
      // reloadRToolStripMenuItem
      // 
      this.reloadRToolStripMenuItem.Name = "reloadRToolStripMenuItem";
      this.reloadRToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.reloadRToolStripMenuItem.Text = "Reload(&R)";
      // 
      // reloadEverylToolStripMenuItem
      // 
      this.reloadEverylToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neverNToolStripMenuItem});
      this.reloadEverylToolStripMenuItem.Name = "reloadEverylToolStripMenuItem";
      this.reloadEverylToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.reloadEverylToolStripMenuItem.Text = "Reload Every(&l)";
      // 
      // neverNToolStripMenuItem
      // 
      this.neverNToolStripMenuItem.Name = "neverNToolStripMenuItem";
      this.neverNToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.neverNToolStripMenuItem.Text = "Never(&N)";
      // 
      // clearCToolStripMenuItem
      // 
      this.clearCToolStripMenuItem.Name = "clearCToolStripMenuItem";
      this.clearCToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.clearCToolStripMenuItem.Text = "Clear(&C)";
      this.clearCToolStripMenuItem.Click += new System.EventHandler(this.clearCToolStripMenuItem_Click);
      // 
      // SpeedDialDial
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "SpeedDialDial";
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem deleteDToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem reloadRToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reloadEverylToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem neverNToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearCToolStripMenuItem;

  }
}
