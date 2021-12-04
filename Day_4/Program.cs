using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    class Program
    {
        static int size = 5;

        static void Main(string[] args)
        {
            //get input from file
            List<string> input = File.ReadAllLines("Day4.txt").ToList();
            //get bingo drawn numbers from input
            List<int> draw = input[0].Split(',').ToList().ConvertAll(x => int.Parse(x));
            //get list of card arrays from input          
            List<int[,]> cards = new List<int[,]>();
            for (int i = 2; i < input.Count; i = i + 6)
            {
                int[,] card = MakeBingoCard(input, i, i + 4);
                cards.Add(card);
            }

            //execute and print part one and part two
            Console.WriteLine(PartOne(input, draw, cards));
            Console.WriteLine(PartTwo(input, draw, cards));

        }

        public static long PartOne(List<string> input, List<int> draw, List<int[,]> cards)
        {
            //loop through all drawn bingo nrs
            foreach (int nr in draw) 
            {
                //loop through each bingo card
                foreach (int[,] card in cards)
                {
                    //check if card has nr
                    isNrOnCard(card, nr);
                    //if card has bingo, calc and return answer
                    if (checkHasBingo(card)) return bingo(card, nr);
                }
            }
            return 0;
        }

        public static long PartTwo(List<string> input, List<int> draw, List<int[,]> cards)
        {
            //loop through all drawn bingo nrs
            foreach (int nr in draw)
            {
                //keep track of only unfinished cards
                List<int[,]> removedList = new List<int[,]>(cards);

                //loop through each bingo card
                foreach (int[,] card in cards)
                {
                    //check if card has nr
                    isNrOnCard(card, nr);

                    //if card has bingo, remove from list
                    if (checkHasBingo(card))
                    {
                        removedList.Remove(card);
                        //for last card, calc and return answer
                        if (removedList.Count == 0) return bingo(card, nr);
                    }
                }
                //new cards list only has unfinished cards
                cards = removedList;
            }
            //no one has full card
            return 0;
        }


        public static void isNrOnCard(int[,] card, int nr)
        {
            //loop through each nr on card
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    //if nr is on card
                    if (card[i, j] == nr)
                    {
                        //mark nr on card and exit
                        card[i, j] = int.MaxValue;
                        break;
                    }
                }
            }
        }

        public static bool checkHasBingo(int[,] card)
        {
            int counter;

            //check rows
            for (int row = 0; row < size; row++)
            {
                counter = 0;
                for (int column = 0; column < size; column++)
                {
                    if (card[row, column] == int.MaxValue)
                    {
                        counter++;
                    }
                }

                if (counter == size) return true; //bingo!
            }

            //check columns
            for (int column = 0; column < size; column++)
            {
                counter = 0;
                for (int row = 0; row < size; row++)
                {
                    if (card[row, column] == int.MaxValue)
                    {
                        counter++;
                    }
                }

                if (counter == size) return true; // bingo!
            }
            return false; // no bingo :(
        }


        public static long bingo(int[,] card, int lastNr)
        {
            //calc sum of all nrs that weren't called
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (card[i,j] != int.MaxValue) sum += card[i, j];
                }
            }
            
            return sum * lastNr;
        }


        //convert list input to array for each card
        public static int[,] MakeBingoCard(List<string> input, int start, int end) {
            int[,] card = new int[size, size];
            
            for(int i = start; i <= end; i++)
            {
                List<int> lines = input[i].Trim().Replace("  ", " ").Split(" ").ToList().ConvertAll(x=> int.Parse(x));

                for (int j = 0; j < size; j++)
                {
                    card[i - start, j] = lines[j];
                }
            }

            return card;
        }
    }
}