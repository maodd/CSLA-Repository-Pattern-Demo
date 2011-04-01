using System;
using System.Threading;
using Core.Models;
using Csla;
using Csla.Server;
using DataAccess;

namespace BusinessLibrary.ObjectFactories
{
    public class OrderFactory2 : ObjectFactory
    {
        static int _lastId;

        /// <summary>
        /// Ensure OrderRepository to get latest instance for each call especially for mocking
        /// </summary>
        public IOrderRepository OrderRepository
        {
            get { return IoC.Get<IOrderRepository>(); }
        }

        public TweekedCsla.Order Create()
        {
            var obj = (TweekedCsla.Order) Activator.CreateInstance(typeof (TweekedCsla.Order), true);
            int id = Interlocked.Decrement(ref _lastId);
            LoadProperty(obj, TweekedCsla.Order.IdProperty, id);
            MarkNew(obj);
            CheckRules(obj);
            return obj;
        }

        public TweekedCsla.Order Fetch(SingleCriteria<TweekedCsla.Order, int> criteria)
        {
            TweekedCsla.Order obj = TweekedCsla.Order.NewOrder();
            MarkOld(obj);

            OrderModel data = OrderRepository.FetchById(criteria.Value);

            using (BypassPropertyChecks(obj))
            {
                LoadProperty(obj, TweekedCsla.Order.IdProperty, data.Id);

                obj.CustomerName = data.CustomerName;

                LineItems list = LineItems.NewList();
                foreach (LineItemModel item in data.LineItems)
                {
                    LineItem current = LineItem.NewItem();
                    MarkOld(current);
            
                    
                    using (BypassPropertyChecks(current))
                    {
                        LoadProperty(current, LineItem.IdProperty, item.Id);
                        LoadProperty(current, LineItem.NameProperty, item.Name);
                    }
                    
                    CheckRules(current);
                    list.Add(current);
                }

                LoadProperty(obj, TweekedCsla.Order.LineItemsProperty, list);
            }

            CheckRules(obj);
            return obj;
        }

        public TweekedCsla.Order Update(TweekedCsla.Order obj)
        {
            if (!obj.IsDirty) return obj;

            OrderModel orderToSave;
            if (obj.Id > 0)
            {
                orderToSave = OrderRepository.FetchById(obj.Id);

                orderToSave.LineItems.Clear();
            }
            else
            {
                orderToSave = new OrderModel();
            }


            orderToSave.CustomerName = obj.CustomerName;


            foreach (LineItem lineItem in obj.LineItems)
            {
                orderToSave.AddLineItem(new LineItemModel
                                            {
//                                                Id = lineItem.Id, 
// LineItem treat as ValueObject, delete/insert everytime, 
// id will change after each save,
// but who cares? name is the only thing important in order
                                                Name = lineItem.Name,
                                                Order = orderToSave
                                            });
            }

            OrderRepository.SaveOrUpdate(orderToSave);

            TweekedCsla.Order result = Fetch(new SingleCriteria<TweekedCsla.Order, int>(orderToSave.Id));
            return result;
        }

        public void Delete(SingleCriteria<TweekedCsla.Order, int> criteria)
        {
            OrderRepository.DeleteById(criteria.Value);
        }
    }
}