using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HomeWork1
{
    class ProbabilitiesAndMarginCalculation
    {
        public static void Calculate(in double h, in double d, in double a, 
                                        out double probH, out double probD, out double probA, out double margin)
        {
            margin = Math.Round((1 - 1 / (1 / h + 1 / d + 1 / a)) * 100);       //margin in percents

            probH = Math.Round(100 / h - margin / 3);            //probability of the first participant's victory in percents
            probD = Math.Round(100 / d - margin / 3);            //probability of the draw in percents
            probA = 100 - probH - probD;                         //probability of the second participant's victory in percents

        }
    }
}
