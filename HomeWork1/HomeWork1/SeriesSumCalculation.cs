using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class SeriesSumCalculation
    {
        //Calculates the sum from i = 1 to infinity = 1 / (i * (i+1)) with stop condition "element < eps"
        public static double Calculate(double eps)
        {
           
            double sum = 0;             //sum accumulator
            double seriesElem;          //element of the series at one iteration
            int i = 1;                  //i in formula

            while (true)
            {
                seriesElem = 1.0 / (i * (i + 1));

                if (seriesElem < eps)       //stop condition
                    return sum;

                sum += seriesElem;
                i++;
            }
        }
    }
}
