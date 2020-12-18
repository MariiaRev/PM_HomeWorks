using System;
using System.Collections.Generic;
using Library;
using Library.Exceptions;

namespace Task_4._2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ///Console.WriteLine-s are for hints about which methods you are going to test


            PaymentService paymentService = new PaymentService();

            try
            {
                //CREDIT CARD
                Console.WriteLine("--------------------------------- CREDIT CARD ---------------------------------\n");

                //try deposit 100 USD (2836 UAH)                                                        - OK
                Console.WriteLine("\n\t\t    Try deposit 100 USD on a credit card\n");
                paymentService.StartDeposit(100, "USD");

                //try deposit 150 USD (4254 UAH)                                                        - not OK, > Internet limit
                Console.WriteLine("\n\n\n\t\t    Try deposit 150 USD on a credit card\n");
                paymentService.StartDeposit(150, "USD");
            }
            catch (LimitExceededException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (PaymentServiceException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }


            try
            {
                //PRIVET48
                Console.WriteLine("\n\n\n--------------------------------- PRIVET48 ---------------------------------\n");

                //try deposit 7000UAH from card (for example, login "login", "Gold" card)               - OK                                       
                Console.WriteLine("\n\t\t Try deposit 7000 UAH with login \"login\"\n");
                paymentService.StartDeposit(7000, "UAH");


                //try withdrawal 5000UAH on the other card (not "login")                                                  
                Console.WriteLine("\n\n\n\t      Try withdrawal 5000 UAH not with login \"login\"\n");
                paymentService.StartWithdrawal(5000, "UAH");


                //try withdrawal 5000UAH on the same card (for example, login "login", "Gold" card)     - not OK, > limit of transactions sum
                Console.WriteLine("\n\n\n\t\t  Try withdrawal 5000 UAH with login \"login\"\n");
                paymentService.StartWithdrawal(5000, "UAH");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (LimitExceededException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (PaymentServiceException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }



            //STEREOBANK
            Console.WriteLine("\n\n\n--------------------------------- STEREOBANK ---------------------------------\n");

            try
            {
                //try deposit 3000UAH from card (for example, login "login")                            - OK    
                Console.WriteLine("\n\t\t     Try deposit 3000 UAH with login \"login\"\n");
                paymentService.StartDeposit(3000, "UAH");


                //try deposit 4000UAH from the same card (for example, login "login")                   - not OK > Internet limit
                Console.WriteLine("\n\n\n\t\t     Try deposit 4000 UAH with login \"login\"\n");
                paymentService.StartDeposit(4000, "UAH");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (LimitExceededException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (PaymentServiceException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }


            try
            {
                //try deposit 3000UAH from the same card (for example, login "login")                   - OK
                Console.WriteLine("\n\n\n\t\t     Try deposit 3000 UAH with login \"login\"\n");
                paymentService.StartDeposit(3000, "UAH");


                //try withdrawal 1500 on the other card (for example, login "login")                    - not OK > limit of transactions sum
                Console.WriteLine("\n\n\n\t       Try withdrawal 1500 UAH not with login \"login\"\n");
                paymentService.StartWithdrawal(1500, "UAH");


                //try withdrawal 1500 on the same card (for example, login "login")                     - not OK > limit of transactions sum
                Console.WriteLine("\n\n\n\t\t  Try withdrawal 1500 UAH with login \"login\"\n");
                paymentService.StartWithdrawal(1500, "UAH");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (LimitExceededException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (PaymentServiceException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }



            try
            {
                //GIFT VOUCHER 
                Console.WriteLine("\n\n\n--------------------------------- GIFT VOUCHER ---------------------------------\n");

                //try deposit from giftVoucher (for example, 55555 22222)
                Console.WriteLine("\n\tTry deposit gift voucher (1000 UAH) with the number 55555 22222\n");
                paymentService.StartDeposit(1000, "UAH");


                //try deposit from the another giftVoucher (not 55555 22222)
                Console.WriteLine("\n\n\n\tTry deposit the another gift voucher (not 55555 22222)\n");
                paymentService.StartDeposit(1000, "UAH");


                //try deposit from the same giftVoucher (for example, 55555 22222)
                Console.WriteLine("\n\n\n\tTry deposit again the same gift voucher (55555 22222)\n");
                paymentService.StartDeposit(1000, "UAH");
            }

            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (LimitExceededException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (PaymentServiceException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (Exception ex)
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
