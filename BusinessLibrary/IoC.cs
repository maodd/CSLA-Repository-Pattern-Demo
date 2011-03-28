using System;
using System.Collections.Generic;

namespace BusinessLibrary
{
    public static class IoC
    {
        static Dictionary<Type, object> lookup;

        static IoC()
        {
            lookup = new Dictionary<Type, object>();
            lookup.Add(typeof(IOrderRepository), new OrderRepository());
        }

        public static T Get<T>() where T : class
        {
            return (T) lookup[typeof(T)];
        }

        public static void Inject<T>(object fake)
        {
            lookup[typeof (T)] = fake;
        }
    }
}