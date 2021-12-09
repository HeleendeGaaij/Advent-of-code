using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] txt = File.ReadAllLines("Day9.txt").ToArray();

            int[,] matrix = new int[txt[0].Length + 2, txt.Length + 2];

            matrix = convertInputToMatrix(matrix, txt);            

            Console.WriteLine(SmokeBasinPartOne(matrix));
            Console.WriteLine(SmokeBasinPartTwo(matrix));
        }

        public static long SmokeBasinPartOne(int[,] matrix)
        {
            int sum = 0;
            foreach (Point lowPoint in findLowPoints(matrix))
            {
                sum += 1 + matrix[lowPoint.X, lowPoint.Y];
            }
            return sum;
        }

        public static List<Point> findLowPoints(int[,] matrix)
        {
            List<Point> lowPoints = new List<Point>();
            //loop through matrix
            for (int x = 1; x < matrix.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < matrix.GetLength(1) - 1; y++)
                {
                    //if all neighbours are bigger than point itself
                    if (matrix[x, y] < matrix[x - 1, y] && matrix[x, y] < matrix[x + 1, y] && matrix[x, y] < matrix[x, y - 1] && matrix[x, y] < matrix[x, y + 1])
                    {
                        //lowest point
                        lowPoints.Add(new Point(x,y));
                    }
                }
            }

            return lowPoints;
        }

        public static long SmokeBasinPartTwo(int[,] matrix)
        {
            List<int> basinSizes = new List<int>();
            foreach(Point lowPoint in findLowPoints(matrix)) //start at low point (part one)
            {
                List<Point> knownPoints = new List<Point>(); //record known point in recursive method
                basinSizes.Add(findBasinSize(matrix, lowPoint.X, lowPoint.Y, knownPoints)); //size of one basin
            }

            //order large to small en get 3
            basinSizes = basinSizes.OrderByDescending(x => x).Take(3).ToList();
            //multiply basin size of 3 largest basins
            return basinSizes.Aggregate((a, x) => a * x);
        }

        public static int findBasinSize(int[,] matrix, int x, int y, List<Point> knownPoints)
        {
            // until point is 9 or int.max or known
            if (matrix[x, y] == int.MaxValue || matrix[x, y] == 9 || knownPoints.Contains(new Point(x,y))) return 0;
            
            knownPoints.Add(new Point(x,y));
            //go left, right, up, down(recursive)
            return 1 + findBasinSize(matrix, x-1, y, knownPoints) + findBasinSize(matrix, x + 1, y, knownPoints) + findBasinSize(matrix, x, y - 1, knownPoints) + findBasinSize(matrix, x, y + 1, knownPoints);
        }



        public static int[,] convertInputToMatrix(int[,] matrix, string[] txt)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x > txt[0].Length || y > txt.Length) //borders get maxvalues
                    {
                        matrix[x, y] = int.MaxValue;
                    }
                    else
                    {

                        string line = txt[y - 1];
                        matrix[x, y] = Convert.ToInt32(new string(line[x - 1], 1));
                    }
                }
            }

            return matrix;
        }
    }
}
