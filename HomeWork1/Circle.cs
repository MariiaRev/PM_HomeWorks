using System;
namespace HomeWork1
{
    class Circle: IFigure
    {
        public double Radius { get; set; }

        public Circle(double r)
        {
            Radius = r;
        }
        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
