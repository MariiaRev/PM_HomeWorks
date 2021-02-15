namespace DesignPatterns.IoC
{
    public class SomeSingleton
    {
        private readonly int _counter = 0;

        public SomeSingleton()
        {
            _counter++;
        }

        public int Counter => _counter;
    }
}
