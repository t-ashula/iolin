namespace OperaLink.Forms
{
  partial class TypedHistoryPanel
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
      this.TypedHistoryList = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.deleteDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TypedHistoryList
      // 
      this.TypedHistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.TypedHistoryList.ContextMenuStrip = this.contextMenuStrip1;
      this.TypedHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TypedHistoryList.FullRowSelect = true;
      this.TypedHistoryList.Location = new System.Drawing.Point(0, 0);
      this.TypedHistoryList.Name = "TypedHistoryList";
      this.TypedHistoryList.Size = new System.Drawing.Size(454, 327);
      this.TypedHistoryList.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.TypedHistoryList.TabIndex = 0;
      this.TypedHistoryList.UseCompatibleStateImageBehavior = false;
      this.TypedHistoryList.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Date";
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Type";
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Content";
      this.columnHeader3.Width = 282;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteDToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(134, 26);
      // 
      // deleteDToolStripMenuItem
      // 
      this.deleteDToolStripMenuItem.Name = "deleteDToolStripMenuItem";
      this.deleteDToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.deleteDToolStripMenuItem.Text = "Delete(&D)";
      this.deleteDToolStripMenuItem.Click += new System.EventHandler(this.deleteDToolStripMenuItem_Click);
      // 
      // TypedHistoryPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.TypedHistoryList);
      this.Name = "TypedHistoryPanel";
      this.Size = new System.Drawing.Size(454, 327);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView TypedHistoryList;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem deleteDToolStripMenuItem;
  }
}
