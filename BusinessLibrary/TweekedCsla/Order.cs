using System;
using Core.Models;
using Csla;
using DataAccess;

namespace BusinessLibrary.TweekedCsla
{
    /// <summary>
    /// Wrapper class created for inject IRepository and Translater/autoMapper into Csla Object
    /// </summary>
    [Serializable]
    public class Order : BusinessBase<Order>
    {
        public static PropertyInfo<int> IdProperty = RegisterProperty(new PropertyInfo<int>("Id", "Id"));
        public int Id
        {
            get { return GetProperty(IdProperty); }
            protected set { SetProperty(IdProperty, value); }
        }

        public static PropertyInfo<string> CustomerNameProperty = RegisterProperty(new PropertyInfo<string>("CustomerName", "Customer name"));
        public string CustomerName
        {
            get { return GetProperty(CustomerNameProperty); }
            set { SetProperty(CustomerNameProperty, value); }
        }

        public static PropertyInfo<LineItems> LineItemsProperty = RegisterProperty(new PropertyInfo<LineItems>("LineItems", "Line items"));
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
            ValidationRules.AddRule(Csla.Validation.CommonRules.StringRequired, CustomerNameProperty);
        }

        // Removed for easise unit test
//        protected Order()
//        { /* require use of factory methods */ }

        public static Order NewOrder()
        {
            return DataPortal.Create<Order>();
        }

        /// <summary>
        /// Test purpose
        /// </summary>
        /// <returns></returns>
        public static int MaxId()
        {
            return OrderRepository.MaxId();
        }
        
        /// <summary>
        /// Ensure OrderRepository to get latest instance for each call especially for mocking
        /// </summary>
        public static IOrderRepository OrderRepository
        {
            get { return IoC.Get<IOrderRepository>(); }
        }


        public override void Delete()
        {
            
            OrderRepository.Delete(OrderRepository.FetchById(Id));
 
            return;
        }

        public override Order Save()
        {
            OrderModel orderToSave;
            if (Id > 0)
            {
                orderToSave = OrderRepository.FetchById(Id);
            }
            else
            {
                orderToSave = new OrderModel();
            }

            orderToSave.CustomerName = CustomerName;

             OrderRepository.SaveOrUpdate(orderToSave);
 
             this.Id = orderToSave.Id;
            return this;
        }

        /// <summary>
        /// Fectch NHibernate object from repository
        /// Translate to csla object using AutoMapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Order FetchById(int id)
        {
            OrderModel result = OrderRepository.FetchById(id);

            return Translater.From(result);
        }

        public new static void DeleteById(int id)
        {

            OrderRepository.DeleteById(id);
 
        }
    }
}