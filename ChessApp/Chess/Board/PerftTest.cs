using ChessV1;
using System.Diagnostics;

namespace PerftTester
{
    public class PerftTest
    {
        public static void Start()
        {
            // warmup 
            UnsafeBoard foo1 = new(); PossibleMovesGenerator foo2 = new(foo1); Perft(foo1, foo2, 4);




            UnsafeBoard ub = new UnsafeBoard();
            PossibleMovesGenerator pmg = new PossibleMovesGenerator(ub);


            Stopwatch sw = new Stopwatch();
            sw.Start();
            MyLib.DebugConsole.WriteLine(Perft(ub, pmg, 4) + "");
            sw.Stop();
            MyLib.DebugConsole.WriteLine(sw.Elapsed.TotalSeconds + "s");
        }

        public static long Perft(UnsafeBoard board, PossibleMovesGenerator pmg, int Depth)
        {
            long count = 0;
            PerftMove(Depth);

            void PerftMove(int depth)
            {
                pmg.GenerateMoves();
                Move[] moves = pmg.GetMoves();
                int MoveCount = moves.Count();
                if (depth == 1)
                {
                    count += MoveCount;
                    return;
                }
                for (int i = 0; i < MoveCount; i++)
                {
                    board.MakeMove(moves[i]);
                    PerftMove(depth - 1);
                    board.UnMakeMove();
                }
            }

            return count;
        }
    }
}