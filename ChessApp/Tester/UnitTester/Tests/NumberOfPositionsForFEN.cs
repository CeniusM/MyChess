using MyChess;
using MyChess.ChessBoard;
using MyChess.FEN;
using System.Diagnostics;
using MyChess.PossibleMoves;

// https://en.wikipedia.org/wiki/Shannon_number
// https://www.chessprogramming.org/Perft_Results

// can both be used for speed and testing of the system


namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        private static void Print(string str) => MyLib.MyConsole.WriteLine(str);
        public static void PerftResults()
        {




            MyLib.MyConsole.WriteLine(PerftSearchWithAVGMoves(new("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"), 4865609, 5));
            MyLib.MyConsole.WriteLine(PerftSearchWithAVGMoves(new("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0"), 193690690, 5));
            MyLib.MyConsole.WriteLine(PerftSearchWithAVGMoves(new("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1"), 15833292, 5));





            // long time = 0;
            // Stopwatch sw = new Stopwatch();
            // sw.Start();
            // long combinations = GetPerftResultAtDepth(new ChessGame(), 6);
            // time = sw.ElapsedMilliseconds;
            // Print("Combinations: " + combinations + ". time Ms: " + time);







            // ChessGame chessGame = new ChessGame("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0");


            // chessGame.possibleMoves.GenerateMoves();
            // MoveList movesRef = chessGame.GetPossibleMoves();
            // int Count = movesRef.Count();
            // Move[] moves = new Move[Count];
            // movesRef.CopyTo(moves);

            // for (int i = 0; i < Count; i++)
            // {
            //     long combinations = GetPerftResultAtDepth(chessGame, 5);
            //     chessGame.board.MakeMove(moves[i]);
            //     Print(Board.MoveToStr(moves[i]) + ": " + combinations
            //     );
            //     chessGame.board.UnMakeMove();
            //     if (combinations == 0)
            //     {

            //     }
            //     else if (combinations == 97862)
            //     {

            //     }
            //     else
            //     {

            //     }
            // }

            // Print("Done\n");

            // int fooo = 0;





            // MyLib.MyConsole.WriteLine("Standerd Pos: Initial Position");
            // long[] foo1 = { 20, 400, 8902, 197281, 4865609, 119060324, 3195901860 };
            // MyLib.MyConsole.WriteLine(FullSearchPrint("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", foo1, 5));

            // MyLib.MyConsole.WriteLine("Test Casteling: Position 2");
            // long[] foo2 = { 48, 2039, 97862, 4085603, 193690690, 8031647685 };
            // MyLib.MyConsole.WriteLine(FullSearchPrint("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", foo2, 5));


            // MyLib.MyConsole.WriteLine("Test Pawn Rook Endgame: Position 3");
            // long[] foo3 = { 14, 191, 2812, 43238, 674624, 11030083, 178633661, 3009794393 };
            // MyLib.MyConsole.WriteLine(FullSearchPrint("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", foo3, 6));

            // MyLib.MyConsole.WriteLine("Standerd Pos: Initial Position");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 20, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 400, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 8902, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 197281, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4865609, 5));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 119060324, 6));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(true, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3195901860, 7));


            // MyLib.MyConsole.WriteLine("");
            // MyLib.MyConsole.WriteLine("Test Casteling: Position 2");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 48, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 2039, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 97862, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 4085603, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 193690690, 5));
            // /*MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 8031647685, 6));*/


            // MyLib.MyConsole.WriteLine("");
            // MyLib.MyConsole.WriteLine("Test Pawn Rook Endgame: Position 3");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 14, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 191, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 2812, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 43238, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 674624, 5));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 11030083, 6));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 178633661, 7));
            // /*MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 3009794393, 8));*/


            // MyLib.MyConsole.WriteLine("");
            // MyLib.MyConsole.WriteLine("Test White side Casteling: Position 4");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 6, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 264, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 9467, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 422333, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 15833292, 5));
            // /*MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 706045033, 6));*/


            // MyLib.MyConsole.WriteLine("");
            // MyLib.MyConsole.WriteLine("Test Black side Casteling: Position 4");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 264, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 9467, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 422333, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 15833292, 5));
            // /*MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 706045033, 6));*/


            // MyLib.MyConsole.WriteLine("");
            // MyLib.MyConsole.WriteLine("Perft Position 5 game");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 44, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 1486, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 62379, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 2103487, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 89941194, 5));


            // MyLib.MyConsole.WriteLine("");
            // MyLib.MyConsole.WriteLine("Perft Position 6 game");
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 46, 1));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 2079, 2));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 89890, 3));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 3894594, 4));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 164075551, 5));





            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/8/6k1/8/8/2K5/8/8 w - - 0 1", 0, 8));
            // MyLib.MyConsole.WriteLine(PerftResultSearch(false, "8/8/6k1/8/8/2K5/8/8 w - - 0 1", 54948733, 9));


        }

        private static string PerftResultSearch(bool PrintStats, string FEN, long ExpectedValue, int Depth)
        {
            ChessGame chessGame = new ChessGame(FEN);
            int searchDepth = Depth;

            long moveCount = 0;


            long captures = 0;
            long enPassent = 0;
            long castle = 0;
            long Promotions = 0;
            long check = 0;

            SearchMove(searchDepth);

            void SearchMove(int depth)
            {
                // if (depth == 0)
                //     return;

                chessGame.possibleMoves.GenerateMoves();
                MoveList movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count;
                Move[] moves = movesRef.MoveArr;

                for (int i = 0; i < Count; i++)
                {
                    if (depth == 1)
                    {
                        moveCount++;
                        if (moves[i].CapturedPiece != 0)
                            captures++;
                        if (moves[i].MoveFlag == Move.Flag.EnPassantCapture)
                            enPassent++;
                        if (moves[i].MoveFlag == Move.Flag.Castling)
                            castle++;
                        if (moves[i].MoveFlag > 3)
                            Promotions++;
                        if (chessGame.possibleMoves.IsSquareAttacked(chessGame.possibleMoves.GetKingsPos(chessGame.board.playerTurn ^ Board.ColorMask), chessGame.board.playerTurn ^ Board.ColorMask))
                            check++;
                        continue;
                    }
                    chessGame.board.MakeMove(moves[i]);
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }

            return Depth + ": " + moveCount + (moveCount == ExpectedValue ? "" : (" Failed! Expected: " + ExpectedValue + "")) + (PrintStats ?
            "\n" + "Captures: " + captures + ", enPassent: " + enPassent + ", castle: " + castle + ", Promotions: " + Promotions + ", check: " + check
            + "\n"
             : "")
             ;
        }

        private static string FullSearchPrint(string FEN, long[] ExpectedValue, int Depth, bool printIfFailed = true)
        {
            ChessGame chessGame = new ChessGame(FEN);
            int searchDepth = Depth;

            long[] moveCount = new long[Depth + 1];


            long[] captures = new long[Depth + 1];
            long[] enPassent = new long[Depth + 1];
            long[] castle = new long[Depth + 1];
            long[] Promotions = new long[Depth + 1];
            long[] check = new long[Depth + 1];

            SearchMove(searchDepth);

            void SearchMove(int depth)
            {
                if (depth == 0)
                    return;

                chessGame.possibleMoves.GenerateMoves();
                MoveList movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count;
                Move[] moves = movesRef.MoveArr;

                for (int i = 0; i < Count; i++)
                {
                    chessGame.board.MakeMove(moves[i]);
                    moveCount[depth]++;
                    if (moves[i].CapturedPiece != 0)
                        captures[depth]++;
                    if (moves[i].MoveFlag == Move.Flag.EnPassantCapture)
                        enPassent[depth]++;
                    if (moves[i].MoveFlag == Move.Flag.Castling)
                        castle[depth]++;
                    if (moves[i].MoveFlag > 3)
                        Promotions[depth]++;
                    if (chessGame.possibleMoves.IsSquareAttacked(chessGame.possibleMoves.GetKingsPos(chessGame.board.playerTurn ^ Board.ColorMask), chessGame.board.playerTurn ^ Board.ColorMask))
                        check[depth]++;
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }

            string Report = FEN + "\n";
            Report += "+-------+-----------------+--------------+--------------+---------------+----------------+" + "\n";
            Report += "| Depth |      Nodes      |   Captures   |     E.P      |    Castles    |     Checks     |" + "\n";
            Report += "+-------+-----------------+--------------+--------------+---------------+----------------+" + "\n";


            int depthNum = 1;
            for (int i = Depth; i >= 1; i--)
            {
                try
                {

                    Report += String.Format("|{0,7}|{1,17:N0}|{2,14:N0}|{3,14:N0}|{4,15:N0}|{5,16:N0}|", depthNum, moveCount[i], captures[i], enPassent[i], castle[i], check[i]) +
                    (moveCount[i] == ExpectedValue[depthNum - 1] ? "" : "Failed! Expected: " + ExpectedValue[depthNum - 1]) +
                    "\n";
                    Report += "+-------+-----------------+--------------+--------------+---------------+----------------+" + "\n";
                    depthNum++;
                }
                catch
                {
                    Report += "Might Have Missing Data" + "\n";
                    break;
                }
            }

            return Report;
        }

        public static long GetPerftResultAtDepth(string FEN, int Depth)
        => GetPerftResultAtDepth(new ChessGame(FEN), Depth);
        public static long GetPerftResultAtDepth(ChessGame chessGame, int Depth)
        {
            long moveCount = 0;

            SearchMove(Depth);

            void SearchMove(int depth)
            {
                // if (depth == 0)
                //     return;

                chessGame.possibleMoves.GenerateMoves();
                MoveList movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count;
                Move[] moves = movesRef.MoveArr;

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

            return moveCount;
        }

        // public static void SearchForMistakes()
        // {

        // }

        public static long PerftSearch(ChessGame chessGame, int Depth)
        {
            long moveCount = 0;

            SearchMove(Depth);

            void SearchMove(int depth)
            {
                chessGame.possibleMoves.GenerateMoves();
                MoveList movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count;
                Move[] moves = movesRef.MoveArr;

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

            return moveCount;
        }

        public static string PerftSearchWithAVGMoves(ChessGame chessGame, long ExpectedValue, int Depth)
        {
            long moveCount = 0;

            ulong MovesAVG = 0;
            int MovesRecorded = 0;
            int MaxMoves = 0;

            SearchMove(Depth);

            MovesAVG = MovesAVG / (ulong)MovesRecorded;

            void SearchMove(int depth)
            {
                chessGame.possibleMoves.GenerateMoves();
                MoveList movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count;
                Move[] moves = movesRef.MoveArr;

                MovesAVG += (ulong)Count;
                MovesRecorded++;
                if (Count > MaxMoves)
                    MaxMoves = Count;



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

            string str = "AVGMoves: " + MovesAVG + ", MaxValue: " + MaxMoves + ", MoveCount: " + moveCount + (moveCount == ExpectedValue ? "" : (", Expected: " + ExpectedValue + "!"));

            return str;
        }
    }
}











// if (depth == 1)
// {
//     MoveNum++;
//     Move theMove = chessGame.board.moves.Peek();
//     MyLib.MyConsole.WriteLine(MoveNum + ": " + moves.Length + 
//     ", S: " + Board.IntToLetterNum(theMove.StartSquare)
//     + ", T: " + Board.IntToLetterNum(theMove.TargetSquare)
//     + ", F: " + theMove.MoveFlag
//     + ", C: " + theMove.CapturedPiece);
//     MyLib.MyConsole.WriteLine("");
//     for (int i = 0; i < moves.Length; i++)
//     {
//         MyLib.FileWriter.Write("(" + Board.IntToLetterNum(moves[i].StartSquare) + 
//         " " + Board.IntToLetterNum(moves[i].TargetSquare) + ")");
//     }
//     MyLib.MyConsole.WriteLine("");
// }











/*


        private static TestReport NumberOfPositionsAfter5plies(string FEN, long ExpectedValue, int Depth)
            => NumberOfPositionsAfter5plies(new ChessGame(FEN), ExpectedValue, Depth);

        private static TestReport NumberOfPositionsAfter5plies(ChessGame chessGame, long ExpectedValue, int Depth)
        {
            // debuging
            // int MoveNum = 0;




            int SearchDepth = Depth;

            // int moveCount = 0;




            // int[] moveCount = new int[Depth + 1];

            //MoveList moves = chessGame.GetPossibleMoves();



            long moveCount = 0;

            SearchMove(SearchDepth);

            void SearchMove(int depth)
            {
                if (depth == 0)
                    return;

                chessGame.possibleMoves.GenerateMoves();
                MoveList movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count;
                Move[] moves = movesRef.MoveArr;

                for (int i = 0; i < Count; i++)
                {
                    chessGame.board.MakeMove(moves[i]);
                    // moveCount[depth]++;
                    if (depth == 1)
                        moveCount++;
                    SearchMove(depth - 1);
                    chessGame.board.UnMakeMove();
                }
            }
            return new(
                moveCount //[1]
            +" amount of combinations after " + SearchDepth + " moves" + ", Expected: " + ExpectedValue + "\n"
            + MyFEN.GetFENFromBoard(chessGame.board),
            moveCount[1] == ExpectedValue ? TestReport.SuccesFlag.Succes : TestReport.SuccesFlag.Failed); // succes flag
        }
*/