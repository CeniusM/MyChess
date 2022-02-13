using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Rook
    {
        public static bool IsMovePoseble(Board board, PosebleMoves.Move move)
        {
            int[] directionValues =
            {
                -8, // north
                8, // south
                1, // east
                -1, // west
            };

            bool IsMovePoseble = false;

            for (int i = 0; i < 4; i++)
            {
                
            }

            return IsMovePoseble;
        }
    }
}