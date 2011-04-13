using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary;
using Core.Models;
using NUnit.Framework;

namespace Test.UnitTests
{
    [TestFixture]
    class TranslatorSpecs
    {
        [Test]
        public void ShouldMapRootPropertiesWithChildList()
        {
            var orderModel = new OrderModel(){CustomerName = "A customer"

                                             };
            orderModel.AddLineItem(new LineItemModel(){Name = "Linteitme1"});
            var result = new OrderTranslator().From(orderModel);

            Assert.AreEqual(orderModel.CustomerName, result.CustomerName);
            Assert.Greater(result.LineItems.Count, 0);
            Assert.AreEqual(result.LineItems[0].Name, "Linteitme1");

            Console.WriteLine(result.CustomerName);
            Console.WriteLine(result.LineItems.Count);
        }
    }


}
