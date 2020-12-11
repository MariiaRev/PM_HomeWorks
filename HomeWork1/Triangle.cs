using System;

namespace HomeWork1
{
    class Triangle: IFigure
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public Triangle(double a, double b, double c)
        {
            SideA = a;
            SideB = b;
            SideC = c;
        }
        public double GetArea()
        {
            double p = (SideA + SideB + SideC) / 2;     //semi-perimeter

            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC) );
        }


        //check if the given sides form a triangle
        public static bool IsTriangle(double a, double b, double c)
        {
            if (a + b <= c || a + c <= b || b + c <= a)
                return false;
            else
                return true;
        }
    }
}
