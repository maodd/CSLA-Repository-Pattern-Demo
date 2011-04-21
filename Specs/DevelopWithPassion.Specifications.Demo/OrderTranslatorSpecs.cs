using BusinessLibrary;
using Core.Models;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Order = BusinessLibrary.TweekedCsla.Order;

namespace DevelopWithPassion.Specifications.Demo
{
    public class OrderTranslatorSpecs
    {


        [Subject(typeof(OrderTranslator))]
        public class When_translate_NHibernate_model_to_csla_bo : Observes<OrderTranslator>
        {
            static Order result;

            Because b = () =>
                {
                    var orderModel = new OrderModel
                                         {
                                             CustomerName = "A customer"
                                         };
                    orderModel.AddLineItem(new LineItemModel { Name = "Linteitme1" });

                    result = sut.From(orderModel);
                };

            It should_return_valid_csla_bo = () =>
                {
                    result.IsValid.ShouldBeTrue();
                    result.CustomerName.ShouldEqual("A customer");
                    result.LineItems.Count.ShouldEqual(1);
                    result.IsDirty.ShouldBeFalse();
                    result.IsNew.ShouldBeFalse();
                };
        }
    }
}