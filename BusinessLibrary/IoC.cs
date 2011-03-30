using System;
using System.Collections.Generic;

namespace BusinessLibrary
{
    public static class IoC
    {
        static Dictionary<Type, object> lookup;

        static IoC()
        {
            Reset();
        }

        public static T Get<T>() where T : class
        {
            return (T) lookup[typeof(T)];
        }

        public static void Inject<T>(object fake)
        {
            lookup[typeof(T)] = fake;
//            if (!(Equals(lookup[typeof (T)], fake)))
//            {
//                Console.WriteLine("Injecting fake");
//                lookup[typeof (T)] = fake;
//            }
        }

        public static void Reset()
        {
            lookup = null;
            lookup = new Dictionary<Type, object>();
            lookup.Add(typeof(IOrderRepository), new OrderRepository());
        }
    }
}