using System;
using System.Linq;
using System.Collections.Generic;
using Library.Exceptions;

namespace Library
{
    public class BettingPlatformEmulator
    {
        List<Player> Players;       //list of the platform players
        Player ActivePlayer;
        Account Account;
        BetService betService;
        PaymentService paymentService;

        public BettingPlatformEmulator()
        {
            Account = new Account("USD");           //USD - base currency
            Players = new List<Player>();
            betService = new BetService();
            paymentService = new PaymentService();
        }

        //configuration for user's set parameters
        //if change configuration, check the code where it is used
        readonly static string[] data = new[] { "first name", "last name", "email", "password" };
        readonly static int[] minDataValue = new[] { 3, 2, 5, 4 };
        readonly static bool[] replaceSpacesForData = new[] { false, true, true, true };


        //entering the chat bot menu and starting interaction with the player
        public void Start()
        {
            uint userChoice;
            bool success;

            while(true)
            {
                if (ActivePlayer == null)
                {
                    Console.WriteLine("\n\nSelect menu item and enter its number\n");
                    Console.WriteLine("1. Register\n2. Login\n3. Stop\n");

                    success = uint.TryParse(Console.ReadLine(), out userChoice);

                    while (!success)
                    {
                        Console.WriteLine("\nEnter 1 to register, 2 to login or 3 to exit.");
                        success = uint.TryParse(Console.ReadLine(), out userChoice);
                    }

                    switch (userChoice)
                    {
                        case 1:
                            {
                                Register();

                            }; break;

                        case 2:
                            {
                                Login();

                            }; break;

                        case 3:
                            {
                                Exit();
                                return;
                            };

                        default: Console.WriteLine("\nThere are no such command. Try again."); break;
                    }
                }


                //ActivePlayer != null
                if (ActivePlayer != null)
                {
                    Console.WriteLine("\n\nSelect menu item and enter its number\n");
                    Console.WriteLine("1. Deposit\n2. Withdraw\n3. Get odds\n4. Bet\n5. Logout\n");

                    success = uint.TryParse(Console.ReadLine(), out userChoice);

                    while (!success)
                    {
                        Console.WriteLine("\nEnter 1 to top up your wallet, 2 to withdraw money from your wallet, " +
                            "3 to get odds, 4 to make a bet or 5 to logout.");
                        success = uint.TryParse(Console.ReadLine(), out userChoice);
                    }

                    switch (userChoice)
                    {
                        case 1:
                            {
                                Deposit();

                            }; break;

                        case 2:
                            {
                                Withdraw();

                            }; break;

                        case 3:
                            {
                                GetOdds();

                            }; break;
                        case 4:
                            {
                                Bet();

                            }; break;
                        case 5:
                            {
                                Logout();

                            }; break;

                        default: Console.WriteLine("\nThere are no such command. Try again."); break;
                    }
                }
            }

        }

        //exit the chat bot menu
        public void Exit()
        {
            Console.WriteLine("\n\nYou exit the betting platform. Bye!\n\n");
        }

        //register new player
        void Register()
        {
            string[] newPlayerData = new string[4];

            //accept string data from user
            for(int i=0; i<data.Length; i++)
            {
                ValidateUserStringData(data[i], minDataValue[i], out newPlayerData[i], replaceSpacesForData[i]);
            }

            //accept account's currency from user
            AcceptCurrencyFromUser(out string userCurrency, "Enter currency for your account, please");

            try
            {
                Player newPlayer = new Player(newPlayerData[0], newPlayerData[1], newPlayerData[2], newPlayerData[3], userCurrency);
                Players.Add(newPlayer);

                Console.WriteLine("\n\nYou are successfully registered on the platform!");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("\n\nWe're sorry, registration is temporarily unavailable...\n");
                return;
            }
            catch(Exception){ }

        }

        //login
        void Login()
        {
            //accept login from user
            ValidateUserStringData("login", minDataValue[2], out string userLogin, replaceSpacesForData[2]);

            //find player by login
            Player player = Players.Find(pl => pl.Email == userLogin);

            if (player == null)
            {
                Console.Write($"\nThere is no player with login {userLogin}.");
                return;
            }

            //accept password
            ValidateUserStringData("login", minDataValue[3], out string userPswrd, replaceSpacesForData[3]);

            //check password
            if (!player.IsPasswordValid(userPswrd))
            {
                Console.Write($"\nWrong password {userPswrd}.");
                return;
            }

            ActivePlayer = player;
            Console.WriteLine($"\n\nWelcome, {ActivePlayer.FirstName} {ActivePlayer.LastName}!");
        }               

        //logout
        void Logout()
        {
            Console.WriteLine($"\n\nGood bye, {ActivePlayer.FirstName} {ActivePlayer.LastName}! Hope to see you soon.\n");
            ActivePlayer = null;
        }

        void Deposit()
        {
            if(ActivePlayer != null)
            {
                //accept amount from user
                AcceptAmountFromUser(out decimal amount, "Please, enter the amount to top up your account");

                //accept currency from user
                AcceptCurrencyFromUser(out string currency, "Enter currency for your account, please");

                try
                {
                    //withdraw from card
                    paymentService.StartDeposit(amount, currency);

                    //deposit the player's wallet and platform's balance
                    ActivePlayer.Deposit(amount, currency);                         //top up player's wallet
                    Account.Deposit(amount, currency);                              //top up platform's balance

                    Console.WriteLine($"\n\nYour wallet at the betting platform account has been replenished by {amount} {currency}.");
                }
                catch (InsufficientFundsException ex)
                {
                    if (ex.MethodName == "GiftVoucher")
                        IfPaymentExceptionOcurred(ex.Message, null, 0, null);
                    else
                        IfPaymentExceptionOcurred(ex.Message, "Please, try to make a new transaction with lower amount.", 0, null);
                }
                catch (LimitExceededException ex)
                {
                    IfPaymentExceptionOcurred(ex.Message, "Please, try to make a transaction with lower amount " +
                        "or change the payment method.", 0, null);
                }
                catch (PaymentServiceException ex)
                {
                    if(ex.MethodName == "GiftVoucher")
                        IfPaymentExceptionOcurred(ex.Message, null, 0, null);
                    else
                        IfPaymentExceptionOcurred(null, "Something went wrong. Try again later...", 0, null);
                }

            }
        }

        void Withdraw()
        {
            if (ActivePlayer != null)
            {
                //accept amount from user
                AcceptAmountFromUser(out decimal amount, "Please, enter the amount to withdraw");
                
                //accept currency from user
                AcceptCurrencyFromUser(out string currency, "Enter currency for your account, please");

                try
                {
                    ActivePlayer.Withdraw(amount, currency);                //withdraw the amount from player's balance

                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("\n\nThere is insufficient funds on your account.");
                    return;
                }

                try
                {
                    Account.Withdraw(amount, currency);                     //withdraw the amount from platform's balance
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"\n\nThere is some problem on the platform side. Please try it later.");

                    //return the withdrawed amount to players account if not success to withdraw from the platform balance
                    ActivePlayer.Deposit(amount, currency);
                    return;
                }


                //successful withdrawal

                //deposit player's card
                try
                {
                    paymentService.StartWithdrawal(amount, currency);
                    Console.WriteLine($"\n\nYou have withdrawn {amount} {currency} from the betting platform account.");
                }
                catch (InsufficientFundsException ex)
                {
                    IfPaymentExceptionOcurred(ex.Message, "Please, try to make a new transaction with lower amount.",
                        amount, currency);

                }
                catch (LimitExceededException ex)
                {
                    IfPaymentExceptionOcurred(ex.Message, "Please, try to make a transaction with lower amount " +
                        "or change the payment method.", amount, currency);
                }
                catch (PaymentServiceException)
                {
                    IfPaymentExceptionOcurred(null, "Something went wrong. Try again later...", amount, currency);
                }
            }
        }

        //return odd from bet service
        void GetOdds()
        {
            Console.WriteLine($"\n\nCurrent odds = {betService.GetOdds()}");          //Odd or GetOdds()???
        }

        void Bet()
        {
            if (ActivePlayer != null)
            {
                decimal betResult;

                //accept amount from user
                AcceptAmountFromUser(out decimal amount, "Please, enter the amount to withdraw");

                //accept currency from user
                AcceptCurrencyFromUser(out string currency, "Enter currency for your account, please");


                //try to withdraw money from active player's account
                try
                {
                    ActivePlayer.Withdraw(amount, currency);
                }
                catch(InvalidOperationException ex)
                {
                    Console.WriteLine($"\n\n{ex.Message}");
                    return;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"\n\n{ex.Message}");
                    return;
                }


                //bet
                betResult = betService.Bet(amount);
                Console.WriteLine($"\n\nI’ve bet {amount} {currency} with the odd {betService.Odd}");

                if (betResult > 0)       //player won
                {
                    //top up active player's wallet
                    ActivePlayer.Deposit(betResult, currency);

                    //print congrats
                    Console.WriteLine($"You won {betResult} {currency}.");
                }
                else
                {
                    Console.WriteLine($"You lost.");
                }

            }
        }


        void AcceptAmountFromUser(out decimal amount, string messageToDisplay)
        {
            //set the amount by user
            Console.Write($"\n\n{messageToDisplay}: ");
            bool success = decimal.TryParse(Console.ReadLine(), out amount);

            while (!success || amount <= 0)
            {
                Console.Write("\n\nYou need to enter the amount. Try again: ");
                success = decimal.TryParse(Console.ReadLine(), out amount);
            }
        }

        void AcceptCurrencyFromUser(out string currency, string messageToDisplay)
        {
            string[] possibleCurrencies = Account.GetSupportedCurrencies();

            //set the currency by user
            Console.Write($"\n\n{messageToDisplay} (");
            PrintSupportedCurrencies(possibleCurrencies);
            Console.Write("): ");

            currency = Console.ReadLine();

            while (!possibleCurrencies.Contains(currency))
            {
                Console.Write("\n\nThis currency is not supported. You can select: ");
                PrintSupportedCurrencies(possibleCurrencies);
                Console.Write("\nTry again: ");

                currency = Console.ReadLine();
            }
        }
        

        void ValidateUserStringData(string data, int minValue, out string dataValue, bool replaceEmptySpacesBetween = false)
        {
            Console.WriteLine($"\n\nPlease, enter your {data}: ");
            dataValue = Console.ReadLine().Trim();

            if (replaceEmptySpacesBetween)
                dataValue = dataValue.Replace(" ", "");

            while (dataValue == "" || dataValue.Length < minValue)
            {
                Console.WriteLine($"\n\nThe {data} must be minimum {minValue} symbols length!");
                Console.Write("Try again, please: ");
                dataValue = Console.ReadLine();
                Console.WriteLine();
            }
        }

        void PrintSupportedCurrencies(string[] possibleCurrencies)
        {
            for (int i = 0; i < possibleCurrencies.Length; i++)
            {
                if (i < possibleCurrencies.Length - 2)
                    Console.Write($"{possibleCurrencies[i]}, ");
                else if (i == possibleCurrencies.Length - 2)
                    Console.Write($"{possibleCurrencies[i]} or ");
                else Console.Write($"{possibleCurrencies[i]}");
            }
        }

        void IfPaymentExceptionOcurred(string message, string advice, decimal amount, string currency)
        {
            Console.WriteLine("\n\n");
            if (message != null)
                Console.WriteLine($"{message}\n");

            if (advice != null)
                Console.WriteLine($"{advice}");


            if (amount != 0 && currency != null)                //if amount and currency were given
            {
                //if smth went wrong, return money to the player and platform
                ActivePlayer.Deposit(amount, currency);
                Account.Deposit(amount, currency);
            }
        }
    }
}
