using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    class Castle
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            int castle = board.castle;

            if (castle == 0)
                return;

            // only check the first and second square, the third will be checked later
            if ((castle & CASTLE.W_King_Side) == CASTLE.W_King_Side)
            {
                if (board[61] == 0)
                {
                    if (board[62] == 0)
                    {
                        // board.MakeMove(new(60, 61, 0, 0));
                        // if (CheckKingInCheck(board, 60, (board.playerTurn ^ Board.ColorMask)))
                        // {
                        //     board.MakeMove(new(61, 62, 0, 0));
                        //     if (CheckKingInCheck(board, 62, (board.playerTurn ^ Board.ColorMask)))
                        //     {
                        //         moves.Add(new(60, 62, Move.Flag.Castling));
                        //     }
                        //     board.UnMakeMove();
                        // }
                        // board.UnMakeMove();
                    }
                }
            }




            if ((castle & CASTLE.W_Queen_Side) == CASTLE.W_Queen_Side)
            {
                if (board[59] == 0)
                {
                    if (board[58] == 0)
                    {
                        if (board[57] == 0)
                        {

                        }
                    }
                }
            }





            if ((castle & CASTLE.B_King_Side) == CASTLE.B_King_Side)
            {
                if (board[5] == 0)
                {
                    if (board[6] == 0)
                    {

                    }
                }
            }





            if ((castle & CASTLE.B_Queen_Side) == CASTLE.B_Queen_Side)
            {
                if (board[1] == 0)
                {
                    if (board[2] == 0)
                    {
                        if (board[3] == 0)
                        {

                        }
                    }
                }
            }
        }

        private static bool CheckKingInCheck(Board board, int kingPos, int opesitColor)
        {
            if (kingPos == -1)
                return true;

            // check Knights
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KnightMoves[kingPos, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board[kingPos + MovesFromSquare.KnightMoves[kingPos, i]] == (Piece.Knight | opesitColor))
                    return true;
            }

            // check King
            for (int i = 0; i < 8; i++)
            {
                if (MovesFromSquare.KingMoves[kingPos, i] == MovesFromSquare.InvalidMove)
                    continue;
                if (board[kingPos + MovesFromSquare.KingMoves[kingPos, i]] == (Piece.King | opesitColor))
                    return true;
            }

            // check pawns
            if (board.playerTurn == Board.WhiteMask)
            {
                if (Board.IsPieceInBound(kingPos - 7))
                    if (board[kingPos - 7] == Piece.BPawn)
                        return true;
                if (Board.IsPieceInBound(kingPos - 9))
                    if (board[kingPos - 9] == Piece.BPawn)
                        return true;
            }
            else
            {
                if (Board.IsPieceInBound(kingPos + 7))
                    if (board[kingPos + 7] == Piece.WPawn)
                        return true;
                if (Board.IsPieceInBound(kingPos + 9))
                    if (board[kingPos + 9] == Piece.WPawn)
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


                    if (board[move] != 0)
                    {
                        if ((board[move] & Board.ColorMask) == opesitColor)
                        {
                            switch (board[move] & Piece.PieceBits)
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