using System;
using HomeWork1;

namespace Exercise2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Tasks.Task2_2();

                //Console.Write("Enter any key: ");
                //Console.ReadKey();
            }
            else
            {
                Console.WriteLine(Tasks.Task2_2(args));
            }
        }
    }
}
