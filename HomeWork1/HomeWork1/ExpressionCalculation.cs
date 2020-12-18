using static System.Math;

namespace HomeWork1
{
    static class ExpressionCalculation
    {
        public static double CalculateExpression(in double a, int b, int c, int d)
        {
            double x1 = (Exp(a) + 4*Log10(c))/Sqrt(b);
            double x2 = Abs(Atan(d));                               //d in radians
            double x3 = 5 / Sin(a);                                 //a in radians 

            double y = x1*x2 + x3;

            return y;
        }
    }
}
