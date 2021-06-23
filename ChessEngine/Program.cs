using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    class Program
    {
        public static int kingWhite, kingBlack;

        public static string[,] board = new string[8, 8] 
        { 
            {"r","k","b","q","a","b","k","r"},
            {"p","p","p","p","p","p","p","p"},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {"P","P","P","P","P","P","P","P"},
            {"R","K","B","Q","A","B","K","R"}};


        public static List<string> GetPossibleMoves()
        {
            List<string> possibleMoves = new List<string>();

            for(int i=0; i<64; i++)
            {
                //if (Char.IsUpper((board[i / 8, i % 8])[0]))
                switch(board[i/8,i%8])
                {
                    case "P": 
                        possibleMoves.AddRange(GetPossiblePawnMoves(i));
                        break;
                    case "K":
                        possibleMoves.AddRange(GetPossibleKnightMoves(i));
                        break;
                    case "R":
                        possibleMoves.AddRange(GetPossibleRookMoves(i));
                        break;
                    case "B":
                        possibleMoves.AddRange(GetPossibleBishopMoves(i));
                        break;
                    case "Q":
                        possibleMoves.AddRange(GetPossibleQueenMoves(i));
                        break;
                    case "X":
                        possibleMoves.AddRange(GetPossibleKingMoves(i));
                        break;
                    
                }
            }

            return possibleMoves;
        }

        private static List<string> GetPossibleKingMoves(int index)
        {

            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            for(int i = row-1; i <= row + 1; i++)
            {
                for(int j = col - 1; j <= col + 1; j++)
                {
                    if((i != row || j != col) && i >=0 && i <= 7 && j >=0 && j<=7)
                    {
                        {

                            if (char.IsLower((board[i, j])[0]) || board[i, j].Equals(" "))
                            {
                                int temp = kingWhite;
                                kingWhite = j * 8 + i * 8;
                                board[row, col] = " ";
                                string attackedPiece = board[i, j];
                                board[i, j] = "X";
                                if (IsKingSafe())
                                {
                                    //StringBuilder builder = new StringBuilder();
                                    //builder.Append(row, col, i, j, attackedPiece);
                                    //possibleMoves.Add(builder.ToString());
                                    string s = row.ToString() + col.ToString() + i.ToString() + j.ToString() + attackedPiece.ToString();
                                    possibleMoves.Add(s);
                                }
                                board[row, col] = "X";
                                board[i, j] = attackedPiece;
                                kingWhite = temp;
                            }
                        }
                    }
                }
            }

            return possibleMoves;
        }

        

        private static List<string> GetPossibleQueenMoves(int index)
        {
            List<string> possibleMoves = new List<string>();

            return possibleMoves;
        }

        private static List<string> GetPossibleBishopMoves(int index)
        {
            List<string> possibleMoves = new List<string>();

            return possibleMoves;
        }

        private static List<string> GetPossibleRookMoves(int index)
        {
            List<string> possibleMoves = new List<string>();

            return possibleMoves;
        }

        private static List<string> GetPossibleKnightMoves(int index)
        {
            List<string> possibleMoves = new List<string>();

            return possibleMoves;
        }

        private static List<string> GetPossiblePawnMoves(int index)
        {
            List<string> possibleMoves = new List<string>();

            return possibleMoves;
        }

        private static bool IsKingSafe()
        {
            return true;
        }


        static void Main(string[] args)
        {
            List<String> testKing = GetPossibleKingMoves(32);
            for(int i = 0; i<testKing.Count; i++)
            {
                Console.WriteLine(testKing[i]);
            }


        }

    }
}


