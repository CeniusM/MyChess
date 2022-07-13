using MyChess.ChessBoard;

namespace MyChess.PossibleMoves.Pieces
{
    public class Pawn
    {
        private static bool IsOponent(int p1, int p2) => ((p1 & Piece.ColorBits) != (p2 & Piece.ColorBits) && (p2 & Piece.PieceBits) != 0);
        public static void AddMoves(Board board, List<Move> moves)
        {
            for (int i = 0; i < board.piecePoses.Count; i++)
            {
                int pos = board.piecePoses[i];
                if (board[pos] == (Piece.Pawn | board.playerTurn))
                {
                    if (board.playerTurn == Board.WhiteMask)
                    {
                        // two forward
                        if (56 > pos && pos > 47)
                            if (board[pos - 16] == 0)
                                if (board[pos - 8] == 0)
                                    moves.Add(new(pos, pos - 16, Move.Flag.PawnTwoForward));



                        if (Board.IsPieceInBound(pos - 7)) // left
                        {
                            int inBoundPos = pos - 7;
                            if ((inBoundPos) >> 3 == (pos >> 3) - 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.BPawn));

                                if (IsOponent(board[pos], board[inBoundPos]))
                                {
                                    if (inBoundPos > -1 && inBoundPos < 8) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board[inBoundPos]));
                                    }
                                    else if ((board[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board[inBoundPos]));
                                }
                            }
                        }
                        if (Board.IsPieceInBound(pos - 8)) // foward
                        {
                            int inBoundPos = pos - 8;
                            if (board[inBoundPos] == 0)
                            {
                                if (inBoundPos > -1 && inBoundPos < 8) // check if its on promotion
                                {
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop));
                                }
                                else
                                    moves.Add(new(pos, inBoundPos, 0));
                            }
                        }
                        if (Board.IsPieceInBound(pos - 9)) // right
                        {
                            int inBoundPos = pos - 9;
                            if ((inBoundPos) >> 3 == (pos >> 3) - 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.BPawn));

                                if (IsOponent(board[pos], board[inBoundPos]))
                                {
                                    if (inBoundPos > -1 && inBoundPos < 8) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board[inBoundPos]));
                                    }
                                    else if ((board[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board[inBoundPos]));
                                }
                            }
                        }
                    }
                    else
                    {
                        // two forward
                        if (16 > pos && pos > 7)
                            if (board[pos + 16] == 0)
                                if (board[pos + 8] == 0)
                                    moves.Add(new(pos, pos + 16, Move.Flag.PawnTwoForward));



                        if (Board.IsPieceInBound(pos + 7)) // left
                        {
                            int inBoundPos = pos + 7;
                            if ((inBoundPos) >> 3 == (pos >> 3) + 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.WPawn));

                                if (IsOponent(board[pos], board[inBoundPos]))
                                {
                                    if (inBoundPos > 55 && inBoundPos < 64) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board[inBoundPos]));
                                    }
                                    else if ((board[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board[inBoundPos]));
                                }
                            }
                        }
                        if (Board.IsPieceInBound(pos + 8)) // foward
                        {
                            int inBoundPos = pos + 8;
                            if (board[inBoundPos] == 0)
                            {
                                if (inBoundPos > 55 && inBoundPos < 64) // check if its on promotion
                                {
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook));
                                    moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop));
                                }
                                else
                                    moves.Add(new(pos, inBoundPos, 0, 0));
                            }
                        }
                        if (Board.IsPieceInBound(pos + 9)) // right
                        {
                            int inBoundPos = pos + 9;
                            if ((inBoundPos) >> 3 == (pos >> 3) + 1)
                            {
                                if (inBoundPos == board.enPassantPiece)
                                    moves.Add(new(pos, inBoundPos, Move.Flag.EnPassantCapture, Piece.WPawn));

                                if (IsOponent(board[pos], board[inBoundPos]))
                                {
                                    if (inBoundPos > 55 && inBoundPos < 64) // check if its on promotion
                                    {
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToQueen, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToKnight, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToRook, board[inBoundPos]));
                                        moves.Add(new Move(pos, inBoundPos, Move.Flag.PromoteToBishop, board[inBoundPos]));
                                    }
                                    else if ((board[inBoundPos] & Piece.PieceBits) != 0)
                                        moves.Add(new(pos, inBoundPos, 0, board[inBoundPos]));
                                }
                            }
                        }

                    }
                }
            }



























            // for (int i = 0; i < board.piecePoses.Count; i++)
            // {
            //     int pos = board.piecePoses[i];
            //     if (board[pos] == (Piece.Pawn | board.playerTurn))
            //     {

            //         // walks towards negativ
            //         if (board.playerTurn == Piece.White)
            //         {
            //             // two forward
            //             if (56 > pos && pos > 47)
            //                 if (Board.IsPieceInBound(pos - 16))
            //                     if (board[pos - 16] == 0)
            //                         moves.Add(new(pos, pos - 16, Move.Flag.PawnTwoForward));

            //             // right
            //             if (Board.IsPieceInBound(pos - 7))
            //                 if ((board[pos - 7] & Piece.ColorBits) == Piece.Black)
            //                     if ((pos - 7) >> 3 == (pos >> 3) - 1)
            //                         moves.Add(new(pos, pos - 7, Move.Flag.None));

            //             // left
            //             if (Board.IsPieceInBound(pos - 9))
            //                 if ((board[pos - 9] & Piece.ColorBits) == Piece.Black)
            //                     if ((pos - 9) >> 3 == (pos >> 3) - 1)
            //                         moves.Add(new(pos, pos - 9, Move.Flag.None));

            //             // enpassent left
            //             if (Board.IsPieceInBound(pos - 9))
            //                 if (pos - 9 == board.enPassantPiece)
            //                     if ((pos - 9) >> 3 == (pos >> 3) - 1)
            //                         moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture, Piece.BPawn));

            //             // enpassent right
            //             if (Board.IsPieceInBound(pos - 7))
            //                 if (pos - 7 == board.enPassantPiece)
            //                     if ((pos - 7) >> 3 == (pos >> 3) - 1)
            //                         moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture));

            //             // promotion
            //             if (Board.IsPieceInBound(pos - 8))
            //             {
            //                 if (pos - 8 > -1 && pos - 8 < 8)
            //                 {
            //                     moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToQueen));
            //                     moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToKnight));
            //                     moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToRook));
            //                     moves.Add(new Move(pos, pos - 8, Move.Flag.PromoteToBishop));
            //                 }
            //                 // one forward
            //                 else if (board[pos - 8] == Piece.None)
            //                     moves.Add(new(pos, pos - 8, Move.Flag.None));
            //             }

            //         }

            //         else
            //         {
            //             // two forward
            //             if (16 > pos && pos > 7)
            //                 if (Board.IsPieceInBound(pos + 16))
            //                     if (board[pos + 16] == 0)
            //                         moves.Add(new(pos, pos + 16, Move.Flag.PawnTwoForward));

            //             // right
            //             if (Board.IsPieceInBound(pos + 7))
            //                 if ((board[pos + 7] & Piece.ColorBits) == Piece.White)
            //                     moves.Add(new(pos, pos + 7, Move.Flag.None));

            //             // left
            //             if (Board.IsPieceInBound(pos + 9))
            //                 if ((board[pos + 9] & Piece.ColorBits) == Piece.White)
            //                     moves.Add(new(pos, pos + 9, Move.Flag.None));

            //             // enpassent left
            //             if (Board.IsPieceInBound(pos + 9))
            //                 if (pos + 9 == board.enPassantPiece)
            //                     if ((pos + 9) >> 3 == (pos >> 3) + 1)
            //                         moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture));

            //             // enpassent right
            //             if (Board.IsPieceInBound(pos + 7))
            //                 if (pos + 7 == board.enPassantPiece)
            //                     if ((pos + 7) >> 3 == (pos >> 3) + 1)
            //                         moves.Add(new(pos, board.enPassantPiece, Move.Flag.EnPassantCapture, Piece.WPawn));

            //             // promotion
            //             if (Board.IsPieceInBound(pos + 8))
            //             {
            //                 if (pos + 8 > 55 && pos + 8 < 64)
            //                 {
            //                     moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToQueen));
            //                     moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToKnight));
            //                     moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToRook));
            //                     moves.Add(new Move(pos, pos + 8, Move.Flag.PromoteToBishop));
            //                 }
            //                 // one forward
            //                 else if (board[pos + 8] == Piece.None)
            //                     moves.Add(new(pos, pos + 8, Move.Flag.None));
            //             }
            //         }

            //     }
            // }
        }
    }
}