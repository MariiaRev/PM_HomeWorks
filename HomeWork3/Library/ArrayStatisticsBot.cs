using System;
using System.Linq;

namespace Library
{
    public static class ArrayStatisticsBot
    {
        public static void Start()
        {
            while (true)
            {
                // get array entered by user until it's correct
                bool userDataIsIncorrect = AcceptAndValidateUserArray("\n\n\n\nEnter an array, please:", out string[] splitedUserData, out bool exit);

                while (userDataIsIncorrect && exit == false)
                {
                    userDataIsIncorrect = AcceptAndValidateUserArray("\n\nInput data is incorrect! Enter an array of " +
                        "integer numbers separated by comma.\nTry again:", out splitedUserData, out exit);
                }


                //if user entered "exit" - end program
                if (exit)
                    return;


                //array of integers
                int[] array = splitedUserData.Select(nums => int.Parse(nums.Trim())).ToArray();


                //print array statistics
                double avg = array.Average();                                                                // average
                double strdDev = Math.Sqrt(array.Sum(x => (x - avg) * (x - avg)) / array.Count());           // standart deviation

                Console.WriteLine($"\n\n\n\n{"",21}{"* * *  ARRAY STATISTICS  * * *"}\n\n");
                Console.WriteLine($"{"",21}{"Sum",-10} {array.Sum(),19}");
                Console.WriteLine($"{"",21}{"Minimum",-10} {array.Min(),19}");
                Console.WriteLine($"{"",21}{"Maximum",-10} {array.Max(),19}");
                Console.WriteLine($"{"",21}{"Average",-10} {Math.Round(avg, 4),19}");
                Console.WriteLine($"{"",21}{"Deviation",-10} {Math.Round(strdDev, 4),19}");


                //sort array and take only unique elements
                int[] sortedArray = array.Distinct().OrderBy(x => x).ToArray();


                //print sorted array
                Console.WriteLine("\n\nSorted array of unique elements:");
                foreach (int elem in sortedArray)
                    Console.Write(elem + " ");
                Console.WriteLine();
            }
        }

        static bool AcceptAndValidateUserArray(string message, out string[] splitedUserData, out bool exit)
        {
            Console.WriteLine($"{message}");
            string userData = Console.ReadLine();

            if (userData == "exit")
            {
                exit = true;
                splitedUserData = null;
                return false;
            }

            exit = false;
            splitedUserData = userData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            return splitedUserData.Any(n => !int.TryParse(n, out _)) || userData.Trim() == "";
        }
    }
}
