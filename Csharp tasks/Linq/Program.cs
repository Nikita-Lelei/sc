using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    class Program
    {
        public static void NumArray()
        {
            Random rnd = new Random();
            int[] arr = new int[10];

            // arr.Select(x => rnd.Next(-9, 10)).ToArray();

            Console.WriteLine("Array is:");
            for(int i = 0; i< arr.Length;i++)
            {
                arr[i] = rnd.Next(-9, 10);
                Console.Write($"{arr[i]} ");
            }
           

            Console.WriteLine($"result = {arr.Where(n => n > 0).Min()}");

        }

        public static void StringArray()
        {
            string[] strnArr = { "OUIORER", "YTERTE", "REGDFGDGRR", "ADADA", "DADAD", "EWEWEFA" };
            var result = strnArr.OrderBy(w => w.Length).ThenBy(w => w);
            Console.WriteLine("result is:");
            foreach(var w in result)
            {
                Console.Write($"{w} ");
            }

        }

        static void Main(string[] args)
        {
            NumArray();
            StringArray();
        }
    }
}
