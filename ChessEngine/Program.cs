using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ChessEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //while (!"K".Equals(board[whiteKing / 8, whiteKing % 8]))
            //{
            //    whiteKing++;
            //}
            //while (!"k".Equals(board[blackKing / 8, blackKing % 8]))
            //{
            //    blackKing++;
            //}

            //List<string> testKing = GetPossibleMoves();
            //for (int i = 0; i < testKing.Count; i++)
            //{
            //    Console.WriteLine(testKing[i]);
            //}


            //Console.WriteLine(Environment.NewLine);
            //PrintBoard();
            //Move("6545 ");
            //Undo("6545 ");
            //Console.WriteLine(Environment.NewLine);
            //PrintBoard();

            //List<string> testKing = GetPossibleMoves();
            //for (int i = 0; i < testKing.Count; i++)
            //{
            //    Console.WriteLine(testKing[i]);
            //}
            //Console.WriteLine(Minimax(DEPTH, "", -100000, 100000, 0));


            //string input = Console.ReadLine();
            //SetBoard(input);
            //SwitchPlayer();
            string move = Engine.Minimax(Engine.DEPTH, "", -100000, 100000, 0);
            Console.WriteLine(Engine.GetMove(move));
            Console.WriteLine(move);
            Engine.Move(move);
            Engine.PrintBoard();


            //int counter = 0;
            //while (true)
            //{
            //    Thread.Sleep(1000);
            //    SwitchPlayer();
            //    if (counter % 2 == 0)
            //    {
            //        Move(Minimax(DEPTH, "", -100000, 100000, 0));
            //        PrintBoard();
            //    }
            //    else
            //    {

            //        Move(Minimax(DEPTH, "", -100000, 100000, 1));
            //        SwitchPlayer();
            //        PrintBoard();
            //        SwitchPlayer();
            //    }
            //    Console.WriteLine(Environment.NewLine);


            //}


            //OrderMoves(testKing);
            //Console.WriteLine(Environment.NewLine);


            //for (int i = 0; i < testKing.Count; i++)
            //{
            //    Console.WriteLine(testKing[i]);
            //}


            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);

        }
    }
}
