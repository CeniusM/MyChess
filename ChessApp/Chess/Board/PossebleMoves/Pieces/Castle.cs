using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    public class Castle
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            Board original = Board.GetCopy(board);


            int castle = board.castle;

            if (castle == 0)
                return;

            if (board.playerTurn == 8)
            {
                // only check the first and second square, the third will be checked later
                if ((castle & CASTLE.W_King_Side) == CASTLE.W_King_Side)
                {
                    if (board.Square[61] == 0)
                    {
                        if (board.Square[62] == 0)
                        {
                            if (!IsSquareAttacked(board, 60, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                            {
                                if (!IsSquareAttacked(board, 61, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                                {
                                    moves.Add(new(60, 62, Move.Flag.Castling)); // check at 62 will be checked later
                                }
                            }
                        }
                    }
                }
                if ((castle & CASTLE.W_Queen_Side) == CASTLE.W_Queen_Side)
                {
                    if (board.Square[59] == 0)
                    {
                        if (board.Square[58] == 0)
                        {
                            if (board.Square[57] == 0)
                            {
                                if (!IsSquareAttacked(board, 60, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                                {
                                    if (!IsSquareAttacked(board, 59, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                                    {
                                        moves.Add(new(60, 58, Move.Flag.Castling)); // check at 62 will be checked later
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if ((castle & CASTLE.B_King_Side) == CASTLE.B_King_Side)
                {
                    if (board.Square[5] == 0)
                    {
                        if (board.Square[6] == 0)
                        {
                            if (!IsSquareAttacked(board, 4, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                            {
                                if (!IsSquareAttacked(board, 5, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                                {
                                    moves.Add(new(4, 6, Move.Flag.Castling)); // check at 62 will be checked later
                                }
                            }
                        }
                    }
                }
                if ((castle & CASTLE.B_Queen_Side) == CASTLE.B_Queen_Side)
                {
                    if (board.Square[1] == 0)
                    {
                        if (board.Square[2] == 0)
                        {
                            if (board.Square[3] == 0)
                            {
                                if (!IsSquareAttacked(board, 4, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                                {
                                    if (!IsSquareAttacked(board, 3, board.playerTurn, (board.playerTurn ^ Board.ColorMask)))
                                    {
                                        moves.Add(new(4, 2, Move.Flag.Castling)); // check at 62 will be checked later
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool IsSquareAttacked(Board board, int kingPos, int playerTurn, int opesitColor)
        {
            if (kingPos == -1)
                return true;

            // check Knights
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KnightMoves[kingPos, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[kingPos + MovesFromSquare.KnightMoves[kingPos, i]] == (Piece.Knight | opesitColor))
                    return true;
            }

            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[kingPos, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board.Square[kingPos + MovesFromSquare.KingMoves[kingPos, i]] == (Piece.King | opesitColor))
                    return true;
            }

            // check pawns
            if (playerTurn == Board.WhiteMask)
            {
                if (Board.IsPieceInBound(kingPos - 7))
                    if (board.Square[kingPos - 7] == Piece.BPawn)
                        return true;
                if (Board.IsPieceInBound(kingPos - 9))
                    if (board.Square[kingPos - 9] == Piece.BPawn)
                        return true;
            }
            else
            {
                if (Board.IsPieceInBound(kingPos + 7))
                    if (board.Square[kingPos + 7] == Piece.WPawn)
                        return true;
                if (Board.IsPieceInBound(kingPos + 9))
                    if (board.Square[kingPos + 9] == Piece.WPawn)
                        return true;
            }

            // check Sliding Pieces
            for (int dir = 0; dir < 8; dir++)
            {
                int move = kingPos;
                for (int moveCount = 0; moveCount < 7; moveCount++)
                {
                    if (MovesFromSquare.SlidingpieceMoves[kingPos, dir, moveCount] == MovesFromSquare.InvalidMove)
                        break;
                    move = MovesFromSquare.SlidingpieceMoves[kingPos, dir, moveCount];


                    if (board.Square[move] != 0)
                    {
                        if ((board.Square[move] & Board.ColorMask) == opesitColor)
                        {
                            switch (board.Square[move] & Piece.PieceBits)
                            {
                                case Piece.Queen:
                                    return true;
                                case Piece.Rook:
                                    if (dir < 4)
                                        return true;
                                    break;
                                case Piece.Bishop:
                                    if (dir > 3)
                                        return true;
                                    break;
                            }
                        }
                        break;
                    }
                }
            }


            return false;
        }
    }
}