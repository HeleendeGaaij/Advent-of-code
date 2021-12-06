using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LanternFishPartOne(80));
            Console.WriteLine(LanternFishPartTwo(256));
        }

        public static long LanternFishPartOne(int days)
        {
            //get input from file
            List<string> input = File.ReadAllLines("Day6.txt").ToList();
            List<int> fishes = input[0].Split(',').ToList().ConvertAll(x => int.Parse(x));

            for (int i = 1; i <= days; i++) // each day
            {
                for (int j = 0; j < fishes.Count; j++) // each fish
                {
                    if (fishes[j] == 0)
                    {
                        fishes[j] = 6; //restart fish
                        fishes.Add(9); // new fish
                    }
                    else { fishes[j]--; } // fish days - 1
                }
            }

            return fishes.Count;
        }

        public static long LanternFishPartTwo(int days)
        {
            //get input from file
            List<string> input = File.ReadAllLines("Day6.txt").ToList();
            List<int> fishes = input[0].Split(',').ToList().ConvertAll(x => int.Parse(x));

            long[] fishDays = new long[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //register how many fish are at a specific day

            foreach(int fish in fishes) //start positions
            {
                fishDays[fish]++;
            }

            for (int i = 0; i < days; i++) // for each day
            {
                long nextGen = 0; // nr of fish to add
                long[] fishDaysTemp = new long[9];

                if (fishDays[0] > 0) nextGen = fishDays[0]; // count new spawned fishes

                for (int d = 0; d < 8; d++) //switch all fishes to one day less
                {
                    fishDaysTemp[d] = fishDays[d + 1];
                }

                fishDaysTemp[6] += nextGen;
                fishDaysTemp[8] += nextGen;

                fishDays = fishDaysTemp;
            }

            //count all fishes
            long counter = 0;

            foreach (long fish in fishDays)
            {
                counter += fish;
            }

            return counter;
        }
    }
}
