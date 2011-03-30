using System;
using Core.Models;

namespace BusinessLibrary
{
    public static class Translater
    {
        static Translater()
        {
            AutoMapper.Mapper.CreateMap<OrderModel, BusinessLibrary.TweekedCsla.Order>();
        }

        public static TweekedCsla.Order From(OrderModel source)
        {
            return AutoMapper.Mapper.Map<OrderModel, TweekedCsla.Order>(source);
        }
    }
}
