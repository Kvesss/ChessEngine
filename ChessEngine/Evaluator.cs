using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    class Evaluator
    {


        public static readonly int[,] pawnPosition = new int[8, 8]
        {
            { 0,  0,  0,  0,  0,  0,  0,  0},
            {50, 50, 50, 50, 50, 50, 50, 50},
            {10, 10, 20, 30, 30, 20, 10, 10},
            { 5,  5, 10, 25, 25, 10,  5,  5},
            { 0,  0,  0, 20, 20,  0,  0,  0},
            { 5, -5,-10,  0,  0,-10, -5,  5},
            { 5, 10, 10,-20,-20, 10, 10,  5},
            { 0,  0,  0,  0,  0,  0,  0,  0}};

        public static readonly int[,] knightPosition = new int[8, 8]
        {
            {-50,-40,-30,-30,-30,-30,-40,-50},
            {-40,-20,  0,  0,  0,  0,-20,-40},
            {-30,  0, 10, 15, 15, 10,  0,-30},
            {-30,  5, 15, 20, 20, 15,  5,-30},
            {-30,  0, 15, 20, 20, 15,  0,-30},
            {-30,  5, 10, 15, 15, 10,  5,-30},
            {-40,-20,  0,  5,  5,  0,-20,-40},
            {-50,-40,-30,-30,-30,-30,-40,-50}};
        public static readonly int[,] bishopPosition = new int[8, 8]
        {
            {-20,-10,-10,-10,-10,-10,-10,-20},
            {-10,  0,  0,  0,  0,  0,  0,-10},
            {-10,  0,  5, 10, 10,  5,  0,-10},
            {-10,  5,  5, 10, 10,  5,  5,-10},
            {-10,  0, 10, 10, 10, 10,  0,-10},
            {-10, 10, 10, 10, 10, 10, 10,-10},
            {-10,  5,  0,  0,  0,  0,  5,-10},
            {-20,-10,-10,-10,-10,-10,-10,-20}};
        public static readonly int[,] rookPosition = new int[8, 8]
        {
            { 0,  0,  0,  0,  0,  0,  0,  0},
            {50, 50, 50, 50, 50, 50, 50, 50},
            {10, 10, 20, 30, 30, 20, 10, 10},
            { 5,  5, 10, 25, 25, 10,  5,  5},
            { 0,  0,  0, 20, 20,  0,  0,  0},
            { 5, -5,-10,  0,  0,-10, -5,  5},
            { 5, 10, 10,-20,-20, 10, 10,  5},
            { 0,  0,  0, 10, 10,  0,  0,  0}};
        public static readonly int[,] queenPosition = new int[8, 8]
        {
            {-20,-10,-10, -5, -5,-10,-10,-20},
            {-10,  0,  0,  0,  0,  0,  0,-10},
            {-10,  0,  5,  5,  5,  5,  0,-10},
            { -5,  0,  5,  5,  5,  5,  0, -5},
            {  0,  0,  5,  5,  5,  5,  0, -5},
            {-10,  5,  5,  5,  5,  5,  0,-10},
            {-10,  0,  5,  0,  0,  0,  0,-10},
            {-20,-10,-10, -5, -5,-10,-10,-20}};


        public static readonly int[,] kingPosition = new int[8, 8]
        {
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-20,-30,-30,-40,-40,-30,-30,-20},
            {-10,-20,-20,-20,-20,-20,-20,-10},
            { 20, 20,  0,  0,  0,  0, 20, 20},
            { 20, 30, 10,  0,  0, 10, 30, 20}};


        public static int Evaluate(int count, int depth)
        {
            int evaluationValue = 0;
            int piecesValue = EvaluatePieces();
            evaluationValue += EvaluateAttack();
            evaluationValue += piecesValue;
            evaluationValue += EvaluateMobility();
            evaluationValue += EvaluatePosition(piecesValue);
            Engine.FlipBoard();
            piecesValue = EvaluatePieces();
            evaluationValue -= EvaluateAttack();
            evaluationValue -= piecesValue;
            evaluationValue -= EvaluateMobility();
            evaluationValue -= EvaluatePosition(piecesValue);
            Engine.FlipBoard();
            return -(evaluationValue + depth * 50);
        }

        private static int EvaluatePosition(int piecesValue)
        {
            int value = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (Engine.board[i, j])
                    {
                        case "P":
                            value += pawnPosition[i, j];
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
                            value += kingPosition[i, j];
                            break;
                    }
                }
            }
            return value;
        }

        private static int EvaluateMobility()
        {
            return 0;
        }

        private static int EvaluateAttack()
        {
            return 0;
        }

        public static int EvaluatePieces()
        {
            int value = 0;
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
                            value += 300;
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
