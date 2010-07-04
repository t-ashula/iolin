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
      client_.LastStautsChanged += new EventHandler(clientLastStautsChanged);
      client_.SyncFailed += new EventHandler(client__SyncFailed);
      client_.SyncSuccessed += new EventHandler(client__SyncSuccessed);
    }

    void client__SyncSuccessed(object sender, EventArgs e)
    {
      UpdateTables();
    }

    void client__SyncFailed(object sender, EventArgs e)
    {
      MessageBox.Show("Sync Failed");
    }

    private void clientLastStautsChanged(object sender, EventArgs e)
    {
      this.toolStripStatusLabel1.Text = client_.LastStatus;
    }

    private void initSyncPanels()
    {
      addSyncPanel(new OperaLink.Forms.TypedHistoryPanel(client_));
      addSyncPanel(new OperaLink.Forms.BookmarkPanel(client_));
      addSyncPanel(new OperaLink.Forms.NotePanel(client_));
      addSyncPanel(new OperaLink.Forms.SearchEnginePanel(client_));
      addSyncPanel(new OperaLink.Forms.SpeedDialPanel(client_));
    }

    private void addSyncPanel(ISyncPanel sp)
    {
      var tp = new TabPage(sp.Title);
      tp.Controls.Add(sp);
      tabControl1.TabPages.Add(tp);
    }

    private void quitQToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }
    
    private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
    {
      (new OperaLink.AboutBox()).ShowDialog();
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
        client_.Sync();
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