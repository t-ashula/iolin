using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace OperaLink.Data
{
    public class SearchEngine
    {
        public Guid Uuid { get; set; }
        public enum SEType { Normal, HistorySearch, FindInPage };
        public SEType Type { get; set; }
        public enum SEGroup { Custome, DesktopDefault };
        public SEGroup Group { get; set; }
        public bool Deleted { get; set; }
        public bool IsPost { get; set; }
        public int PersonalBarPos { get; set; }
        public bool ShowInPersonal { get; set; }
        public string Title { get; set; }
        public Uri Uri { get; set; }
        public string Key { get; set; }
        public string Encoding { get; set; }
        public string PostQuery { get; set; }
        public Image Icon { get; set; }
    }

    public class SearchEngineWrapper : ISyncDataWrapper<SearchEngine>
    {
        public SearchEngineWrapper()
        {
            Content = new SearchEngine();
            State = SyncState.Added;
        }

        public override bool IsSameContent(ISyncDataWrapper<SearchEngine> other)
        {
            return this.Content.Uuid == other.Content.Uuid;
        }

        public override void FromOperaLinkXml(string xmlString)
        {
            var xd = new XmlDocument();
            xd.LoadXml(xmlString);
            var nsm = new XmlNamespaceManager(xd.NameTable);
            nsm.AddNamespace("oplink", OperaLinkXmlNameSpaces.LINK_XML_NAME_SPACE);
            var t = xd.GetElementsByTagName("search_engine")[0];
            OperaLink.Utils.ODS(t.OuterXml);

            Content = new SearchEngine()
            {
                Encoding = t.SelectSingleNode("//oplink:encoding", nsm).InnerText,
                Title = t.SelectSingleNode("//oplink:title", nsm).InnerText,
                Key = t.SelectSingleNode("//oplink:key", nsm).InnerText,
                Uuid = new Guid(t.Attributes["id"].Value),
                Type = StringToSEType(t.Attributes["type"].Value),
                Group = StringToSEGroup(t.SelectSingleNode("//oplink:group", nsm).FirstChild.Value),
                IsPost = t.SelectSingleNode("//oplink:is_post", nsm).FirstChild.Value != "0",
                PersonalBarPos = System.Convert.ToInt32(t.SelectSingleNode("//oplink:personal_bar_pos", nsm).FirstChild.Value),
                ShowInPersonal = t.SelectSingleNode("//oplink:show_in_personal_bar", nsm).FirstChild.Value != "0",
            };

            if (!String.IsNullOrEmpty(t.SelectSingleNode("//oplink:uri", nsm).InnerText))
            {
                Content.Uri = new Uri(t.SelectSingleNode("//oplink:uri", nsm).InnerText);
            }
            if (Content.IsPost)
            {
                Content.PostQuery = t.SelectSingleNode("//oplink:post_query", nsm).InnerText;
            }
            var h = t.SelectSingleNode("//oplink:hidden", nsm);
            if (h != null)
            {
                Content.Deleted = h.FirstChild.Value != "0";
            }

            try
            {
                Content.Icon = Image.FromStream(new MemoryStream(Convert.FromBase64String(t.SelectSingleNode("//oplink:icon", nsm).FirstChild.Value)));
            }

            catch (System.Exception ex)
            {
                Utils.ODS(ex.StackTrace);
            }
            State = Utils.StringToState(t.Attributes["status"].Value);
        }

        private string SETypeToString(SearchEngine.SEType t)
        {
            return
              t == SearchEngine.SEType.HistorySearch ? "history_search" :
              t == SearchEngine.SEType.FindInPage ? "find_in_page" :
              t == SearchEngine.SEType.Normal ? "normal" : "";
        }

        private SearchEngine.SEType StringToSEType(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new ArgumentException();
            }
            return
              s == "find_in_page" ? SearchEngine.SEType.FindInPage :
              s == "history_search" ? SearchEngine.SEType.HistorySearch :
              s == "normal" ? SearchEngine.SEType.Normal : SearchEngine.SEType.Normal;
        }

        private string SEGroupToString(SearchEngine.SEGroup g)
        {
            return
              g == SearchEngine.SEGroup.DesktopDefault ? "desktop_default" :
              g == SearchEngine.SEGroup.Custome ? "custom" : "custom";
        }

        private SearchEngine.SEGroup StringToSEGroup(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new ArgumentException();
            }
            return
              s == "desktop_default" ? SearchEngine.SEGroup.DesktopDefault :
              s == "custom" ? SearchEngine.SEGroup.Custome : SearchEngine.SEGroup.Custome;
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
                    xw.WriteStartElement("search_engine");
                    xw.WriteAttributeString("status", Utils.StateToString(State));
                    xw.WriteAttributeString("id", Content.Uuid.ToString().Replace("-", ""));
                    xw.WriteAttributeString("type", SETypeToString(Content.Type));
                    xw.WriteStartElement("group"); xw.WriteString(SEGroupToString(Content.Group)); xw.WriteEndElement();
                    xw.WriteStartElement("hidden"); xw.WriteString((Content.Deleted ? 1 : 0).ToString()); xw.WriteEndElement();
                    xw.WriteStartElement("is_post"); xw.WriteString((Content.IsPost ? 1 : 0).ToString()); xw.WriteEndElement();
                    xw.WriteStartElement("personal_bar_pos"); xw.WriteString((Content.PersonalBarPos).ToString()); xw.WriteEndElement();
                    xw.WriteStartElement("show_in_personal_bar"); xw.WriteString((Content.ShowInPersonal ? 1 : 0).ToString()); xw.WriteEndElement();
                    xw.WriteStartElement("title"); xw.WriteString((Content.Title.Replace("<", "&lt;"))); xw.WriteEndElement();
                    xw.WriteStartElement("uri"); xw.WriteString((Content.Uri.ToString())); xw.WriteEndElement();
                    xw.WriteStartElement("key"); xw.WriteString((Content.Key)); xw.WriteEndElement();
                    xw.WriteStartElement("encoding"); xw.WriteString((Content.Encoding)); xw.WriteEndElement();
                    xw.WriteStartElement("post_query"); xw.WriteString((Content.PostQuery)); xw.WriteEndElement();
                    xw.WriteStartElement("icon"); xw.WriteString((Utils.IconToString(Content.Icon))); xw.WriteEndElement();
                    xw.WriteEndElement();
                }

                ms.Position = 0;
                x = (new StreamReader(ms)).ReadToEnd();
            }
            return x;
        }

        /*
          * <search_engine status="added" id="91BA29D03CBE8A40B439D0AEAC790289" type="normal">
          * <group>custom</group>
          * <hidden>0</hidden>
          * <is_post>0</is_post>
          * <personal_bar_pos>-1</personal_bar_pos>
          * <show_in_personal_bar>0</show_in_personal_bar>
          * <title>Wikipedia</title>
          * <uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s</uri>
          * <key>jw</key>
          * <encoding>UTF-8</encoding>
          * <post_query/>
          * <icon/>
          * </search_engine>
          */
        public override void ModContent(ISyncDataWrapper<SearchEngine> other)
        {
            //my @keys = ( 'hidden', 'is_post', 'pb_pos', 'in_pb', 'title', 'uri', 'key', 'encoding', 'post_query', 'icon' );
            Content.Deleted = other.Content.Deleted;
            Content.IsPost = other.Content.IsPost;
            Content.PersonalBarPos = other.Content.PersonalBarPos;
            Content.PostQuery = other.Content.PostQuery;
            Content.ShowInPersonal = other.Content.ShowInPersonal;
            Content.Title = other.Content.Title;
            Content.Uri = other.Content.Uri;
            Content.Key = other.Content.Key; Content.Encoding = other.Content.Encoding;
            Content.Icon = other.Content.Icon;
        }
    }

    public class SearchEngineManager : ISyncDataManager<SearchEngine, SearchEngineWrapper>
    {
        public SearchEngineManager()
            : base(new string[] { "search_engine" })
        {
        }
    }

}