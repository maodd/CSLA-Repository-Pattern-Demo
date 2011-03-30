using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Models;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class OrderMap : ClassMap<OrderModel>
    {
        public OrderMap()
        {
            Table("Order_Tab");

            Id(x=>x.Id).GeneratedBy.Identity();

            Map(x => x.CustomerName);
     
        }
    }
}
