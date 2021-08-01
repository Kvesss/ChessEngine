using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    class TranspositionTable
    {

        private int[,] randomTable;
        private Random randomGenerator;
        private Dictionary<int, string> hashTable;

     
        public TranspositionTable()
        {
            hashTable = new Dictionary<int, string>();
            randomGenerator = new Random();
            randomTable = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for(int j=0; j < 8; j++)
                {
                    randomTable[i, j] = randomGenerator.Next(int.MinValue/100, int.MaxValue/100);
                }
            }
        }

        public void PrintTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(randomTable[i, j] + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
