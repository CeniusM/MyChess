using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Rook
    {
        private static int[] directionValues =
        {
            -8, // north
            1, // east
            8, // south
            -1, // west
        };

        public static bool IsMovePossible(Board board, Move move)
        {
            bool IsMovePossible = true;

            int rookDirection;

            if (((move.StartSquare - move.TargetSquare) % 8) == 0)
                rookDirection = 8;
            else if (((move.StartSquare >> 3) == move.TargetSquare >> 3))
                rookDirection = 1;
            else
                return false;
            if ((move.StartSquare - move.TargetSquare) > 0)
                rookDirection *= -1;

            int startSquareToSide = Directions.DirectionValues[move.StartSquare, Array.IndexOf(directionValues, rookDirection)];
            int targetSquareToSide = Directions.DirectionValues[move.TargetSquare, Array.IndexOf(directionValues, rookDirection)];

            for (int i = 1; i < (startSquareToSide - targetSquareToSide); i++)
            {
                if (Board.IsPiecesSameColor(board.board[move.StartSquare], board.board[move.StartSquare + (rookDirection * i)]))
                {
                    IsMovePossible = false;
                    break;
                }
                else if (Board.IsPieceOpposite(board.board[move.StartSquare], board.board[move.StartSquare + (rookDirection * i)]))
                {
                    if (move.StartSquare + (rookDirection * i) == move.TargetSquare)
                        IsMovePossible = true;
                    else
                        IsMovePossible = false;
                    break;
                }
            }

            return IsMovePossible;
        }

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            return posssibleMoves;
        }
    }
}