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
      Dock = DockStyle.Fill;
    }

    public override void UpdateSyncItems()
    {
      var items = client_.Bookmarks;
      System.Diagnostics.Debug.WriteLine(items.Count());
      BookmarkList.Items.Clear();
      BookmarkList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] {
          i.Type.ToString(),
          (i.Created != null)? i.Created.ToW3CDTFInUtc() : "",
          i.Title,   
          (i.Uri != null)? i.Uri.ToString() : "",
        })).ToArray());
    }
  }
}