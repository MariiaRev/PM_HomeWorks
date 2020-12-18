using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Exceptions;

namespace Library
{
    public class GiftVoucher : PaymentMethodBase, ISupportDeposit
    {
        public GiftVoucher()
        {
            Name = "GiftVoucher";
        }

        static List<decimal> GiftVoucherDenomination = new List<decimal> { 100, 500, 1000 };
        static HashSet<ulong> usedGiftVouchers = new HashSet<ulong>();
        public void StartDeposit(decimal amount, string currency)
        {
            Bank.SomeBankWork();

            if (!GiftVoucherDenomination.Contains(amount))
            {
                string errorMessage = "\n\nThe amount does not correspond to the gift voucher denomination.\n" +
                    $"Please, try again with another sum.\n{AvailableDenominationStr()}";

                throw new PaymentServiceException(errorMessage, nameof(GiftVoucher));
            }
            else
            {
                string cardNumberStr;                                           //card number entered by user
                ulong cardNumber;                                               //parsed card number
                string cardNumberPattern = @"^[0-9]{10}$";


                //set card number by user

                Console.Write("\n\nEnter your card number, please (you can use spaces): ");
                cardNumberStr = Console.ReadLine().Replace(" ", "");

                Regex regex = new Regex(cardNumberPattern);

                while (!regex.Match(cardNumberStr).Success || !ulong.TryParse(cardNumberStr, out cardNumber))
                {
                    Console.Write("\n\nInvalid card number format. Try again: ");
                    cardNumberStr = Console.ReadLine().Replace(" ", "");
                }

                //add voucher to the used voucher's list, if can't because it was used - cancel operation
                bool notUsedGiftVoucher = usedGiftVouchers.Add(cardNumber);

                if (!notUsedGiftVoucher)            //if gift voucher were already used
                    throw new InsufficientFundsException($"Gift voucher funds {cardNumber} have already been used.", nameof(GiftVoucher));

                Console.WriteLine($"\n\nYou’ve withdrawn {amount} {currency} from your {cardNumber} card successfully");
            }
        }

        string AvailableDenominationStr()
        {
            string availableDenomination = "";

            if (GiftVoucherDenomination.Any())
            {
                availableDenomination += "\nAvailable denominations: ";

                for (int i = 0; i < GiftVoucherDenomination.Count; i++)
                {
                    if (i != GiftVoucherDenomination.Count - 1)
                        availableDenomination += GiftVoucherDenomination[i] + " / ";
                    else
                        availableDenomination += GiftVoucherDenomination[i] + "\n";
                }
            }
            
            return availableDenomination;
        }
    }
}
