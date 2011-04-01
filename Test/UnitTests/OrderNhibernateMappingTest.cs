using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary;
using Core.Models;
using DataAccess.Mappings;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class OrderNhibernateMappingTest : InMemoryDatabaseTestFixture
    {
        public OrderNhibernateMappingTest()
            : base(typeof(OrderMap).Assembly)
        {
        }

        [Test]
        public void should_save_and_load()
        {
            object id;

            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new OrderModel(){CustomerName = "A customer"});

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var name = Session.Get<OrderModel>(id);

                Assert.That(name.CustomerName, Is.EqualTo("A customer"));

                tx.Commit();
            }
        }
    }
}
