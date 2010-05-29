using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperaLink.Data
{
  /// <summary>
  /// Opera Link Data State
  /// </summary>
  public enum SyncState { Added, Deleted, Modified };

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="ContentData"></typeparam>
  public class ISyncDataWrapper<ContentData>
  {
    /// <summary>
    /// Data content
    /// </summary>
    public ContentData Content { get; set; }
    /// <summary>
    /// Data State
    /// </summary>
    public SyncState State { get; set; }
    /// <summary>
    /// convert data into operalink xml
    /// </summary>
    /// <returns></returns>
    public virtual string ToOperaLinkXml() { return ""; }
    /// <summary>
    /// assign data from operalink xml
    /// </summary>
    /// <param name="xmlString">operalink xml; only one element</param>
    public virtual void FromOperaLinkXml(string xmlString) { /* nothing */ }
    /// <summary>
    /// compare data content to other instance
    /// </summary>
    /// <param name="other">other instance</param>
    /// <returns>return true if same data in operalink context</returns>
    public virtual bool IsSameContent(ISyncDataWrapper<ContentData> other) { return false; }
    /// <summary>
    /// modify content from other instance
    /// </summary>
    /// <param name="other">other instance</param>
    public virtual void ModContent(ISyncDataWrapper<ContentData> other){/* nothing */}
  }

  /// <summary>
  /// OperaLink data manager
  /// </summary>
  /// <typeparam name="ContentData">Data type</typeparam>
  public class ISyncDataManager<ContentData>
  {
    private List<ISyncDataWrapper<ContentData>> inner_items_;
    private List<ISyncDataWrapper<ContentData>> to_sync_items_;

    /// <summary>
    /// ctor.
    /// </summary>
    public ISyncDataManager()
    {
      inner_items_ = new List<ISyncDataWrapper<ContentData>>();
      to_sync_items_ = new List<ISyncDataWrapper<ContentData>>();
    }

    /// <summary>
    /// create xml string to sync OperaLink 
    /// </summary>
    /// <returns>OperaLink xml string</returns>
    public string ToOperaLinkXml()
    {
      return to_sync_items_.Aggregate("", (x, i) => x + i.ToOperaLinkXml());
    }

    /// <summary>
    /// update internal list from OperaLink 
    /// </summary>
    /// <param name="xmlString">xml string from OperaLink server containes 0 or more elements</param>
    public virtual void FromOperaLinkXml(string xmlString) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    protected void ChangeInnerList(ISyncDataWrapper<ContentData> d)
    {
      switch (d.State)
      {
        case SyncState.Added: addItem(d); break;
        case SyncState.Modified: modItem(d); break;
        case SyncState.Deleted: delItem(d); break;
      }
    }

    /// <summary>
    /// Add Data to sync
    /// </summary>
    /// <param name="d">data</param>
    /// <returns>list items count.</returns>
    public int Add(ContentData d)
    {
      var item = new ISyncDataWrapper<ContentData>
      {
        Content = d,
        State = SyncState.Added
      };
      if (addItem(item))
      {
        addSyncItem(item);
      }
      return inner_items_.Count;
    }
    private bool addItem(ISyncDataWrapper<ContentData> d)
    {
      if (inner_items_.Exists(i => i.IsSameContent(d)))
      {
        return false;
      }
      inner_items_.Add(d);
      return true;
    }
    private bool addSyncItem(ISyncDataWrapper<ContentData> d)
    {
      to_sync_items_.Add(d);
      return true;
    }

    /// <summary>
    /// update data.
    /// </summary>
    /// <param name="d">to update content</param>
    /// <returns>list items count</returns>
    public int Mod(ContentData d)
    {
      var item = new ISyncDataWrapper<ContentData>
      {
        State = SyncState.Modified,
        Content = d
      };
      modItem(item);
      modSyncItem(item);
      return inner_items_.Count;
    }
    private bool modItem(ISyncDataWrapper<ContentData> d)
    {
      var idx = inner_items_.FindIndex(i => i.IsSameContent(d));
      if (idx < 0)
      {
        return false;
      }
      inner_items_[idx].ModContent(d);
      return true;
    }
    private bool modSyncItem(ISyncDataWrapper<ContentData> d)
    {
      var idx = to_sync_items_.FindIndex(i => i.IsSameContent(d));
      if (idx < 0)
      {
        return false;
      }
      if (to_sync_items_[idx].State == SyncState.Deleted)
      {
        return false;
      }
      to_sync_items_[idx].ModContent(d);
      return true;
    }

    /// <summary>
    /// delete data
    /// </summary>
    /// <param name="d">to delete content</param>
    /// <returns>list items count</returns>
    public int Del(ContentData d)
    {
      var item = new ISyncDataWrapper<ContentData>
      {
        State = SyncState.Deleted,
        Content = d
      };
      if (delItem(item))
      {
        delSyncItem(item);
      }
      return inner_items_.Count;
    }
    private bool delItem(ISyncDataWrapper<ContentData> d)
    {
      var idx = inner_items_.ToList().FindIndex(i => i.IsSameContent(d));
      if (idx < 0)
      {
        return false;
      }
      inner_items_.RemoveAt(idx);
      return true;
    }
    private bool delSyncItem(ISyncDataWrapper<ContentData> d)
    {
      var in_sync = to_sync_items_.FindIndex(i => i.IsSameContent(d));
      if (in_sync < 0)
      {
        d.State = SyncState.Deleted;
        to_sync_items_.Add(d);
      }
      else
      {
        to_sync_items_[in_sync].State = SyncState.Deleted;
      }
      return true;
    }

    /// <summary>
    /// Content data only list
    /// </summary>
    public IEnumerable<ContentData> Items { get { return inner_items_.Select(i => i.Content); } }

    /// <summary>
    /// Content state only list
    /// </summary>
    public IEnumerable<SyncState> States { get { return inner_items_.Select(i => i.State); } }

    /// <summary>
    /// load data from local file such as speeddial.ini etc.
    /// </summary>
    /// <param name="storagePath">file path</param>
    /// <returns>data count load successed</returns>
    public virtual int LoadFromLocalStorage(string storagePath) { return -1; }

    /// <summary>
    /// save data to local file such as speeddial.ini etc.
    /// </summary>
    /// <param name="storagePath">file path</param>
    /// <returns>return true if save successed</returns>
    public virtual bool SaveToLocalStorage(string storagePath) { return false; }

    /// <summary>
    /// clear to sync data list
    /// </summary>
    public void SyncDone()
    {
      to_sync_items_.Clear();
    }
  }
}