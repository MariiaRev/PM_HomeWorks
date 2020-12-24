using System;
using System.Collections.Generic;

namespace Task_1._4
{
    class Program
    {
        //not finished
        //clarify and check again!
        static void Main(string[] args)
        {
            const int STRING_LENGTH_LIMIT = 100;            //limit for the length of a string entered by user

            //print greetings
            Console.WriteLine($"{"",22}This is Homework 3 Task 1.4.\n{"",27}Balanced brackets");
            Console.WriteLine($"\n{"",21}### Made by Mariia Revenko ###\n\n");


            //print RULES
            Console.WriteLine("Enter an expression to check if brackets in it are balanced." +
                "\nIf brackets aren't balanced you'll see the index where an error occured.");
            Console.WriteLine("Enter \"exit\" to exit the program.");


            while (true)
            {
                string userExpression;

                Console.Write($"\n\n\nEnter an expression or the \"exit\" command, please: ");
                userExpression = Console.ReadLine();

                if (userExpression.Trim() == "")
                {
                    Console.WriteLine("\n\nEmpty expression! Try again.");
                    continue;
                }

                if (userExpression == "exit")
                {
                    Console.WriteLine("\n\n\n\nYou exit the program. Bye!\n\n\n");
                    return;
                }

                if (userExpression.Length > STRING_LENGTH_LIMIT)
                {
                    Console.WriteLine("\n\nThe expression is too long. Please, try again with a shorter expression.");
                    continue;
                }


                //if expression OK

                //if brackets are balanced
                if(IsWithBalancedBrackets(userExpression, out int errorIndex))
                {
                    Console.WriteLine("\n\nBrackets are balanced.");
                }
                else
                {
                    Console.WriteLine($"\n\nBrackets are not balanced. Error is at {errorIndex} index.");
                }

            }
        }

        //check if an expression contains balanced brackets
        static bool IsWithBalancedBrackets(string expr, out int errorIndex)
        {
            Stack<char> bracketsStack = new Stack<char>();
            Stack<int> indexStack = new Stack<int>();
            List<char> startBrackets = new List<char> { '(', '{', '[', '<' };
            List<char> endBrackets = new List<char> { ')', '}', ']', '>' };

            for (int i = 0; i < expr.Length; i++)
            {
                //if an starting bracket - push it to the stack
                if(startBrackets.Contains(expr[i]))
                {
                    bracketsStack.Push(expr[i]);
                    indexStack.Push(i);
                    continue;
                }

                if(endBrackets.Contains(expr[i]))
                {
                    //if stack is not empty
                    if (bracketsStack.TryPeek(out char bracket))
                    {
                        // If an closing bracket is without a pair then return false 
                        if (!IsMatchingPair(bracket, expr[i]))
                        {
                            errorIndex = i;
                            return false;
                        }
                        else
                        {
                            bracketsStack.Pop();
                            indexStack.Pop();
                        }
                    }
                    //if the stack is empty then there is an closing bracket without a starting one
                    else
                    {
                        errorIndex = i;
                        return false;
                    }
                }
            }

            // If there is something left in expression then there is a starting bracket without a closing bracket 
            if (bracketsStack.Count == 0)
            {
                errorIndex = -1;
                return true;
            }
            else
            {
                errorIndex = indexStack.Peek();
                return false;
            }
        }

        static bool IsMatchingPair(char bracket1, char bracket2)
        {
            if ((bracket1 == '(' && bracket2 == ')')
                || (bracket1 == '{' && bracket2 == '}')
                || (bracket1 == '[' && bracket2 == ']')
                || (bracket1 == '<' && bracket2 == '>'))

                return true;
            else
                return false;
        }
    }
}
