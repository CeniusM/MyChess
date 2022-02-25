using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Queen
    {
        private static int[] directionValues =
        {
            DirectionOffSets.North,
            DirectionOffSets.South,
            DirectionOffSets.East,
            DirectionOffSets.West,
            DirectionOffSets.NorthEast,
            DirectionOffSets.SouthEast,
            DirectionOffSets.SouthWest,
            DirectionOffSets.NothWest
        };

        public static bool IsMovePossible(Board board, Move move)
        {
            bool IsMovePossible = true; // note, 3 - 31 should not work
            int queenDirection;

            if (((move.StartSquare - move.TargetSquare) % 8) == 0)
                queenDirection = 8;
            else if (((move.StartSquare >> 3) == move.TargetSquare >> 3))
                queenDirection = 1;
            else
            {
                int start = move.StartSquare;
                int target = move.TargetSquare;
                int diff = start - target;
                int xDiff = Math.Abs(start % 8 - target % 8);
                int yDiff = Math.Abs(start / 8 - target / 8);
                bool isDiagonal = xDiff == yDiff;
                if (isDiagonal)
                {
                    if (((move.StartSquare - move.TargetSquare) % 9) == 0)
                        queenDirection = 9;
                    else if (((move.StartSquare - move.TargetSquare) % 7) == 0)
                        queenDirection = 7;
                    else
                        return false;
                }
                else
                    return false;
            }

            if ((move.StartSquare - move.TargetSquare) > 0)
                queenDirection *= -1;

            int startSquareToSide = Directions.DirectionValues[move.StartSquare, Array.IndexOf(directionValues, queenDirection)];
            int targetSquareToSide = Directions.DirectionValues[move.TargetSquare, Array.IndexOf(directionValues, queenDirection)];
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

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            // clean :D
            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.Queen + playerTurn))
                    continue;

                for (int direction = 0; direction < directionValues.Length; direction++)
                {
                    for (int toEdge = 1; toEdge < (Directions.DirectionValues[square, direction] + 1); toEdge++)
                    {
                        if (board.board[square + (direction * toEdge)] == 0)
                        {
                            posssibleMoves.Add(new Move(square, square + (direction * toEdge)));
                        }
                        else if (Board.IsPieceOpposite(board.board[square + (direction * toEdge)], playerTurn))
                        {
                            posssibleMoves.Add(new Move(square, square + (direction * toEdge)));
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