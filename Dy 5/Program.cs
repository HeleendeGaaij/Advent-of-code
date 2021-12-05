using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dy_5
{
    class Program
    {
        static int DIAGRAM_SIZE = 1000;

        static void Main(string[] args)
        {
            //get input from file
            List<string> input = File.ReadAllLines("Day5.txt").ToList();

            //execute and print part one and part two
            Console.WriteLine(HydrothermalVenturePartOne(input));
            Console.WriteLine(HydrothermalVenturePartTwo(input));
        }

        public static long HydrothermalVenturePartOne(List<string> input)
        {
            int[,] diagram = new int[DIAGRAM_SIZE, DIAGRAM_SIZE];

            foreach (string vent in input)
            {
                putLines(vent, diagram, false);
            }

            return countLineOverlap(diagram);
        }

        public static long HydrothermalVenturePartTwo(List<string> input)
        {
            int[,] diagram = new int[DIAGRAM_SIZE, DIAGRAM_SIZE];

            foreach (string vent in input)
            {
                putLines(vent, diagram, true);
            }

            return countLineOverlap(diagram);
        }

        public static long countLineOverlap(int[,] diagram)
        {
            //return the number of points where at least two lines overlap
            int count = 0;
            foreach (int point in diagram)
            {
                if (point > 1) count++;
            }
            return count;
        }

        public static void putLines(string vent, int[,] diagram, bool isPartTwo)
        {
            //get x1,y1,x2,y2 from vent
            string[] firstSplit = vent.Split(',');
            int x1 = int.Parse(firstSplit[0]);
            int y2 = int.Parse(firstSplit[2]);
            string[] secondsplit = firstSplit[1].Split(' ');
            int y1 = int.Parse(secondsplit[0]);
            int x2 = int.Parse(secondsplit[2]);

            // horizontal lines
            if (y1 == y2)
            {
                int startX = x1 > x2 ? x2 : x1; //smallest x
                int endX = x1 > x2 ? x1 : x2; //biggest x

                while (startX <= endX)
                {
                    diagram[startX, y1]++; //diagram line count
                    startX++;
                }
            }
            // vertical lines
            else if (x1 == x2)
            {
                int startY = y1 > y2 ? y2 : y1; //smallest y
                int endY = y1 > y2 ? y1 : y2; //biggest y

                while (startY <= endY)
                {
                    diagram[x1, startY]++; //diagram line count
                    startY++;
                }
            }
            //diagonal lines
            else if (isPartTwo)
            {
                //left point first
                int x = x1 < x2 ? x1 : x2;
                int y = x1 < x2 ? y1 : y2;
                int endX = x == x1 ? x2 : x1;
                int endY = y == y1 ? y2 : y1;
                //2 scenario's: \ /
                if (y < endY) //scenario: \
                {
                    while (x <= endX)
                    {
                        diagram[x, y]++;
                        x++;
                        y++;
                    }
                } else //scenario: /
                {
                    while (y >= endY)
                    {
                        diagram[x, y]++;
                        x++;
                        y--;
                    }
                }
            }
        }
    }
}
