using System;
using System.Linq;
using System.Windows.Forms;

namespace OperaLink.Forms
{
  public partial class BookmarkPanel : ISyncPanel
  {
    public BookmarkPanel(OperaLink.Client c)
      : base(c, "Bookmark")
    {
      InitializeComponent();
      InitViews();

      Dock = DockStyle.Fill;
    }

    private void InitViews()
    {
      InitAdvancedView();
      InitRawListView();
    }

    private void InitAdvancedView()
    {
      //throw new NotImplementedException();
    }

    private void InitRawListView()
    {
      BookmarkRawList.Columns.AddRange(
        new[] { "Type","Name", "Nick", "Address" }
        .Select(n => new ColumnHeader { Text = n })
        .ToArray());
    }

    public override void UpdateSyncItems()
    {
      var items = client_.Bookmarks;
      Utils.ODS(items.Count().ToString());
      BookmarkRawList.Items.Clear();
      BookmarkRawList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] {
          i.Type.ToString(),
          string.IsNullOrEmpty(i.Title) ? "" : i.Title,
          string.IsNullOrEmpty(i.NickName)? "" :i.NickName,
          string.IsNullOrEmpty(i.Uri)? "" : i.Uri.ToString(),
        })).ToArray());
    }
  }
}