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

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            // clean
            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.Rook + playerTurn))
                    continue;

                for (int direction = 0; direction < directionValues.Length; direction++)
                {
                    for (int toEdge = 1; toEdge < (Directions.DirectionValues[square, direction] + 1); toEdge++)
                    {
                        if (board.board[square + (directionValues[direction] * toEdge)] == 0)
                        {
                            posssibleMoves.Add(new Move(square, square + (directionValues[direction] * toEdge)));
                        }
                        else if (Board.IsPieceOpposite(board.board[square + (directionValues[direction] * toEdge)], playerTurn))
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