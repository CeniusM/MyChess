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
            bool IsMovePossible = true; // note, 3 - 31 should not work
            int queenDirection;

            if (((move.StartSquare - move.TargetSquare) % 8) == 0)
                queenDirection = 8;
            else if (((move.StartSquare >> 3) == move.TargetSquare >> 3))
                queenDirection = 1;
            else if (((move.StartSquare - move.TargetSquare) % 9) == 0)
                queenDirection = 9;
            else if (((move.StartSquare - move.TargetSquare) % 7) == 0)
                queenDirection = 7;
            else
                return false;

            if ((move.StartSquare - move.TargetSquare) > 0)
                queenDirection *= -1;

            int startSquareToSide = V2.Directions.DirectionValues[move.StartSquare, Array.IndexOf(directionValues, queenDirection)];
            int targetSquareToSide = V2.Directions.DirectionValues[move.TargetSquare, Array.IndexOf(directionValues, queenDirection)];

            if (startSquareToSide <= targetSquareToSide)
                return false;

            for (int i = 1; i < (startSquareToSide - targetSquareToSide); i++)
            {
                if (Board.IsPiecesSameColor(board.board[move.StartSquare], board.board[move.StartSquare + (queenDirection * i)]))
                {
                    IsMovePossible = false;
                    break;
                }
                else if (Board.IsPieceOpposite(board.board[move.StartSquare], board.board[move.StartSquare + (queenDirection * i)]))
                {
                    if (move.StartSquare + (queenDirection * i) == move.TargetSquare)
                        IsMovePossible = true;
                    else
                        IsMovePossible = false;
                    break;
                }
            }

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