using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperaLink.Data
{
  class SearchContent
  { 
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
  class Search : ISyncData<SearchContent>
  {
    #region ISyncData<SearchContent> メンバ

    public string ToOperaLinkXml()
    {
      throw new NotImplementedException();
    }

    public void FromOperaLinkXml(string xmlString)
    {
      throw new NotImplementedException();
    }

    public void Added(SearchContent d)
    {
      throw new NotImplementedException();
    }

    public void Modified(SearchContent d)
    {
      throw new NotImplementedException();
    }

    public void Deleted()
    {
      throw new NotImplementedException();
    }

    public SyncState State
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public string StateToString(SyncState s)
    {
      throw new NotImplementedException();
    }

    public SyncState StringToState(string s)
    {
      throw new NotImplementedException();
    }

    public SearchContent Content
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    #endregion
  }
}
