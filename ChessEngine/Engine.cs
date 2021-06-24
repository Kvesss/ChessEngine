using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
    class Engine
    {
        public static int kingWhite, kingBlack;

        public static string[,] board = new string[8, 8] 
        { 
            {"r","k","b","q","x","b","k","r"},
            {"p","p","p","p","p","p","p","p"},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," ","P"," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {"P","P","P","P"," ","P","P","P"},
            {"R","K","B","Q","X","B","K","R"}};


        public static List<string> GetPossibleMoves()
        {
            List<string> possibleMoves = new List<string>();

            for(int i=0; i<64; i++)
            {
                
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
                    if((i != row || j != col) && IsInsideBounds(i,j))
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
                                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(i).Append(j).Append(attackedPiece);
                                    possibleMoves.Add(builder.ToString());
                                    
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
            int temp = 1;
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        try
                        {
                            while ((board[row + temp * i, col + temp * j]).Equals(" ") && IsInsideBounds(row + temp * i, col + temp * j))
                            {

                                string attackedPiece = board[row + temp * i, col + temp * j];
                                board[row, col] = " ";
                                board[row + temp * i, col + temp * j] = "Q";
                                if (IsKingSafe())
                                {
                                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + temp * i).Append(col + temp * j).Append(attackedPiece);
                                    possibleMoves.Add(builder.ToString());

                                }
                                board[row, col] = "Q";
                                board[row + temp * i, col + temp * j] = attackedPiece;
                                temp++;


                            }
                            if (char.IsLower((board[row + temp * i, col + temp * j])[0]) && IsInsideBounds(row + temp * i, col + temp * j))
                            {
                                string attackedPiece = board[row + temp * i, col + temp * j];
                                board[row, col] = " ";
                                board[row + temp * i, col + temp * j] = "Q";
                                if (IsKingSafe())
                                {
                                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + temp * i).Append(col + temp * j).Append(attackedPiece);
                                    possibleMoves.Add(builder.ToString());

                                }
                                board[row, col] = "Q";
                                board[row + temp * i, col + temp * j] = attackedPiece;

                            }
                        }
                        catch (Exception e) { }
                        temp = 1;
                    }


                }
            }

            return possibleMoves;
        }

        private static List<string> GetPossibleBishopMoves(int index)
        {
            int temp = 1;
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            for (int i = -1; i <= 1; i+=2)
            {
                for (int j = -1; j <= 1; j+=2)
                {
                    try
                    {
                        while ((board[row + temp * i, col + temp * j]).Equals(" ") && IsInsideBounds(row + temp * i, col + temp * j))
                        {

                            string attackedPiece = board[row + temp * i, col + temp * j];
                            board[row, col] = " ";
                            board[row + temp * i, col + temp * j] = "B";
                            if (IsKingSafe())
                            {
                                StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + temp * i).Append(col + temp * j).Append(attackedPiece);
                                possibleMoves.Add(builder.ToString());

                            }
                            board[row, col] = "B";
                            board[row + temp * i, col + temp * j] = attackedPiece;
                            temp++;


                        }
                        if (char.IsLower((board[row + temp * i, col + temp * j])[0]) && IsInsideBounds(row + temp * i, col + temp * j))
                        {
                            string attackedPiece = board[row + temp * i, col + temp * j];
                            board[row, col] = " ";
                            board[row + temp * i, col + temp * j] = "B";
                            if (IsKingSafe())
                            {
                                StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + temp * i).Append(col + temp * j).Append(attackedPiece);
                                possibleMoves.Add(builder.ToString());

                            }
                            board[row, col] = "B";
                            board[row + temp * i, col + temp * j] = attackedPiece;

                        }
                    }
                    catch (Exception e) { }
                    temp = 1;

                }
            }

            return possibleMoves;
        }

        private static List<string> GetPossibleRookMoves(int index)
        {
            int temp = 1;
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            for(int i=-1;i <= 1; i += 2)
            {
                try
                {
                    while ((board[row + temp * i, col]).Equals(" "))
                    {

                        string attackedPiece = board[row + temp * i, col];
                        board[row, col] = " ";
                        board[row + temp * i, col] = "R";
                        if (IsKingSafe())
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + temp * i).Append(col).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());

                        }
                        board[row, col] = "R";
                        board[row + temp * i, col] = attackedPiece;
                        temp++;


                    }
                    if (char.IsLower((board[row + temp * i, col])[0]) && IsInsideBounds(row + temp * i, col))
                    {
                        string attackedPiece = board[row + temp * i, col];
                        board[row, col] = " ";
                        board[row + temp * i, col] = "R";
                        if (IsKingSafe())
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + temp * i).Append(col).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());

                        }
                        board[row, col] = "R";
                        board[row + temp * i, col] = attackedPiece;

                    }

                }
                catch (Exception e) { }
                temp = 1;
            }
            for (int j = -1; j <= 1; j += 2)
            {
                try
                {
                    while ((board[row, col + temp*j]).Equals(" "))
                    {

                        string attackedPiece = board[row, col + temp * j];
                        board[row, col] = " ";
                        board[row, col + temp * j] = "R";
                        if (IsKingSafe())
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row).Append(col + temp * j).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());

                        }
                        board[row, col] = "R";
                        board[row, col + temp * j] = attackedPiece;
                        temp++;


                    }
                    if (char.IsLower((board[row, col + temp * j])[0]) && IsInsideBounds(row, col + temp * j))
                    {
                        string attackedPiece = board[row, col + temp * j];
                        board[row, col] = " ";
                        board[row, col + temp * j] = "R";
                        if (IsKingSafe())
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row).Append(col + temp * j).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());

                        }
                        board[row, col] = "R";
                        board[row, col + temp * j] = attackedPiece;

                    }

                }
                catch (Exception e) { }
                temp = 1;

            }


            return possibleMoves;
        }

        private static List<string> GetPossibleKnightMoves(int index)
        {
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            if(IsInsideBounds(row - 2, col + 1) && !char.IsUpper(board[row - 2, col + 1][0]))
            {
                string attackedPiece = board[row - 2, col + 1];
                board[row, col] = " ";
                board[row - 2, col + 1] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 2).Append(col + 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row - 2, col + 1] = attackedPiece;
            }
            if (IsInsideBounds(row - 1, col + 2) && !char.IsUpper(board[row - 1, col + 2][0]))
            {
                string attackedPiece = board[row - 1, col + 2];
                board[row, col] = " ";
                board[row - 1, col + 2] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col + 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row - 1, col + 2] = attackedPiece;
            }
            if (IsInsideBounds(row + 1, col + 2) && !char.IsUpper(board[row + 1, col + 2][0]))
            {
                string attackedPiece = board[row + 1, col + 2];
                board[row, col] = " ";
                board[row + 1, col + 2] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 1).Append(col + 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row + 1, col + 2] = attackedPiece;
            }
            if (IsInsideBounds(row + 2, col + 1) && !char.IsUpper(board[row + 2, col + 1][0]))
            {
                string attackedPiece = board[row + 2, col + 1];
                board[row, col] = " ";
                board[row + 2, col + 1] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 2).Append(col + 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row + 2, col + 1] = attackedPiece;
            }
            if (IsInsideBounds(row + 2, col - 1) && !char.IsUpper(board[row + 2, col - 1][0]))
            {
                string attackedPiece = board[row + 2, col - 1];
                board[row, col] = " ";
                board[row + 2, col - 1] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 2).Append(col - 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row + 2, col - 1] = attackedPiece;
            }
            if (IsInsideBounds(row + 1, col - 2) && !char.IsUpper(board[row + 1, col - 2][0]))
            {
                string attackedPiece = board[row + 1, col - 2];
                board[row, col] = " ";
                board[row + 1, col - 2] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 1).Append(col - 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row + 1, col - 2] = attackedPiece;
            }
            if (IsInsideBounds(row - 1, col - 2) && !char.IsUpper(board[row - 1, col - 2][0]))
            {
                string attackedPiece = board[row - 1, col - 2];
                board[row, col] = " ";
                board[row - 1, col - 2] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col - 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row - 1, col - 2] = attackedPiece;
            }
            if (IsInsideBounds(row - 2, col - 1) && !char.IsUpper(board[row - 2, col - 1][0]))
            {
                string attackedPiece = board[row - 2, col - 1];
                board[row, col] = " ";
                board[row - 2, col - 1] = "K";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 2).Append(col - 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "K";
                board[row - 2, col - 1] = attackedPiece;
            }

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

        private static bool IsInsideBounds(int i, int j)
        {
            return i >= 0 && i <= 7 && j >= 0 && j <= 7;
        }

        static void Main(string[] args)
        {
            List<String> testKing = GetPossibleMoves();
            for(int i = 0; i<testKing.Count; i++)
            {
                Console.WriteLine(testKing[i]);
            }


        }

    }
}


