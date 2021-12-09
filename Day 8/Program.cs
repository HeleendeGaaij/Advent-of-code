using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input from file
            List<string> txt = File.ReadAllLines("Day8.txt").ToList();
            List<string> input = new List<string>();
            List<string> output = new List<string>();
            foreach (string line in txt)
            {
                string[] split = line.Split('|');
                input.Add(split[0]);
                output.Add(split[1]);
            }

            //execute and print part one and part two
            Console.WriteLine(SevenSegmentSearchPartOne(output));
            Console.WriteLine(SevenSegmentSearchPartTwo(input, output));
        }

        public static long SevenSegmentSearchPartOne(List<string> output)
        {
            //count all digits with unique segment string length
            int count = 0;
            for (int i = 0; i < output.Count; i++)
            {
                string[] segments = output[i].Split(' ');
                foreach (string segment in segments) 
                {
                    int length = segment.Length;
                    if (length == 2 || length == 3 || length == 4 || length == 7)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static long SevenSegmentSearchPartTwo(List<string> input, List<string> output)
        {
            //find which segment string computes with which digit
            string[] digits = new string[10];
            int sum = 0;

            for (int i = 0; i < input.Count; i++)
            {
                digits = new string[10];

                string[] segments = input[i].Split(' ').OrderBy(x => x.Length).ToArray();
                foreach (string segment in segments)
                {
                    // Get char array, then sort it, and convert back to string.
                    var letters = segment.ToCharArray();
                    Array.Sort(letters);
                    string sortedSegment = new string(letters);

                    //segment string length predetermines some digits with unique length
                    switch (sortedSegment.Length)
                    {
                        case 2:
                            digits[1] = sortedSegment;
                            break;
                        case 3:
                            digits[7] = sortedSegment;
                            break;
                        case 4:
                            digits[4] = sortedSegment;
                            break;
                        case 5:
                            //length 5 remaining options: 2, 3, 5
                            string seven = digits[7];
                            string four = digits[4];
                            string one = digits[1];
                            string fourMinusOne = new string(four.ToCharArray().Except(one.ToCharArray()).ToArray());

                            //determine which is which by overlap with existing digits
                            if (segment.Contains(seven[0]) && segment.Contains(seven[1]) && segment.Contains(seven[2]))
                            {
                                digits[3] = sortedSegment;
                            } 
                            else if (segment.Contains(fourMinusOne[0]) && segment.Contains(fourMinusOne[1]))
                            {
                                digits[5] = sortedSegment;
                            } 
                            else
                            {
                                digits[2] = sortedSegment;
                            }
                            break;
                        case 6:
                            //length 5 remaining options: 0, 6, 9
                            one = digits[1];
                            four = digits[4];

                            //determine which is which by overlap with existing digits
                            if (segment.Contains(four[0]) && segment.Contains(four[1]) && segment.Contains(four[2]) && segment.Contains(four[3]))
                            {
                                digits[9] = sortedSegment;
                            } 
                            else if (segment.Contains(one[0]) && segment.Contains(one[1]))
                            {
                                digits[0] = sortedSegment;
                            }
                            else 
                            {
                                digits[6] = sortedSegment;
                            }

                                break;
                        case 7:
                            digits[8] = sortedSegment;
                            break;
                    }
                }

                string nr = "";
                foreach (string digit in output[i].Split(' '))
                {
                    if (digit == "") continue;
                    var letters = digit.ToCharArray();
                    Array.Sort(letters);
                    string sortedDigit = new string(letters);

                    //find string and convert to digit
                    int index = Array.IndexOf(digits, sortedDigit);
                    nr += index;
                }
                sum += int.Parse(nr);
            }

            //add all output values and return
            return sum; // sum output values omgerekend
        }
    }
}