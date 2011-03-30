using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using DataAccess.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        public ISession Session { get; set; }
        protected Configuration Configuration { get; set; }
        ISessionFactory SessionFactory;

        public OrderRepository()
        {
            if (Configuration == null)
            {
                FluentConfiguration config = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(@"C:\lib\CslaNet\cs\CSLA-Repository-Pattern-Demo\db\order_track.sqlite")
                    .ShowSql
                    )
                    
                    .Mappings(m =>
                    {
                        m.FluentMappings.AddFromAssembly(typeof(OrderMap).Assembly);
                           
                    }
                    );

                Configuration = config.BuildConfiguration();
                SessionFactory = config.BuildSessionFactory();
            }

            Session = SessionFactory.OpenSession();
        }

        public int MaxId()
        {
//            return (int) Session.CreateCriteria(typeof (OrderModel))
//                             .SetProjection(Projections.Max("Id"))
//                             .UniqueResult();
            return Session.QueryOver<OrderModel>().SelectList(list => list.SelectMax(c => c.Id))
                .List<int>().FirstOrDefault();
        }

        public IList<OrderModel> FetchAll()
        {
            return Session.CreateCriteria(typeof(OrderModel)).List<OrderModel>().ToList(); ;
//            return Session.QueryOver<OrderModel>().List();
        }

        public OrderModel FetchById(int id)
        {
            return Session.Get<OrderModel>(id);
        }

        public void SaveOrUpdate(OrderModel order)
        {
            Session.SaveOrUpdate(order);
            Session.Flush(); // For demo purpose
        }

        public void Delete(OrderModel order)
        {
            Session.Delete(order);
            Session.Flush();
        }        
        
        public void DeleteById(int id)
        {
            Session.Delete(this.FetchById(id));
            Session.Flush();
        }
    }
}