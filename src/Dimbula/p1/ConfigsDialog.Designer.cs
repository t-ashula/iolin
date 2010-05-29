namespace OperaLink
{
  partial class ConfigsDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.OKBtn = new System.Windows.Forms.Button();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.UsernameBox = new System.Windows.Forms.TextBox();
      this.configsBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.PasswordBox = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.configsBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // OKBtn
      // 
      this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OKBtn.Location = new System.Drawing.Point(239, 186);
      this.OKBtn.Name = "OKBtn";
      this.OKBtn.Size = new System.Drawing.Size(75, 23);
      this.OKBtn.TabIndex = 0;
      this.OKBtn.Text = "OK";
      this.OKBtn.UseVisualStyleBackColor = true;
      this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
      // 
      // CancelBtn
      // 
      this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.CancelBtn.Location = new System.Drawing.Point(320, 186);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size(75, 23);
      this.CancelBtn.TabIndex = 1;
      this.CancelBtn.Text = "Cancel";
      this.CancelBtn.UseVisualStyleBackColor = true;
      this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
      // 
      // UsernameBox
      // 
      this.UsernameBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configsBindingSource, "UserName", true));
      this.UsernameBox.ImeMode = System.Windows.Forms.ImeMode.Off;
      this.UsernameBox.Location = new System.Drawing.Point(127, 64);
      this.UsernameBox.Name = "UsernameBox";
      this.UsernameBox.Size = new System.Drawing.Size(268, 19);
      this.UsernameBox.TabIndex = 2;
      // 
      // configsBindingSource
      // 
      this.configsBindingSource.DataSource = typeof(OperaLink.Configs);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(63, 67);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 12);
      this.label1.TabIndex = 3;
      this.label1.Text = "UserName";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(63, 107);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(54, 12);
      this.label2.TabIndex = 4;
      this.label2.Text = "Password";
      // 
      // PasswordBox
      // 
      this.PasswordBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configsBindingSource, "Password", true));
      this.PasswordBox.ImeMode = System.Windows.Forms.ImeMode.Off;
      this.PasswordBox.Location = new System.Drawing.Point(127, 104);
      this.PasswordBox.Name = "PasswordBox";
      this.PasswordBox.PasswordChar = '*';
      this.PasswordBox.Size = new System.Drawing.Size(268, 19);
      this.PasswordBox.TabIndex = 5;
      // 
      // ConfigsDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(407, 220);
      this.Controls.Add(this.PasswordBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.UsernameBox);
      this.Controls.Add(this.CancelBtn);
      this.Controls.Add(this.OKBtn);
      this.Name = "ConfigsDialog";
      this.Text = "Config";
      ((System.ComponentModel.ISupportInitialize)(this.configsBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button OKBtn;
    private System.Windows.Forms.Button CancelBtn;
    private System.Windows.Forms.TextBox UsernameBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox PasswordBox;
    private System.Windows.Forms.BindingSource configsBindingSource;
  }
}