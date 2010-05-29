using System.Linq;
using System.Windows.Forms;

namespace OperaLink.Forms
{
  public partial class TypedHistoryPanel : ISyncPanel
  {
    public TypedHistoryPanel(OperaLink.Client c)
      : base(c, "TypedHistory")
    {
      InitializeComponent();
      Dock = DockStyle.Fill;
    }

    public override void UpdateSyncItems()
    {
      var items = client_.TypedHistories;
      System.Diagnostics.Debug.WriteLine(items.Count());
      this.TypedHistoryList.Items.Clear();
      TypedHistoryList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] {
          i.LastTyped.ToW3cDtfInUtc(), i.Type, i.Content })).ToArray());
    }
  }
}