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
      OperaLink.Utils.ODS(items.Count().ToString());
      TypedHistoryList.Items.Clear();
      TypedHistoryList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] { i.LastTyped.ToW3CDTFInUtc(), i.Type, i.Content }) { Tag = i }).ToArray());
    }

    private void deleteDToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      DeleteSelectedItems();
      UpdateSyncItems();
    }

    private void DeleteSelectedItems()
    {
      TypedHistoryList
        .SelectedItems.OfType<ListViewItem>()
        .Select(i => (OperaLink.Data.TypedHistory)i.Tag).ToList()
        .ForEach(i => client_.DelTypedHistory(i));
    }
  }
}