using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using FluentNHibernate.Automapping;

namespace BusinessLibrary
{
    /// <summary>
    /// Wrapper class created for inject IRepository and Translater/autoMapper into Csla Object
    /// </summary>
  [Serializable]
  public class Order2  : Order
  {

      /// <summary>
      /// Ensure OrderRepository to get latest instance for each call especially for mocking
      /// </summary>
      public static IOrderRepository OrderRepository { get { return IoC.Get<IOrderRepository>(); } }

      protected Order2() : this("Default Customer"){}

      public Order2(string customerName)
      {
          CustomerName = customerName;
      }   
 

      public override void Delete()
      {
          OrderRepository.Delete(this);
          return;
      }
      public override Order Save()
      {
          OrderRepository.SaveOrUpdate(this);
          OrderRepository.FetchById(this.Id);
          return this;
      }

      /// <summary>
      /// Fectch NHibernate object from repository
      /// Translate to csla object using AutoMapper
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
    public new static Order FetchById(int id)
    {
        var result = OrderRepository.FetchById(id);

        return Translater.From(result);
    }

      public new static void DeleteById(int id)
      {
          OrderRepository.DeleteById(id);
      }
     
  }
}
