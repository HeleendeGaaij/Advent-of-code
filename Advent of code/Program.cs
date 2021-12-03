using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_code
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> lines = File.ReadAllLines("Day1.txt").ToList().ConvertAll(x => int.Parse(x));
            
            Console.WriteLine(SonarSweepPartOne(lines));
            Console.WriteLine(SonarSweepPartTwo(lines));
        }

        //assignment: the number of times a depth measurement increases
        static int SonarSweepPartOne(List<int> lines)
        {
            int count = 0;

            for (int i = 1; i < lines.Count; i++)
            {
                if (lines[i] - lines[i - 1] > 0) count++; //compare current and previous input
            }
        
            return count;
        }

        //assignment: the number of times the sum of measurements in this sliding window (of 3) increases
        static long SonarSweepPartTwo(List<int> lines)
        {
            int count = 0;
            
            for (int i = 3; i < lines.Count; i++)
            {
                var previousSet = lines.Skip(i - 3).Take(3); //get previous window of 3
                var currentSet = lines.Skip(i - 2).Take(3); //get current window of 3
                if (currentSet.Sum() - previousSet.Sum() > 0) count++; //compare
            }

            return count;
        }


    }
}