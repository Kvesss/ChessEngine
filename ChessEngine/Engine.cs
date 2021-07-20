using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ChessEngine
{
    class Engine
    {
        public static int whiteKing, blackKing;
        public const int DEPTH = 5;


        //r*b***k*******p*P*n*pr*pq*p******b*P******N**N**PP*QBPPPR***K**R
        //**nq*nk******p*p****p*pQpb*pP*NP*p*P**P**P****N*P****PB*******K*
        //***********r**p*pp*Bp*p**kP******n**K*********R**P***P**********
        //************kb*p**p***pP*pP*P*P**P***K***B**********************
        //b*R**nk******ppp*p***n*******N***b**p****P**BP**q***BQPP******K*
        //***rr*k*pp***pbp**bp*np*q***p*B***B*P*****N****PPPPQ*PP****RR*K*
        //r*b*qrk**ppn*pb*p**p*npp***Pp*****P*P**B**N*****PP*NBPPPR**Q*RK*
        //**R*r********k**pBP*n**p******p**************P*P**P***P********K
        //**r**rk**p*R*pp*p***p**p************B******QB*P*q*P***KP********
        //r*bq*rk*p****ppp*pnp*n****p*******PPpP***NP*P***P***B*PPR*BQ*RK*
        public static void SetBoard(string input)
        {
            for (int i=0; i < 64; i++)
            {
                if (input[i] == '*')
                {
                    board[i / 8, i % 8] = " ";
                }
                else
                {
                    board[i / 8, i % 8] = input[i].ToString();
                }
            }
        }

        public static string GetMove(string move)
        {
            string returnMove = "";
            returnMove += (char)(65 + int.Parse(move[1].ToString()));
            returnMove += 8 - int.Parse(move[0].ToString());
            returnMove += " , ";
            returnMove += (char)(65 + int.Parse(move[3].ToString()));
            returnMove += 8 - int.Parse(move[2].ToString());

            return returnMove;
        }


        public static string[,] board = new string[8, 8] 
        { 
            {"r","n","b","q","k","b","n","r"},
            {"p","p","p","p","p","p","p","p"},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "},
            {"P","P","P","P","P","P","P","P"},
            {"R","N","B","Q","K","B","N","R"}};


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
                    case "N":
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
                    case "K":
                        possibleMoves.AddRange(GetPossibleKingMoves(i));
                        break;
                    
                }
            }

            return possibleMoves;
        }


        public static void Move(string move)
        {
            if (!move.Contains('Q'))
            {
                board[(int)char.GetNumericValue(move[2]), (int)char.GetNumericValue(move[3])] = board[(int)char.GetNumericValue(move[0]), (int)char.GetNumericValue(move[1])];
                board[(int)char.GetNumericValue(move[0]), (int)char.GetNumericValue(move[1])] = " ";
            }
            else
            {
                board[(int)char.GetNumericValue(move[2]), (int)char.GetNumericValue(move[3])] = "Q";
                board[(int)char.GetNumericValue(move[0]), (int)char.GetNumericValue(move[1])] = " ";
            }
        }


        public static void Undo(string move)
        {
            if (!move.Contains('Q'))
            {
                board[(int)char.GetNumericValue(move[0]), (int)char.GetNumericValue(move[1])] = board[(int)char.GetNumericValue(move[2]), (int)char.GetNumericValue(move[3])];
                board[(int)char.GetNumericValue(move[2]), (int)char.GetNumericValue(move[3])] = move[4].ToString();
            }
            else
            {
                board[(int)char.GetNumericValue(move[0]), (int)char.GetNumericValue(move[1])] = "P";
                board[(int)char.GetNumericValue(move[2]), (int)char.GetNumericValue(move[3])] = move[4].ToString();
            }
        }



        public static string Minimax(int depth, string move, int alpha, int beta, int maximizingPlayer)
        {
            List<string> availableMoves = GetPossibleMoves();
            if (depth == 0 || availableMoves.Count.Equals(0))
            {
                return move + (Evaluator.Evaluate(availableMoves.Count, depth) * (maximizingPlayer * 2 - 1)).ToString();
            }
            OrderMoves(availableMoves);
            maximizingPlayer = 1 - maximizingPlayer;
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Move(availableMoves[i]);
                SwitchPlayer();
                string oppositeMove = Minimax(depth - 1, availableMoves[i], alpha, beta, maximizingPlayer);
                int moveValue = (!oppositeMove.Contains('Q')) ? int.Parse(oppositeMove.Substring(5)) : int.Parse(oppositeMove.Substring(6));
                SwitchPlayer();
                Undo(availableMoves[i]);

                if (maximizingPlayer == 0)
                {
                    if (moveValue <= beta)
                    {
                        beta = moveValue;
                        if (depth == DEPTH)
                        {
                            move = (!oppositeMove.Contains('Q')) ? oppositeMove.Substring(0, 5) : oppositeMove.Substring(0,6);
                        }
                    }
                }
                else if (moveValue > alpha)
                {
                    alpha = moveValue;
                    if (depth == DEPTH)
                    {
                        move = (!oppositeMove.Contains('Q')) ? oppositeMove.Substring(0, 5) : oppositeMove.Substring(0, 6);
                    }
                }
                if (alpha >= beta)
                {
                    return (maximizingPlayer == 0) ? move + beta : move + alpha;
                }
            }
            return (maximizingPlayer == 0) ? move + beta : move + alpha;

        }

        public static void OrderMoves(List<string> moves)
        {
            List<int> values = new List<int>();
            List<Move> sortedMoves = new List<Move>();
            for (int i = 0; i<moves.Count; i++)
            {
                Move(moves[i].Substring(0, 5));
                values.Add(Evaluator.Evaluate(-1, 0));
                sortedMoves.Add(new Move(moves[i], values[i]));
                Undo(moves[i].Substring(0, 5));
            }
            for (int i = 0; i< sortedMoves.Count; i++)
            {
                for (int j = 0; j < sortedMoves.Count - 1; j++)
                {
                    if(sortedMoves[j].GetValue() > sortedMoves[j + 1].GetValue())
                    {
                        Move temp = sortedMoves[j + 1];
                        sortedMoves[j + 1] = sortedMoves[j];
                        sortedMoves[j] = temp;
                    }
                }
            }
            moves.Clear();
            for(int i = 0; i<sortedMoves.Count; i++)
            {
                moves.Add(sortedMoves[i].GetMove());
            }

        }

        public static void SwitchPlayer()
        {
            int kingTemp = whiteKing;
            whiteKing = 63 - blackKing;
            blackKing = 63 - kingTemp;
            for (int i =0;i <32; i++)
            {
                string temp;
                int r = i / 8, c = i % 8;
                if (char.IsUpper(board[r,c][0]))
                {
                    temp = board[r,c].ToLower();
                }
                else
                {
                    temp = board[r,c].ToUpper();
                }
                if (char.IsUpper(board[7 - r,7 - c][0]))
                {
                    board[r,c] = board[7 - r,7 - c].ToLower();
                }
                else
                {
                    board[r,c] = board[7 - r,7 - c].ToUpper();
                }
                board[7 - r,7 - c] = temp;
            }
        }

        public static List<string> GetPossibleKingMoves(int index)
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
                                int temp = whiteKing;
                                whiteKing = j + i * 8;
                                string attackedPiece = board[i, j];
                                board[row, col] = " ";
                                board[i, j] = "K";
                                if (IsKingSafe())
                                {
                                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(i).Append(j).Append(attackedPiece);
                                    possibleMoves.Add(builder.ToString());
                                    
                                }
                                board[row, col] = "K";
                                board[i, j] = attackedPiece;
                                whiteKing = temp;
                            }
                        }
                    }
                }
            }

            return possibleMoves;
        }



        public static List<string> GetPossibleQueenMoves(int index)
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
                        while(IsInsideBounds(row + temp * i, col + temp * j))
                            if ((board[row + temp * i, col + temp * j]).Equals(" "))
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
                            else
                            {
                                if (char.IsLower((board[row + temp * i, col + temp * j])[0]))
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
                                break;
                            }
                            
                        temp = 1;
                    }


                }
            }

            return possibleMoves;
        }

        public static List<string> GetPossibleBishopMoves(int index)
        {
            int temp = 1;
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            for (int i = -1; i <= 1; i+=2)
            {
                for (int j = -1; j <= 1; j+=2)
                {
                    while(IsInsideBounds(row + temp * i, col + temp * j)) { 
                        if(board[row + temp * i, col + temp * j].Equals(" "))
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
                        else
                        {
                            if (char.IsLower((board[row + temp * i, col + temp * j])[0]))
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
                            break;
                        }
                        temp++;
                    }
                    temp = 1;

                }
            }

            return possibleMoves;
        }

        public static List<string> GetPossibleRookMoves(int index)
        {
            int temp = 1;
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            for (int i = -1; i <= 1; i += 2)
            {
                while (IsInsideBounds(row + temp * i, col))
                {
                    if ((board[row + temp * i, col]).Equals(" "))
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
                    else
                    {
                        if (char.IsLower(board[row + temp * i, col][0]) && IsInsideBounds(row + temp * i, col))
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
                        break;
                    }
                    temp++;
                }

                temp = 1;
            }
            for (int i = -1; i <= 1; i += 2)
            {
                while (IsInsideBounds(row, col + temp * i))
                {
                    if ((board[row, col + temp * i]).Equals(" "))
                    {
                        string attackedPiece = board[row, col + temp * i];
                        board[row, col] = " ";
                        board[row, col + temp * i] = "R";
                        if (IsKingSafe())
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row).Append(col + temp * i).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());
                        }
                        board[row, col] = "R";
                        board[row, col + temp * i] = attackedPiece;

                    }
                    else
                    {
                        if (char.IsLower((board[row, col + temp * i])[0]) && IsInsideBounds(row, col + temp * i))
                        {
                            string attackedPiece = board[row, col + temp * i];
                            board[row, col] = " ";
                            board[row, col + temp * i] = "R";
                            if (IsKingSafe())
                            {
                                StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row).Append(col + temp * i).Append(attackedPiece);
                                possibleMoves.Add(builder.ToString());

                            }
                            board[row, col] = "R";
                            board[row, col + temp * i] = attackedPiece;
                        }
                        break;
                    }
                    temp++;
                }

                temp = 1;
            }
            return possibleMoves;
        }

        public static List<string> GetPossibleKnightMoves(int index)
        {
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            if(IsInsideBounds(row - 2, col + 1) && !char.IsUpper(board[row - 2, col + 1][0]))
            {
                string attackedPiece = board[row - 2, col + 1];
                board[row, col] = " ";
                board[row - 2, col + 1] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 2).Append(col + 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row - 2, col + 1] = attackedPiece;
            }
            if (IsInsideBounds(row - 1, col + 2) && !char.IsUpper(board[row - 1, col + 2][0]))
            {
                string attackedPiece = board[row - 1, col + 2];
                board[row, col] = " ";
                board[row - 1, col + 2] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col + 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row - 1, col + 2] = attackedPiece;
            }
            if (IsInsideBounds(row + 1, col + 2) && !char.IsUpper(board[row + 1, col + 2][0]))
            {
                string attackedPiece = board[row + 1, col + 2];
                board[row, col] = " ";
                board[row + 1, col + 2] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 1).Append(col + 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row + 1, col + 2] = attackedPiece;
            }
            if (IsInsideBounds(row + 2, col + 1) && !char.IsUpper(board[row + 2, col + 1][0]))
            {
                string attackedPiece = board[row + 2, col + 1];
                board[row, col] = " ";
                board[row + 2, col + 1] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 2).Append(col + 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row + 2, col + 1] = attackedPiece;
            }
            if (IsInsideBounds(row + 2, col - 1) && !char.IsUpper(board[row + 2, col - 1][0]))
            {
                string attackedPiece = board[row + 2, col - 1];
                board[row, col] = " ";
                board[row + 2, col - 1] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 2).Append(col - 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row + 2, col - 1] = attackedPiece;
            }
            if (IsInsideBounds(row + 1, col - 2) && !char.IsUpper(board[row + 1, col - 2][0]))
            {
                string attackedPiece = board[row + 1, col - 2];
                board[row, col] = " ";
                board[row + 1, col - 2] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row + 1).Append(col - 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row + 1, col - 2] = attackedPiece;
            }
            if (IsInsideBounds(row - 1, col - 2) && !char.IsUpper(board[row - 1, col - 2][0]))
            {
                string attackedPiece = board[row - 1, col - 2];
                board[row, col] = " ";
                board[row - 1, col - 2] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col - 2).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row - 1, col - 2] = attackedPiece;
            }
            if (IsInsideBounds(row - 2, col - 1) && !char.IsUpper(board[row - 2, col - 1][0]))
            {
                string attackedPiece = board[row - 2, col - 1];
                board[row, col] = " ";
                board[row - 2, col - 1] = "N";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 2).Append(col - 1).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());

                }
                board[row, col] = "N";
                board[row - 2, col - 1] = attackedPiece;
            }

            return possibleMoves;
        }

        public static List<string> GetPossiblePawnMoves(int index)
        {
            int row = index / 8, col = index % 8;
            List<string> possibleMoves = new List<string>();
            if (row == 6 && board[row - 1, col].Equals(" ") && board[row - 2, col].Equals(" "))
            {
                string attackedPiece = board[row - 2, col];
                board[row, col] = " ";
                board[row - 2, col] = "P";
                if (IsKingSafe())
                {
                    StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 2).Append(col).Append(attackedPiece);
                    possibleMoves.Add(builder.ToString());
                }
                board[row - 2, col] = attackedPiece;
                board[row, col] = "P";

            }
            if (IsInsideBounds(row - 1, col - 1))
            {
                if (char.IsLower(board[row - 1, col - 1][0]))
                {
                    string attackedPiece = board[row - 1, col - 1];
                    board[row, col] = " ";
                    board[row - 1, col - 1] = "P";
                    if (IsKingSafe())
                    {
                        if(row-1 == 0)
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col - 1).Append(attackedPiece).Append('Q');
                            possibleMoves.Add(builder.ToString());
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col - 1).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());
                        }
                        
                    }
                    board[row - 1, col - 1] = attackedPiece;
                    board[row, col] = "P";
                }
            }
            if(IsInsideBounds(row - 1, col + 1))
            {
                if (char.IsLower(board[row - 1, col + 1][0]))
                {
                    string attackedPiece = board[row - 1, col + 1];
                    board[row, col] = " ";
                    board[row - 1, col + 1] = "P";
                    if (IsKingSafe())
                    {
                        if (row - 1 == 0)
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col + 1).Append(attackedPiece).Append('Q');
                            possibleMoves.Add(builder.ToString());
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col + 1).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());
                        }
                    }
                    board[row - 1, col + 1] = attackedPiece;
                    board[row, col] = "P";
                }
            }
            if (IsInsideBounds(row - 1, col))
            {
                if (board[row - 1, col].Equals(" "))
                {
                    string attackedPiece = board[row - 1, col];
                    board[row, col] = " ";
                    board[row - 1, col] = "P";
                    if (IsKingSafe())
                    {
                        if (row - 1 == 0)
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col).Append(attackedPiece).Append('Q');
                            possibleMoves.Add(builder.ToString());
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder().Append(row).Append(col).Append(row - 1).Append(col).Append(attackedPiece);
                            possibleMoves.Add(builder.ToString());
                        }

                    }
                    board[row - 1, col] = attackedPiece;
                    board[row, col] = "P";

                }
            }
                
            return possibleMoves;
        }

        public static bool IsKingSafe()
        {
            /*
            //FlipBoard();
            //List<string> enemyMoves = GetPossibleMoves();
            //for (int i = 0; i < enemyMoves.Count; i++)
            //{
            //    if (enemyMoves[i].Contains("k"))
            //    {
            //        FlipBoard();
            //        return false;
            //    }
            //}
            //FlipBoard();*/



            //int row = whiteKing / 8, col = whiteKing % 8;
            ////bishop / queen
            //int temp = 1;
            //for (int i = -1; i <= 1; i += 2)
            //{
            //    for (int j = -1; j <= 1; j += 2)
            //    {
            //        while (IsInsideBounds(row + temp * i, col + temp * j))
            //        {
            //            if (" ".Equals(board[row + temp * i, col + temp * j])) { temp++; }
            //            else
            //            {
            //                if ("b".Equals(board[row + temp * i, col + temp * j]) ||
            //                    "q".Equals(board[row + temp * i, col + temp * j]))
            //                {
            //                    return false;
            //                }
            //                break;
            //            }
            //        }
            //        temp = 1;
            //    }
            //}
            ////rook/queen
            //for (int i = -1; i <= 1; i += 2)
            //{
            //    while (IsInsideBounds(row, col + temp * i))
            //    {
            //        if (" ".Equals(board[row, col + temp * i])) { temp++; }
            //        else
            //        {
            //            if ("b".Equals(board[row, col + temp * i]) ||
            //                "q".Equals(board[row, col + temp * i]))
            //            {
            //                return false;
            //            }
            //            break;
            //        }
            //    }
            //    while (IsInsideBounds(row + temp * i, col))
            //    {
            //        if (" ".Equals(board[row + temp * i, col])) { temp++; }
            //        else
            //        {
            //            if ("b".Equals(board[row + temp * i, col]) ||
            //                "q".Equals(board[row + temp * i, col]))
            //            {
            //                return false;
            //            }
            //            break;
            //        }
            //    }
            //    temp = 1;
            //}
            ////knight
            //for (int i = -1; i <= 1; i += 2)
            //{
            //    for (int j = -1; j <= 1; j += 2)
            //    {
            //        if (IsInsideBounds(row + i, col + j * 2))
            //        {
            //            if ("n".Equals(board[row + i, col + j * 2]))
            //            {
            //                return false;
            //            }
            //        }


            //        if (IsInsideBounds(row + i * 2, col + j))
            //        {
            //            if ("n".Equals(board[row + i * 2, col + j]))
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}
            ////pawn
            //if (whiteKing >= 16)
            //{
            //    if (IsInsideBounds(row - 1, col - 1))
            //    {
            //        if ("p".Equals(board[row - 1, col - 1]))
            //        {
            //            return false;
            //        }
            //    }
            //    if (IsInsideBounds(row - 1, col + 1))
            //    {
            //        if ("p".Equals(board[row - 1, col + 1]))
            //        {
            //            return false;
            //        }
            //    }
            //}
            ////king
            //for (int i = -1; i <= 1; i++)
            //{
            //    for (int j = -1; j <= 1; j++)
            //    {
            //        if (i != 0 || j != 0)
            //        {
            //            if (IsInsideBounds(row + i, col + j))
            //            {
            //                if ("k".Equals(board[row + i, col + j]))
            //                {
            //                    return false;
            //                }
            //            }

            //        }
            //    }
            //}
            return true;
        }

        public static bool IsInsideBounds(int i, int j)
        {
            return i >= 0 && i <= 7 && j >= 0 && j <= 7;
        }


        public static void PrintBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j<8; j++)
                {
                    if(board[i,j].Equals(" ")){
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write(board[i,j]);
                        Console.Write(" ");
                    }
                }
                Console.Write(Environment.NewLine);
            }
        }


        //static void Main(string[] args)
        //{
        //    var stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    while (!"K".Equals(board[whiteKing / 8, whiteKing % 8]))
        //    {
        //        whiteKing++;
        //    }
        //    while (!"k".Equals(board[blackKing / 8, blackKing % 8]))
        //    {
        //        blackKing++;
        //    }

        //    //List<string> testKing = GetPossibleMoves();
        //    //for (int i = 0; i < testKing.Count; i++)
        //    //{
        //    //    Console.WriteLine(testKing[i]);
        //    //}


        //    //Console.WriteLine(Environment.NewLine);
        //    //PrintBoard();
        //    //Move("6545 ");
        //    //Undo("6545 ");
        //    //Console.WriteLine(Environment.NewLine);
        //    //PrintBoard();

        //    //List<string> testKing = GetPossibleMoves();
        //    //for (int i = 0; i < testKing.Count; i++)
        //    //{
        //    //    Console.WriteLine(testKing[i]);
        //    //}
        //    //Console.WriteLine(Minimax(DEPTH, "", -100000, 100000, 0));


        //    //string input = Console.ReadLine();
        //    //SetBoard(input);
        //    //SwitchPlayer();
        //    string move = Minimax(DEPTH, "", -100000, 100000, 0);
        //    Console.WriteLine(GetMove(move));
        //    Console.WriteLine(move);
        //    Move(move);
        //    PrintBoard();


        //    //int counter = 0;
        //    //while (true)
        //    //{
        //    //    Thread.Sleep(1000);
        //    //    SwitchPlayer();
        //    //    if (counter % 2 == 0)
        //    //    {
        //    //        Move(Minimax(DEPTH, "", -100000, 100000, 0));
        //    //        PrintBoard();
        //    //    }
        //    //    else
        //    //    {

        //    //        Move(Minimax(DEPTH, "", -100000, 100000, 1));
        //    //        SwitchPlayer();
        //    //        PrintBoard();
        //    //        SwitchPlayer();
        //    //    }
        //    //    Console.WriteLine(Environment.NewLine);


        //    //}


        //    //OrderMoves(testKing);
        //    //Console.WriteLine(Environment.NewLine);


        //    //for (int i = 0; i < testKing.Count; i++)
        //    //{
        //    //    Console.WriteLine(testKing[i]);
        //    //}


        //    stopwatch.Stop();

        //    Console.WriteLine(stopwatch.Elapsed);

        //}

    }
}


