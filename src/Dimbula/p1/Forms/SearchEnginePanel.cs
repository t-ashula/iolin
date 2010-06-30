using System;
using System.Linq;
using System.Windows.Forms;

namespace OperaLink.Forms
{
  public partial class SearchEnginePanel : ISyncPanel
  {
    public SearchEnginePanel(OperaLink.Client c)
      : base(c, "SearchEngine")
    {
      InitializeComponent();
      Dock = DockStyle.Fill;
    }

    public override void UpdateSyncItems()
    {
      var items = client_.SearchEngines;
      System.Diagnostics.Debug.WriteLine(items.Count());
      SearchEngineList.Items.Clear();
      SearchEngineList.Items.AddRange(
        items.Select(i => new ListViewItem(new string[] {
          i.Type.ToString(), 
          i.Group.ToString(), 
          i.Title!= null ? i.Title.ToString() : "", 
          i.Key != null ? i.Key.ToString() : "", 
          i.Uri != null ? i.Uri.ToString() : "" })).ToArray());
    }
  }
}