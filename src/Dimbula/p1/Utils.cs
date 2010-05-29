using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperaLink.Data;

namespace OperaLink
{
  
  static class ExDateTime
  {
    public static String ToW3cDtfInUtc(this DateTime d)
    {
      return d.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
  }

  static class Utils
  {
    public static SyncState StringToState(string s)
    {
      if (String.IsNullOrEmpty(s))
      {
        throw new ArgumentException();
      }
      s = s.ToLower();
      return
        s == "added" ? SyncState.Added :
        s == "deleted" ? SyncState.Deleted :
        s == "modified" ? SyncState.Modified :
        SyncState.Modified;
    }

    public static string StateToString(SyncState s)
    {
      return
        s == SyncState.Added ? "added" :
        s == SyncState.Deleted ? "deleted" :
        "modified";
    }

    public static string XmlEntitize(string raw)
    {
      return string.IsNullOrEmpty(raw) ? "" :
        raw.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;");
    }

    public static string XmlUnEntitize(string cooked)
    {
      return string.IsNullOrEmpty(cooked) ? "" :
        cooked.Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&amp;", "&");
    }

    public static void ODS(string s)
    {
      System.Diagnostics.Debug.WriteLine(s);
    }

    public static string IconToString(System.Drawing.Image icon)
    {
      var ms = new System.IO.MemoryStream();
      icon.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
      ms.Position = 0;
      return (new System.IO.StreamReader(ms)).ReadToEnd();
    }

  }
}
