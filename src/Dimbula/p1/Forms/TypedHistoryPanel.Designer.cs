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
      this.TypedHistoryList = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // TypedHistoryList
      // 
      this.TypedHistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.TypedHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
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
      // TypedHistoryPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.TypedHistoryList);
      this.Name = "TypedHistoryPanel";
      this.Size = new System.Drawing.Size(454, 327);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView TypedHistoryList;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
  }
}
