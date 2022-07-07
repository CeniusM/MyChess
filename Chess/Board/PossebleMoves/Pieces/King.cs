using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    class King
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board[board.piecePoses[i]] == (Piece.King | board.playerTurn))
                {
                    int kingPos = board.piecePoses[i];
                    for (int j = 0; j < 8; j++)
                    {
                        int kingMove = kingPos + MovesFromSquare.KingMoves[kingPos, j];
                        if (MovesFromSquare.KingMoves[kingPos, j] == MovesFromSquare.InvalidMove)
                            continue;
                        else if ((board[kingMove] & Piece.ColorBits) != board.playerTurn)
                        {
                            moves.Add(new(kingPos, kingMove, 0, board[kingMove]));
                        }
                    }
                }
            }
        }
    }
}


//int kingPos;
//if (board.playerTurn == 8)  // white king
//    kingPos = board.piecePoses[0];
//else                        // black king
//    kingPos = board.piecePoses[1];
//
//for (int i = 0; i < 8; i++)
//{
//    int kingeMove = kingPos + MovesFromSquare.KingMoves[kingPos, i];
//    if (MovesFromSquare.KingMoves[kingPos, i] == MovesFromSquare.InvalidMove)
//        continue;
//    else if ((board[kingeMove] & Piece.ColorBits) != board.playerTurn)
//    {
//        moves.Add(new(kingPos, kingeMove, 0));
//    }                
//}