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
        
        public static bool IsMovePossible(Board board, PossibleMoves.Move move)
        {
            if (KingMoves.Contains((move.StartSquare - move.TargetSquare)))
                if (Board.IsPieceOppositeOrNone(board.board[move.StartSquare], board.board[move.TargetSquare]))
                    return true;
            return false;
        }

        public static List<PossibleMoves.Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<PossibleMoves.Move> posssibleMoves = new List<PossibleMoves.Move>();

            return posssibleMoves;
        }
    }
}