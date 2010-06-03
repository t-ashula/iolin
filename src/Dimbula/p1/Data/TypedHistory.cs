using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace OperaLink.Data
{
  /*
   * <typed_history status="added" content="ashula.info" type="text">
   *  <last_typed>2010-04-14T18:22:42Z</last_typed>
   * </typed_history>
   * <typed_history status="modified" content="ashula.info" type="selected">
   *  <last_typed>2010-04-14T18:24:12Z</last_typed>
   * </typed_history>
   * <typed_history status="deleted" content="ashula.info" type="selected">
   *  <last_typed>2010-04-14T18:24:12Z</last_typed>
   * </typed_history>
   * <typed_history status="added" content="&quot;foo bar &amp;'()*&lt;>" type="text">
   *  <last_typed>2010-04-14T18:50:18Z</last_typed>
   * </typed_history>
   * <typed_history status="added" content="あ" type="text">
   *  <last_typed>2010-04-14T18:51:25Z</last_typed>
   * </typed_history>
   */
  public class TypedHistory
  {
    public string Type { get; set; }
    public string Content { get; set; }
    public DateTime LastTyped { get; set; }
    public TypedHistory()
    {
      Type = "text";
      Content = "";
      LastTyped = System.DateTime.Now;
    }
  }

  public class TypedHistoryWrapper : ISyncDataWrapper<TypedHistory>
  {
    public TypedHistoryWrapper()
    {
      Content = new TypedHistory();
      State = SyncState.Added;
    }

    public override bool IsSameContent(ISyncDataWrapper<TypedHistory> other)
    {
      return this.Content.Content == other.Content.Content;
    }

    public override string ToOperaLinkXml()
    {
      return string.IsNullOrEmpty(Content.Content) ?
        "<typed_history />" :
        string.Format(
          "<typed_history status=\"{0}\" content=\"{1}\" type=\"{2}\"><last_typed>{3}</last_typed></typed_history>",
          Utils.StateToString(State),
          Utils.XmlEntitize(Content.Content),
          State == SyncState.Added ? "text" : "selected", Content.LastTyped.ToW3CDTFInUtc());
    }

    public override void FromOperaLinkXml(string xmlString)
    {
      var xd = new XmlDocument();
      xd.LoadXml(xmlString);
      var t = xd.GetElementsByTagName("typed_history")[0];
      Content = new TypedHistory
      {
        Content = Utils.XmlUnEntitize(t.Attributes["content"].Value),
        Type = t.Attributes["type"].Value,
      };
      State = Utils.StringToState(t.Attributes["status"].Value);
      if (State != SyncState.Deleted)
      {
        var last = t.FirstChild.FirstChild.Value;
        var last_typed = DateTime.Now;
        var result = new DateTime();
        if (DateTime.TryParse(last, out result))
        {
          last_typed = result;
        }
        Content.LastTyped = last_typed;
      }
    }

    public override void ModContent(ISyncDataWrapper<TypedHistory> other)
    {
      State = SyncState.Modified;
      Content.LastTyped = other.Content.LastTyped;
      Content.Type = "selected";
    }
  }

  public class TypedHistoryManager : ISyncDataManager<TypedHistory, TypedHistoryWrapper>
  {
    public TypedHistoryManager()
      : base(new string[] { "typed_history" })
    {
    }
  }
}
