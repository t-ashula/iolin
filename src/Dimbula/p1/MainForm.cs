using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OperaLink.Data;
using OperaLink.Forms;

namespace OperaLink
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      configs_ = new Configs();
      client_ = new Client(configs_);
      initSyncPanels();
      toolStripStatusLabel1.Text = client_.LastStatus;
    }

    private void initSyncPanels()
    {
      var thtab = new OperaLink.Forms.TypedHistoryPanel(client_);
      var tp = new TabPage(thtab.Title);
      tp.Controls.Add(thtab);
      this.tabControl1.TabPages.Add(tp);

      var setab = new OperaLink.Forms.SearchEnginePanel(client_);
      tp = new TabPage(setab.Title);
      tp.Controls.Add(setab);
      this.tabControl1.TabPages.Add(tp);
    }

    private void quitQToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
    {
      (new OperaLink.AboutBox()).Show();
    }

    private OperaLink.Configs configs_;
    private OperaLink.Client client_;
    private OperaLink.ConfigsDialog configDlg_;
    private void configCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      showConfigDlg();
    }

    private void showConfigDlg()
    {
      if (configDlg_ == null)
      {
        configDlg_ = new ConfigsDialog();
        configDlg_.Configs = configs_;
      }
      configDlg_.ShowDialog();
    }

    private void syncSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      sync();
    }

    private void sync()
    {
      try
      {
        if (string.IsNullOrEmpty(configs_.UserName))
        {
          showConfigDlg();
        }
        if (client_.Sync())
        {
          UpdateTables();
        }
        toolStripStatusLabel1.Text = client_.LastStatus;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.StackTrace, ex.Message);
      }
    }
    private void UpdateTables()
    {
      for (int i = 0; i < tabControl1.TabPages.Count; ++i)
      {
        ((ISyncPanel)tabControl1.TabPages[i].Controls[0]).UpdateSyncItems();
      }
    }
  }
}