using System;
using HomeWork1;

namespace Exercise2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Tasks.Task2_3();

                //Console.Write("Enter any key: ");
                //Console.ReadKey();
            }
            else
            {
                Tasks.Task2_3(args);
            }
        }
    }
}
