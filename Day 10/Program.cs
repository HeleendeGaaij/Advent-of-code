using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadAllLines("Day10.txt").ToList();

            Console.WriteLine(SyntaxScoringPartOne(input));
            Console.WriteLine(SyntaxScoringPartTwo(input));
        }

        static long SyntaxScoringPartOne(List<string> input)
        {
            List<char> characters = new List<char>(); //corrupted characters

            foreach (string inputLine in input)
            {
                Stack<char> stack = new Stack<char>(); //current line of chars

                foreach (char character in inputLine)
                {
                    if (MATCHES.ContainsValue(character)) //opening tag
                    {
                        stack.Push(character);
                    }
                    else //closing tag
                    {
                        if (stack.Count == 0) break;
                        //opening and closing tag don't match
                        if (stack.Pop() != MATCHES[character]) characters.Add(character);
                    }
                }
            }

            int sum = 0;
            foreach (char character in characters)
            {
                sum += POINTS_PART_ONE[character];
            }
            return sum;
        }

        static long SyntaxScoringPartTwo(List<string> input)
        {
            Stack<char> stack = new Stack<char>();
            List<Stack<char>> nonCorruptedLines = new List<Stack<char>>();

            //filter corrupted lines
            foreach (string line in input)
            {
                stack = new Stack<char>();
                foreach (char character in line)
                {
                    if (MATCHES.ContainsValue(character)) //opening tag
                    {
                        stack.Push(character);
                    }
                    else //closing tag
                    {
                        if (stack.Count != 0)
                        {
                            //opening and closing tag don't match
                            if (stack.Pop() != MATCHES[character])
                            {
                                //forget about line
                                stack = new Stack<char>();
                                break;
                            }
                        }
                    }
                }
                if (stack.Count != 0) nonCorruptedLines.Add(stack);
            }

            //get  scores to complete each line
            List<long> totalScores = new List<long>();
            for (int i = 0; i < nonCorruptedLines.Count; i++) 
            {
                stack = nonCorruptedLines[i];

                //completion score
                long total = 0;
                foreach (char character in stack)
                {
                    total *= 5;
                    total += POINTS_PART_TWO[character];
                }
                totalScores.Add(total);
            }

            //get middle score
            totalScores = totalScores.OrderBy(x => x).ToList();
            return totalScores[totalScores.Count/2];
        }


        static Dictionary<char, char> MATCHES = new Dictionary<char, char>
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' },
            { '>', '<' }
        };

        static Dictionary<char, int> POINTS_PART_ONE = new Dictionary<char, int>
        {
            {')', 3 },
            {']', 57 },
            {'}', 1197 },
            {'>', 25137 }
        };

        static Dictionary<char, int> POINTS_PART_TWO = new Dictionary<char, int>
        {
            {'(', 1 },
            {'[', 2 },
            {'{', 3 },
            {'<', 4 }
        };
    }
}
