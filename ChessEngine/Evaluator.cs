using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    class Evaluator
    {

        private static int[,] randomTable;
        private static Random randomGenerator;
        public static HashSet<int> hashSet;

        static Evaluator()
        {
            hashSet = new HashSet<int>();
            randomGenerator = new Random();
            randomTable = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    randomTable[i, j] = randomGenerator.Next(int.MinValue, int.MaxValue);
                }
            }
        }

        public static void PrintTable()
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

        public static bool CheckHashValue()
        {
            int hashValue = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (Engine.board[i, j])
                    {
                        case "P":
                            hashValue += 336546 * randomTable[i, j];

                            break;
                        case "N":
                            hashValue += 546564 * randomTable[i, j];

                            break;
                        case "B":
                            hashValue += 756489 * randomTable[i, j];

                            break;
                        case "R":
                            hashValue += 117988 * randomTable[i, j];

                            break;
                        case "Q":
                            hashValue += 136785 * randomTable[i, j];

                            break;
                        case "K":
                            hashValue += 167557 * randomTable[i, j];

                            break;
                        case "p":
                            hashValue += 474767 * randomTable[i, j];

                            break;
                        case "n":
                            hashValue += 356356 * randomTable[i, j];

                            break;
                        case "b":
                            hashValue += 567567 * randomTable[i, j];

                            break;
                        case "r":
                            hashValue += 957445 * randomTable[i, j];

                            break;
                        case "q":
                            hashValue += 657457 * randomTable[i, j];

                            break;
                        case "k":
                            hashValue += 356743 * randomTable[i, j];
                            break;
                    }
                }
            }
            if (hashSet.Contains(hashValue))
            {
                return true;
            }
            hashSet.Add(hashValue);
            return false;
        }



        private static readonly int[,] pawnPosition = new int[8, 8]
        {
            { 0,  0,  0,  0,  0,  0,  0,  0},
            {50, 50, 50, 50, 50, 50, 50, 50},
            {10, 10, 20, 30, 30, 20, 10, 10},
            { 5,  5, 10, 25, 25, 10,  5,  5},
            { 0,  0,  0, 20, 20,  0,  0,  0},
            { 5, -5,-10,  0,  0,-10, -5,  5},
            { 5, 10, 10,-20,-20, 10, 10,  5},
            { 0,  0,  0,  0,  0,  0,  0,  0}};

        private static readonly int[,] knightPosition = new int[8, 8]
        {
            {-50,-40,-30,-30,-30,-30,-40,-50},
            {-40,-20,  0,  0,  0,  0,-20,-40},
            {-30,  0, 10, 15, 15, 10,  0,-30},
            {-30,  5, 15, 20, 20, 15,  5,-30},
            {-30,  0, 15, 20, 20, 15,  0,-30},
            {-30,  5, 10, 15, 15, 10,  5,-30},
            {-40,-20,  0,  5,  5,  0,-20,-40},
            {-50,-40,-30,-30,-30,-30,-40,-50}};
        private static readonly int[,] bishopPosition = new int[8, 8]
        {
            {-20,-10,-10,-10,-10,-10,-10,-20},
            {-10,  0,  0,  0,  0,  0,  0,-10},
            {-10,  0,  5, 10, 10,  5,  0,-10},
            {-10,  5,  5, 10, 10,  5,  5,-10},
            {-10,  0, 10, 10, 10, 10,  0,-10},
            {-10, 10, 10, 10, 10, 10, 10,-10},
            {-10,  5,  0,  0,  0,  0,  5,-10},
            {-20,-10,-10,-10,-10,-10,-10,-20}};
        private static readonly int[,] rookPosition = new int[8, 8]
        {
            { 0,  0,  0,  0,  0,  0,  0,  0},
            {50, 50, 50, 50, 50, 50, 50, 50},
            {10, 10, 20, 30, 30, 20, 10, 10},
            { 5,  5, 10, 25, 25, 10,  5,  5},
            { 0,  0,  0, 20, 20,  0,  0,  0},
            { 5, -5,-10,  0,  0,-10, -5,  5},
            { 5, 10, 10,-20,-20, 10, 10,  5},
            { 0,  0,  0, 10, 10,  0,  0,  0}};
        private static readonly int[,] queenPosition = new int[8, 8]
        {
            {-20,-10,-10, -5, -5,-10,-10,-20},
            {-10,  0,  0,  0,  0,  0,  0,-10},
            {-10,  0,  5,  5,  5,  5,  0,-10},
            { -5,  0,  5,  5,  5,  5,  0, -5},
            {  0,  0,  5,  5,  5,  5,  0, -5},
            {-10,  5,  5,  5,  5,  5,  0,-10},
            {-10,  0,  5,  0,  0,  0,  0,-10},
            {-20,-10,-10, -5, -5,-10,-10,-20}};


        private static readonly int[,] kingPosition = new int[8, 8]
        {
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-20,-30,-30,-40,-40,-30,-30,-20},
            {-10,-20,-20,-20,-20,-20,-20,-10},
            { 20, 20,  0,  0,  0,  0, 20, 20},
            { 20, 30, 10,  0,  0, 10, 30, 20}};

        private static readonly int[,] kingEndPosition = new int[8, 8]
        {
            {-50,-40,-30,-20,-20,-30,-40,-50},
            {-30,-20,-10,  0,  0,-10,-20,-30},
            {-30,-10, 20, 30, 30, 20,-10,-30},
            {-30,-10, 30, 40, 40, 30,-10,-30},
            {-30,-10, 30, 40, 40, 30,-10,-30},
            {-30,-10, 20, 30, 30, 20,-10,-30},
            {-30,-30,  0,  0,  0,  0,-30,-30},
            {-50,-30,-30,-30,-30,-30,-30,-50}};


        public static int Evaluate(int count, int depth)
        {
            int evaluationValue = 0;
            int piecesValue = EvaluatePieces();
            evaluationValue += piecesValue;
            evaluationValue += EvaluateMobility(count);
            evaluationValue += EvaluatePosition(piecesValue);
            Engine.SwitchPlayer();
            piecesValue = EvaluatePieces();
            evaluationValue -= piecesValue;
            evaluationValue -= EvaluateMobility(count);
            evaluationValue -= EvaluatePosition(piecesValue);
            Engine.SwitchPlayer();
            return (evaluationValue + depth * 50)*-1;

        }

        private static int EvaluatePosition(int piecesValue)
        {
            int value = 0;
            int[] pawnsInLine = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (Engine.board[i, j])
                    {
                        case "P":
                            value += pawnPosition[i, j];
                            pawnsInLine[j]++;
                            if(pawnsInLine[j] > 1)
                                value -= 50;
                            //if (Engine.IsInsideBounds(i - 1, j - 1))
                            //{
                            //    if (Engine.board[i - 1, j - 1] == "P")
                            //        value += 10;
                            //}
                            //if (Engine.IsInsideBounds(i - 1, j + 1))
                            //{
                            //    if (Engine.board[i - 1, j + 1] == "P")
                            //        value += 10;
                            //}
                            break;
                        case "N":
                            value += knightPosition[i, j];
                            break;
                        case "B":
                            value += bishopPosition[i, j];
                            break;
                        case "R":
                            value += rookPosition[i, j];
                            break;
                        case "Q":
                            value += queenPosition[i, j];
                            break;
                        case "K":
                            if(piecesValue >= 12000)
                            {
                                value += kingPosition[i, j];
                            }
                            else
                            {
                                value += kingEndPosition[i, j];
                            }
                            break;
                    }
                }
            }

            return value;
        }
        

        private static int EvaluateMobility(int count)
        {
            return 5*count;
        }


        public static int EvaluatePieces()
        {
            int value = 0;
            bool bishopPair = false;
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j <8; j++)
                {
                    switch (Engine.board[i, j])
                    {
                        case "P":
                            value += 100;
                            break;
                        case "N":
                            value += 300;
                            break;
                        case "B":
                            value += bishopPair ? 400 : 300;
                            bishopPair = !bishopPair;
                            break;
                        case "R":
                            value += 500;
                            break;
                        case "Q":
                            value += 900;
                            break;
                        case "K":
                            value += 9000;
                            break;
                    }
                }  
            }
            return value;
        }
    }
}
