using ChessV1;
using System.Diagnostics;

namespace PerftTester
{
    public class PerftTest
    {
        public static void Start()
        {
            // warmup 
            UnsafeBoard foo1 = new(); PossibleMovesGeneratorV2 foo2 = new(foo1); Perft(foo1, foo2, 4);




            UnsafeBoard ub = new UnsafeBoard();
            PossibleMovesGeneratorV2 pmg = new PossibleMovesGeneratorV2(ub);


            Stopwatch sw = new Stopwatch();
            sw.Start();
            MyLib.DebugConsole.WriteLine(Perft(ub, pmg, 5) + "");
            sw.Stop();
            MyLib.DebugConsole.WriteLine(sw.Elapsed.TotalSeconds + "s");




            pmg.GenerateMoves();
            Move[] moves = pmg.GetMoves();
            int MoveCount = moves.Count();
            for (int i = 0; i < MoveCount; i++)
            {
                ub.MakeMove(moves[i]);
                Console.WriteLine(moves[i].ToString() + ", Combinations: " + Perft(ub, pmg, 3));
                ub.UnMakeMove();
            }
        }

        public static long Perft(UnsafeBoard board, PossibleMovesGeneratorV2 pmg, int Depth)
        {
            long count = 0;
            PerftMove(Depth);

            void PerftMove(int depth)
            {
                if (depth == 0)
                {
                    count++;
                    return;
                }
                pmg.GenerateMoves();
                Move[] moves = pmg.GetMoves();
                int MoveCount = moves.Count();
                // if (depth == 1)
                // {
                //     count += MoveCount;
                //     return;
                // }
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