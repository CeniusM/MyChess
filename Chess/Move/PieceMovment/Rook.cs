using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Rook
    {
        public static bool IsMovePossible(Board board, PossibleMoves.Move move)
        {
            int[] directionValues =
            {
                -8, // north
                1, // east
                8, // south
                -1, // west
            };

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

            int startSquareToSide = Directions.DirectionValuesArr[move.StartSquare, Array.IndexOf(directionValues, rookDirection)];
            int targetSquareToSide = Directions.DirectionValuesArr[move.TargetSquare, Array.IndexOf(directionValues, rookDirection)];

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
    }
}