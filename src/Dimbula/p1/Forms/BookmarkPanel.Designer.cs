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
<<<<<<< HEAD
      this.BookmarkList = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // BookmarkList
      // 
      this.BookmarkList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
      this.BookmarkList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.BookmarkList.FullRowSelect = true;
      this.BookmarkList.Location = new System.Drawing.Point(0, 0);
      this.BookmarkList.Name = "BookmarkList";
      this.BookmarkList.Size = new System.Drawing.Size(454, 327);
      this.BookmarkList.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.BookmarkList.TabIndex = 0;
      this.BookmarkList.UseCompatibleStateImageBehavior = false;
      this.BookmarkList.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Type";
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Content";
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Uri";
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Created";
=======
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
>>>>>>> adc8b8bbf751bd573a6890289b283baf468bbeb4
      // 
      // BookmarkPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
      this.Controls.Add(this.BookmarkList);
      this.Name = "BookmarkPanel";
      this.Size = new System.Drawing.Size(454, 327);
=======
      this.Controls.Add(this.BookmarkTabPages);
      this.Name = "BookmarkPanel";
      this.Size = new System.Drawing.Size(454, 327);
      this.BookmarkTabPages.ResumeLayout(false);
      this.BookmarkRawView.ResumeLayout(false);
>>>>>>> adc8b8bbf751bd573a6890289b283baf468bbeb4
      this.ResumeLayout(false);

    }

    #endregion

<<<<<<< HEAD
    private System.Windows.Forms.ListView BookmarkList;   
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
=======
    private System.Windows.Forms.TabControl BookmarkTabPages;
    private System.Windows.Forms.TabPage BookmarkTreeView;
    private System.Windows.Forms.TabPage BookmarkRawView;
    private System.Windows.Forms.ListView BookmarkRawList;

>>>>>>> adc8b8bbf751bd573a6890289b283baf468bbeb4
  }
}
