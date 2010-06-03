using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;


namespace OperaLink.Data
{
  public class SpeedDial
  {
    public int Position { get; set; }
    public System.Drawing.Image Icon { get; set; }
    public string Title { get; set; }
    public Uri Uri { get; set; }
    public bool ReloadEnabled { get; set; }
    public bool ReloadOnlyIfExpired { get; set; }
    public Int64 ReloadInterval { get; set; }
  }

  public class SpeedDialContent : ISyncDataWrapper<SpeedDial>
  {
    public SpeedDialContent()
    {
      Content = new SpeedDial();
      State = SyncState.Added;
    }

    public override bool IsSameContent(ISyncDataWrapper<SpeedDial> other)
    {
      return Content.Position == other.Content.Position;
    }

    public override void FromOperaLinkXml(string xmlString)
    {
      var xd = new XmlDocument();
      xd.LoadXml(xmlString); var nsm = new XmlNamespaceManager(xd.NameTable);
      nsm.AddNamespace("oplink", "http://xmlns.opera.com/2006/link");
      var t = xd.GetElementsByTagName("speeddial")[0];

      Content = new SpeedDial
      {
        Title = t.SelectSingleNode("//oplink:title", nsm).InnerText,
        Position = Convert.ToInt32(t.Attributes["position"].Value),
        ReloadEnabled = t.SelectSingleNode("//oplink:reload_enabled", nsm).InnerText == "1",
        ReloadOnlyIfExpired = t.SelectSingleNode("//oplink:reload_only_if_expired", nsm).InnerText == "1",
        ReloadInterval = Convert.ToInt64(t.SelectSingleNode("//oplink:reload_interval", nsm).InnerText),
      };
      var uri = t.SelectSingleNode("//oplink:uri", nsm).InnerText;
      if (!string.IsNullOrEmpty(uri))
      {
        Content.Uri = new Uri(uri);
      }
      try
      {
        Content.Icon = Image.FromStream(new MemoryStream(Convert.FromBase64String(t.SelectSingleNode("//oplink:icon", nsm).InnerText)));
      }
      catch (System.Exception ex)
      {
        Utils.ODS(ex.Message);
      }
      State = Utils.StringToState(t.Attributes["status"].Value);
    }

    public override string ToOperaLinkXml()
    {
      var x = "";
      var xml_settings = new XmlWriterSettings
      {
        Encoding = System.Text.Encoding.UTF8,
        NewLineOnAttributes = false,
        Indent = false,
      };

      using (var ms = new MemoryStream())
      {
        using (var xw = XmlWriter.Create(ms, xml_settings))
        {
          xw.WriteStartElement("speeddial");
          xw.WriteAttributeString("status", Utils.StateToString(State));
          xw.WriteAttributeString("position", Content.Position.ToString());
          xw.WriteStartElement("title"); xw.WriteString((Content.Title)); xw.WriteEndElement();
          xw.WriteStartElement("uri"); xw.WriteString((Content.Uri.ToString())); xw.WriteEndElement();
          xw.WriteStartElement("reload_enabled"); xw.WriteString((Content.ReloadEnabled ? 1 : 0).ToString()); xw.WriteEndElement();
          xw.WriteStartElement("reload_only_if_expired"); xw.WriteString((Content.ReloadOnlyIfExpired?1:0).ToString()); xw.WriteEndElement();
          xw.WriteStartElement("reload_interval"); xw.WriteString((Content.ReloadInterval).ToString()); xw.WriteEndElement();
          xw.WriteStartElement("icon"); xw.WriteString((Utils.IconToString(Content.Icon))); xw.WriteEndElement();
          xw.WriteEndElement();
        }

        ms.Position = 0;
        x = (new StreamReader(ms)).ReadToEnd();
      }
      return x;
    }

  }

  public class SpeedDialManager : ISyncDataManager<SpeedDial, SpeedDialContent>
  {
    public SpeedDialManager()
      : base( new string[] { "speeddial" })
    {
    }
  }
}
