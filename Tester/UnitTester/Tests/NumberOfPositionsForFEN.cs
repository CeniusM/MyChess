using MyChess;
using MyChess.ChessBoard;
using MyChess.FEN;

// https://en.wikipedia.org/wiki/Shannon_number
// https://www.chessprogramming.org/Perft_Results

// can both be used for speed and testing of the system

namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        public static void PerftResults()
        {
            // CS_MyConsole.MyConsole.WriteLine("Standerd Pos: Initial Position");
            // long[] foo1 = { 20, 400, 8902, 197281, 4865609, 119060324, 3195901860 };
            // CS_MyConsole.MyConsole.WriteLine(FullSearchPrint("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", foo1, 5));

            // CS_MyConsole.MyConsole.WriteLine("Test Casteling: Position 2");
            // long[] foo2 = { 48, 2039, 97862, 4085603, 193690690, 8031647685 };
            // CS_MyConsole.MyConsole.WriteLine(FullSearchPrint("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", foo2, 5));


            // CS_MyConsole.MyConsole.WriteLine("Test Pawn Rook Endgame: Position 3");
            // long[] foo3 = { 14, 191, 2812, 43238, 674624, 11030083, 178633661, 3009794393 };
            // CS_MyConsole.MyConsole.WriteLine(FullSearchPrint("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", foo3, 6));

            CS_MyConsole.MyConsole.WriteLine("Standerd Pos: Initial Position");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 20, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 400, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 8902, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 197281, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4865609, 5));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 119060324, 6));
            /*CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3195901860, 7));*/


            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Test Casteling: Position 2");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 48, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 2039, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 97862, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 4085603, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 193690690, 5));
            /*CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0", 8031647685, 6));*/


            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Test Pawn Rook Endgame: Position 3");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 14, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 191, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 2812, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 43238, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 674624, 5));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 11030083, 6));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 178633661, 7));
            /*CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 3009794393, 8));*/


            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Test White side Casteling: Position 4");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 6, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 264, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 9467, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 422333, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 15833292, 5));
            /*CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 706045033, 6));*/


            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Test Black side Casteling: Position 4");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 264, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 9467, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 422333, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 15833292, 5));
            /*CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 706045033, 6));*/


            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Perft Position 5 game");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 44, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 1486, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 62379, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 2103487, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 89941194, 5));


            CS_MyConsole.MyConsole.WriteLine("");
            CS_MyConsole.MyConsole.WriteLine("Perft Position 6 game");
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 46, 1));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 2079, 2));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 89890, 3));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 3894594, 4));
            CS_MyConsole.MyConsole.WriteLine(PerftResultSearch(false, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 164075551, 5));
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
                if (depth == 0)
                    return;

                chessGame.possibleMoves.GenerateMoves();
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);

                for (int i = 0; i < Count; i++)
                {
                    chessGame.board.MakeMove(moves[i]);
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
                        if (chessGame.possibleMoves.CheckKingInCheck(chessGame.possibleMoves.GetKingsPos(chessGame.board.playerTurn ^ Board.ColorMask), chessGame.board.playerTurn ^ Board.ColorMask))
                            check++;
                    }
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
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);

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
                    if (chessGame.possibleMoves.CheckKingInCheck(chessGame.possibleMoves.GetKingsPos(chessGame.board.playerTurn ^ Board.ColorMask), chessGame.board.playerTurn ^ Board.ColorMask))
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
    }
}











// if (depth == 1)
// {
//     MoveNum++;
//     Move theMove = chessGame.board.moves.Peek();
//     CS_MyConsole.MyConsole.WriteLine(MoveNum + ": " + moves.Length + 
//     ", S: " + Board.IntToLetterNum(theMove.StartSquare)
//     + ", T: " + Board.IntToLetterNum(theMove.TargetSquare)
//     + ", F: " + theMove.MoveFlag
//     + ", C: " + theMove.CapturedPiece);
//     CS_MyConsole.MyConsole.WriteLine("");
//     for (int i = 0; i < moves.Length; i++)
//     {
//         CS_MyConsole.MyConsole.Write("(" + Board.IntToLetterNum(moves[i].StartSquare) + 
//         " " + Board.IntToLetterNum(moves[i].TargetSquare) + ")");
//     }
//     CS_MyConsole.MyConsole.WriteLine("");
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

            //List<Move> moves = chessGame.GetPossibleMoves();



            long moveCount = 0;

            SearchMove(SearchDepth);

            void SearchMove(int depth)
            {
                if (depth == 0)
                    return;

                chessGame.possibleMoves.GenerateMoves();
                List<Move> movesRef = chessGame.GetPossibleMoves();
                int Count = movesRef.Count();
                Move[] moves = new Move[Count];
                movesRef.CopyTo(moves);

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