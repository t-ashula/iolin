using System;
using System.Xml;
namespace OperaLink
{
  public class Configs
  {
    private readonly string DEFAULT_USERAGENT_STRING = "Opera/9.80 (Windows NT 6.1; U; en) Presto/2.6.30 Version/10.60";
    public enum DeviceTypes
    {
      desktop,
    };
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public bool SyncBookmark { get; set; }
    public bool SyncPersonalBar { get; set; }
    public bool SyncTypedHistory { get; set; }
    public bool SyncSpeedDial { get; set; }
    public bool SyncNotes { get; set; }
    public bool SyncSearches { get; set; }
    public string UserAgent { get; set; }
    public string SystemName { get; set; }
    public int BuildNumber { get; set; }
    public DeviceTypes DeviceType { get; set; }
    public string DeviceTypeString { get { return DeviceType.ToString().ToLower(); } }
    public Configs()
    {
      UserAgent = DEFAULT_USERAGENT_STRING;
      Password = "";
      UserName = "";
      SyncBookmark = true;
      SyncNotes = true;
      SyncPersonalBar = true;
      SyncSearches = true;
      SyncSpeedDial = true;
      SyncTypedHistory = true;
      SystemName = "win32";
      BuildNumber = 3445;
      DeviceType = DeviceTypes.desktop;
    }
  }
}