using System;
using HomeWork1;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Tasks.Task3();

                //Console.Write("Enter any key: ");
                //Console.ReadKey();
            }
            else
            {
                Tasks.Task3(args);
            }
        }
    }
}
