using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, object> _serviceCollection;

        public ServiceCollection()
        {
            _serviceCollection = new Dictionary<Type, object>();
        }

        public IServiceCollection AddTransient<T>() where T : class
        {
            Func<T> factory = () => (T)Activator.CreateInstance(typeof(T));
            _serviceCollection.Add(typeof(T), factory);

            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory) where T : class
        {
            _serviceCollection.Add(typeof(T), factory);

            return this;
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory) where T : class
        {
            _serviceCollection.Add(typeof(T), factory);

            return this;
        }

        public IServiceCollection AddSingleton<T>() where T : class
        {
            _serviceCollection.Add(typeof(T), Activator.CreateInstance(typeof(T)));

            return this;
        }

        public IServiceCollection AddSingleton<T>(T service) where T : class
        {
            _serviceCollection.Add(typeof(T), service);

            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory) where T : class
        {
            _serviceCollection.Add(typeof(T), factory.Invoke());

            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory) where T : class
        {
            var provider = new ServiceProvider(_serviceCollection);
            _serviceCollection.Add(typeof(T), factory.Invoke(provider));

            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {            
            return new ServiceProvider(_serviceCollection);
        }
    }
}