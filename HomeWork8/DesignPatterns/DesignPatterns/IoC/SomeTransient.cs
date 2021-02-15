namespace DesignPatterns.IoC
{
    public class SomeTransient
    {
        private readonly int _counter = 0;

        public SomeTransient()
        {
            _counter++;
        }

        public int Counter => _counter;
    }
}