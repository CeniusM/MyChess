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

            for (int i = 0; i < 8; i++)
            {
                int kingeMove = MovesFromSquare.KingMoves[kingPos, i];
                if (kingeMove == -1)
                    break;
                else if ((board[kingeMove] & Piece.ColorBits) != board.playerTurn)
                {
                    moves.Add(new(kingPos, board[kingeMove], 0));
                }                
            }
        }
    }
}