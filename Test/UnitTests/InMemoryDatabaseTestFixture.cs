using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Test
{
    public class InMemoryDatabaseTestFixture : IDisposable
    {
        protected static Configuration Configuration;
        protected static ISessionFactory SessionFactory;
        protected ISession Session;

        public InMemoryDatabaseTestFixture(Assembly assemblyContainingMapping)
        {
            if (Configuration == null)
            {
                FluentConfiguration config = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory()
                    )
                    .Mappings(m =>
                    {
                        m.FluentMappings.AddFromAssembly(assemblyContainingMapping);
                         
                    }
                    );

                Configuration = config.BuildConfiguration();
                SessionFactory = config.BuildSessionFactory();
            }

            Session = SessionFactory.OpenSession();

            new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Session.Dispose();
        }

        #endregion
    }
}
