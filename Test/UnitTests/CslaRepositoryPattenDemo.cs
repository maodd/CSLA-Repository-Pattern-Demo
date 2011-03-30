using BusinessLibrary;
using Core.Models;
using DataAccess;
using NUnit.Framework;
using Rhino.Mocks;

namespace CslaRepositoryPattenDemo
{
    [TestFixture]
    public class When_using_underlying_data_portal
    {
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

        [Test]
        public void Should_fetch_old_order()
        {
            Order order = Order.FetchById(1);
        }

        [Test]
        public void Should_insert_new_order()
        {
            Order order = Order.NewOrder();
            order.CustomerName = "A Nice customer";
            order.Save();
        }

        [Test]
        public void Should_update_old_order()
        {
            Order order = Order.FetchById(1);
            order.CustomerName = "Angry customer";
            order.Save();
        }
    }

    [TestFixture]
    public class When_using_repistory
    {
        [Test]
        public void Should_delete_old_order()
        {
            var order = BusinessLibrary.TweekedCsla.Order.FetchById(BusinessLibrary.TweekedCsla.Order.MaxId());
            order.Delete();
        }

        [Test]
        public void Should_delete_order_by_id()
        {
            Order.DeleteById(1);
        }

        [Test]
        public void Should_fetch_old_order()
        {
            var order = BusinessLibrary.TweekedCsla.Order.FetchById(1);
        }

        [Test]
        public void Should_insert_new_order()
        {
            var order = new BusinessLibrary.TweekedCsla.Order() { CustomerName = "Happy Customer" };
            order.CustomerName = "A Nice customer";
            order.Save();
        }

        [Test]
        public void Should_update_old_order()
        {
            var order = BusinessLibrary.TweekedCsla.Order.FetchById(BusinessLibrary.TweekedCsla.Order.MaxId());
            order.CustomerName = "Angry customer";
            order.Save();
        }
    }

    [TestFixture]
    public class When_using_repistory_and_test_using_mock
    {
 
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
             
            _mockRepository = MockRepository.GenerateMock<IOrderRepository>();

            IoC.Inject<IOrderRepository>(_mockRepository);
 
            _mockRepository.Stub(x => x.FetchById(1)).Return(new OrderModel(){CustomerName = "Any customer"});
        }
        [TearDown]
        public void TearDown()
        {
 
        }

        #endregion

        IOrderRepository _mockRepository;

        [Test]
        public void Should_delete_old_order()
        {
 
            var order =  BusinessLibrary.TweekedCsla.Order.FetchById(1);
            order.Delete();
            _mockRepository.AssertWasCalled(x => x.Delete(null), opt=>opt.IgnoreArguments());
        }

        [Test]
        public void Should_delete_order_by_id()
        {
            BusinessLibrary.TweekedCsla.Order.DeleteById(1);
            _mockRepository.AssertWasNotCalled(x => x.FetchById(1), opt => opt.IgnoreArguments());
            _mockRepository.AssertWasCalled(x => x.DeleteById(1));
        }

        [Test]
        public void Should_fetch_old_order()
        {
            var order = BusinessLibrary.TweekedCsla.Order.FetchById(1);
            _mockRepository.AssertWasCalled(x => x.FetchById(1));
        }

        [Test]
        public void Should_insert_new_order()
        {
            var order = new BusinessLibrary.TweekedCsla.Order() { CustomerName = "Happy Customer" };
            order.CustomerName = "A Nice customer";
            order.Save();

            _mockRepository.AssertWasCalled(x => x.SaveOrUpdate(null), opt=>opt.IgnoreArguments());
        }

        [Test]
        public void Should_update_old_order()
        {

            var order = BusinessLibrary.TweekedCsla.Order.FetchById(1);
            order.CustomerName = "Angry customer";
            order.Save();

            _mockRepository.AssertWasCalled(x => x.SaveOrUpdate(null), opt => opt.IgnoreArguments());
        }
    }
}