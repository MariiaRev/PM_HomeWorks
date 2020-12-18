using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class Square: IFigure
    {
        public double SideA { get; set; }

        public Square(double a)
        {
            SideA = a;
        }

        public double GetArea()
        {
            return SideA * SideA;
        }
    }
}
