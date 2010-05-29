﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using OperaLink.Data;

namespace OperaLink
{
  ///my $OPERA_UA_STRING = 'Opera/9.80 (Windows NT 6.0; U; en) Presto/2.5.24 Version/10.52';
  ///my $LOGIN_API = 'https://auth.opera.com/xml';
  ///my $LINK_API = 'https://link-server.opera.com/pull';
  /// <summary>
  /// 
  /// </summary>
  public class Client
  {
    private readonly string LOGIN_API = "https://auth.opera.com/xml";
    private readonly string LINK_API = "https://link-server.opera.com/pull";

    private OperaLink.Configs conf_;
    private long sync_state_;
    private int short_interval_;
    private int long_interval_;
    private string token_;
    private XmlWriterSettings xml_settings_;
    private TypedHistoryManager typeds_;
    private SpeedDialManager sds_;

    public IEnumerable<TypedHistory> TypedHistories { get { return typeds_.Items; } }

    public Client(OperaLink.Configs conf)
    {
      conf_ = conf;
      typeds_ = new TypedHistoryManager();
      xml_settings_ = new XmlWriterSettings
      {
        Encoding = System.Text.Encoding.UTF8,
        NewLineOnAttributes = false,
      };
      sync_state_ = 0;
      short_interval_ = 60;
      long_interval_ = 120;
      token_ = "";
    }

    private string createLoginXml()
    {
      var x = "";
      using (var ms = new MemoryStream())
      {
        using (var xw = XmlWriter.Create(ms, xml_settings_))
        {
          xw.WriteStartDocument();
          xw.WriteStartElement("auth", "http://xmlns.opera.com/2007/auth");
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

    /*<?xml version="1.0" encoding="utf-8"?>
     * <auth version="1.1" xmlns="http://xmlns.opera.com/2007/auth">
     * <token>44c08bc467e30f70441876f5571134bc</token>
     * <message>Login sucessful</message>
     * <code>200</code>
     * </auth>
     */
    public bool Login()
    {
      LastStatus = "Login to OperaLink Server... ";
      var enc = Encoding.GetEncoding("utf-8");
      var wc = new WebClient();
      wc.Headers["User-Agent"] = conf_.UserAgent;
      var res = wc.UploadData(LOGIN_API, "POST", enc.GetBytes(createLoginXml()));
      var resXml = enc.GetString(res);
      System.Diagnostics.Debug.WriteLine(resXml);
      var msg = "";
      int code = -1;
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
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
          }
        }
      }

      //return resXml.IndexOf("Login successful") != -1;
      System.Diagnostics.Debug.WriteLine(string.Format("{0}:{1}", "token", token_));
      System.Diagnostics.Debug.WriteLine(string.Format("{0}:{1}", "message", msg));
      System.Diagnostics.Debug.WriteLine(string.Format("{0}:{1}", "code", code));
      return code == 200;
    }

    private string createLinkXml()
    {
      /* <?xml version="1.0" encoding="utf-8"?>
       * <link user="', $user, '" password="', $pass, '" syncstate="', $state ,'" dirty="0" version="1.0" xmlns="http://xmlns.opera.com/2006/link">
       * <clientinfo>
       * <supports>bookmark</supports>
       * <supports>speeddial</supports>
       * <supports>note</supports>
       * <supports target="desktop">search_engine</supports>
       * <supports>typed_history</supports>
       * <build>3315</build>
       * <system>win32</system>
       * </clientinfo>
       * <data/>
       * </link>
       */
      var x = "";
      using (var ms = new MemoryStream())
      {
        using (var xw = XmlWriter.Create(ms))
        {
          xw.WriteStartDocument();
          xw.WriteStartElement("link", "http://xmlns.opera.com/2006/link");
          xw.WriteAttributeString("version", "1.0");
          xw.WriteAttributeString("user", conf_.UserName);
          xw.WriteAttributeString("password", conf_.Password);
          xw.WriteAttributeString("syncstate", sync_state_.ToString());
          xw.WriteAttributeString("dirty", "0");
          xw.WriteStartElement("clientinfo");
          xw.WriteStartElement("supports"); xw.WriteString("typed_history"); xw.WriteEndElement();
          xw.WriteStartElement("build"); xw.WriteString("3374"); xw.WriteEndElement();
          xw.WriteStartElement("system"); xw.WriteString("win32"); xw.WriteEndElement();
          xw.WriteEndElement();
          xw.WriteStartElement("data");
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

    public string LastStatus { get; private set; }
    public bool Sync()
    {
      if (string.IsNullOrEmpty(token_))
      {
        if (!Login())
        {
          return false;
        }
        LastStatus = "Login Success";
      }
      var enc = Encoding.GetEncoding("utf-8");
      var wc = new WebClient();
      wc.Headers["User-Agent"] = conf_.UserAgent;
      var lxml = createLinkXml();
      System.Diagnostics.Debug.WriteLine(lxml);
      try
      {
        //var resXml = wc.UploadString(LINK_API, "POST", lxml);
        var res = wc.UploadData(LINK_API, "POST", enc.GetBytes(lxml));
        var resXml = enc.GetString(res);
        System.Diagnostics.Debug.WriteLine(resXml);
        readServerInfo(resXml);
        typeds_.FromOperaLinkXml(resXml);
        LastStatus = "Synced";
      }
      catch (WebException wex)
      {
        Utils.ODS(wex.StackTrace);
        LastStatus = "Sync Failed";
        return false;
      }
      return true;
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
        System.Diagnostics.Debug.WriteLine(xex.StackTrace);
      }
    }

    public void AddBookmark(OperaLink.Data.Bookmark d) { }
    public void ModBookmark(OperaLink.Data.Bookmark d) { }
    public void DelBookmark(OperaLink.Data.Bookmark d) { }

    public void AddNote(OperaLink.Data.Note d) { }
    public void ModNote(OperaLink.Data.Note d) { }
    public void DelNote(OperaLink.Data.Note d) { }

    public void AddSearchEngine(OperaLink.Data.SearchEngine d) { }
    public void ModSearchEngine(OperaLink.Data.SearchEngine d) { }
    public void DelSearchEngine(OperaLink.Data.SearchEngine d) { }

    public void AddSpeedDial(OperaLink.Data.SpeedDial d) { }
    public void ModSpeedDial(OperaLink.Data.SpeedDial d) { }
    public void DelSpeedDial(OperaLink.Data.SpeedDial d) { }

    public void AddTypedHistory(TypedHistory d) { typeds_.Add(d); }
    public void ModTypedHistory(TypedHistory d) { typeds_.Mod(d); }
    public void DelTypedHistory(TypedHistory d) { typeds_.Del(d); }
  }
}