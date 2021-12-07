using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input from file
            List<string> input = File.ReadAllLines("Day7.txt").ToList();
            List<int> crabs = input[0].Split(',').ToList().ConvertAll(x => int.Parse(x));

            //execute and print part one and part two
            Console.WriteLine(PartOne(crabs));
            Console.WriteLine(PartTwo(crabs));
        }

        public static long PartOne(List<int> crabs)
        {
            int high = crabs.Max();
            int highestFuel = int.MaxValue;

            for (int i = 0; i < high; i++) // try every position
            {
                int fuel = 0;

                foreach (int crabposition in crabs) // calc fuel for position
                {
                    fuel += Math.Abs(crabposition - i);
                }

                if (fuel < highestFuel) highestFuel = fuel; // remember lowest fuel
            }

            return highestFuel;
        }

        public static long PartTwo(List<int> crabs)
        {
            int high = crabs.Max();
            long highestFuel = int.MaxValue;

            for (int i = 0; i < high; i++) // every position
            {
                long fuel = 0;

                foreach (int crabposition in crabs) // calc fuel for position
                {
                    long crabFuel = addNrs(Math.Abs(crabposition - i));
                    fuel += crabFuel;
                }

                if (fuel < highestFuel) highestFuel = fuel; // remember lowest fuel
            }

            return highestFuel;
        }

        //helper, adds nr and nr-- until nr = 1
        public static long addNrs(int nr)
        {
            if (nr == 0) return 0;
            if (nr == 1) return 1;
            return nr + addNrs(nr - 1);
        }
    }
}
