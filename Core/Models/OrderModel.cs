using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            LineItems = new List<LineItemModel>();
        }
        public virtual int Id { get; set; }
        public virtual string CustomerName { get; set; }

        public virtual IList<LineItemModel> LineItems { get; set; }

        public virtual void AddLineItem(LineItemModel lineItem)
        {
            lineItem.Order = this;
            LineItems.Add(lineItem);
        }
    }
}
