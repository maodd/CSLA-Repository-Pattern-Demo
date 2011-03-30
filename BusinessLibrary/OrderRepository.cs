using System;

namespace BusinessLibrary
{
    public class OrderRepository : IOrderRepository
    {
        public Order FetchById(int id)
        {
            Console.WriteLine("Fetching in repository");
            return (Order)Activator.CreateInstance(typeof(Order), true); 
        }

        public void SaveOrUpdate(Order order)
        {
            Console.WriteLine("Saving in repository");
        }

        public void Delete(Order id)
        {
            Console.WriteLine("Deleteing in repository");
        }        
        
        public void DeleteById(int id)
        {
            Console.WriteLine("Deleteing ini repository");
        }
    }
}