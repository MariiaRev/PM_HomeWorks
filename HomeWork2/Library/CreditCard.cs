using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Exceptions;

namespace Library
{
    //this class still needs to be brought to the DRY principle
    public class CreditCard: PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public CreditCard()
        {
            Name = "CreditCard";
        }

        const decimal internetLimitUAH = 3000;

        //chat bot for conducting deposit transactions
        public void StartDeposit(decimal amount, string currency)
        {
            Bank.SomeBankWork();
            checkLimits(amount, currency);                          //first, check limits for internet transactions

            string cardNumberStr;                                           //card number entered by user
            ulong cardNumber;                                               //parsed card number
            string cardNumberPattern = @"^[54][0-9]{15}$";
            string cardType;

            string ExpiryDate;                                              //expiry date entered by user
            string ExpiryDatePattern = @"^(0[1-9]|1[0-2])/[0-9]{2}$";

            string CvvStr;                                                  //cvv code entered by user
            string CvvPattern = @"^[0-9]{3}$";
            int CVV;                                                        //parsed cvv code

            Regex regex;


            //accept data from user

            //set card number by user

            Console.Write("\n\nEnter your card number, please (you can use spaces): ");
            cardNumberStr = Console.ReadLine().Replace(" ", "");

            regex = new Regex(cardNumberPattern);

            while (!regex.Match(cardNumberStr).Success || !ulong.TryParse(cardNumberStr, out cardNumber))
            {
                Console.Write("\n\nWrong card number. Try again: ");
                cardNumberStr = Console.ReadLine().Replace(" ", "");
            }

            //set card type
            cardType = cardNumber.ToString().StartsWith("5") ? "Mastercard" : "Visa";

            //set expiry date by user
            Console.Write("\n\n\nEnter expiry date of your card, please: ");
            ExpiryDate = Console.ReadLine();

            regex = new Regex(ExpiryDatePattern);

            while(!regex.Match(ExpiryDate).Success)
            {
                Console.Write("\n\nWrong expiry date. Try again: ");
                ExpiryDate = Console.ReadLine();
            }


            //set CVV by user
            Console.Write("\n\n\nEnter CVV of your card, please: ");
            CvvStr = Console.ReadLine();

            regex = new Regex(CvvPattern);

            while (!regex.Match(CvvStr).Success || !int.TryParse(CvvStr, out CVV))
            {
                Console.Write("\n\nWrong CVV. Try again: ");
                CvvStr = Console.ReadLine();
            }


            Console.WriteLine($"\n\nYou’ve withdrawn {amount} {currency} from your card successfully");
            Console.WriteLine($"\nCard number: {cardNumber}\t{cardType}\nExpiry date: {ExpiryDate}\nCVV: {CVV}");
        }

        //chat bot for conducting withdrawal transactions
        public void StartWithdrawal(decimal amount, string currency)
        {
            Bank.SomeBankWork();
            checkLimits(amount, currency);                          //first, check limits for internet transactions

            string cardNumberStr;                                           //card number entered by user
            ulong cardNumber;                                               //parsed card number
            string cardNumberPattern = @"^[54][0-9]{15}$";
            string cardType;
            Regex regex;


            //accept data from user

            //set card number by user

            Console.Write("\n\nEnter your card number, please (you can use spaces): ");
            cardNumberStr = Console.ReadLine().Replace(" ", "");

            regex = new Regex(cardNumberPattern);

            while (!regex.Match(cardNumberStr).Success || !ulong.TryParse(cardNumberStr, out cardNumber))
            {
                Console.Write("\n\nWrong card number. Try again: ");
                cardNumberStr = Console.ReadLine().Replace(" ", "");
            }


            //set card type
            cardType = cardNumber.ToString().StartsWith("5") ? "Mastercard" : "Visa";

            Console.WriteLine($"\n\nYou’ve deposited {amount} {currency} to your card successfully");
            Console.WriteLine($"\nCard number: {cardNumber}\t{cardType}");
        }


        void checkLimits(decimal amount, string currency)
        {
            decimal amountConvertedToUAH = ConvertMoney.ConvertAmount(amount, currency, "UAH", ExchangeRate.GetExchangeRate());

            if (amountConvertedToUAH > internetLimitUAH)
                throw new LimitExceededException($"Exceeded the limit of {internetLimitUAH} UAH for Internet transactions.");
        }
    }
}
