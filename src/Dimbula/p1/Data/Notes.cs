using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperaLink.Data
{
  public class Note{ }

  public class NoteWrapper : ISyncDataWrapper<Note>
  {
  }
  public class NoteManager : ISyncDataManager<Note>
  {

  }
}
