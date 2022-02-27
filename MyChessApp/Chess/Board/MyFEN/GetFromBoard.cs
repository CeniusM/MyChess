namespace Chess.ChessBoard
{
    partial class MyFEN
    {
        public static string GetFENFromBoard(Board board)
        {
            char[] FEN = new char[100];
            int FENIndexer = 0;

            int gap = 0;
            for (int i = 0; i < board.board.Length; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    if (gap != 0)
                    {
                        FEN[FENIndexer] = ((char)(gap + 48));
                        gap = 0;
                        FENIndexer++;
                    }
                    FEN[FENIndexer] = '/';
                    FENIndexer++;
                }

                if (board.board[i] != 0)
                {
                    if (gap != 0)
                    {
                        FEN[FENIndexer] = ((char)(gap + 48));
                        gap = 0;
                        FENIndexer++;
                    }
                }

                if (Board.IsPieceThisPiece(board.board[i], Piece.Pawm))
                {
                    if (Board.IsPieceWhite(board.board[i]))
                    {
                        FEN[FENIndexer] = 'P';
                        FENIndexer++;
                    }
                    else
                    {
                        FEN[FENIndexer] = 'p';
                        FENIndexer++;
                    }
                }
                else if (Board.IsPieceThisPiece(board.board[i], Piece.Knight))
                {
                    if (Board.IsPieceWhite(board.board[i]))
                    {
                        FEN[FENIndexer] = 'N';
                        FENIndexer++;
                    }
                    else
                    {
                        FEN[FENIndexer] = 'n';
                        FENIndexer++;
                    }
                }
                else if (Board.IsPieceThisPiece(board.board[i], Piece.Bishop))
                {
                    if (Board.IsPieceWhite(board.board[i]))
                    {
                        FEN[FENIndexer] = 'B';
                        FENIndexer++;
                    }
                    else
                    {
                        FEN[FENIndexer] = 'b';
                        FENIndexer++;
                    }
                }
                else if (Board.IsPieceThisPiece(board.board[i], Piece.Rook))
                {
                    if (Board.IsPieceWhite(board.board[i]))
                    {
                        FEN[FENIndexer] = 'R';
                        FENIndexer++;
                    }
                    else
                    {
                        FEN[FENIndexer] = 'r';
                        FENIndexer++;
                    }
                }
                else if (Board.IsPieceThisPiece(board.board[i], Piece.Queen))
                {
                    if (Board.IsPieceWhite(board.board[i]))
                    {
                        FEN[FENIndexer] = 'Q';
                        FENIndexer++;
                    }
                    else
                    {
                        FEN[FENIndexer] = 'q';
                        FENIndexer++;
                    }
                }
                else if (Board.IsPieceThisPiece(board.board[i], Piece.King))
                {
                    if (Board.IsPieceWhite(board.board[i]))
                    {
                        FEN[FENIndexer] = 'K';
                        FENIndexer++;
                    }
                    else
                    {
                        FEN[FENIndexer] = 'k';
                        FENIndexer++;
                    }
                }
                else
                {
                    gap++;
                }
            }
            if (gap != 0)
            {
                FEN[FENIndexer] = ((char)(gap + 48));
                gap = 0;
                FENIndexer++;
            }

            FEN[FENIndexer] = ' ';
            FENIndexer++;

            if (board.PlayerTurn == 8)
            {
                FEN[FENIndexer] = 'w';
                FENIndexer++;
            }
            else
            {
                FEN[FENIndexer] = 'b';
                FENIndexer++;
            }

            FEN[FENIndexer] = ' ';
            FENIndexer++;

            if ((board.castle & 0b1000) == 0b1000)
            {
                FEN[FENIndexer] = 'K';
                FENIndexer++;
            }
            if ((board.castle & 0b100) == 0b100)
            {
                FEN[FENIndexer] = 'Q';
                FENIndexer++;
            }
            if ((board.castle & 0b10) == 0b10)
            {
                FEN[FENIndexer] = 'k';
                FENIndexer++;
            }
            if ((board.castle & 0b1) == 0b1)
            {
                FEN[FENIndexer] = 'q';
                FENIndexer++;
            }
            FENIndexer++;

            if (board.enPassantPiece == -1)
            {
                FEN[FENIndexer] = '-';
                FENIndexer += 2;
            }
            // else if () // do the letter+num
            // {

            // }




            FEN[FENIndexer] = '*';
            string FENString = string.Empty;
            for (int i = 0; i < FEN.Length; i++) // crap method. but it goes for now...
            {
                if (FEN[i] == '*')
                    break;
                FENString += FEN[i];
            }

            // return FEN.ToString()!;
            return FENString;
        }
    }
}