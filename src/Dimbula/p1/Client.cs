using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Threading;
using OperaLink.Data;

namespace OperaLink
{
  /// <summary>
  /// 
  /// </summary>
  public partial class Client
  {
    private readonly string LOGIN_API = "https://auth.opera.com/xml";
    private readonly string LINK_API = "https://link-server.opera.com/pull";

    private OperaLink.Configs conf_;
    private long sync_state_;
    private int short_interval_;
    private int long_interval_;
    private string token_;

    private XmlWriterSettings xml_settings_;
    private Encoding enc_;
    private TypedHistoryManager typeds_;
    private SpeedDialManager sds_;
    private SearchEngineManager ses_;
    private NoteManager notes_;
    private BookmarkManager bms_;

    public IEnumerable<TypedHistory> TypedHistories { get { return typeds_.Items; } }
    public IEnumerable<SearchEngine> SearchEngines { get { return ses_.Items; } }
    public IEnumerable<SpeedDial> SpeedDials { get { return sds_.Items; } }
    public IEnumerable<Note> Notes { get { return notes_.Items; } }
    public IEnumerable<Bookmark> Bookmarks { get { return bms_.Items; } }

    private string lastStatus_;
    public string LastStatus
    {
      get { return lastStatus_; }
      private set
      {
        if (value != lastStatus_)
        {
          lastStatus_ = value;
          OnLastStatusChanged(new EventArgs());
        }
      }
    }

    public bool Logined { get { return !String.IsNullOrEmpty(this.token_); } }

    public Client(OperaLink.Configs conf)
    {
      conf_ = conf;
      typeds_ = new TypedHistoryManager();
      ses_ = new SearchEngineManager();
      sds_ = new SpeedDialManager();
      notes_ = new NoteManager();
      bms_ = new BookmarkManager();
      xml_settings_ = new XmlWriterSettings
      {
        Encoding = System.Text.Encoding.UTF8,
        NewLineOnAttributes = false,
      };
      sync_state_ = 0;
      short_interval_ = 60;
      long_interval_ = 120;
      token_ = "";
      LastStatus = "";
      enc_ = Encoding.GetEncoding("utf-8");
    }

    private string createLoginXml()
    {
      var x = "";
      using (var ms = new MemoryStream())
      {
        using (var xw = XmlWriter.Create(ms, xml_settings_))
        {
          xw.WriteStartDocument();
          xw.WriteStartElement("auth", OperaLinkXmlNameSpaces.AUTH_XML_NAME_SPACE);
          xw.WriteAttributeString("version", "1.1");
          xw.WriteStartElement("login");
          xw.WriteStartElement("username"); xw.WriteString(conf_.UserName); xw.WriteEndElement();
          xw.WriteStartElement("password"); xw.WriteString(conf_.Password); xw.WriteEndElement();
          xw.WriteEndElement();
          xw.WriteEndElement();
          xw.WriteEndDocument();
        }
        ms.Position = 0;
        x = (new StreamReader(ms)).ReadToEnd();
      }
      return x;
    }

    public bool Login()
    {
      LastStatus = "Login to OperaLink Server... ";
      var loginxml = createLoginXml();
      Utils.ODS(loginxml);

      var wc = new WebClient();
      wc.Headers["User-Agent"] = conf_.UserAgent;
      //wc.UploadDataCompleted += new UploadDataCompletedEventHandler(uploadLoginXmlCompleted);
      var res = wc.UploadData(new Uri(LOGIN_API), "POST", enc_.GetBytes(loginxml));
      return uploadLoginXmlCompleted(res);
    }

    bool uploadLoginXmlCompleted(byte[] res)
    {
      var msg = "";
      int code = -1;
      token_ = null;
      using (var ms = new MemoryStream())
      {
        ms.Write(res, 0, res.Count());
        ms.Position = 0;
        var xrs = new XmlReaderSettings();
        using (var xr = XmlTextReader.Create(ms, xrs))
        {
          try
          {
            xr.ReadStartElement("auth");
            xr.ReadStartElement("token"); token_ = xr.ReadContentAsString(); xr.ReadEndElement();
            xr.ReadStartElement("message"); msg = xr.ReadContentAsString(); xr.ReadEndElement();
            xr.ReadStartElement("code"); code = xr.ReadContentAsInt(); xr.ReadEndElement();
            xr.ReadEndElement();
          }
          catch (XmlException ex)
          {
            OperaLink.Utils.ODS(ex.Message);
            LastStatus = "Login Failed." + ex.Message;
            return false;
          }
        }
      }

      //return resXml.IndexOf("Login successful") != -1;
      OperaLink.Utils.ODS(string.Format("{0}:{1}", "token", token_));
      OperaLink.Utils.ODS(string.Format("{0}:{1}", "message", msg));
      OperaLink.Utils.ODS(string.Format("{0}:{1}", "code", code));
      return code == 200;
    }

    private string createLinkXml()
    {
      var x = "";
      using (var ms = new MemoryStream())
      {
        using (var xw = XmlWriter.Create(ms))
        {
          xw.WriteStartDocument();
          xw.WriteStartElement("link", OperaLinkXmlNameSpaces.LINK_XML_NAME_SPACE);
          xw.WriteAttributeString("version", "1.0");
          xw.WriteAttributeString("user", conf_.UserName);
          xw.WriteAttributeString("password", conf_.Password);
          xw.WriteAttributeString("syncstate", (sync_state_).ToString());
          xw.WriteAttributeString("dirty", "0");
          xw.WriteStartElement("clientinfo");
          xw.WriteStartElement("build"); xw.WriteString(conf_.BuildNumber.ToString()); xw.WriteEndElement();
          xw.WriteStartElement("system"); xw.WriteString(conf_.SystemName); xw.WriteEndElement();
          if (conf_.SyncBookmark) { xw.WriteStartElement("supports"); xw.WriteString("bookmark"); xw.WriteEndElement(); }
          if (conf_.SyncNotes) { xw.WriteStartElement("supports"); xw.WriteString("note"); xw.WriteEndElement(); }
          //if (conf_.SyncPersonalBar) { xw.WriteStartElement("supports"); xw.WriteString("bookmark"); xw.WriteEndElement(); }
          if (conf_.SyncSearches) { xw.WriteStartElement("supports"); xw.WriteString("search_engine"); xw.WriteEndElement(); }
          if (conf_.SyncSpeedDial) { xw.WriteStartElement("supports"); xw.WriteString("speeddial"); xw.WriteEndElement(); }
          if (conf_.SyncTypedHistory) { xw.WriteStartElement("supports"); xw.WriteString("typed_history"); xw.WriteEndElement(); }
          
          xw.WriteEndElement();
          xw.WriteStartElement("data");
          if (conf_.SyncBookmark) { xw.WriteRaw(bms_.ToOperaLinkXml()); }
          if (conf_.SyncNotes) { xw.WriteRaw(notes_.ToOperaLinkXml()); }
          if (conf_.SyncSearches) { xw.WriteRaw(ses_.ToOperaLinkXml()); }
          if (conf_.SyncSpeedDial) { xw.WriteRaw(sds_.ToOperaLinkXml()); }
          if (conf_.SyncTypedHistory) { xw.WriteRaw(typeds_.ToOperaLinkXml()); }
          xw.WriteEndElement();
          xw.WriteEndElement();
          xw.WriteEndDocument();
          xw.Flush();
        }
        ms.Position = 0;
        x = (new StreamReader(ms)).ReadToEnd();
      }
      return x;
    }

    public bool Sync()
    {
      if (!Logined)
      {
        if (!Login())
        {
          LastStatus = "Login Failed";
          OnSyncFailed(new EventArgs());
          return false;
        }
      }
      var lxml = createLinkXml();
      Utils.ODS(lxml);
      var wc = new WebClient(); ;
      wc.Headers["User-Agent"] = conf_.UserAgent;
      wc.UploadDataCompleted += new UploadDataCompletedEventHandler(uploadSyncXmlCompleted);
      wc.UploadDataAsync(new Uri(LINK_API), "POST", enc_.GetBytes(lxml));
      return true;
    }

    private void uploadSyncXmlCompleted(object sender, UploadDataCompletedEventArgs e)
    {
      var res = e.Result;
      try
      {
        var resXml = enc_.GetString(res);
        OperaLink.Utils.ODS(resXml);
        readServerInfo(resXml);
        { typeds_.FromOperaLinkXml(resXml); typeds_.SyncDone(); }
        { ses_.FromOperaLinkXml(resXml); ses_.SyncDone(); }
        { sds_.FromOperaLinkXml(resXml); sds_.SyncDone(); }
        { notes_.FromOperaLinkXml(resXml); notes_.SyncDone(); }
        { bms_.FromOperaLinkXml(resXml); bms_.SyncDone(); }
        LastStatus = "Sync Success.";
        OnSyncSuccessed(new EventArgs());
      }
      catch (WebException wex)
      {
        Utils.ODS(wex.StackTrace);
        LastStatus = "Sync Failed.";
        OnSyncFailed(new EventArgs());
      }
    }

    private void readServerInfo(String xmlString)
    {
      if (string.IsNullOrEmpty(xmlString))
      {
        return;
      }
      var xd = new XmlDocument();
      xd.LoadXml(xmlString);
      try
      {
        {
          long ss = 0;
          if (long.TryParse(xd.DocumentElement.Attributes["syncstate"].Value, out ss))
          {
            sync_state_ = ss;
            Utils.ODS(string.Format("{0}:{1}", "sync_state", ss));
          }
        }
        {
          int si = 0;
          if (int.TryParse(xd.GetElementsByTagName("shortinterval")[0].FirstChild.InnerText, out si))
          {
            short_interval_ = si;
            Utils.ODS(string.Format("{0}:{1}", "short_interval_", si));
          }
        }
        {
          int li = 0;
          if (int.TryParse(xd.GetElementsByTagName("longinterval")[0].FirstChild.InnerText, out li))
          {
            long_interval_ = li;
            Utils.ODS(string.Format("{0}:{1}", "long_interval_", li));
          }
        }
      }
      catch (XmlException xex)
      {
        Utils.ODS(string.Format("{0} {1}", xex.Message, xex.StackTrace));
      }
    }

    public void AddBookmark(OperaLink.Data.Bookmark d) { bms_.Add(d); }
    public void ModBookmark(OperaLink.Data.Bookmark d) { bms_.Mod(d); }
    public void DelBookmark(OperaLink.Data.Bookmark d) { bms_.Del(d); }

    public void AddNote(OperaLink.Data.Note d) { notes_.Add(d); }
    public void ModNote(OperaLink.Data.Note d) { notes_.Mod(d); }
    public void DelNote(OperaLink.Data.Note d) { notes_.Del(d); }
    
    public void AddSearchEngine(OperaLink.Data.SearchEngine d) { ses_.Add(d); }
    public void ModSearchEngine(OperaLink.Data.SearchEngine d) { ses_.Mod(d); }
    public void DelSearchEngine(OperaLink.Data.SearchEngine d) { ses_.Del(d); }

    public void AddSpeedDial(OperaLink.Data.SpeedDial d) { sds_.Add(d); }
    public void ModSpeedDial(OperaLink.Data.SpeedDial d) { sds_.Mod(d); }
    public void DelSpeedDial(OperaLink.Data.SpeedDial d) { sds_.Del(d); }

    public void AddTypedHistory(TypedHistory d) { typeds_.Add(d); }
    public void ModTypedHistory(TypedHistory d) { typeds_.Mod(d); }
    public void DelTypedHistory(TypedHistory d) { typeds_.Del(d); }
  }
}