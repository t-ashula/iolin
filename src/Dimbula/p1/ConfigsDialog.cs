using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OperaLink
{
  public partial class ConfigsDialog : Form
  {
    public ConfigsDialog()
    {
      InitializeComponent();
    }

    private OperaLink.Configs configs_;
    public OperaLink.Configs Configs
    {
      get { return configs_; }
      set
      {
        if (configs_ == value)
        {
          return;
        } 
        configs_ = value;
        UsernameBox.Text = configs_.UserName;
        PasswordBox.Text = configs_.Password;
      }
    }
    private void OKBtn_Click(object sender, EventArgs e)
    {
      configs_.UserName = UsernameBox.Text;
      configs_.Password = PasswordBox.Text;
      this.Close();
    }

    private void CancelBtn_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
