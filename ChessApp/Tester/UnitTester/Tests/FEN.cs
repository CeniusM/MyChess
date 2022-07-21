using MyChess;
using MyChess.ChessBoard;
using MyChess.FEN;

namespace MyChess.UnitTester.Tests
{
    partial class Test
    {
        public static TestReport TestGetBoardFromFEN()
        {
            const int Failed = TestReport.SuccesFlag.Failed;
            int succes = TestReport.SuccesFlag.Succes;

            Board board = MyFEN.GetBoardFromFEN("rn1b1rk1/ppp3p1/3p1p2/1N1Pp1Pp/4P3/3Q1N2/PPP2P1P/R3K2R w KQ h6 0 15");

            if (board.castle != 0b1100)
                succes = Failed;
            if (board.enPassantPiece != 23)
                succes = Failed;
            if (board.playerTurn != Board.WhiteMask)
                succes = Failed;
            if (board.halfMove != 0)
                succes = Failed;
            if (board.fullMove != 15)
                succes = Failed;
            if (board.GameStatus != GameStatusFlag.Running)
                succes = Failed;

            // random pieces
            if (board.Square[0] != Piece.BRook)
                succes = Failed;
            if (board.Square[1] != Piece.BKnight)
                succes = Failed;
            if (board.Square[8] != Piece.BPawn)
                succes = Failed;
            if (board.Square[9] != Piece.BPawn)
                succes = Failed;
            if (board.Square[19] != Piece.BPawn)
                succes = Failed;
            if (board.Square[43] != Piece.WQueen)
                succes = Failed;
            if (board.Square[45] != Piece.WKnight)
                succes = Failed;
            if (board.Square[56] != Piece.WRook)
                succes = Failed;
            if (board.Square[60] != Piece.WKing)
                succes = Failed;
            if (board.Square[63] != Piece.WRook)
                succes = Failed;

            return new("Get Board from FEN Test", succes);
        }
        public static TestReport TestGetFENFromBoard()
        {
            // only works if the get FEN works
            if (TestGetBoardFromFEN().succesStatus != TestReport.SuccesFlag.Succes)
            {
                return new("Get Board from FEN string could not be executed becous couldent get board from FEN",
                            TestReport.SuccesFlag.Undetermined);
            }

            const int Failed = TestReport.SuccesFlag.Failed;
            int succes = TestReport.SuccesFlag.Succes;

            string[] FENs = {
                "rn1b1rk1/ppp3p1/3p1p2/1N1Pp1Pp/4P3/3Q1N2/PPP2P1P/R3K2R w KQ h6 0 15",
                "8/5k2/3p4/1p1Pp2p/pP2Pp1P/P4P1K/8/8 b - - 99 50",
                "4B3/3nk1p1/5r2/3b1p1Q/8/4p3/p1p1pPR1/4K1n1 w - - 0 1"
            };
            
            for (int i = 0; i < FENs.Length; i++)
            {
                Board board = MyFEN.GetBoardFromFEN(FENs[i]);
                if (MyFEN.GetFENFromBoard(board) != FENs[i])
                    succes = Failed;
            }

            return new("Get FEN string from board Test", succes);
        }
    }
}