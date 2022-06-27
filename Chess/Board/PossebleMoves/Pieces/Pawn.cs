using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    class Pawn
    {
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                int pos = board.piecePoses[i];
                if (board[pos] == (Piece.Pawn | board.playerTurn))
                {

                    // walks towards negativ
                    if (board.playerTurn == Piece.White)
                    {
                        // two forward
                        if (56 > pos && pos > 47)
                            moves.Add(new(pos, pos - 16, Move.Flag.PawnTwoForward));

                        // right
                        if ((board[pos - 7] & Piece.ColorBits) == Piece.Black)
                            if ((pos - 7) >> 3 == (pos >> 3) - 1)
                                moves.Add(new(pos, pos - 7, Move.Flag.None));

                        // left
                        if ((board[pos - 9] & Piece.ColorBits) == Piece.Black)
                            if ((pos - 9) >> 3 == (pos >> 3) - 1)
                                moves.Add(new(pos, pos - 9, Move.Flag.None));

                        // enpassent left
                        if (pos - 9 == board.enPassantPiece)
                            if ((pos - 9) >> 3 == (pos >> 3) - 1)
                                moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture));

                        // enpassent right
                        if (pos - 7 == board.enPassantPiece)
                            if ((pos - 7) >> 3 == (pos >> 3) - 1)
                                moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture));

                        // promotion
                        if (Board.IsPieceInBound(pos - 8))
                        {
                            if (pos - 8 > -1 && pos - 8 < 8)
                            {
                                moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToQueen));
                                moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToRook));
                                moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToBishop));
                                moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToKnight));
                            }
                            // one forward
                            else if (board[pos - 8] == Piece.None)
                                moves.Add(new(pos, pos - 8, Move.Flag.None));
                        }

                    }

                    else
                    {
                        // two forward
                        if (16 > pos && pos > 7)
                            moves.Add(new(pos, pos + 16, Move.Flag.PawnTwoForward));

                        // right
                        if ((board[pos + 7] & Piece.ColorBits) == Piece.White)
                            moves.Add(new(pos, pos + 7, Move.Flag.None));

                        // left
                        if ((board[pos + 9] & Piece.ColorBits) == Piece.White)
                            moves.Add(new(pos, pos + 9, Move.Flag.None));

                        // enpassent left
                        if (pos + 9 == board.enPassantPiece)
                            if ((pos + 9) >> 3 == (pos >> 3) + 1)
                                moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture));

                        // enpassent right
                        if (pos + 7 == board.enPassantPiece)
                            if ((pos + 7) >> 3 == (pos >> 3) + 1)
                                moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture));

                        // promotion
                        if (Board.IsPieceInBound(pos + 8))
                        {
                            if (pos + 8 > 55 && pos + 8 < 64)
                            {
                                moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToQueen));
                                moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToRook));
                                moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToBishop));
                                moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToKnight));
                            }
                            // one forward
                            else if (board[pos + 8] == Piece.None)
                                moves.Add(new(pos, pos + 8, Move.Flag.None));
                        }
                    }

                }
            }
        }
    }
}