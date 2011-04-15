using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BusinessLibrary.TweekedCsla;
using Core.Models;

namespace BusinessLibrary
{
    public class OrderTranslator  
    {
        public OrderTranslator()
        {
            Mapper.CreateMap<OrderModel, BusinessLibrary.TweekedCsla.Order>()
                 .AfterMap((s, d) =>
                 {
                     d.MarkOld();
                 })
                ;
            Mapper.CreateMap<LineItemModel, LineItem>().AfterMap((s, d) =>
                 {
                     d.MarkOld();
                 });
        }

        public TweekedCsla.Order From(object orderModel)
        {
            return Mapper.Map<OrderModel, TweekedCsla.Order>((OrderModel) orderModel);
        }
    }
}
