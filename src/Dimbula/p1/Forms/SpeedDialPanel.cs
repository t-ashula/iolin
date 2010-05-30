using System;
using System.Linq;
using System.Windows.Forms;

namespace OperaLink.Forms
{
  public partial class SpeedDialPanel : ISyncPanel
  {
    public SpeedDialPanel(OperaLink.Client c)
      : base(c, "SpeedDial")
    {
      InitializeComponent();
      Dock = DockStyle.Fill;
    }

    public override void UpdateSyncItems()
    {
      var items = client_.SpeedDials;
      System.Diagnostics.Debug.WriteLine(items.Count());
      SpeedDialList.Items.Clear();
      SpeedDialList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] {
          i.Position.ToString(), 
          i.Title!= null ? i.Title.ToString() : "", 
          i.Uri != null ? i.Uri.ToString() : "" })).ToArray());
    }
  }
}