using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class ArrayCustom <T> where T : unmanaged 
    {
        public T[] Array { get; set; }
        public ArrayCustom(int elemsAmount)
        {
            Array = new T[elemsAmount];
        }

        //indexator !
        public void Sort()
        {
            
            if(Array != null && Array.Length > 1)
            {
                T temp;
                //Bubble sort - should be optimized
                for (int i = 0; i < Array.Length; i++)
                {
                    for (int j = 0; j < Array.Length - 1; j++)
                    {
                        //if (Array[j] > Array[j + 1])
                        //{

                        //}
                    }
                }
            }
        }
    }
}
