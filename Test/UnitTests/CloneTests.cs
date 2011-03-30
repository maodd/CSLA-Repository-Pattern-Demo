using AutoMapper;
using BusinessLibrary;
using Core.Models;
using NUnit.Framework;

namespace CloneTests
{
    [TestFixture]
    public class When_map_from_order_model_to_csla
    {
        [Test]
        public void Should_get_csla_object()
        {
            Mapper.CreateMap<OrderModel, Order>();
            Order result = Mapper.Map<OrderModel, Order>(new OrderModel {Id = 1, CustomerName = "Which customer?"});

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Which customer?", result.CustomerName);
        }
    }
}