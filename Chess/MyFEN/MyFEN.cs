using MyChess.ChessBoard;


// https://www.chess.com/terms/fen-chess


namespace MyChess.FEN
{
    class MyFEN
    {
        public static Board GetBoardFromFEN(string FENString)
        {
            Board board = new Board();
            char[] FEN = FENString.ToCharArray();

            int boardIndexer = 0;
            void SetPiece(int piece)
            {
                board.Square[boardIndexer] = piece;
                boardIndexer++;
            }

            int FENIndex = 0;

            for (FENIndex = 0; FENIndex < FEN.Length; FENIndex++)
            {
                if (char.IsNumber(FEN[FENIndex]))
                    boardIndexer += Convert.ToInt32(new string(FEN[FENIndex], 1));
                else if (FEN[FENIndex] == 'P') // Pawn
                    SetPiece(Piece.WPawn);
                else if (FEN[FENIndex] == 'p')
                    SetPiece(Piece.BPawn);

                else if (FEN[FENIndex] == 'R') // Rook
                    SetPiece(Piece.WRook);
                else if (FEN[FENIndex] == 'r')
                    SetPiece(Piece.BRook);

                else if (FEN[FENIndex] == 'N') // Knight
                    SetPiece(Piece.WKnight);
                else if (FEN[FENIndex] == 'n')
                    SetPiece(Piece.BKnight);

                else if (FEN[FENIndex] == 'B') // Bishop
                    SetPiece(Piece.WBishop);
                else if (FEN[FENIndex] == 'b')
                    SetPiece(Piece.BBishop);

                else if (FEN[FENIndex] == 'Q') // Queen
                    SetPiece(Piece.WQueen);
                else if (FEN[FENIndex] == 'q')
                    SetPiece(Piece.BQueen);

                else if (FEN[FENIndex] == 'K') // King
                    SetPiece(Piece.WKing);
                else if (FEN[FENIndex] == 'k')
                    SetPiece(Piece.BKing);

                else if (FEN[FENIndex] == ' ')
                    break;
            }
            FENIndex++;
            if (FEN[FENIndex] == 'b')
                board.ChangePlayer();
            FENIndex += 2;

            board.castle = 0;
            int addedNextTime = 0;
            for (int i = 0; i < 4; i++)
            {

                if (FEN[FENIndex + i] == 'K')
                {
                    board.castle = (board.castle | 0b1000);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == 'Q')
                {
                    board.castle = (board.castle | 0b0100);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == 'k')
                {
                    board.castle = (board.castle | 0b0010);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == 'q')
                {
                    board.castle = (board.castle | 0b0001);
                    addedNextTime++;
                }
                else if (FEN[FENIndex + i] == ' ')
                    break;
            }
            FENIndex += addedNextTime + 1;

            if (board.enPassantPiece == 64)
            {
                FEN[FENIndex] = '-';
                FENIndex += 2;
            }
            else
            {
                int enPassantPiecePlacement = (8 - FEN[FENIndex + 1]) * 8; // the number
                if (FEN[FENIndex] == 'a')
                    enPassantPiecePlacement += 1;
                else if (FEN[FENIndex] == 'b')
                    enPassantPiecePlacement += 2;
                else if (FEN[FENIndex] == 'c')
                    enPassantPiecePlacement += 3;
                else if (FEN[FENIndex] == 'd')
                    enPassantPiecePlacement += 4;
                else if (FEN[FENIndex] == 'e')
                    enPassantPiecePlacement += 5;
                else if (FEN[FENIndex] == 'f')
                    enPassantPiecePlacement += 6;
                else if (FEN[FENIndex] == 'g')
                    enPassantPiecePlacement += 7;
                else if (FEN[FENIndex] == 'h')
                    enPassantPiecePlacement += 8;

                board.enPassantPiece = enPassantPiecePlacement;

                FENIndex += 3;
            }

            return board;
        }

        public static string GetFENFromBoard(Board board)
        {
            char[] FEN = new char[100];
            int FENIndexer = 0;

            int gap = 0;
            for (int i = 0; i < board.Square.Length; i++)
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

                if (board.Square[i] != 0)
                {
                    if (gap != 0)
                    {
                        FEN[FENIndexer] = ((char)(gap + 48));
                        gap = 0;
                        FENIndexer++;
                    }
                }

                if (IsPieceThisPiece(board.Square[i], Piece.Pawn))
                {
                    if (IsPieceWhite(board.Square[i]))
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
                else if (IsPieceThisPiece(board.Square[i], Piece.Knight))
                {
                    if (IsPieceWhite(board.Square[i]))
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
                else if (IsPieceThisPiece(board.Square[i], Piece.Bishop))
                {
                    if (IsPieceWhite(board.Square[i]))
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
                else if (IsPieceThisPiece(board.Square[i], Piece.Rook))
                {
                    if (IsPieceWhite(board.Square[i]))
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
                else if (IsPieceThisPiece(board.Square[i], Piece.Queen))
                {
                    if (IsPieceWhite(board.Square[i]))
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
                else if (IsPieceThisPiece(board.Square[i], Piece.King))
                {
                    if (IsPieceWhite(board.Square[i]))
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

            if (board.playerTurn == 8)
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

            if (board.enPassantPiece == 64)
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

        private static bool IsPieceWhite(int p) => (p & Piece.ColorBits) == Piece.White;
        private static bool IsPieceThisPiece(int p, int pType) => (p & Piece.PieceBits) == pType;
    }
}