using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

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

            string[] testCasesBlack =
            {
                "r*b***k*******p*P*n*pr*pq*p******b*P******N**N**PP*QBPPPR***K**R",
                "r*b*qrk**ppn*pb*p**p*npp***Pp*****P*P**B**N*****PP*NBPPPR**Q*RK*",
                "r*bq*rk*p****ppp*pnp*n****p*******PPpP***NP*P***P***B*PPR*BQ*RK*"
            };

            string[] testCasesWhite =
            {
                "**nq*nk******p*p****p*pQpb*pP*NP*p*P**P**P****N*P****PB*******K*",
                "***********r**p*pp*Bp*p**kP******n**K*********R**P***P**********",
                "************kb*p**p***pP*pP*P*P**P***K***B**********************",
                "b*R**nk******ppp*p***n*******N***b**p****P**BP**q***BQPP******K*",
                "***rr*k*pp***pbp**bp*np*q***p*B***B*P*****N****PPPPQ*PP****RR*K*",
                "**R*r********k**pBP*n**p******p**************P*P**P***P********K",
                "**r**rk**p*R*pp*p***p**p************B******QB*P*q*P***KP********"
            };

            for (int i = 0; i < 3; i++)
            {

                Engine.SetBoard(testCasesBlack[i]);
                Engine.SwitchPlayer();
                string move = Engine.Minimax(Engine.DEPTH, "", -100000, 100000, 0);
                Console.WriteLine(Engine.GetMove(move));
                Evaluator.hashSet.Clear();
            }
            for (int i = 0; i < 7; i++)
            {

                Engine.SetBoard(testCasesWhite[i]);
                string move = Engine.Minimax(Engine.DEPTH, "", -100000, 100000, 0);
                Console.WriteLine(Engine.GetMove(move));
                Evaluator.hashSet.Clear();

            }



            //string input = Console.ReadLine();
            //Engine.SetBoard(input);
            //Engine.SwitchPlayer();
            //string move = Engine.Minimax(Engine.DEPTH, "", -100000, 100000, 0);
            //Console.WriteLine(Engine.GetMove(move));
            //Console.WriteLine(move);
            //Console.WriteLine("Moves searched: " + Engine.numberOfMoves);
            //Engine.Move(move);
            //Engine.PrintBoard();
            //Console.WriteLine(Evaluator.hashSet.Count);







            //int counter = 0;
            //while (true)
            //{
            //    Thread.Sleep(1000);
            //    Engine.SwitchPlayer();
            //    if (counter % 2 == 0)
            //    {
            //        string move = Engine.Minimax(Engine.DEPTH, "", -100000, 100000, 0);
            //        if (move.Contains('k'))
            //        {
            //            Console.WriteLine("Game over!");
            //            break;
            //        }
            //        Engine.Move(move);
            //        Engine.PrintBoard();
            //    }
            //    else
            //    {

            //        string move = Engine.Minimax(Engine.DEPTH, "", -100000, 100000, 1);
            //        if (move.Contains('k'))
            //        {
            //            Console.WriteLine("Game over!");
            //            break;
            //        }
            //        Engine.Move(move);
            //        Engine.SwitchPlayer();
            //        Engine.PrintBoard();
            //        Engine.SwitchPlayer();
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
