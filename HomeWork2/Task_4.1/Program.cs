using System;
using Library.Exceptions;

namespace Task_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new LimitExceededException();
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"The {ex.GetType()} occured:");
            }
            catch (LimitExceededException ex)
            {
                Console.WriteLine($"The {ex.GetType()} occured:");
            }
            catch (PaymentServiceException ex)
            {
                Console.WriteLine($"The {ex.GetType()} occured:");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The {ex.GetType()} occured:");
            }



            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }
    }
}
