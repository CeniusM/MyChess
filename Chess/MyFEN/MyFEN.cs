using System.ComponentModel;
using MyChess.ChessBoard;


// https://www.chess.com/terms/fen-chess


namespace MyChess.FEN
{
    class MyFEN
    {
        public static Board GetBoardFromFEN(string FENString)
        {
            int FENPointer = 0;
            Board board = new Board();

            int boardPointer = 0;
            while (true)
            {
                if (boardPointer > 64)
                    throw new Exception("Board pointer out of range");
                if (FENPointer > 72)
                    throw new Exception("FEN string too long");

                char c = FENString[FENPointer];

                if (Char.IsDigit(c))
                {
                    boardPointer += c - '0' - 1;
                }
                if (c == '/')
                {
                    boardPointer--;
                }
                else 
                {
                    switch (c)
                    {
                        case 'P':
                            board[boardPointer] = Piece.WPawn;
                            break;
                        case 'N':
                            board[boardPointer] = Piece.WKnight;
                            break;
                        case 'B':
                            board[boardPointer] = Piece.WBishop;
                            break;
                        case 'R':
                            board[boardPointer] = Piece.WRook;
                            break;
                        case 'Q':
                            board[boardPointer] = Piece.WQueen;
                            break;
                        case 'K':
                            board[boardPointer] = Piece.WKing;
                            break;
                        case 'p':
                            board[boardPointer] = Piece.BPawn;
                            break;
                        case 'n':
                            board[boardPointer] = Piece.BKnight;
                            break;
                        case 'b':
                            board[boardPointer] = Piece.BBishop;
                            break;
                        case 'r':
                            board[boardPointer] = Piece.BRook;
                            break;
                        case 'q':
                            board[boardPointer] = Piece.BQueen;
                            break;
                        case 'k':
                            board[boardPointer] = Piece.BKing;
                            break;
                    }
                }
                if (c == ' ')
                    break;
                boardPointer++;
                FENPointer++;
            } FENPointer++;

            if (FENString[FENPointer] == 'w')
                board.playerTurn = 1;
            else
                board.playerTurn = 2;
            FENPointer += 2;
            
            board.castle = 0;
            for (int i = 0; i < 4; i++)
            {
                if (FENString[FENPointer] == 'K')
                {
                    board.castle = (board.castle | 0b1000);
                }
                else if (FENString[FENPointer] == 'Q')
                {
                    board.castle = (board.castle | 0b0100);
                }
                else if (FENString[FENPointer] == 'k')
                {
                    board.castle = (board.castle | 0b0010);
                }
                else if (FENString[FENPointer] == 'q')
                {
                    board.castle = (board.castle | 0b0001);
                }
                else if (FENString[FENPointer] == ' ')
                    break;
                FENPointer++;
            } FENPointer++;
            
            board.enPassantPiece = 0;
            if (FENString[FENPointer] != '-')
            {
                board.enPassantPiece += FENString[FENPointer] - 'a';
                FENPointer++;
                board.enPassantPiece += 64 - ((FENString[FENPointer] - '0') * 8);
            }
            FENPointer++;
            FENPointer++;

            if (FENString[FENPointer + 1] != ' ')
            {
                board.halfMove += (FENString[FENPointer] - '0') * 10;
                FENPointer++;
                board.halfMove += (FENString[FENPointer] - '0');
            }
            else
                board.halfMove += (FENString[FENPointer] - '0');
            FENPointer++;
            FENPointer++;

            if (FENString.Length > FENPointer + 1 && FENString[FENPointer] + 1 != ' ')
            {
                board.fullMove += (FENString[FENPointer] - '0') * 10;
                FENPointer++;
                board.fullMove += (FENString[FENPointer] - '0');
            }
            else
                board.fullMove += (FENString[FENPointer] - '0');             

            return board;
        }

        public static string GetFENFromBoard(Board board)
        {
            char[] CString = new char[100];
            int CStringPointer = 0;

            int gap = 0;
            for (int i = 0; i < 64; i++)
            {
                if (board[i] == 0)
                {
                    gap++;
                }
                else
                {
                    switch (board[i])
                    {
                        case Piece.WPawn:
                            CString[CStringPointer] = 'P';
                            break;
                        case Piece.WKnight:
                            CString[CStringPointer] = 'N';
                            break;
                        case Piece.WBishop:
                            CString[CStringPointer] = 'B';
                            break;
                        case Piece.WRook:
                            CString[CStringPointer] = 'R';
                            break;
                        case Piece.WQueen:
                            CString[CStringPointer] = 'Q';
                            break;
                        case Piece.WKing:
                            CString[CStringPointer] = 'K';
                            break;
                        case Piece.BPawn:
                            CString[CStringPointer] = 'p';
                            break;
                        case Piece.BKnight:
                            CString[CStringPointer] = 'n';
                            break;
                        case Piece.BBishop:
                            CString[CStringPointer] = 'b';
                            break;
                        case Piece.BRook:
                            CString[CStringPointer] = 'r';
                            break;
                        case Piece.BQueen:
                            CString[CStringPointer] = 'q';
                            break;
                        case Piece.BKing:
                            CString[CStringPointer] = 'k';
                            break;
                        default:
                            break;
                    }
                    gap++;                   
                }


                if (i % 8 == 0)
                {
                    if (gap > 0)
                    {
                        CStringPointer++;
                        CString[CStringPointer] = (char)(gap + '0');
                        gap = 0;
                    }
                    CStringPointer++;
                    CString[CStringPointer] = '/';
                }
                if (gap > 0 && board[i] != 0)
                {
                    CStringPointer++;
                    CString[CStringPointer] = (char)(gap + '0');
                    gap = 0;
                }
                CStringPointer++;
            }CStringPointer++;
            
            

            char[] temp = new char[CStringPointer];
            string FENString = new string(temp);
            return FENString;
        }

        private static bool IsPieceWhite(int p) => (p & Piece.ColorBits) == Piece.White;
        private static bool IsPieceThisPiece(int p, int pType) => (p & Piece.PieceBits) == pType;
    }
}

/*
GraveYard


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

            return board;
            














            
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
*/