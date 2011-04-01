using System;
 
using Core.Models;
using Csla;
using Csla.Validation;
using DataAccess;

namespace BusinessLibrary.TweekedCsla
{
    /// <summary>
    /// Created for use tweeked OrderFactory2
    /// </summary>
    [Serializable]
    [Csla.Server.ObjectFactory("BusinessLibrary.ObjectFactories.OrderFactory2, BusinessLibrary")]
    public class Order : BusinessBase<Order>
    {
        public static PropertyInfo<int> IdProperty = RegisterProperty(new PropertyInfo<int>("Id", "Id"));

        public static PropertyInfo<string> CustomerNameProperty =
            RegisterProperty(new PropertyInfo<string>("CustomerName", "Customer name"));

        public static PropertyInfo<LineItems> LineItemsProperty =
            RegisterProperty(new PropertyInfo<LineItems>("LineItems", "Line items"));

        public int Id
        {
            get { return GetProperty(IdProperty); }
            protected set { SetProperty(IdProperty, value); }
        }

        public string CustomerName
        {
            get { return GetProperty(CustomerNameProperty); }
            set { SetProperty(CustomerNameProperty, value); }
        }

        public LineItems LineItems
        {
            get
            {
                if (!FieldManager.FieldExists(LineItemsProperty))
                    LoadProperty(LineItemsProperty, LineItems.NewList());
                return GetProperty(LineItemsProperty);
            }
        }

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, CustomerNameProperty);
        }
 
        [RunLocal]
        public static Order NewOrder()
        {
            return DataPortal.Create<Order>();
        }
  
        public static Order FetchById(int id)
        {
            return (Order)DataPortal.Fetch(new SingleCriteria<Order, int>(id));
        }

        public static void DeleteById(int id)
        {
            DataPortal.Delete(new SingleCriteria<Order, int>(id));
        }

    }
}