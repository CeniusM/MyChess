using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class King
    {
        private static int[] KingMoves =
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

        public static List<Move> GetPossibleMoves(Board board)
        {
            int playerTurn = board.PlayerTurn; // so i dont need to get it each time
            List<Move> posssibleMoves = new List<Move>();

            void TryMove(int square, int move, int lineDiff)
            {
                if (square + move < 64 && square + move > -1)
                    if (((square + move) >> 3) - (square >> 3) == lineDiff)
                        if (Board.IsPieceOppositeOrNone(board.board[square], (board.board[square + move])))
                            posssibleMoves.Add(new Move(square, square + move));
            };
            for (int square = 0; square < 64; square++)
            {
                if (!(board.board[square] == Piece.King + playerTurn))
                    continue;

                TryMove(square, -8, -1);
                TryMove(square, -7, -1);
                TryMove(square, 1, 0);
                TryMove(square, 9, 1);
                TryMove(square, 8, 1);
                TryMove(square, 7, 1);
                TryMove(square, -1, 0);
                TryMove(square, -9, -1);
            }

            return posssibleMoves;
        }
    }
}