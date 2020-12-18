using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    static class ArrayIntStatistics
    {
        public static int[] Sort(int[] array)
        {
            int temp;

            //Bubble sort
            for(int i=0; i<array.Length; i++)
            {
                for(int j=0; j<array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j+1] = temp;
                    }
                }
            }

            return array;
        }

        public static int Max(int[] array)
        {
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                max = max < array[i] ? array[i] : max;
            }

            return max;
        }

        public static int Min(int[] array)
        {
            int min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                min = min > array[i] ? array[i] : min;
            }

            return min;            
        }

        public static int Sum(int[] array)
        {
            int sum = 0;

            //if > int32.Max ???

            for(int i=0; i<array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static double Avg(int[] array)
        {
            //if > int32.Max ???
            return (double)Sum(array) / array.Length;

        }

        public static double StandardDeviation(int[] array)
        {
            double avg = Avg(array);
            double numerator = 0;
            int denominator = array.Length;

            for (int i = 0; i < array.Length; i++)
            {
                numerator += (array[i] - avg) * (array[i] - avg);
            }

            return numerator / denominator;
        }
    }
}
