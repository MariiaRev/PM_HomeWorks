namespace DesignPatterns.IoC
{
    public interface IServiceProvider
    {
        T GetService<T>() where T : class;
    }
}