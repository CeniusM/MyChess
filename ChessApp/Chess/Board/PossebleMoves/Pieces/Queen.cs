using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    public class Queen
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board[board.piecePoses[i]] == (Piece.Queen | board.playerTurn))
                {
                    int pos = board.piecePoses[i];
                    for (int dir = 0; dir < 8; dir++)
                    {
                        int move = pos;
                        for (int moveCount = 0; moveCount < 7; moveCount++)
                        {
                            if (MovesFromSquare.SlidingpieceMoves[pos, dir, moveCount] == MovesFromSquare.InvalidMove)
                                break;
                            move = MovesFromSquare.SlidingpieceMoves[pos, dir, moveCount];

                            if (board[move] == 0)
                            {
                                moves.Add(new(pos, move, 0, board[move]));
                            }
                            else if ((board[move] & Piece.ColorBits) != board.playerTurn)
                            {
                                moves.Add(new(pos, move, 0, board[move]));
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
            }            
        }
    }
}