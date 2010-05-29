using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperaLink.Data
{
  public class Bookmark
  {
  }

  internal class BookmarkWrapper : ISyncDataWrapper<Bookmark>
  {
  }

  public class BookmarkManager : ISyncDataManager<Bookmark>
  {
  }
}
