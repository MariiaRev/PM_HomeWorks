using System.Collections.Generic;

namespace PrimeNumbersSearch_Threads
{
    public static class ThreadSafeSortedSet
    {
        private static readonly SortedSet<int> _data;
        private static readonly object locker = new object();

        static ThreadSafeSortedSet()
        {
            _data = new SortedSet<int>();
        }

        public static void Add(List<int> numbers)
        {
            lock (locker)
            {
                foreach (var number in numbers)
                    _data.Add(number);
            }
        }

        public static SortedSet<int> GetData()
        {
            lock (locker)
            {
                return _data;
            }
        }
    }
}
