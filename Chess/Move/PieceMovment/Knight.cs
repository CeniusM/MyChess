using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Knight
    {
        private static int[] KnightMoves =
        {
            -6,
            -10,
            -15,
            -17,
            6,
            10,
            15,
            17
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
                if (!(board.board[square] == Piece.Knight + playerTurn))
                    continue;

                TryMove(square, -6, -1);
                TryMove(square, -10, -1);
                TryMove(square, -15, -2);
                TryMove(square, -17, -2);
                TryMove(square, 6, 1);
                TryMove(square, 10, 1);
                TryMove(square, 15, 2);
                TryMove(square, 17, 2);
            }

            return posssibleMoves;
        }
    }
}


// for (int i = 0; i < pos.Length; i++)
// {
//     for (int j = 0; j < KnightMoves.Count(); j++)
//     {
//         if (Board.IsPieceOppositeOrNone(board.board[pos[i]], board.board[pos[i] + KnightMoves[i]]))
//             posssibleMoves.Add(new Move(pos[i], KnightMoves[i]));
//     }
// }