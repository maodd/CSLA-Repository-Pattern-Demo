using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary;
using Core.Models;
using DataAccess;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class OrderRepositroySpecs
    {
        OrderRepository _systemUnderTest;
        [SetUp]
        public void Setup()
        {
            _systemUnderTest = new OrderRepository();
        }
        [Test]
        public void should_insert_new_order()
        {
            
            string newName = "A very funny customer " + new Random().Next(14345);

            _systemUnderTest.SaveOrUpdate(new OrderModel(){CustomerName = newName});
            
            int maxId = _systemUnderTest.MaxId();
 
            var check = _systemUnderTest.FetchById(maxId);

            Assert.AreEqual(newName, check.CustomerName);
        }

        [Test]
        public void should_delete_last_order()
        {
            int maxId = _systemUnderTest.MaxId();
            _systemUnderTest.DeleteById(maxId);
            _systemUnderTest.Session.Flush();

            var check = _systemUnderTest.FetchById(maxId);
            Assert.IsNull(check);
        }

        [Test]
        public void should_update_exisitn_order()
        {
            int maxId = _systemUnderTest.MaxId();
            var order = _systemUnderTest.FetchById(maxId);
            order.CustomerName = "xixi";
            _systemUnderTest.SaveOrUpdate(order);
            _systemUnderTest.Session.Flush();

            var check = _systemUnderTest.FetchById(maxId);
            Assert.AreEqual("xixi", check.CustomerName);
        }

    }
}
