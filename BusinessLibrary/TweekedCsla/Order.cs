﻿using System;
 
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

        public BusinessLibrary.LineItems LineItems
        {
            get
            {
                if (!FieldManager.FieldExists(LineItemsProperty))
                    LoadProperty(LineItemsProperty, BusinessLibrary.LineItems.NewList());
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
 

        [RunLocal]
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