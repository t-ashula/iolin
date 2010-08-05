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
  public partial class SpeedDialDial : UserControl
  {
    public SpeedDialDial()
    {
      InitializeComponent();
    }

    private OperaLink.Client c_;
    public void setClient(OperaLink.Client c)
    {
      c_ = c;
    }

    private void clearSpeedDial()
    {
      c_.DelSpeedDial((OperaLink.Data.SpeedDial)this.Tag);
      this.Tag = null;
    }

    private void clearCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      clearSpeedDial();
    }
  }
}
