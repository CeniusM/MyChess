using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Bishop
    {
        private static int[] directionValues =
        {
            DirectionValues.NorthEast,
            DirectionValues.SouthEast,
            DirectionValues.SouthWest,
            DirectionValues.NothWest
        };

        public static bool IsMovePossible(Board board, PossibleMoves.Move move)
        {
            int start = move.StartSquare;
            int target = move.TargetSquare;
            int diff = start - target;
            int xDiff = Math.Abs(start % 8 - target % 8);
            int yDiff = Math.Abs(start / 8 - target / 8);
            bool isDiagonal = xDiff == yDiff;

            if (isDiagonal)
            {
                int bishopDirection;
                if (((move.StartSquare - move.TargetSquare) % 9) == 0)
                    bishopDirection = 9;
                else if (((move.StartSquare - move.TargetSquare) % 7) == 0)
                    bishopDirection = 7;
                else
                    return false;
                if ((move.StartSquare - move.TargetSquare) > 0)
                    bishopDirection *= -1;

                int startSquareToSide = V2.Directions.DirectionValues[move.StartSquare, Array.IndexOf(directionValues, bishopDirection)];
                int targetSquareToSide = V2.Directions.DirectionValues[move.TargetSquare, Array.IndexOf(directionValues, bishopDirection)];
                if (startSquareToSide <= targetSquareToSide)
                    return false;

                for (int i = 1; i < (startSquareToSide - targetSquareToSide); i++)
                {
                    if (Board.IsPiecesSameColor(board.board[move.StartSquare], board.board[move.StartSquare + (bishopDirection * i)]))
                        return false;
                    else if (Board.IsPieceOpposite(board.board[move.StartSquare], board.board[move.StartSquare + (bishopDirection * i)]))
                        if (!(move.StartSquare + (bishopDirection * i) == move.TargetSquare))
                            return false;
                }
                return true;
            }
            else
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