using System;
using System.Linq;
using System.Windows.Forms;

namespace OperaLink.Forms
{
  public partial class NotePanel : ISyncPanel
  {
    public NotePanel(OperaLink.Client c)
      : base(c, "Note")
    {
      InitializeComponent();
      Dock = DockStyle.Fill;
    }

    public override void UpdateSyncItems()
    {
      var items = client_.Notes;
      System.Diagnostics.Debug.WriteLine(items.Count());
      NoteList.Items.Clear();
      NoteList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] {
          i.Type.ToString(),
          (i.Created != null)? i.Created.ToW3CDTFInUtc() : "",
          i.Content,   
          (i.Uri != null)? i.Uri.ToString() : "",
        })).ToArray());
    }
  }
}