using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Queen
    {
        private static int[] directionValues =
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
            bool IsMovePossible = true;

            return IsMovePossible;
        }

        public static List<PossibleMoves.Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<PossibleMoves.Move> posssibleMoves = new List<PossibleMoves.Move>();

            return posssibleMoves;
        }
    }
}