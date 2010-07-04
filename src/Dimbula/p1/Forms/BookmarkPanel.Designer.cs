namespace OperaLink.Forms
{
  partial class BookmarkPanel
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
      this.BookmarkRawList = new System.Windows.Forms.ListView();
      this.BookmarkTreeView = new System.Windows.Forms.TabPage();
      this.BookmarkTabPages = new System.Windows.Forms.TabControl();
      this.BookmarkRawView = new System.Windows.Forms.TabPage();
      this.BookmarkTabPages.SuspendLayout();
      this.BookmarkRawView.SuspendLayout();
      this.SuspendLayout();
      // 
      // BookmarkRawList
      // 
      this.BookmarkRawList.FullRowSelect = true;
      this.BookmarkRawList.Location = new System.Drawing.Point(3, 3);
      this.BookmarkRawList.Name = "BookmarkRawList";
      this.BookmarkRawList.Size = new System.Drawing.Size(440, 292);
      this.BookmarkRawList.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.BookmarkRawList.TabIndex = 0;
      this.BookmarkRawList.UseCompatibleStateImageBehavior = false;
      this.BookmarkRawList.View = System.Windows.Forms.View.Details;
      // 
      // BookmarkTreeView
      // 
      this.BookmarkTreeView.Location = new System.Drawing.Point(4, 25);
      this.BookmarkTreeView.Name = "BookmarkTreeView";
      this.BookmarkTreeView.Padding = new System.Windows.Forms.Padding(3);
      this.BookmarkTreeView.Size = new System.Drawing.Size(446, 298);
      this.BookmarkTreeView.TabIndex = 0;
      this.BookmarkTreeView.Text = "TreeView";
      this.BookmarkTreeView.UseVisualStyleBackColor = true;
      // 
      // BookmarkTabPages
      // 
      this.BookmarkTabPages.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.BookmarkTabPages.Controls.Add(this.BookmarkTreeView);
      this.BookmarkTabPages.Controls.Add(this.BookmarkRawView);
      this.BookmarkTabPages.Dock = System.Windows.Forms.DockStyle.Fill;
      this.BookmarkTabPages.Location = new System.Drawing.Point(0, 0);
      this.BookmarkTabPages.Name = "BookmarkTabPages";
      this.BookmarkTabPages.SelectedIndex = 0;
      this.BookmarkTabPages.Size = new System.Drawing.Size(454, 327);
      this.BookmarkTabPages.TabIndex = 1;
      // 
      // BookmarkRawView
      // 
      this.BookmarkRawView.Controls.Add(this.BookmarkRawList);
      this.BookmarkRawView.Location = new System.Drawing.Point(4, 25);
      this.BookmarkRawView.Name = "BookmarkRawView";
      this.BookmarkRawView.Padding = new System.Windows.Forms.Padding(3);
      this.BookmarkRawView.Size = new System.Drawing.Size(446, 298);
      this.BookmarkRawView.TabIndex = 1;
      this.BookmarkRawView.Text = "Raw";
      this.BookmarkRawView.UseVisualStyleBackColor = true;
      // 
      // BookmarkPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.BookmarkTabPages);
      this.Name = "BookmarkPanel";
      this.Size = new System.Drawing.Size(454, 327);
      this.BookmarkTabPages.ResumeLayout(false);
      this.BookmarkRawView.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl BookmarkTabPages;
    private System.Windows.Forms.TabPage BookmarkTreeView;
    private System.Windows.Forms.TabPage BookmarkRawView;
    private System.Windows.Forms.ListView BookmarkRawList;

  }
}
