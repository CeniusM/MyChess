using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    class King
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            int kingPos;
            if (board.playerTurn == 1)  // white king
                kingPos = board[board.PiecePoses[0]];
            else                        // black king
                kingPos = board[board.PiecePoses[1]];

            for (int i = -1; i < 2; i++) // unrap later
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        return;
                    int piece = board[kingPos + i + j];
                    if (piece == 0 || (piece & Piece.ColorBits) != board.playerTurn)
                    {
                        moves.Add(new Move(kingPos, kingPos + i + j, 0));
                        return;
                    }
                }
            }
        }
    }
}