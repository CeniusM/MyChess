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

            if (KnightMoves.Contains((move.StartSquare - move.TargetSquare)))
                if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    IsMovePoseble = true;

            return IsMovePoseble;
        }
    }
}