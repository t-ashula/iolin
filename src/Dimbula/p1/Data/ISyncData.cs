using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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
  public class ISyncDataWrapper<ContentData> : IEqualityComparer<ISyncDataWrapper<ContentData>>
  {
    public ISyncDataWrapper() { }
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
    public virtual void ModContent(ISyncDataWrapper<ContentData> other) { /* nothing */ }


    #region IEqualityComparer<ISyncDataWrapper<ContentData>> メンバ

    public bool Equals(ISyncDataWrapper<ContentData> x, ISyncDataWrapper<ContentData> y)
    {
      return x.IsSameContent(y);
    }

    public int GetHashCode(ISyncDataWrapper<ContentData> obj)
    {
      return (1023 * obj.State.GetHashCode()) ^ obj.Content.GetHashCode();
    }

    #endregion
  }

  /// <summary>
  /// OperaLink data manager
  /// </summary>
  /// <typeparam name="ContentData">Data type</typeparam>
  public class ISyncDataManager<ContentData, DataWrapper>
    where DataWrapper : ISyncDataWrapper<ContentData>, new()
  {
    private List<DataWrapper> inner_items_;
    private List<DataWrapper> to_sync_items_;
    private readonly string[] OwnElements;
    
    /// <summary>
    /// ctor.
    /// </summary>
    public ISyncDataManager( string[] ownElements )
    {
      inner_items_ = new List<DataWrapper>();
      to_sync_items_ = new List<DataWrapper>();
      OwnElements = ownElements;
    }

    /// <summary>
    /// create xml string to sync OperaLink 
    /// </summary>
    /// <returns>OperaLink xml string</returns>
    public virtual string ToOperaLinkXml()
    {
      return to_sync_items_.Aggregate("", (x, i) => x + i.ToOperaLinkXml());
    }

    /// <summary>
    /// update internal list from OperaLink 
    /// </summary>
    /// <param name="xmlString">xml string from OperaLink server contains 0 or more elements</param>
    public virtual void FromOperaLinkXml(string xmlString)
    {
      if (string.IsNullOrEmpty(xmlString))
      {
        return;
      }
      var xd = new XmlDocument();
      xd.LoadXml(xmlString);
      foreach (var elementName in OwnElements)
      {
        var eles = xd.GetElementsByTagName(elementName);
        for (int i = 0; i < eles.Count; ++i)
        {
          var item = new DataWrapper();
          item.FromOperaLinkXml(eles[i].OuterXml);
          ChangeInnerList(item);
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    protected void ChangeInnerList(DataWrapper d)
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
      var item = new DataWrapper
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
    private bool addItem(DataWrapper d)
    {
      if (inner_items_.Exists(i => i.IsSameContent(d)))
      {
        return false;
      }
      inner_items_.Add(d);
      return true;
    }
    private bool addSyncItem(DataWrapper d)
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
      var item = new DataWrapper
      {
        State = SyncState.Modified,
        Content = d
      };
      modItem(item);
      modSyncItem(item);
      return inner_items_.Count;
    }
    private bool modItem(DataWrapper d)
    {
      var idx = inner_items_.FindIndex(i => i.IsSameContent(d));
      if (idx < 0)
      {
        return false;
      }
      inner_items_[idx].ModContent(d);
      return true;
    }
    private bool modSyncItem(DataWrapper d)
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
      var item = new DataWrapper
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
    private bool delItem(DataWrapper d)
    {
      var idx = inner_items_.ToList().FindIndex(i => i.IsSameContent(d));
      if (idx < 0)
      {
        return false;
      }
      inner_items_.RemoveAt(idx);
      return true;
    }
    private bool delSyncItem(DataWrapper d)
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
    public void SyncDone() { to_sync_items_.Clear(); }
  }
}