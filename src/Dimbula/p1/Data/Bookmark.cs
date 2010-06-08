using System;
using System.Xml;

namespace OperaLink.Data
{
  /* <bookmark status="added" id="2E96EEB4F694CB4F8C5802AD692B8F7C" created="2010-03-28T11:33:33Z" parent="" previous="14C645A5B8A3470FB3B52CC32C97E2B8">
   * <uri>http://ashula.info/</uri>
   * <title>Ashula.info - [Ashula.info]</title>
   * <show_in_personal_bar>0</show_in_personal_bar> 
   * <personal_bar_pos>-1</personal_bar_pos> 
   * <show_in_panel>0</show_in_panel> 
   * <panel_pos>-1</panel_pos>
   * </bookmark>
   */
  public class Bookmark
  {
    public Guid ID { get; set; }
    public Guid Parent { get; set; }
    public Guid Previous { get; set; }
    public DateTime Created { get; set; }
    public String Uri { get; set; }
    public String Title { get; set; }
    public int PersonalBarPos { get; set; }
    public bool ShowInPersonal { get; set; }
    public int PanelPos { get; set; }
    public bool ShowInPanel { get; set; }
    public enum BookmarkType { Item, Folder, Trash, Separator } ;
    public BookmarkType Type { get; set; }
  }

  public class BookmarkWrapper : ISyncDataWrapper<Bookmark>
  {
    public override bool IsSameContent(ISyncDataWrapper<Bookmark> other)
    {
      return Content.ID == other.Content.ID
        && Content.Parent == other.Content.Parent;
    }

    public override void FromOperaLinkXml(string xmlString)
    {
      if (xmlString.StartsWith("<bookmark_folder"))
      {
        FromOperaLinkXmlFolder(xmlString);
      }
      else
      {
        FromOperaLinkXmlBookmark(xmlString);
      }
    }
    /*
     * <bookmark status="added" id="2E96EEB4F694CB4F8C5802AD692B8F7C" created="2010-03-28T11:33:33Z" parent="" previous="14C645A5B8A3470FB3B52CC32C97E2B8">
     * <uri>http://ashula.info/</uri>
     * <title>Ashula.info - [Ashula.info]</title>
     * <show_in_personal_bar>0</show_in_personal_bar> 
     * <personal_bar_pos>-1</personal_bar_pos> 
     * <show_in_panel>0</show_in_panel> 
     * <panel_pos>-1</panel_pos> 
     * </bookmark>
     */
    private void FromOperaLinkXmlBookmark(string xmlString)
    {
      var xd = new XmlDocument();
      xd.LoadXml(xmlString);
      var nsm = new XmlNamespaceManager(xd.NameTable);
      nsm.AddNamespace("oplink", "http://xmlns.opera.com/2006/link");
      var t = xd.GetElementsByTagName("bookmark")[0];
      Content = new Bookmark
      {
        ID = new Guid(t.Attributes["id"].Value),
        Type = Bookmark.BookmarkType.Item,
      };
      var pa = t.Attributes["parent"].Value;
      if (!string.IsNullOrEmpty(pa)) { Content.Parent = new Guid(pa); }
      var pre = t.Attributes["previous"].Value;
      if (!string.IsNullOrEmpty(pre)) { Content.Previous = new Guid(pre); }
      var ct = t.Attributes["created"];
      if (ct != null) { Content.Created = DateTime.Parse(ct.Value); }
      var c = t.SelectSingleNode("//oplink:title", nsm);
      if (c != null) { Content.Title = c.InnerText; }
      var uri = t.SelectSingleNode("//oplink:uri", nsm);//.InnerText;
      if (uri != null) { Content.Uri = uri.InnerText; }
      var pbpos = t.SelectSingleNode("//oplink:personal_bar_pos", nsm);
      if (pbpos != null)
      {
        Content.PersonalBarPos = Convert.ToInt32(pbpos.InnerText);
        Content.ShowInPersonal = t.SelectSingleNode("//oplink:show_in_personal_bar", nsm).InnerText != "0";
      }
      var panelpos = t.SelectSingleNode("//oplink:panel_pos", nsm);
      if (panelpos != null)
      {
        Content.PanelPos = Convert.ToInt32(panelpos.InnerText);
        Content.ShowInPanel = t.SelectSingleNode("//oplink:show_in_panel", nsm).InnerText != "0";
      }
      State = Utils.StringToState(t.Attributes["status"].Value);
    }
    /* <bookmark_folder status="added" type="trash" id="14C645A5B8A3470FB3B52CC32C97E2B8" parent="" previous=""> 
     * <show_in_personal_bar>0</show_in_personal_bar> 
     * <personal_bar_pos>-1</personal_bar_pos> 
     * <show_in_panel>0</show_in_panel> 
     * <panel_pos>-1</panel_pos> 
     * <title>Trash</title> </bookmark_folder>
     */
    private void FromOperaLinkXmlFolder(string xmlString)
    {
      var xd = new XmlDocument();
      xd.LoadXml(xmlString);
      var nsm = new XmlNamespaceManager(xd.NameTable);
      nsm.AddNamespace("oplink", "http://xmlns.opera.com/2006/link");
      var t = xd.GetElementsByTagName("bookmark_folder")[0];
      Content = new Bookmark
      {
        ID = new Guid(t.Attributes["id"].Value),
        Type = Bookmark.BookmarkType.Folder,
      };
      var pa = t.Attributes["parent"].Value;
      if (!string.IsNullOrEmpty(pa)) { Content.Parent = new Guid(pa); }
      var pre = t.Attributes["previous"].Value;
      if (!string.IsNullOrEmpty(pre)) { Content.Previous = new Guid(pre); }
      var ct = t.Attributes["created"];
      if (ct != null) { Content.Created = DateTime.Parse(ct.Value); }
      var ty = t.Attributes["type"];
      if (ty != null && ty.Value == "trash") { Content.Type = Bookmark.BookmarkType.Trash; }
      var c = t.SelectSingleNode("//oplink:title", nsm);
      if (c != null) { Content.Title = c.InnerText; }
      var pbpos = t.SelectSingleNode("//oplink:personal_bar_pos", nsm);
      if (pbpos != null)
      {
        Content.PersonalBarPos = Convert.ToInt32(pbpos.InnerText);
        Content.ShowInPersonal = t.SelectSingleNode("//oplink:show_in_personal_bar", nsm).InnerText != "0";
      }
      var panelpos = t.SelectSingleNode("//oplink:panel_pos", nsm);
      if (panelpos != null)
      {
        Content.PanelPos = Convert.ToInt32(panelpos.InnerText);
        Content.ShowInPanel = t.SelectSingleNode("//oplink:show_in_panel", nsm).InnerText != "0";
      }
      State = Utils.StringToState(t.Attributes["status"].Value);

    }

    public override string ToOperaLinkXml()
    {
      var t = Content.Type;
      if (t == Bookmark.BookmarkType.Folder
        || t == Bookmark.BookmarkType.Trash)
      {
        return ToOperaLinkXmlFolder();
      }

      if (t == Bookmark.BookmarkType.Item)
      {
        return ToOperaLinkXmlBookmark();
      }
      return "";
    }

    private string ToOperaLinkXmlBookmark()
    {
      throw new NotImplementedException();
    }

    private string ToOperaLinkXmlFolder()
    {
      throw new NotImplementedException();
    }
  }

  public class BookmarkManager : ISyncDataManager<Bookmark, BookmarkWrapper>
  {
    public BookmarkManager()
      : base(new string[] { "bookmark", "bookmark_folder" }) { }
  }
}
