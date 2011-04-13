using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Core.Models;

namespace BusinessLibrary
{
    public class OrderTranslator  
    {
        public OrderTranslator()
        {
            Mapper.CreateMap<OrderModel, BusinessLibrary.TweekedCsla.Order>();
            Mapper.CreateMap<LineItemModel, LineItem>();
        }

        public TweekedCsla.Order From(OrderModel orderModel)
        {
            return Mapper.Map<OrderModel, TweekedCsla.Order>(orderModel);
        }
    }
}
