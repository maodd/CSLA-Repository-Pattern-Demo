using Core.Models;
using NHibernate;

namespace DataAccess
{
    public interface IOrderRepository
    {
        
        OrderModel FetchById(int id);
        void SaveOrUpdate(OrderModel order);
        void DeleteById(int id);
        void Delete(OrderModel order);
        int MaxId();
    }
}