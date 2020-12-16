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
            for (int i = 0; i < arr.Length; i++)
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
            foreach (var w in result)
            {
                Console.Write($"{w} ");
            }

        }

        static void LastElem()
        {
            int[] arr = { 323, -34556, 746, 224, 64, 777, -33, 1237, 99};

            var maximumNumbers = from u in arr
                         group u by Convert.ToInt32(u.ToString().Last()) into u
                         select u.Max();

            var result = maximumNumbers.OrderBy(n => Convert.ToInt32(n.ToString().Last()));
            foreach (var n in result)
            {
                Console.WriteLine(n);
            }
        }

        class FitnesClient
        {
            public string clientID;
            public DateTime startDate;
            public double duration;

            public FitnesClient(string ID, DateTime date, double duration)
            {
                clientID = ID;
                startDate = date;
                this.duration = duration;
            }

            public override string ToString()
            {
                return $"Client ID = {clientID}, start date = {startDate:dd.MM.yyyy}, duration = {duration}h";
            }
        }

        class Fitnes
        {
            List<FitnesClient> fitnesClients = new List<FitnesClient> {
                new FitnesClient("first",new DateTime(2020,10,12),10),
                new FitnesClient("second",new DateTime(2020,11,12),10),
                new FitnesClient("third",new DateTime(2020,12,12),10),
                new FitnesClient("first",new DateTime(2020,8,12),1),
                new FitnesClient("second",new DateTime(2020,8,12),4),
                new FitnesClient("first",new DateTime(2020,8,12),5),
                new FitnesClient("second",new DateTime(2020,8,12),2),
            };

            public void DurationSumm(DateTime data)
            {
                var clients = fitnesClients.Where(w => w.startDate == data).OrderBy(w => w.duration);
                var result = clients.Select(d => d.duration).Sum();

                foreach (var c in clients)
                {
                    Console.WriteLine(c);
                }
                Console.WriteLine($"Summary duration: {result}h");

            }
        }

    static void Main(string[] args)
        {
            ////1 task
            //NumArray();
            ////2 task
            //  StringArray();
            //3 task
            LastElem();
            ////4 task
            Fitnes tr = new Fitnes();
            tr.DurationSumm(new DateTime(2020, 8, 12));



            ;
        }
    }
}
