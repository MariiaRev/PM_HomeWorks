using System;
using Library;

namespace BettingPlatformEmulatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{"", 23}### BETTING PLATFORM EMULATOR ###");

            //Task 1.6 and 2.2
            BettingPlatformEmulator platform = new BettingPlatformEmulator();
            platform.Start();


            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }
    }
}
