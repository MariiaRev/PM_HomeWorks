using System;


namespace HomeWork1
{

    //unary and binary operations calculator
    static class Calculator
    {
        //unary operations

        //recursive tree traversal function for calculating factorial
        private static ulong FactTree(uint left, uint right)
        {
            checked
            {
                if (left > right)
                    return 1;

                if (left == right)
                    return left;

                if (right - left == 1)
                    return (ulong)left * right;

                uint middle = (left + right) / 2;

                return FactTree(left, middle) * FactTree(middle + 1, right);
            }
        }

        //the factorial is calculated using a tree
        public static ulong Factorial(uint a)
        {
            checked
            {

                if (a == 0)
                    return 1;

                if (a <= 2)
                    return a;

                return FactTree(2, a);
            }
        }

        //binary operations

        public static double Add(double a, double b)
        {
            checked
            {
                return a + b;
            }
        }
        public static double Substract(double a, double b)
        {
            checked
            {
                return a - b;
            }
        }
        public static double Multiply(double a, double b)
        {
            checked
            {
                return a * b;
            }
        }
        public static double Divide(double a, double b)
        {
            checked
            {
                return a / b;
            }
        }

        //division remainder
        public static double DivRem(double a, double b)
        {
            checked
            {
                return a % b;
            }
    }

        // a to power b
        public static double Pow(double a, double b)
        {
            checked
            {
                return Math.Pow(a, b);
            }
        }

        //unary bitwise operations

        // !
        public static int Not(int a)
        {
            return ~a;
        }

        //binary bitwise operations

        // &
        public static int And(int a, int b)
        {
            return a & b;
        }

        // |
        public static int Or(int a, int b)
        {
            return a | b;
        }

        // ^
        public static int Xor(int a, int b)
        {
            return a ^ b;
        }

    }
}
