using System;
using Library;
using Library.Exceptions;

namespace Task_3._2
{
    class Program
    {
       
        static void Main(string[] args)
        {
            ///Console.WriteLine-s are for hints about which methods you are going to test

            PaymentService paymentService = new PaymentService();


            //CREDIT CARD
            Console.WriteLine("--------------------------------- CREDIT CARD ---------------------------------\n");

            //try deposit (enter mastercard)
            Console.WriteLine("\n\tTry deposit with Mastercard (16-digit number, first digit is 5)\n");
            paymentService.StartDeposit(50, "USD");

            //try withdrawal (enter not visa, not mastercard)
            Console.WriteLine("\n\n\n\t\tTry withdrawal with unknown card (n-digit number)" +
                "\n\t\t    if n is 16 - first digit is not 5 or 4)\n");
            paymentService.StartWithdrawal(50, "USD");

            //try withdrawal (enter visa)
            Console.WriteLine("\n\n\n\tTry withdrawal with Visa (16-digit number, first digit is 4)\n");
            paymentService.StartWithdrawal(50, "USD");


            //PRIVET48
            Console.WriteLine("\n\n\n--------------------------------- PRIVET48 ---------------------------------\n");


            //try deposit (enter Gold)
            Console.WriteLine("\n\t\t\t  Try deposit with Gold card\n");
            paymentService.StartDeposit(50, "USD");


            //STEREOBANK
            Console.WriteLine("\n\n\n--------------------------------- STEREOBANK ---------------------------------\n");


            //try withdrawal (enter White)
            Console.WriteLine("\n\t\t\tTry withdrawal with White card\n");
            paymentService.StartWithdrawal(50, "USD");



            //GIFT VOUCHER 
            Console.WriteLine("\n\n\n--------------------------------- GIFT VOUCHER ---------------------------------\n");


            //try deposit
            Console.WriteLine("\t\t\t    Try deposit with 50 USD\n");
            try
            {
                paymentService.StartDeposit(50, "USD");
            }
            catch(PaymentServiceException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }

            //try deposit (enter wrong card number)
            Console.WriteLine("\n\n\n\tTry deposit with 500 USD and wrong (not 10-digit) card number\n");
            paymentService.StartDeposit(500, "USD");

            //try deposit (enter valid card number - 10-digit number)
            Console.WriteLine("\n\n\n\tTry deposit with 500 USD and valid (10-digit) card number\n");
            try
            {
                paymentService.StartDeposit(500, "USD");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }



            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }
    }
}
