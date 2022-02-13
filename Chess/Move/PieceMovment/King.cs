using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class King
    {
        private static int[] KingMoves =
        {
            DirectionValues.North,
            DirectionValues.South,
            DirectionValues.East,
            DirectionValues.West,
            DirectionValues.NorthEast,
            DirectionValues.SouthEast,
            DirectionValues.SouthWest,
            DirectionValues.NothWest
        };
        public static bool IsMovePoseble(Board board, PosebleMoves.Move move)
        {
            if (KingMoves.Contains((move.StartSquare - move.TargetSquare)))
                if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    return true;
            return false;
        }
    }
}