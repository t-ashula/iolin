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
<<<<<<< HEAD
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
=======
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
>>>>>>> adc8b8bbf751bd573a6890289b283baf468bbeb4
        })).ToArray());
    }
  }
}