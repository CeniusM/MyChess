using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    public class Queen
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                if (board.Square[board.piecePoses[i]] == (Piece.Queen | board.playerTurn))
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

                            if (board.Square[move] == 0)
                            {
                                moves.Add(new(pos, move, 0, board.Square[move]));
                            }
                            else if ((board.Square[move] & Piece.ColorBits) != board.playerTurn)
                            {
                                moves.Add(new(pos, move, 0, board.Square[move]));
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