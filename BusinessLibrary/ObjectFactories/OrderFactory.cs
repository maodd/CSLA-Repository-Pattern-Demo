using System;
using Csla;
using Csla.Server;

namespace BusinessLibrary.ObjectFactories
{
  public class OrderFactory : ObjectFactory
  {
    private static int _lastId;

    public Order Create()
    {
      var obj = (Order)Activator.CreateInstance(typeof(Order), true);
      var id = System.Threading.Interlocked.Decrement(ref _lastId);
      LoadProperty(obj, Order.IdProperty, id);
      MarkNew(obj);
      CheckRules(obj);
      return obj;
    }

    public Order Fetch(SingleCriteria<Order, int> criteria)
    {
      var obj = (Order)Activator.CreateInstance(typeof(Order), true);
      MarkOld(obj);
        Console.WriteLine("Searching for order id: {0}", criteria.Value);
      return obj;
    }

    public Order Update(Order obj)
    {
      if (obj.IsDeleted)
      {
        if (!obj.IsNew)
        {
          // delete data
            Console.WriteLine("Deleting order ");
        }
        MarkNew(obj);
      }
      else
      {
        if (obj.IsNew)
        {
          // insert data
          LoadProperty(obj, Order.IdProperty, System.Math.Abs(obj.Id));

            Console.WriteLine("Inserting order");
        }
        else
        {
          // update data
            Console.WriteLine("Updating order");
        }
        MarkOld(obj);
      }
      return obj;
    }

    public void Delete(SingleCriteria<Order, int> criteria)
    {
      // delete data
        Console.WriteLine("Deleting order id: {0}", criteria.Value);
    }
  }
}
