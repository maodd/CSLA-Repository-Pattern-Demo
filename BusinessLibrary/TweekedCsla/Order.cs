using System;
using BusinessLibrary.Translaters;
using Core.Models;
using Csla;
using Csla.Validation;
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
            // Needed for automaper only
            internal set { SetProperty(LineItemsProperty, value); }
        }

        /// <summary>
        /// Ensure OrderRepository to get latest instance for each call especially for mocking
        /// </summary>
        public static IOrderRepository OrderRepository
        {
            get { return IoC.Get<IOrderRepository>(); }
        }

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, CustomerNameProperty);
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
                // todo: deleting all existing line items?
            }
            else
            {
                orderToSave = new OrderModel();
            }

 
            orderToSave.CustomerName = CustomerName;

            foreach (BusinessLibrary.LineItem lineItem in LineItems)
            {
                orderToSave.AddLineItem(new LineItemModel
                                            {
                                                Id = lineItem.Id,
                                                Name = lineItem.Name,
                                                Order = orderToSave
                                            });
            }

            OrderRepository.SaveOrUpdate(orderToSave);

//            Id = orderToSave.Id;
            SetProperty(IdProperty, orderToSave.Id);
            int i = 0;
            foreach (var lineItem in LineItems)
            {
                lineItem.Id = orderToSave.LineItems[i].Id;
                lineItem.ApplyEdit();
                i++;
            }

            ApplyEdit(); // for list
            ApplyEdit(); // for itself
            MarkClean();
            MarkOld();
            return this;
//            return base.Save();
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

        public static void DeleteById(int id)
        {
            OrderRepository.DeleteById(id);
        }
    }
}