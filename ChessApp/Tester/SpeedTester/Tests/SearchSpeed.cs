using MyChess.FEN;
using System.Diagnostics;

namespace MyChess.SpeedTester.Tests
{
    partial class Test
    {
        public static void Print(string str) => MyLib.MyConsole.WriteLine(str);

        public static void TimeSearch()
        {
            // Print("Standerd Pos: Initial Position");
            // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 20, 1));
            // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 400, 2));
            // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 8902, 3));
            // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 197281, 4));
            // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4865609, 5));
            // // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 119060324, 6)); // usually closed;
            // // Print(PerftSearchWithTime("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3195901860, 7)); // usually closed;


            Print("");
            Print("Test Casteling: Position 2");
            Print(PerftSearchWithTime("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 48, 1));
            Print(PerftSearchWithTime("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 2039, 2));
            Print(PerftSearchWithTime("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 97862, 3));
            Print(PerftSearchWithTime("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 4085603, 4));
            Print(PerftSearchWithTime("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 193690690, 5));
            // Print(PerftSearchWithTime("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 8031647685, 6)); // usually closed;


            // Print("");
            // Print("Test Pawn Rook Endgame: Position 3");
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 14, 1));
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 191, 2));
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 2812, 3));
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 43238, 4));
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 674624, 5));
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 11030083, 6));
            // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 178633661, 7));
            // // Print(PerftSearchWithTime("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 3009794393, 8)); // usually closed;


            // Print("");
            // Print("Test White side Casteling: Position 4");
            // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 6, 1));
            // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 264, 2));
            // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 9467, 3));
            // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 422333, 4));
            // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 15833292, 5));
            // // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 706045033, 6)); // usually closed;


            // Print("");
            // Print("Test Black side Casteling: Position 4");
            // Print(PerftSearchWithTime("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 1));
            // Print(PerftSearchWithTime("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 264, 2));
            // Print(PerftSearchWithTime("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 9467, 3));
            // Print(PerftSearchWithTime("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 422333, 4));
            // Print(PerftSearchWithTime("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 15833292, 5));
            // // Print(PerftSearchWithTime("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 706045033, 6)); // usually closed;


            // Print("");
            // Print("Perft Position 5 game");
            // Print(PerftSearchWithTime("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 44, 1));
            // Print(PerftSearchWithTime("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 1486, 2));
            // Print(PerftSearchWithTime("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 62379, 3));
            // Print(PerftSearchWithTime("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 2103487, 4));
            // Print(PerftSearchWithTime("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 89941194, 5));


            // Print("");
            // Print("Perft Position 6 game");
            // Print(PerftSearchWithTime("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 46, 1));
            // Print(PerftSearchWithTime("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 2079, 2));
            // Print(PerftSearchWithTime("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 89890, 3));
            // Print(PerftSearchWithTime("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 3894594, 4));
            // Print(PerftSearchWithTime("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 164075551, 5));
        }

        public static string PerftSearchWithTime(string FEN, long ExpectedValue, int Depth)
        {
            Stopwatch st = new Stopwatch();
            ChessGame chessGame = new ChessGame(FEN);

            st.Start();

            long moveCount = 0;
            SearchMove(Depth);
            void SearchMove(int depth)
            {
                chessGame.possibleMoves.GenerateMoves();
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);

                for (int i = 0; i < Count; i++)
                {
                    if (depth == 1)
                    {
                        moveCount++;
                        continue;
                    }
                    chessGame.board.MakeMove(moves[i]);
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }

            st.Stop();

            long time = st.ElapsedMilliseconds;

            return "Time: " + time + "ms" + ", Depth: " + Depth + ", MoveCount: " + moveCount + (moveCount == ExpectedValue ? "" : (", Expected: " + ExpectedValue + "!"));
        }
    }
}


