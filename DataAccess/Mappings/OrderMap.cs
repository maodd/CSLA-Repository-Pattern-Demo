using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Order_Tab");
            Id().GeneratedBy.Identity();

            Map(x => x.CustomerName);
     
        }
    }
}
