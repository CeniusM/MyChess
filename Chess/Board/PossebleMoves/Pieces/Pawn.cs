using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    class Pawn
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board[board.piecePoses[i]] == (Piece.Pawn | board.playerTurn))
                {
                    
                    // walks towards negativ
                    if (board.playerTurn == Piece.White)
                    {

                    }

                    else
                    {

                    }

                }
            }
        }
    }
}