using System;

namespace DesignPatterns.IoC
{
    public interface IServiceCollection
    {
        IServiceCollection AddTransient<T>() where T : class;

        IServiceCollection AddTransient<T>(Func<T> factory) where T : class;

        IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory) where T : class;

        IServiceCollection AddSingleton<T>() where T : class;

        IServiceCollection AddSingleton<T>(T service) where T : class;

        IServiceCollection AddSingleton<T>(Func<T> factory) where T : class;

        IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory) where T : class;

        IServiceProvider BuildServiceProvider();
    }
}