using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OperaLink.Forms
{
  public partial class ISyncPanel : UserControl
  {
    public ISyncPanel()
    {
      InitializeComponent();
    }

    public ISyncPanel(OperaLink.Client c, String name) : this()
    {
      Title = name;
      client_ = c;
    }

    public string Title { get; private set; }
    protected OperaLink.Client client_;
    public virtual void UpdateSyncItems(){}
  }
}
