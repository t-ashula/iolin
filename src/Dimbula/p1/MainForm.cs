﻿using System;
using System.Windows.Forms;
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
      addSyncPanel(new TypedHistoryPanel(client_));
      // addSyncPanel(new OperaLink.Forms.BookmarkPanel(client_));
      // addSyncPanel(new OperaLink.Forms.NotePanel(client_));
      // addSyncPanel(new OperaLink.Forms.SearchEnginePanel(client_));
      // addSyncPanel(new OperaLink.Forms.SpeedDialPanel(client_));
    }

    private void addSyncPanel(ISyncPanel sp)
    {
      var tp = new TabPage(sp.Title);
      tp.Controls.Add(sp);
      tabControl1.TabPages.Add(tp);
    }

    private void quitQToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
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