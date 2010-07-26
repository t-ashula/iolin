using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperaLink
{
  /// <summary>
  /// client events
  /// </summary>
  public partial class Client 
  {
    public event EventHandler LastStautsChanged;
    protected virtual void OnLastStatusChanged(EventArgs e)
    {
      if (LastStautsChanged != null)
      {
        LastStautsChanged(this, e);
      }
    }
    
    protected event EventHandler LoginSuccessed;
    protected virtual void OnLoginSuccessed(EventArgs e)
    {
      if (LoginSuccessed != null)
      {
        LoginSuccessed(this, e);
      }
    }

    protected event EventHandler LoginFailed;
    protected virtual void OnLoginFailed(EventArgs e)
    {
      if (LoginFailed != null)
      {
        LoginFailed(this, e);
      }
    }

    public event EventHandler SyncSuccessed;
    protected virtual void OnSyncSuccessed(EventArgs e)
    {
      if (SyncSuccessed != null)
      {
        SyncSuccessed(this, e);
      }
    }

    public event EventHandler SyncFailed;
    protected virtual void OnSyncFailed(EventArgs e)
    {
      if (SyncFailed != null)
      {
        SyncFailed(this, e);
      }
    }
  }
}
