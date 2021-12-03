using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = File.ReadAllLines("Day2.txt").ToList();
            long[] coordinates = { 0, 0 };
            Console.WriteLine(DivePartOne(coordinates, lines));

            long[] coordinatesTwo = { 0, 0, 0 };
            Console.WriteLine(DivePartTwo(coordinatesTwo, lines));
        }

        //assignment: What do you get if you multiply your final horizontal position by your final depth?
        static long DivePartOne(long[] coordinates, List<string> lines)
        {
            for(int i = 0; i < lines.Count; i++)
            {
                string[] command = lines[i].Split(' ');

                switch (command[0]){
                    case "forward":
                        coordinates[0] += int.Parse(command[1]);
                        break;
                    case "down":
                        coordinates[1] += int.Parse(command[1]);
                        break;
                    case "up":
                        coordinates[1] -= int.Parse(command[1]);
                        break;
                }
            }

            return coordinates[0] * coordinates[1];
        }
        static long DivePartTwo(long[] coordinates, List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string[] command = lines[i].Split(' ');

                switch (command[0])
                {
                    case "forward":
                        coordinates[0] += int.Parse(command[1]);
                        coordinates[1] += coordinates[2] * int.Parse(command[1]);
                        break;
                    case "down":
                        coordinates[2] += int.Parse(command[1]);
                        break;
                    case "up":
                        coordinates[2] -= int.Parse(command[1]);
                        break;
                }
            }

            return coordinates[0] * coordinates[1];
        }
    }
}
