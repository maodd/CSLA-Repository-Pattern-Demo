using System;
using Csla.Server;

namespace BusinessLibrary.ObjectFactories
{
  public class LineItemFactory : ObjectFactory
  {
    public LineItem Create()
    {
      var obj = (LineItem)Activator.CreateInstance(typeof(LineItem), true);
      MarkAsChild(obj);
      MarkNew(obj);
      CheckRules(obj);
      return obj;
    }
  }
}
