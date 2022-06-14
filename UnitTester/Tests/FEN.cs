using MyChess;
using MyChess.ChessBoard;

namespace Foo.Tests
{
    partial class Test
    {
        public static TestReport TestGetFEN()
        {
            const int Failed = TestReport.SuccesFlag.Failed;
            int succes = TestReport.SuccesFlag.Undetermined;

            ChessGame game = new("rn1b1rk1/ppp3p1/3p1p2/1N1Pp1Pp/4P3/3Q1N2/PPP2P1P/R3K2R w KQ h6 0 15");


            if (game.board.castle != 0b1100)
                succes = Failed;
            if (game.board.enPassantPiece != 23)
                succes = Failed;
            if (game.board.playerTurn == 0b01)
                succes = Failed;
            if (game.board.halfMove != 0)
                succes = Failed;
            if (game.board.fullMove != 15)
                succes = Failed;
            if (game.board.GameStatus != GameStatusFlag.Running)
                succes = Failed;

            // random pieces
            if (game.board[0] != Piece.BRook)
                succes = Failed;
            if (game.board[1] != Piece.BKnight)
                succes = Failed;

            if (game.board[8] != Piece.BPawn)
                succes = Failed;
            if (game.board[9] != Piece.BPawn)
                succes = Failed;
            if (game.board[1] != Piece.BKnight)
                succes = Failed;

            if (game.board[43] != Piece.WQueen)
                succes = Failed;
            if (game.board[45] != Piece.WKnight)
                succes = Failed;
            if (game.board[56] != Piece.WRook)
                succes = Failed;
            if (game.board[60] != Piece.WKing)
                succes = Failed;
            if (game.board[63] != Piece.WRook)
                succes = Failed;


            return new("King Test", succes);
        }
    }
}