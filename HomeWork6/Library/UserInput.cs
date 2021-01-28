using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public static class UserInput
    {
        /// <summary>
        /// Accept user input from console window, as long as the input is not parsed to int.
        /// </summary>
        /// <param name="message">Message shown when prompted for user input</param>
        /// <returns>User input of type int</returns>
        public static int ReadInt(string message)
        {
            Console.WriteLine(message);
            var data = Console.ReadLine().Trim();
            int number;

            while (!int.TryParse(data, out number))
            {
                Console.WriteLine($"\n\nYou need to enter an integer number. Try again:");
                data = Console.ReadLine().Trim();
            }

            return number;
        }

        /// <summary>
        /// Accept user input from console window, as long as the input is not parsed to int more than zero.
        /// </summary>
        /// <param name="message">Message shown when prompted for user input</param>
        /// <param name="allowZero">Indicates whether zero is included in valid values (true if zero is included, else false)</param>
        /// <returns>User input of type int</returns>
        public static int ReadPositiveInt(string message, bool allowZero)
        {
            Console.WriteLine(message);
            var data = Console.ReadLine().Trim();
            int number;

            if (allowZero)
            {
                while (!int.TryParse(data, out number) || number < 0)
                {
                    Console.WriteLine($"\n\nYou need to enter a positive integer number or 0. Try again:");
                    data = Console.ReadLine().Trim();
                }
            }
            else
            {
                while (!int.TryParse(data, out number) || number <= 0)
                {
                    Console.WriteLine($"\n\nYou need to enter a positive integer number. Try again:");
                    data = Console.ReadLine().Trim();
                }
            }

            return number;
        }

        /// <summary>
        /// Accept user input from console window in a range from start to end (inclusive), as long as the input is not parsed to int.
        /// </summary>
        /// <param name="message">Message shown when prompted for user input</param>
        /// <param name="start">Lower input limit (inclusive)</param>
        /// <param name="end">Upper input limit (inclusive)</param>
        /// <returns>User input of type int</returns>
        public static int ReadInt(string message, int start, int end)
        {
            Console.WriteLine(message);
            var data = Console.ReadLine().Trim();
            int number;

            while (!int.TryParse(data, out number) || number < start || number > end)
            {
                Console.WriteLine($"\n\nYou need to enter an integer number between {start} and {end}. Try again:");
                data = Console.ReadLine().Trim();
            }

            return number;
        }

        /// <summary>
        /// Accept user input from console window, if it is equal to the command parameter return true, else return false.
        /// </summary>
        /// <param name="message">Message shown when prompted for user input</param>
        /// <param name="command">Expected user input to return true</param>
        /// <param name="ignoreCase">Whether to ignore case when comparing command and user input (true if ignore)</param>
        /// <returns>True if user input is equal to the command parameter, false if not.</returns>
        public static bool ReadBoolOneTime(string message, string command, bool ignoreCase = false)
        {
            Console.WriteLine(message);
            var data = Console.ReadLine();

            if (ignoreCase)
                return string.Equals(data, command, StringComparison.OrdinalIgnoreCase);
            else
                return string.Equals(data, command);
        }
    }
}
