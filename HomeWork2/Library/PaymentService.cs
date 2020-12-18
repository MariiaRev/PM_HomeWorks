using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    //aggregation of payment methods and the ability to choose which of them to conduct a transaction
    public class PaymentService
    {
        PaymentMethodBase[] AvailablePaymentMethod;

        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[] { new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher() };
        }


        //chat bot with payment methods which support the possibility of deposit
        public void StartDeposit(decimal amount, string currency)
        {
            ISupportDeposit paymentMethod;
            bool success = false;
            int depositMethodCount = AvailablePaymentMethod.Length;

            while (!success)
            {
                Console.WriteLine("\nAvailable payment methods:");
                Console.WriteLine("1. CreditCard\n2. Privet48\n3. Stereobank\n4. GiftVoucher\n");

                Console.WriteLine("\n\nSelect payment method for deposit by entering its number, please:");

                if (!int.TryParse(Console.ReadLine(), out int chosenMethod) || chosenMethod < 1 || chosenMethod > depositMethodCount)
                {
                    Console.WriteLine($"\n\nYou need to enter a number between 1 and {depositMethodCount}!");
                    continue;
                }

                paymentMethod = AvailablePaymentMethod[chosenMethod-1] as ISupportDeposit;

                if (paymentMethod != null)
                    success = true;
                else                                                           //if chosen method does not support ISupportDeposit
                {
                    Console.WriteLine("\n\nWe're sorry, error ocurred.");
                    break;
                }

                //start deposit with chosen method
                paymentMethod?.StartDeposit(amount, currency);
            }
        }


        public void StartWithdrawal(decimal amount, string currency)
        {
            ISupportWithdrawal paymentMethod;
            bool success = false;
            int withdrawalMethodCount = AvailablePaymentMethod.Length - 1;

            while (!success)
            {
                Console.WriteLine("\nAvailable payment methods:");
                Console.WriteLine("1. CreditCard\n2. Privet48\n3. Stereobank\n");

                Console.WriteLine("\n\nSelect payment method for withdrawal by entering its number, please:");

                if (!int.TryParse(Console.ReadLine(), out int chosenMethod) || chosenMethod < 1 || chosenMethod > withdrawalMethodCount)
                {
                    Console.WriteLine($"\n\nYou need to enter a number between 1 and {withdrawalMethodCount}!");
                    continue;
                }

                paymentMethod = AvailablePaymentMethod[chosenMethod-1] as ISupportWithdrawal;

                if (paymentMethod != null)
                    success = true;
                else                                                           //if chosen method does not support ISupportWithdrawal
                {
                    Console.WriteLine("\n\nWe're sorry, error ocurred.");
                    break;
                }

                //start withdrawal with chosen method
                paymentMethod?.StartWithdrawal(amount, currency);
            }
        }
    }
}
