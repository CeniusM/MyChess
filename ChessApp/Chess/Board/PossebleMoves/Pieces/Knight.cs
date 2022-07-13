using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    public class Knight
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board[board.piecePoses[i]] == (Piece.Knight | board.playerTurn))
                {
                    int knightPos = board.piecePoses[i];
                    for (int j = 0; j < 8; j++)
                    {
                        int knightMove = knightPos + MovesFromSquare.KnightMoves[knightPos, j];
                        if (MovesFromSquare.KnightMoves[knightPos, j] == MovesFromSquare.InvalidMove)
                            continue;
                        else if ((board[knightMove] & Piece.ColorBits) != board.playerTurn)
                        {
                            moves.Add(new(knightPos, knightMove, 0, board[knightMove]));
                        }    
                    }
                }
            }
        }
    }
}