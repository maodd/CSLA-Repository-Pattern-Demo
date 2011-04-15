using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Csla.Core;

namespace BusinessLibrary.TweekedCsla
{
    public static class BusinessBaseEx
    {

        public static void MarkOld(this BusinessBase cslaBo  )
        {
            var markOldMethod = typeof(Csla.Core.BusinessBase).GetMethod("MarkOld", BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy);

            markOldMethod.Invoke(cslaBo, null);
        }
    }
}
