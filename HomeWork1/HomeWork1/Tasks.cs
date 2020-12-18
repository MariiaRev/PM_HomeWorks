using System;
using System.Linq;                      //only for Count(lambda) in game statistics calculations for rock-paper-scissors game
using System.Diagnostics;               //for Debug and Stopwatch
using System.Collections.Generic;
using System.Text.RegularExpressions;   //for parsing the expression entered by user in task 3 (calculator)

namespace HomeWork1
{
    public class Tasks
    {
        const string authorName = "Mariia Revenko";
        enum authorBirthDate
        {
            Year = 1999,
            Month = 9,
            Day = 22,
        };



        //expression calculation
        public static void Task1_1()
        {
            //parameters
            const string expr = "y = (e^a + 4*lg(c)) / sqrt(b) * abs(arctg(d)) + 5/sin(a)";

            const int b = (int)authorBirthDate.Year;    //b = 1999;
            const int c = (int)authorBirthDate.Month;   //c = 9;
            const int d = (int)authorBirthDate.Day;     //d = 22;



            //print greetings
            Console.WriteLine($"{"",18}This is Homework 1 Task 1.1 in which\n{"",15}an expression y = f(a,b,c,d) is calculated.");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");


            //print what expression is calculating and what parameters will be used
            Console.WriteLine($"You're going to calculate the expression:\n{expr}\n");
            Console.WriteLine("Parameters in the expression are:");
            Console.WriteLine("a - user parameter (you can enter it below)");
            Console.WriteLine($"b - parameter \"author birth year\", b = {b}");
            Console.WriteLine($"c - parameter \"author birth  month\", c = {c}");
            Console.WriteLine($"d - parameter \"author birth day\", d = {d}\n\n");


            //set parameter a by user
            Console.Write("Enter parameter a: ");
            double.TryParse(Console.ReadLine(), out double a);


            //calculate expression
            double y = ExpressionCalculation.CalculateExpression(a, b, c, d);

            //print the result
            Console.WriteLine($"\ny = {Math.Round(y, 4)}\n\n");



        }


        //margin calculation
        public static void Task1_2()
        {
            //parameters
            string name1, name2;                //participants' names
            double H, D, A;                     //coefficients "victory of the first" (home), "draw", "victory of the second" (away)


            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 1.2\n{"",28}Margin calculation.");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");


            //set participants' names and parameters by user

            Console.WriteLine("Enter the first participant's name:");
            name1 = Console.ReadLine();
            Console.WriteLine("\nEnter the first participant's name:");
            name2 = Console.ReadLine();


            Console.Write("\nEnter the odd of the first participant's victory: ");
            double.TryParse(Console.ReadLine(), out H);

            Console.Write("\nEnter the odd for a draw: ");
            double.TryParse(Console.ReadLine(), out D);

            Console.Write("\nEnter the odd of the second participant's victory: ");
            double.TryParse(Console.ReadLine(), out A);


            //calculations
            ProbabilitiesAndMarginCalculation.Calculate(H, D, A, out double probH,
                                                            out double probD, out double probA, out double margin);


            //print results
            //additional strings 
            string print1 = "Victory of " + name1 + ": ";
            string print2 = "Victory of " + name2 + ": ";

            Console.WriteLine($"\n\n{"",20}$$$ Calculation results $$$");
            Console.WriteLine($"\n{"",12}{print1,-40} {probH,3}%");
            Console.WriteLine($"{"",12}{"Draw:",-40} {probD,3}%");
            Console.WriteLine($"{"",12}{print2,-40} {probA,3}%");
            Console.WriteLine($"{"",12}{"Bookmaker's margin:",-40} {margin,3}%\n\n");
        }


        //calculation the sum of the series
        public static void Task1_3()
        {
            //set parameters
            double epsilon = 1 / (double)authorBirthDate.Year;
            Debug.WriteLine(epsilon);

            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 1.3\n{"",21}Sum of the series calculation.");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");

            //print description of the series and used parameters
            Console.WriteLine("The series are: the sum from i = 1 to infinity = 1 / (i * (i+1)),\nStop condition: element < epsilon");
            Console.WriteLine($"\nEpsilon = 1/1999 = {Math.Round(epsilon, 4)}");


            //calculate result
            double sum = SeriesSumCalculation.Calculate(epsilon);

            //print result
            Console.WriteLine($"\n\nThe sum of the series is {Math.Round(sum, 4)}.");
        }


        //find prime numbers in range set by user
        public static void Task1_4(int searchWay = 1)
        {
            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 1.4\n{"",18}Prime numbers search in range limits.");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");


            //set search range limits by user
            Console.Write("Enter the lower range limit: ");
            int.TryParse(Console.ReadLine(), out int limit1);

            Console.Write("\nEnter the upper range limit: ");
            int.TryParse(Console.ReadLine(), out int limit2);


            //find primes
            List<int> primes;
            if (searchWay == 2)
                primes = PrimeNumbers.SearchOptimized(limit1, limit2);
            else
                primes = PrimeNumbers.SearchStandard(limit1, limit2);

            //print found primes
            Console.WriteLine("\n\nPrimes: ");
            for (int i = 0; i < primes.Count; i++)
            {
                Console.Write($"{primes[i],5}");
                if ((i + 1) % 10 == 0)
                {
                    //Console.WriteLine(i % 10 + "\ti");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\n\n");
        }



        //rock-paper-scissors game
        public static void Task2_1()
        {
            //additional variables

            List<string> statistics = new List<string>(1);                          //list for saving the results of all matches
            string userName = "Player";                                             // user name (for beautiful results printing)
            string userCommand;                                                     //commands entered by user 
            string computerCommand = "";                                            //commands generated by computer
            string[] RPSCommands = new[] { "rock", "paper", "scissors" };           //available commands for rock-paper-scissors game

            const string availableCommands = "You can enter:\n"                     //user-accessible commands
                + "rock      -  if you choose 'rock' figure\n"
                + "paper     -  if you choose 'paper' figure\n"
                + "scissors  -  if you choose 'scissors' figure\n"
                + "exit      -  if you want to exit the game\n";


            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 2.1\n{"",26}Rock, Paper, Scissors.");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");

            //receive user name (for beautiful results printing)
            Console.Write("Enter your name: ");
            string entered = Console.ReadLine();
            userName = !string.IsNullOrWhiteSpace(entered) ? entered : userName;    //userName = what user entred or "Player" if user entered empty string or only spaces


            // PRINT RULES AND COMMANDS

            //print game rules
            Console.WriteLine($"\nWelcome at 'rock-paper-scissors' game, {userName}!\n");

            Console.WriteLine($"\n\n{"",26}{"* * *  RULES  * * *"}\n\n");

            Console.WriteLine("A play of rock will beat a play of scissors ('a rock blunts scissors').");
            Console.WriteLine("A play of paper will beat a play of rock ('paper covers a rock').");
            Console.WriteLine("A play of scissors will beat a play of paper ('scissors cuts paper').");
            Console.WriteLine("If both you and the computer play the same figure a draw occurs.");

            Console.WriteLine("\n\nAfter you enter 'exit' command you'll see the game statistics.");
            Console.WriteLine("You get one point if you win, a computer gets one point if you lose"
                                + ".\nBoth you and the computer get one point for draw.\n\n");

            //print commands
            Console.WriteLine($"\n{availableCommands}\n");
            Console.WriteLine($"\n\n{"",33}{"* * *"}\n\n");

            // end PRINT RULES AND COMMANDS 



            while (true)
            {
                //receive user command choice
                Console.Write("\n\nYour choice (enter command): ");
                userCommand = Console.ReadLine().ToLower();


                //define and print the results

                if (userCommand == "exit")
                {
                    if (statistics.Count > 0)
                    {
                        Console.WriteLine($"\n\n\nThank you for the game, {userName}!");
                        Console.WriteLine($"\n{"",15}The statistics of played matches\n");

                        for (int i = 0; i < statistics.Count; i++)
                            Console.WriteLine($"{"",17}{"Match " + (i + 1),-12}{"-",3}{statistics[i],12}");

                        Console.WriteLine($"\n{"",5}{"Winnings: " + statistics.Count(elem => elem == "winning"),15}" +
                            $"{"Draws: " + statistics.Count(elem => elem == "draw"),15}" +
                            $"{"Losings: " + statistics.Count(elem => elem == "losing"),15}\n" +
                            $"\n{"",19}{"TOTAL SCORE ",-15}{"",3}{statistics.Count(elem => elem == "winning" || elem == "draw") + ":" + statistics.Count(elem => elem == "losing" || elem == "draw")}" +
                            $"\n{ "",19}{"TOTAL MATCHES",-15}{"",5}{statistics.Count}\n");
                    }
                    else
                    {
                        Console.WriteLine("\n\nNo matches played.");
                    }
                    Console.WriteLine($"\nGoodbye, {userName}!\n\n");
                    break;                                                      //exit game
                }

                if (Array.Exists(RPSCommands, elem => elem == userCommand))
                {
                    //choose computer's command and print it for user

                    Random rand = new Random();                         //random variable
                    int chosenNumber = rand.Next(1, 4);                 //generated random value
                    computerCommand = chosenNumber == 1 ? "rock" : chosenNumber == 2 ? "paper" : "scissors";    //matching command and generated value

                    Console.WriteLine($"Computer: {computerCommand}");



                    //determine the winner

                    RockPaperScissors.WinnerDetermine2(userCommand, computerCommand, out bool userWin, out bool computerWin);

                    if (userWin && computerWin)              //draw
                    {
                        statistics.Add("draw");
                        Console.WriteLine("\nDraw!");
                    }
                    else if (userWin && !computerWin)       //user wins
                    {
                        statistics.Add("winning");
                        Console.WriteLine("\nYou win!");
                    }
                    else if (!userWin && computerWin)       //user loses
                    {
                        statistics.Add("losing");
                        Console.WriteLine("\nYou lose!");
                    }
                }
                else
                {
                    Console.WriteLine($"\nWrong command! {availableCommands}\nTry again.");
                }
            }

        }


        //figure square calculation

        //COMMAND MODE
        public static double Task2_2(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                switch (args[0])
                {
                    case "square":
                        {
                            if (args.Length > 1 && double.TryParse(args[1], out double a))
                            {
                                if (a > 0 && !double.IsInfinity(a))
                                    return Math.Round(new Square(a).GetArea(), 4);
                            }
                            return -1;                  //error
                        };

                    case "rectangle":
                        {
                            if (args.Length > 2
                                    && double.TryParse(args[1], out double a)
                                    && double.TryParse(args[2], out double b))
                            {
                                if (a > 0 && !double.IsInfinity(a) && b > 0 && !double.IsInfinity(b))
                                    return Math.Round(new Rectangle(a, b).GetArea(), 4);
                            }
                            return -1;                  //error
                        };
                    case "triangle":
                        {
                            if (args.Length > 3
                                    && double.TryParse(args[1], out double a)
                                    && double.TryParse(args[2], out double b)
                                    && double.TryParse(args[3], out double c))
                            {
                                if (a > 0 && !double.IsInfinity(a)
                                    && b > 0 && !double.IsInfinity(b)
                                    && c > 0 && !double.IsInfinity(c)
                                    && Triangle.IsTriangle(a, b, c))
                                    return Math.Round(new Triangle(a, b, c).GetArea(), 4);
                            }
                            return -1;                  //error
                        };
                    case "circle":
                        {
                            if (args.Length > 1 && double.TryParse(args[1], out double r))
                            {
                                if (r > 0 && !double.IsInfinity(r))
                                    return Math.Round(new Circle(r).GetArea(), 4);
                            }
                            return -1;
                        };
                    default: return -1;             //error
                }
            }

            return -1;                              //error
        }

        //DIALOG MODE
        public static void Task2_2()
        {
            //additional variables

            string userCommand;                                         //commands entered by user
            IFigure figure;                                             //figure selected by user

            const string availableCommands = "You can enter:\n"         //user-accessible commands
                + "square     -  to get the area of a square\n"
                + "rectangle  -  to get the area of a rectangle\n"
                + "triangle   -  to get the area of a triangle\n"
                + "circle     -  to get the area of a circle\n"
                + "commands   -  to get this list of commands\n"
                + "exit       -  to exit the program\n";


            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 2.2\n{"",24}Figure square calculation");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");



            //print program rules and commands
            Console.WriteLine($"\n\n{"",26}{"* * *  RULES  * * *"}\n\n");

            Console.WriteLine("This program calculates and returns you the area of the figure you selected.");
            Console.WriteLine("The area is calculated based on the figure's sides you entered.");
            Console.WriteLine($"\n{availableCommands}");

            Console.WriteLine($"\n\n{"",33}{"* * *"}\n\n");


            while (true)
            {
                //receive user command choice
                Console.Write("\n\nYour choice (enter command): ");
                userCommand = Console.ReadLine().ToLower();

                switch (userCommand)
                {
                    case "commands": Console.WriteLine($"\n{availableCommands}\n{"***",37}\n"); break;
                    case "exit": Console.WriteLine("\nBye-bye!\n\n"); return;
                    case "square":
                        {
                            double a;                                                   //square's side

                            Console.Write("\nEnter square's side: ");
                            bool success = double.TryParse(Console.ReadLine(), out a);  //shows that user entered (in)correct data

                            while (!success || a <= 0 || double.IsInfinity(a))
                            {
                                Console.WriteLine("\nThat was not square's side! Be careful, please.");
                                Console.Write("Enter square's side: ");
                                success = double.TryParse(Console.ReadLine(), out a);
                            }

                            figure = new Square(a);

                            Console.WriteLine($"\n\nThe area of the square with a side {a} is {Math.Round(figure.GetArea(), 4)}\n");

                        }; break;

                    case "rectangle":
                        {
                            Dictionary<string, double> sidesLengths = new Dictionary<string, double>(2);
                            string[] sides = new[] { "first", "second" };

                            foreach (string str in sides)
                            {
                                Console.Write($"\nEnter {str} rectangle's side: ");
                                bool success = double.TryParse(Console.ReadLine(), out double side);

                                while (!success || side <= 0 || double.IsInfinity(side))
                                {
                                    Console.WriteLine("\nThat was not rectangle's side! Be careful, please.");
                                    Console.Write($"Enter {str} rectangle's side: ");
                                    success = double.TryParse(Console.ReadLine(), out side);
                                }

                                sidesLengths[str] = side;
                            }

                            figure = new Rectangle(sidesLengths[sides[0]], sidesLengths[sides[1]]);

                            Console.WriteLine($"\n\nThe area of the rectangle with sides {sidesLengths[sides[0]]}" +
                                $" and {sidesLengths[sides[1]]} is {Math.Round(figure.GetArea(), 4)}\n");

                        }; break;

                    case "triangle":
                        {
                            Dictionary<string, double> sidesLengths = new Dictionary<string, double>(3);
                            string[] sides = new[] { "first", "second", "third" };


                            foreach (string str in sides)
                            {
                                Console.Write($"\nEnter {str} triangle's side: ");
                                bool success = double.TryParse(Console.ReadLine(), out double side);

                                while (!success || side <= 0 || double.IsInfinity(side))
                                {
                                    Console.WriteLine("\nThat was not triangle's side! Be careful, please.");
                                    Console.Write($"Enter {str} triangle's side: ");
                                    success = double.TryParse(Console.ReadLine(), out side);
                                }

                                sidesLengths[str] = side;
                            }

                            bool isTriangle = Triangle.IsTriangle(sidesLengths[sides[0]], sidesLengths[sides[1]], sidesLengths[sides[2]]);

                            if (!isTriangle)
                            {
                                Console.WriteLine($"\nThere is no triangle with sides {sidesLengths[sides[0]]}," +
                                    $" {sidesLengths[sides[1]]} and {sidesLengths[sides[2]]}.\n");
                                Console.WriteLine("The sum of any 2 sides of a triangle must be greater than the measure of the third side!\n");
                                Console.WriteLine("Try again.");
                                break;
                            }

                            figure = new Triangle(sidesLengths[sides[0]], sidesLengths[sides[1]], sidesLengths[sides[2]]);

                            Console.WriteLine($"\n\nThe area of the triangle with sides {sidesLengths[sides[0]]}," +
                                $" {sidesLengths[sides[1]]} and {sidesLengths[sides[2]]} is {Math.Round(figure.GetArea(), 4)}\n");

                        }; break;
                    case "circle":
                        {
                            double r;                                                   //circle's radius

                            Console.Write("\nEnter circle's radius: ");
                            bool success = double.TryParse(Console.ReadLine(), out r);  //shows that user entered (in)correct data

                            while (!success || r <= 0 || double.IsInfinity(r))
                            {
                                Console.WriteLine("\nThat was not circle's radius! Be careful, please.");
                                Console.Write("Enter circle's radius: ");
                                success = double.TryParse(Console.ReadLine(), out r);
                            }

                            figure = new Circle(r);

                            Console.WriteLine($"\n\nThe area of the circle with a radius {r} is {Math.Round(figure.GetArea(), 4)}\n");

                        }; break;
                    default: Console.WriteLine($"\nWrong command! {availableCommands}\nTry again."); break;
                }
            }

            //IFigure figure = new Square();
        }



        //array's statistics

        //COMMAND MODE
        public static void Task2_3(string[] args)
        {
            int n = args.Length;        //array length
            int[] array = new int[n];

            //try parse an array entered by user
            for(int i=0; i<n; i++)
            {
                if (int.TryParse(args[i], out int value))
                    array[i] = value;
                else
                {
                    //error
                    Console.WriteLine("-1");
                    return;
                }
            }

            //print array statistics
            Console.WriteLine($"Min = {ArrayIntStatistics.Min(array)}");
            Console.WriteLine($"Max = {ArrayIntStatistics.Max(array)}");
            Console.WriteLine($"Sum = {ArrayIntStatistics.Sum(array)}");
            Console.WriteLine($"Average = {ArrayIntStatistics.Avg(array)}");
            Console.WriteLine($"Standard deviation = {ArrayIntStatistics.StandardDeviation(array)}");

            //print sorted array
            Console.WriteLine($"Sorted array: ");
            //int[] sortedArr = 
            foreach(int elem in ArrayIntStatistics.Sort(array))
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();

        }

        //DIALOG MODE
        public static void Task2_3()
        {
            //variables
            int arrayAmount;                //elements amount of an array entered by user
            bool success;                   //shows that user entered (in)correct data

            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 2.3\n{"",27}Array's statistics");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");


            //set array's length by user

            Console.Write("Enter the integer number - an amout of elements for the array you'd like to create: ");
            success = int.TryParse(Console.ReadLine(), out arrayAmount);

            while(!success || arrayAmount <= 0)
            {
                Console.Write("\n\nYou need to enter integer number > 0. Try again: ");
                success = int.TryParse(Console.ReadLine(), out arrayAmount);
            }

            //receive array's elements
            int[] array = new int[arrayAmount];

            Console.WriteLine("Enter the integer elements of the array");

            for(int i=0; i<array.Length; i++)
            {
                Console.Write($"\n\nElement #{i+1}: ");
                success = int.TryParse(Console.ReadLine(), out int value);

                while(!success)
                {
                    Console.Write("\n\nYou need to enter integer number. Try again: ");
                    success = int.TryParse(Console.ReadLine(), out value);
                }

                array[i] = value;
            }

            //print entered array
            Console.WriteLine($"\n\nYou entered {arrayAmount}-element array:");
            for(int i=0; i<array.Length; i++)
                Console.Write($"{array[i]} ");
            Console.WriteLine();

            //calculate and print array's statistics
            Console.WriteLine($"\n\n\n{"", 17}{"* * * Your array's statistics * * *"}\n");
            
            int min = (int)ArrayIntStatistics.Min(array);
            int max = (int)ArrayIntStatistics.Max(array);
            int sum = ArrayIntStatistics.Sum(array);
            double avg = (double)ArrayIntStatistics.Avg(array);
            double std = (double)ArrayIntStatistics.StandardDeviation(array);


            Console.WriteLine($"{"",17}{"Min ", -20}{"", 3}{min, 12}");
            Console.WriteLine($"{"",17}{"Max ", -20}{"", 3}{max, 12}");
            Console.WriteLine($"{"",17}{"Sum ", -20}{"", 3}{sum, 12}");
            Console.WriteLine($"{"",17}{"Average ", -20}{"", 3}{Math.Round(avg, 4), 12}");
            Console.WriteLine($"{"",17}{"Standard deviation ", -20}{"", 3}{Math.Round(std, 4), 12}");


            //sort and print sorted array
            Console.WriteLine($"\n\n\n{"", 24}{"* * * Sorted array * * *"}\n");

            foreach (int elem in ArrayIntStatistics.Sort(array))
            {
                Console.Write($"{elem} ");
            }

            Console.WriteLine("\n\n\n");
        }


        //more-less - guess a number
        public static void Task2_4()
        {
            //consts
            const int MIN_LIMIT = 0;                        //minimum value of the lower range limit
            const int MAX_LIMIT = 1000000;                  //maximum value of the upper range limit

            //variables

            int lower, upper;                               //lower and upper range limits set by user
            bool success;                                   //shows that user entered (in)correct data
            string input;                                   //user's attempt to guess number or exit command
            int attemptsCounter = 0;                        //user attempts counter

            Stopwatch watch = new Stopwatch();              //watches for counting game time
            


            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 2.4\n{"",26}Try to guess a number");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");


            //print RULES
           
            Console.WriteLine($"\n\n{"",26}{"* * *  RULES  * * *"}\n\n");
            
            Console.WriteLine("You need to guess the number chosen by the computer.");
            Console.WriteLine("You set the range in which the computer generates the number you need to guess.");
            Console.WriteLine("There should be at least two numbers in the range you set.");
            Console.WriteLine("The lower and the upper limits of the set range are included for guessing.\n");

            Console.WriteLine("After each of your attempts to guess the number, the computer gives you a hint.\n" +
                "If the number you entered is greater than the chosen one, you will see \"Less!\" on the screen. " +
                "If the number you entered is less than the chosen one, you will see \"More!\" on the screen.");

            Console.WriteLine("If you guess the number, you will see \"You guessed!\" on the screen.\n" +
                "If you want to give up, you can enter the command \"exit\" instead of a number.");

            Console.WriteLine("\nYou get points for the minimum number of guessing attempts you use.\n" +
                "If you give up, you get 0 points.\n\nHave a good time and good luck!");

            Console.WriteLine($"\n\n{"",33}{"* * *"}\n\n");



            //set range limits by user

            //set lower limit
            Console.Write($"\nEnter lower range limit (integer from {MIN_LIMIT} to {MAX_LIMIT - 1}): ");
            success = int.TryParse(Console.ReadLine(), out lower);

            while (!success || lower < MIN_LIMIT || lower > MAX_LIMIT - 1)
            {
                Console.WriteLine($"\n\nYou need to enter an integer number from {MIN_LIMIT} to {MAX_LIMIT - 1}.");
                Console.Write("\nTry again: ");
                success = int.TryParse(Console.ReadLine(), out lower);
            }

            //set upper limit
            Console.Write($"\n\nEnter upper range limit (integer from {lower + 1} to {MAX_LIMIT}): ");
            success = int.TryParse(Console.ReadLine(), out upper);

            while (!success || upper <= lower || upper > MAX_LIMIT)
            {
                Console.WriteLine($"\n\nYou need to enter an integer number from {lower + 1} to {MAX_LIMIT}.");
                Console.Write("\nTry again: ");
                success = int.TryParse(Console.ReadLine(), out upper);
            }


            //generate random number
            Random rand = new Random();
            int chosenNum = rand.Next(lower, upper + 1);
            Console.WriteLine($"\n\nI've thought of a number between {lower} and {upper}");
            //Debug.WriteLine(chosenNum);




            //start count game time
            watch.Start();

            //user guesses the chosen number
            while (true)
            {
                Console.Write("\n\nTry to guess: ");
                input = Console.ReadLine();

                //user entered 'exit' command
                if (input.Trim() == "exit")
                {
                    //stop count game time
                    watch.Stop();

                    Console.WriteLine($"\n\n{"", 30}You gave up.");
                    Console.WriteLine($"{"", 27}The number was {chosenNum}.");
                    GuessNumber_PrintStatistics(0, attemptsCounter, watch.Elapsed);

                    return;
                }

                //user entered a number out of range or not a number
                if(!int.TryParse(input, out int value) || value < lower || value > upper)
                {
                    Console.WriteLine($"\nEnter an integer number between {lower} and {upper}. Try again!");
                    continue;
                }


                //user entered a number between lower and upper:

                //user guessed the number
                if(value == chosenNum)
                {
                    //stop count game time
                    watch.Stop();

                    //the last attempt does not count in score calculations!
                    uint score = GuessNumber_Score(upper - lower + 1, attemptsCounter);

                    attemptsCounter++;

                    //print congrats and statistics
                    Console.WriteLine($"\n\n{"", 30}You guessed!\n");
                    GuessNumber_PrintStatistics(score, attemptsCounter, watch.Elapsed);

                    return;
                }

                //user did not guess the number
                if(value > chosenNum)
                    Console.WriteLine($"\n{"", 35}Less!");
                else
                    Console.WriteLine($"\n{"", 35}More!");
                
                attemptsCounter++;
            }

        }




        //CALCULATOR

        //COMMAND MODE
        public static int Task3(string[] args)
        {
            string userExpression;
            dynamic result;

            userExpression = string.Join(" ", args);


            //remove extra spaces from the beginning and end of the expression
            userExpression = userExpression.Trim();

            //replace points by commas
            userExpression = userExpression.Replace('.', ',');


            //if true, operation != null and operands != null
            if (TryParseUserExpr(userExpression, out string operation, out Operands operands))
            {
                try
                {
                    switch (operation)
                    {
                        case "factorial": result = Calculator.Factorial((uint)operands.F); break;

                        case "add": result = Calculator.Add((double)operands.A, (double)operands.B); break;
                        case "substract": result = Calculator.Substract((double)operands.A, (double)operands.B); break;
                        case "multiply": result = Calculator.Multiply((double)operands.A, (double)operands.B); break;
                        case "divide": result = Calculator.Divide((double)operands.A, (double)operands.B); break;
                        case "divRem": result = Calculator.DivRem((double)operands.A, (double)operands.B); break;
                        case "power": result = Calculator.Pow((double)operands.A, (double)operands.B); break;

                        case "and": result = Calculator.And((int)operands.X, (int)operands.Y); break;
                        case "or": result = Calculator.Or((int)operands.X, (int)operands.Y); break;
                        case "xor": result = Calculator.Xor((int)operands.X, (int)operands.Y); break;
                        case "not": result = Calculator.Not((int)operands.X); break;

                        default: result = null; break;

                    }
                }
                catch (Exception)                //any exception
                {
                    Console.WriteLine(-1);
                    return -1;
                }
            }
            else
            {
                Console.WriteLine(-1);
                return -1;                      //data entry error
            }

            if (result != null)
            {
                Console.WriteLine(result);
                return 0;
            }

            Console.WriteLine(-1);
            return -1;
        }

        //DIALOG MODE
        public static void Task3()
        {
            string userExpression;
            dynamic result;


            string availableCommands = "\nYou can enter next commands:\n" +
                "help - to view available commands and expressions.\n" +
                "exit - to exit the calculator program.\n";

            string availableExpressions = $"\nYou can use next operations:\n" +
                $"\n  unary operations:\n" +
                $"{"",5}a!        to find the factorial of the a operand\n" +
                $"\n  binary operations:\n" +
                $"{"",5}+         to add two operands\n" +
                $"{"",5}-         to substract two operands\n" +
                $"{"",5}/ or \\    to divide two operands\n" +
                $"{"",5}* or x    to multiply two operands\n" +
                $"{"",5}%         to find division remainder\n" +
                $"{"",5}pow       to convert a number to some power\n" +
                $"\n  binary bitwise operations:\n" +
                $"{"",5}&         to find boolean AND of two operands\n" +
                $"{"",5}|         to find boolean OR of two operands\n" +
                $"{"",5}^         to find boolean XOR of two operands\n" +
                $"\n  unary bitwise operations:\n" +
                $"{"",5}!a        to find boolean NOT of the a operand\n" +
                $"";

            string someConstraints = "\nYou can enter only integer numbers for bitwise operations, " +
                "floating point numbers for binary non-bit operations and a number greater than zero for factorial." +
                "\nYou can use any separator - point or comma, for floating point numbers.\n" + 
                "\nRemember, you can enter only one (for unary operations) or two (for binary operations) operands.\n" +
                "Spaces between operands and operation sign are not limited.\n";

            string exprExamples = $"\nThe next expressions are valid:\n\n" +
                $" 1+2       1+2.5       1+2,5     1 + 2      -1 + -2\n" +
                $" 1-2       1-2.5       1-2,5     1 - 2      -1 - -2\n\n" +
                $" 1*2       1*2.5       1*2,5     1 * 2      -1 * -2\n" +
                $" 1x2       1x2.5       1x2,5     1 x 2      -1 x -2\n\n" +
                $" 1/2       1/2.5       1/2,5     1 / 2      -1 / -2\n" +
                $" 1\\2       1\\2.5       1\\2,5     1 \\ 2      -1 \\ -2\n\n" +
                $" 1%2       1%2.5       1%2,5     1 % 2      -1 % -2\n" +
                $"1pow2   1 pow 2.5   1 pow 2,5   1 pow 2    -1 pow -2\n\n" +

                $" 1&2                             1 & 2      -1 & -2\n" +
                $" 1|2                             1 | 2      -1 | -2\n" +
                $" 1^2                             1 ^ 2      -1 ^ -2\n\n" +
                $" !1                                           !-1\n\n" +
                $" 1!\n" +
                $"                                              -1\n" +
                $"\n\nThe next expressions are not valid:\n" +
                $"1+2+3, 1---2, 1*--2, +1, 1-, 1*, *1 and so on.\n";


            //print greetings
            Console.WriteLine($"{"",23}This is Homework 1 Task 3\n{"",31}Calculator");
            Console.WriteLine($"\n{"",21}### Made by {authorName} ###\n\n");



            Console.WriteLine($"\n\n{"",26}{"* * *  RULES  * * *"}\n\n");

            Console.WriteLine(availableCommands);
            Console.WriteLine(availableExpressions);
            Console.WriteLine(someConstraints);
            Console.WriteLine("\nEnter 'help' command to see examples of valid expressions.");

            Console.WriteLine($"\n\n{"",33}{"* * *"}\n\n");



            //user interaction and calculations
            while (true)
            {
                result = null;

                Console.Write("Enter expression/command: ");
                userExpression = Console.ReadLine();
                Console.WriteLine("\n");

                //remove extra spaces from the beginning and end of the expression
                userExpression = userExpression.Trim();

                //replace points by commas
                userExpression = userExpression.Replace('.', ',');


                if (userExpression == "exit")
                {
                    Console.WriteLine("\nBye!\n\n");
                    break;
                }

                if(userExpression == "help")
                {
                    Console.WriteLine(availableCommands);
                    Console.WriteLine(availableExpressions);
                    Console.WriteLine(someConstraints);
                    Console.WriteLine(exprExamples + "\n\n");
                    continue;
                }


                //if true, operation != null and operands != null
                if (TryParseUserExpr(userExpression, out string operation, out Operands operands))
                {
                    try
                    {
                        switch (operation)
                        {
                            case "factorial": result = Calculator.Factorial((uint)operands.F); break;

                            case "add": result = Calculator.Add((double)operands.A, (double)operands.B); break;
                            case "substract": result = Calculator.Substract((double)operands.A, (double)operands.B); break;
                            case "multiply": result = Calculator.Multiply((double)operands.A, (double)operands.B); break;
                            case "divide": result = Calculator.Divide((double)operands.A, (double)operands.B); break;
                            case "divRem": result = Calculator.DivRem((double)operands.A, (double)operands.B); break;
                            case "power": result = Calculator.Pow((double)operands.A, (double)operands.B); break;

                            case "and": result = Calculator.And((int)operands.X, (int)operands.Y); break;
                            case "or": result = Calculator.Or((int)operands.X, (int)operands.Y); break;
                            case "xor": result = Calculator.Xor((int)operands.X, (int)operands.Y); break;
                            case "not": result = Calculator.Not((int)operands.X); break;

                            default: result = null; break;

                        }
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("You cannot divide by zero!");
                        continue;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("You entered too big number(s) for this operation.");
                        continue;
                    }
                    catch (Exception)                //unexpected exception
                    {
                        Console.WriteLine("We're sorry, there is the program crash...");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Data entry error!");
                    Console.WriteLine("You can view available commands and expressions by entering 'help' command.");
                }

                if(result != null)
                    Console.WriteLine($"Result is {result}");

                Console.WriteLine("\n\n");
            }
        }



        //functions for task 2.4 (more-less - guess a number)


        //find power for which 2^power is the nearest to the number
        //for score calculation
        private static int NearestTwoToPowerN(int number)
        {
            int k = 0;                             //power
            uint multiplier = 1;                   //counts 2*2*...*2, initial state 1 = 2^0 = 2^k


            //find k for which 2^k is the closest smallest number to the "number" (variable)
            while (true)
            {
                if (2 * multiplier == number)
                    return k + 1;

                if (2 * multiplier < number)
                {
                    multiplier *= 2;
                    k++;
                    continue;
                }

                //if number - 2 ^ k is nearer to number, return k, else return k + 1
                return ((number - multiplier) < (2 * multiplier - number) ? k : k + 1);
            }
        }

        private static uint GuessNumber_Score(int options, int fails)
        {
            //there are at least two options, if not => throw exception
            if (options < 2)
                throw new ArgumentOutOfRangeException(nameof(options), "There should be at least two options!");

            int n = NearestTwoToPowerN(options);

            
            //formula for score calculation - 100 * (n - f) / n
            //where n - power of 2, f - fails number

            //if too much fails
            if (n <= fails)
                return 0;
            
            return (uint)Math.Round(100.0 * (n - fails) / n);
        }

        private static void GuessNumber_PrintStatistics(uint score, int attempts, TimeSpan time)
        {
            string timeStr = $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";

            Console.WriteLine($"\n\n\n{"", 25}{"* * *  RESULTS  * * *"}\n\n");

            Console.WriteLine($"{"", 25}{"SCORE", -12}{score, 9}\n");
            Console.WriteLine($"{"", 25}{"ATTEMPTS", -12}{attempts, 9}\n");
            Console.WriteLine($"{"", 25}{"TIME", -12}{timeStr, 9}");

            Console.WriteLine($"\n\n{"", 33}{"* * *"}\n\n");

        }


        //end functions for task 2.4



        //function for task3 - calculator

        private static bool TryParseUserExpr(string expr, out string operation, out Operands numbers)
        {
            //define operation
            operation = DefineOperation(expr);

            if (operation != null)
            {
                //define number(s)
                numbers = DefineNumbers(expr, operation);

                if (numbers == null)
                    return false;       //error
                else
                    return true;        //operands are ok
            }

            //error
            numbers = null;
            return false;
            
        }


        private static string DefineOperation(string expr)
        {
            Regex regex;                         //for regular expressions


            //operations patterns

            //a floating point number in which a point can be represented by a point or a comma
            string doubleNumber = @"-?([0-9]+[,]?[0-9]+|[0-9]+)";

            //an integer number
            string intNumber = @"-?[0-9]+";

            //an unsigned integer number
            string uintNumber = @"[0-9]+";


            string addPattern = @"^" + doubleNumber + @"[\s]*[+][\s]*" + doubleNumber + @"$";
            string multiplyPattern = @"^" + doubleNumber + @"[\s]*([*]|x)[\s]*" + doubleNumber + @"$";
            string dividePattern = @"^" + doubleNumber + @"[\s]*[/\\][\s]*" + doubleNumber + @"$";
            string substractPattern = @"^" + doubleNumber + @"[\s]*[-][\s]*" + doubleNumber + @"$";
            //similarly to
            //string substractPattern2 = @"^([0-9]+[.,]?[0-9]+|[0-9]+)[\s]*[-][\s]*([0-9]+[.,]?[0-9]+|[0-9]+)$";

            string divRemPattern = @"^" + doubleNumber + @"[\s]*%[\s]*" + doubleNumber + @"$";


            string powerPattern = @"^" + doubleNumber + @"[\s]*pow[\s]*" + doubleNumber + @"$";

            string factorialPattern = @"^" + uintNumber + @"!$";

            string andPattern = @"^" + intNumber + @"[\s]*&[\s]*" + intNumber + @"$";
            string orPattern = @"^" + intNumber + @"[\s]*\|[\s]*" + intNumber + @"$";
            string xorPattern = @"^" + intNumber + @"[\s]*\^[\s]*" + intNumber + @"$";

            string notPattern = @"^!" + intNumber + @"$";
            string echoPattern = @"^" + doubleNumber + @"$";

            string[] operationsPatterns = new[]
            {
                factorialPattern, echoPattern,
                addPattern, substractPattern, multiplyPattern, dividePattern, divRemPattern, powerPattern,
                notPattern, andPattern, orPattern, xorPattern,
            };

            string[] operations = new[]
            {
                "factorial", "echo", "add", "substract", "multiply", "divide", "divRem", "power",
                "not", "and", "or", "xor",
            };

            //end operations patterns


            //define used operation
            for (int i = 0; i < operationsPatterns.Length; i++)
            {
                regex = new Regex(operationsPatterns[i]);
                if (regex.Match(expr).Success)
                {
                    return operations[i];
                }
            }


            return null;
        }


        private static Operands DefineNumbers(string expr, string operation)
        {
            string[] splited;
            Operands operands = null;

            switch (operation)
            {
                case "factorial":
                    {

                        if (uint.TryParse(expr[0..^1], out uint value))
                            operands = new Operands() { F = value };

                        //Console.WriteLine(operands?.F);

                    }; break;

                case "echo":
                    {
                        if(double.TryParse(expr, out double value))
                            operands = new Operands() { A = value };

                        //Console.WriteLine(operands?.A);

                    }; break;

                case "add":
                case "multiply":
                case "divide":
                case "divRem":
                case "power":
                {
                    List<string> numbers = new List<string>(2);
                    splited = Regex.Split(expr, @"\+|/|\\|%|\*|x|pow|\s+");

                    foreach (string s in splited)
                    {
                        if (s != string.Empty)
                            numbers.Add(s);
                    }

                    if (double.TryParse(numbers[0], out double value1)
                        && double.TryParse(numbers[1], out double value2))
                            operands = new Operands()
                            {
                                A = value1,
                                B = value2,
                            };

                }; break;

                case "substract":
                {
                    int index = FindIndexOfSubstractSign(expr);
                    string newExpr = expr.Remove(index, 1).Insert(index, " ");

                    splited = newExpr.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (double.TryParse(splited[0], out double value1)
                        && double.TryParse(splited[1], out double value2))
                        operands = new Operands()
                        {
                            A = value1,
                            B = value2,
                        };

                }; break;

                case "and":
                case "or":
                case "xor":
                {
                    List<string> numbers = new List<string>(2);
                    splited = Regex.Split(expr, @"&|\||\^|\s+");

                    foreach (string s in splited)
                    {
                        if (s != string.Empty)
                            numbers.Add(s);
                    }

                    if (int.TryParse(numbers[0], out int value1)
                        && int.TryParse(numbers[1], out int value2))
                        operands = new Operands()
                        {
                            X = value1,
                            Y = value2,
                        };

                }; break;

                case "not":
                {
                    if (int.TryParse(expr[1..], out int value))
                        operands = new Operands() { X = value } ;

                }; break;

                default:
                {
                    operands = new Operands() { error = true };
                }; break;

            }

            return operands;
        }

        private static int FindIndexOfSubstractSign(string expr)
        {
            int index;
            string tempExpr = expr;

            while (true)
            {
                index = tempExpr.IndexOf('-');

                if (index == -1)
                    break;


                tempExpr = tempExpr.Remove(index, 1).Insert(index, " ");

                if (index == 0)
                {
                    continue;
                }

                break;
            }
            return index;
        }


        /*Console.WriteLine($"\n\n{"",26}{"* * *  RULES  * * *"}\n\n");
        Console.WriteLine($"\n\n{"",33}{"* * *"}\n\n");*/

    }
}
