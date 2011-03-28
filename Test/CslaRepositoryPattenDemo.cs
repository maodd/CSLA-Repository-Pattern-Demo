using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary;
using Csla;
using DataAccess;
using NUnit.Framework;
using Rhino.Mocks;

namespace CslaRepositoryPattenDemo
{
        [TestFixture]
        public class When_using_underlying_data_portal
        {
            [Test]
            public void Should_insert_new_order()
            {
                Order order = Order.NewOrder();
                order.CustomerName = "A Nice customer";
                order.Save();
            }

            [Test]
            public void Should_fetch_old_order()
            {
                Order order = Order.FetchById(1);
            }

            [Test]
            public void Should_update_old_order()
            {
                Order order = Order.FetchById(1);
                order.CustomerName = "Angry customer";
                order.Save();
            }         
            
            [Test]
            public void Should_delete_old_order()
            {
                Order order = Order.FetchById(1);
                order.Delete();
                order.Save();
            }        
            
            [Test]
            public void Should_delete_order_by_id()
            {
                Order.DeleteById(1);
            }


        } 
    
        [TestFixture]
        public class When_using_repistory
        {
            [Test]
            public void Should_insert_new_order()
            {
                var order = new Order2("Happy Customer");
                order.CustomerName = "A Nice customer";
                order.Save();
            }

            [Test]
            public void Should_fetch_old_order()
            {
                var order = Order2.FetchById(1);
            }

            [Test]
            public void Should_update_old_order()
            {
                var order = Order2.FetchById(1);
                order.CustomerName = "Angry customer";
                order.Save();
            }         
            
            [Test]
            public void Should_delete_old_order()
            {
                var order = Order2.FetchById(1);
                order.Delete();
                 
            }        
            
            [Test]
            public void Should_delete_order_by_id()
            {
                Order.DeleteById(1);
            }


        }

        [TestFixture]
        public class When_using_repistory_and_test_using_mock
        {
            IOrderRepository _mockRepository;

            [SetUp]
            public void Setup()
            {
                _mockRepository = MockRepository.GenerateMock<IOrderRepository>();
                IoC.Inject<IOrderRepository>(_mockRepository);
            }

            [Test]
            public void Should_insert_new_order()
            {
                var order = new Order2("Happy Customer");
                order.CustomerName = "A Nice customer";
                order.Save();

                _mockRepository.AssertWasCalled(x => x.SaveOrUpdate(order));
            }

            [Test]
            public void Should_fetch_old_order()
            {
                var order = Order2.FetchById(1);
                _mockRepository.AssertWasCalled(x => x.FetchById(1));
            }

            [Test]
            public void Should_update_old_order()
            {
                _mockRepository.Stub(x => x.FetchById(1)).Return(new Order2("Any customer"));
                var order = Order2.FetchById(1);
                order.CustomerName = "Angry customer";
                order.Save();

                _mockRepository.AssertWasCalled(x => x.SaveOrUpdate(order));
            }         
            
            [Test]
            public void Should_delete_old_order()
            {
                _mockRepository.Stub(x => x.FetchById(1)).Return(new Order2("Any customer"));
                var order = Order2.FetchById(1);
                order.Delete();
                _mockRepository.AssertWasCalled(x => x.Delete(order));
            }        
            
            [Test]
            public void Should_delete_order_by_id()
            {
                Order2.DeleteById(1);
                _mockRepository.AssertWasNotCalled(x => x.FetchById(1), opt=>opt.IgnoreArguments());
                _mockRepository.AssertWasCalled(x => x.DeleteById(1));
            }


        }
    }
 
