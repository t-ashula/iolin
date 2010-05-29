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
    public int Position;
    public System.Drawing.Image Icon;
    public string Title;
    public Uri Uri;
    public bool ReloadEnabled;
    public bool ReloadOnlyIfExpired;
    public Int64 ReloadInterval;
  }

  internal class SpeedDialContent : ISyncDataWrapper<SpeedDial>
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
      xd.LoadXml(xmlString);
      var t = xd.GetElementsByTagName("speeddial")[0];

      Content = new SpeedDial
      {
        Title = t.SelectSingleNode("//title").Value,
        Uri = new Uri(t.SelectSingleNode("//uri").Value),
        Position = Convert.ToInt32(t.Attributes["position"].Value),
        ReloadEnabled = t.SelectSingleNode("//reload_enabled").Value == "1",
        ReloadOnlyIfExpired = t.SelectSingleNode("//reload_only_if_expired").Value == "1",
        ReloadInterval = Convert.ToInt64(t.SelectSingleNode("//reload_interval").Value),
      };
      try
      {
        Content.Icon = Image.FromStream(new MemoryStream(Convert.FromBase64String(t.SelectSingleNode("//icon").Value)));
      }
      catch (System.Exception ex)
      {
        Utils.ODS(ex.StackTrace);
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

  public class SpeedDialManager : ISyncDataManager<SpeedDial>
  {
    public override void FromOperaLinkXml(string xmlString)
    {
      if (string.IsNullOrEmpty(xmlString))
      {
        return;
      }
      var xd = new XmlDocument();
      xd.LoadXml(xmlString);
      var engines = xd.GetElementsByTagName("speeddial");
      for (int i = 0; i < engines.Count; ++i)
      {
        var item = new SpeedDialContent();
        item.FromOperaLinkXml(engines[i].OuterXml);
        ChangeInnerList(item);
      }
    }
  }

}
