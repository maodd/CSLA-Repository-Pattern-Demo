using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace BusinessLibrary
{
  [Serializable]
   
  public class Order2 : Order
  {
   
      static readonly IOrderRepository _orderRepository;
     

      static Order2()
      {
          _orderRepository = IoC.Get<IOrderRepository>();
      }

      public Order2(string customerName)
      {
          CustomerName = customerName;
      }

      public override void Delete()
      {
           
          _orderRepository.Delete(this);
          return;
      }
      public override Order Save()
      {
          _orderRepository.SaveOrUpdate(this);
          _orderRepository.FetchById(this.Id);
          return this;
      }

    public new static Order2 FetchById(int id)
    {
        var result = _orderRepository.FetchById(id);
        return  result;
    }

      public new static void DeleteById(int id)
      {
          _orderRepository.DeleteById(id);
      }
     
  }

    public interface IOrderRepository
    {
        Order2 FetchById(int id);
        void SaveOrUpdate(Order2 order);
        void DeleteById(int id);
        void Delete(Order2 order2);
    }
}
