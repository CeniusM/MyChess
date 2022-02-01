using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Knight
    {
        private static int[] KnightMoves =
        {
            -6,
            -10,
            -15,
            -17,
            6,
            10,
            15,
            17
        };
        public static bool IsMovePoseble(Board board, PosebleMoves.Move move)
        {
            bool IsMovePoseble = false;
            if (Board.IsPieceWhite(board.board[move.StartSquare]))
            {
                if (KnightMoves.Contains((move.StartSquare - move.TargetSquare)))
                    if (!Board.IsPieceWhite(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
            }
            else
            {
                if (KnightMoves.Contains((move.StartSquare - move.TargetSquare)))
                    if (!Board.IsPieceBlack(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
            }
            return IsMovePoseble;
        }
    }
}