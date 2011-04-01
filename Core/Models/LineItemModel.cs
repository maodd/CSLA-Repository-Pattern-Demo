using System;

namespace Core.Models
{
    public class LineItemModel  
  {
      public virtual int Id { get; set; }
      public virtual string Name { get; set; }

      public virtual OrderModel Order { get; set; }
  }
}
