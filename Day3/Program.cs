using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadAllLines("Day3.txt").ToList();

            Console.WriteLine(BinaryDiagnosticPartOne(input));
            Console.WriteLine(BinaryDiagnosticPartTwo(input));
        }

        static long BinaryDiagnosticPartOne(List<string> input)
        {
            //get length of first binary nr
            int binaryLength = input[0].Length;
            //most common bit on each position
            int[] mostCommonBits = new int[binaryLength];

            //each string in input
            foreach (string binaryNr in input)
            {
                //each char in string
                for (int i = 0; i < binaryLength; i++)
                {
                    mostCommonBits[i] += binaryNr[i] == '1' ? 1 : -1; // if 1 add 1, if 0 subtract 1 
                }
            }

            string gammaStr = "";
            string epsilonStr = "";

            //walk through most common bit in each position
            for (int i = 0; i < binaryLength; i++)
            {
                if (mostCommonBits[i] > 0) // '1' most common
                {
                    gammaStr += "1";
                    epsilonStr += "0";
                }
                else // '0' most common
                {
                    gammaStr += "0";
                    epsilonStr += "1";
                }
            }

            //multiply new binaries
            return Convert.ToInt32(gammaStr, 2) * Convert.ToInt32(epsilonStr, 2);
        }

        static long BinaryDiagnosticPartTwo(List<string> input)
        {
            List<string> CO2_rates = new List<string>(input);
            List<string> oxygen_rates = new List<string>(input);

            int oxy = Convert.ToInt32(getRate(oxygen_rates, '0', '1'), 2);
            int co2 = Convert.ToInt32(getRate(CO2_rates, '1', '0'), 2);

            return co2 * oxy;
        }

        //get binary number from list that meets all criteria
        static string getRate(List<string> rates, char removeIfOneMostCommon, char removeIfTwoMostCommon)
        {
            //length of first binary nr
            int binaryLength = rates[0].Length;

            //get first bit in string
            for (int i = 0; i < binaryLength; i++)
            {
                int mostCommonBit = 0;
                //get for each nr in input
                foreach (string line in rates)
                {
                    string binary = line;
                    mostCommonBit += binary[i] == '1' ? 1 : -1; //add or substract for most common bit
                }

                if (mostCommonBit >= 0) // '1' is most common
                {
                    rates.RemoveAll(x => x[i] == removeIfOneMostCommon); //remove items with char at position
                } else // '0' is most common
                {
                    rates.RemoveAll(x => x[i] == removeIfTwoMostCommon); //remove items with char at position
                }

                if (rates.Count == 1) break; //stop if list contains one item
            }

            return rates.First();
        }
    }
}