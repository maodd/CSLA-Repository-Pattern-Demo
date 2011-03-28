using System;

namespace BusinessLibrary
{
    public class OrderRepository : IOrderRepository
    {
        public Order2 FetchById(int id)
        {
            Console.WriteLine("Fetching in repository");
            return (Order2)Activator.CreateInstance(typeof(Order2), true); 
        }

        public void SaveOrUpdate(Order2 order)
        {
            Console.WriteLine("Saving in repository");
        }

        public void Delete(Order2 id)
        {
            Console.WriteLine("Deleteing in repository");
        }        
        
        public void DeleteById(int id)
        {
            Console.WriteLine("Deleteing ini repository");
        }
    }
}