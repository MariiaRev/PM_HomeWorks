using System;
using System.Linq;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _serviceCollection;

        public ServiceProvider(Dictionary<Type, object> services)
        {
            _serviceCollection = services;
        }

        public T GetService<T>() where T : class
        {
            var service = _serviceCollection.Where(s => s.Key == typeof(T)).FirstOrDefault();
            
            if(service.Key == null || service.Value == null)
            {
                return null;
            }
            if (service.Value is Func<T> func1)
            {
                return func1.Invoke(); ;
            }
            if (service.Value is Func<IServiceProvider, T> func2)
            {
                return func2.Invoke(this);
            }

            return (T)service.Value;
        }
    }
}
