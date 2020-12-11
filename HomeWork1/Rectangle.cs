using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class Rectangle: IFigure
    {

        public double SideA { get; set; }
        public double SideB { get; set; }

        public Rectangle(double a, double b)
        {
            SideA = a;
            SideB = b;
        }

        public double GetArea()
        {
            return SideA * SideB;
        }
    }
}
