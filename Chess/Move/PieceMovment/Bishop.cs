using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Bishop
    {
        private static int[] directionValues =
        {
            DirectionOffSets.NorthEast,
            DirectionOffSets.SouthEast,
            DirectionOffSets.SouthWest,
            DirectionOffSets.NothWest
        };

        public static bool IsMovePossible(Board board, Move move)
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

                int startSquareToSide = Directions.DirectionValues[move.StartSquare, Array.IndexOf(directionValues, bishopDirection)];
                int targetSquareToSide = Directions.DirectionValues[move.TargetSquare, Array.IndexOf(directionValues, bishopDirection)];
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

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            // clean :D
            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.Bishop + playerTurn))
                    continue;

                for (int direction = 0; direction < directionValues.Length; direction++)
                {
                    for (int toEdge = 1; toEdge < (Directions.DirectionValues[square, direction + 4]) + 1; toEdge++)
                    {
                        if (board.board[square + (directionValues[direction] * toEdge)] == 0)
                        {
                            posssibleMoves.Add(new Move(square, square + (directionValues[direction] * toEdge)));
                        }
                        else if (Board.IsPieceOpposite(board.board[square + (directionValues[direction] * toEdge)], playerTurn))
                        {
                            posssibleMoves.Add(new Move(square, square + (directionValues[direction] * toEdge)));
                            break;
                        }
                        else
                            break;
                    }
                }
            }


            return posssibleMoves;
        }
    }
}