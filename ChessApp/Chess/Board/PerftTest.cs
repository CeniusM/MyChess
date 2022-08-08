using ChessV2;
using System.Diagnostics;

namespace PerftTester
{
    public class PerftTest
    {
        public static void Start(string fen = "1k6/p4ppp/1p3p2/8/8/5P2/PPPPP2P/5K2 w - - 0 1", int depth = 5)
        {
            // warmup 
            // UnsafeBoard foo1 = new(); PossibleMovesGenerator foo2 = new(foo1); Perft(foo1, foo2, 4);




            // UnsafeBoard ub = new UnsafeBoard("4k3/ppp2ppp/8/8/8/8/PPP2PPP/4K3 w - - 0 1"); // 971165 at perft 5
            // UnsafeBoard ub = new UnsafeBoard("3k4/p3pp1p/8/8/8/8/P1PP3P/4K3 w - - 0 1"); // pawns and king, 253744 at perft 5
            UnsafeBoard ub = new UnsafeBoard(fen); //
            PossibleMovesGenerator pmg = new PossibleMovesGenerator(ub);


            Stopwatch sw = new Stopwatch();
            sw.Start();
            MyLib.DebugConsole.WriteLine(Perft(ub, pmg, depth) + "");
            sw.Stop();
            MyLib.DebugConsole.WriteLine(sw.Elapsed.TotalSeconds + "s");




            pmg.GenerateMoves();
            Move[] moves = pmg.GetMoves();
            int MoveCount = moves.Count();
            Console.WriteLine("FirstMoves: " + MoveCount);
            for (int i = 0; i < MoveCount; i++)
            {
                ub.MakeMove(moves[i]);
                Console.WriteLine(moves[i].ToString() + ", Combinations: " + Perft(ub, pmg, depth - 1));
                ub.UnMakeMove();
            }
        }

        public static long Perft(UnsafeBoard board, PossibleMovesGenerator pmg, int Depth)
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