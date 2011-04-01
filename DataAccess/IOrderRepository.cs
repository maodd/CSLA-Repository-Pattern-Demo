using Core.Models;
using NHibernate;

namespace DataAccess
{
    public interface IOrderRepository
    {
        ISession Session { get; }
        OrderModel FetchById(int id);
        void SaveOrUpdate(OrderModel order);
        void DeleteById(int id);
        void Delete(OrderModel order);
        int MaxId();
    }
}