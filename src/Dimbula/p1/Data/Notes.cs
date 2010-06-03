using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;

namespace OperaLink.Data
{
  /*
  * <note status="added" id="DD6212DB87BD0C44A683EF1B4B6F9A03" parent="" previous="" created="2010-03-25T16:15:23Z">
  * <uri>http://d.hatena.ne.jp/t_ashula/</uri>
  * <content xml:space="preserve"> font-family:normal; </content>
  * </note>
  * <note_folder status="added" id="A9AAFED0976111DC85AC8946BEF8D2DC" type="trash" parent="" previous=""> 
  * <title>Trash</title> 
  * </note_folder> 
  * <note status="added" id="85BB81A782BCAD42B38F787B6F4948FB" parent="A9AAFED0976111DC85AC8946BEF8D2DC" previous=""/> 
  * <note status="added" created="2010-03-25T16:15:23Z" id="DD6212DB87BD0C44A683EF1B4B6F9A03" parent="" previous="A9AAFED0976111DC85AC8946BEF8D2DC"> 
  * <uri>http://d.hatena.ne.jp/t_ashula/</uri> 
  * <content xml:space="preserve"> font-family:normal; </content> 
  * </note>
  */
  public class Note
  {
    public Guid ID { get; set; }
    public Guid Parent { get; set; }
    public Guid Previous { get; set; }
    public DateTime Created { get; set; }
    public Uri Uri { get; set; }
    public String Content { get; set; } /// as Title when Type == Folder or Type == Trash
    public enum NoteType { Note, Folder, Trash } ;
    public NoteType Type { get; set; }
  }

  public class NoteWrapper : ISyncDataWrapper<Note>
  {
    public NoteWrapper()
    {
      Content = new Note();
      State = SyncState.Added;
    }

    public override bool IsSameContent(ISyncDataWrapper<Note> other)
    {
      return Content.ID == other.Content.ID && Content.Parent == other.Content.Parent;
    }

    private void FromOperaLinkXmlNote(string xmlString)
    {
      var xd = new XmlDocument();
      xd.LoadXml(xmlString); 
      var nsm = new XmlNamespaceManager(xd.NameTable);
      nsm.AddNamespace("oplink", OperaLinkXmlNameSpaces.LINK_XML_NAME_SPACE); 
      var t = xd.GetElementsByTagName("note")[0];

      Content = new Note
      {
        ID = new Guid(t.Attributes["id"].Value),
        Type = Note.NoteType.Note,
      };
      var pa = t.Attributes["parent"].Value;
      if (!string.IsNullOrEmpty(pa)) { Content.Parent = new Guid(pa); }
      var pre = t.Attributes["previous"].Value;
      if (!string.IsNullOrEmpty(pre)) { Content.Previous = new Guid(pre); }
      var ct = t.Attributes["created"];
      if (ct != null) { Content.Created = DateTime.Parse(ct.Value); }
      var c = t.SelectSingleNode("//oplink:content", nsm);
      if (c != null) { Content.Content = c.InnerText; }
      var uri = t.SelectSingleNode("//oplink:uri", nsm);//.InnerText;
      if (uri != null && !string.IsNullOrEmpty(uri.InnerText)) { Content.Uri = new Uri(uri.InnerText); }
      State = Utils.StringToState(t.Attributes["status"].Value);
    }
    private void FromOperaLinkXmlFolder(string xmlString)
    {
      var xd = new XmlDocument();
      xd.LoadXml(xmlString); 
      var nsm = new XmlNamespaceManager(xd.NameTable);
      nsm.AddNamespace("oplink", OperaLinkXmlNameSpaces.LINK_XML_NAME_SPACE); 
      var t = xd.GetElementsByTagName("note_folder")[0];
      OperaLink.Utils.ODS(t.OuterXml);
      Content = new Note
      {
        ID = new Guid(t.Attributes["id"].Value),
        Type = Note.NoteType.Folder,
      };
      var ty = t.Attributes["type"];//.Value;
      if (ty != null && !string.IsNullOrEmpty(ty.Value) && ty.Value.ToLower() == "trash") { Content.Type = Note.NoteType.Trash; }
      var pa = t.Attributes["parent"].Value;
      if (!string.IsNullOrEmpty(pa)) { Content.Parent = new Guid(pa); }
      var pre = t.Attributes["previous"].Value;
      if (!string.IsNullOrEmpty(pre)) { Content.Previous = new Guid(pre); }
      var ct = t.Attributes["created"];
      if (ct != null) { Content.Created = DateTime.Parse(ct.Value); }
      var c = t.SelectSingleNode("//oplink:title", nsm);
      if (c != null) { Content.Content = c.InnerText; }
      State = Utils.StringToState(t.Attributes["status"].Value);
    }
    public override void FromOperaLinkXml(string xmlString)
    {
      if (xmlString.StartsWith("<note_folder"))
      {
        FromOperaLinkXmlFolder(xmlString);
      }
      else
      {
        FromOperaLinkXmlNote(xmlString);
      }
    }

    private string ToOperaLinkXmlNote()
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
          xw.WriteStartElement("note");
          xw.WriteAttributeString("status", Utils.StateToString(State));
          xw.WriteAttributeString("id", Content.ID.ToFlatString());
          if (Content.Parent != null) { xw.WriteAttributeString("parent", Content.Parent.ToFlatString()); }
          if (Content.Previous != null) { xw.WriteAttributeString("previous", Content.Previous.ToFlatString()); }
          if (Content.Created != null) { xw.WriteAttributeString("created", Content.Created.ToW3CDTFInUtc()); }
          xw.WriteStartElement("content");
          if (Content.Content != null)
          {
            xw.WriteString(Content.Content);
          }
          xw.WriteEndElement();
          xw.WriteStartElement("uri");
          if (Content.Uri != null)
          {
            xw.WriteString(Content.Uri.ToString());
          }
          xw.WriteEndElement();
        }
        ms.Position = 0;
        x = (new StreamReader(ms)).ReadToEnd();
      }
      return x;
    }
    private string ToOperaLinkXmlFolder()
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
          xw.WriteStartElement("note_folder");
          xw.WriteAttributeString("status", Utils.StateToString(State));
          xw.WriteAttributeString("id", Content.ID.ToFlatString());
          if (Content.Parent != null) { xw.WriteAttributeString("parent", Content.Parent.ToFlatString()); }
          if (Content.Previous != null) { xw.WriteAttributeString("previous", Content.Previous.ToFlatString()); }
          if (Content.Created != null) { xw.WriteAttributeString("created", Content.Created.ToW3CDTFInUtc()); }
          xw.WriteAttributeString("type", Content.Type == Note.NoteType.Trash ? "trash" : "folder");
          xw.WriteStartElement("title"); xw.WriteString((Content.Content)); xw.WriteEndElement();
          xw.WriteEndElement();
        }
        ms.Position = 0;
        x = (new StreamReader(ms)).ReadToEnd();
      }
      return x;
    }
    public override string ToOperaLinkXml()
    {
      return (Content.Type == Note.NoteType.Note) ? ToOperaLinkXmlNote() : ToOperaLinkXmlFolder();
    }
  }

  public class NoteManager : ISyncDataManager<Note, NoteWrapper>
  {
    public NoteManager()
      : base(new string[] { "note", "note_folder" })
    {
    }
  }
}
