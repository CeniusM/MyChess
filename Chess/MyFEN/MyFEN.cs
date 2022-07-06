using System.ComponentModel;
using MyChess.ChessBoard;


// https://www.chess.com/terms/fen-chess

// note:
// gotta make the GetboardFromFEN faster becous it will be called alot and alot more then GetFenFromBoard

namespace MyChess.FEN
{
    class MyFEN
    {
        public static readonly string StartPostion = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public static int GetPieceFromChar(char c)
        {
            switch (c)
            {
                case 'P':
                    return Piece.WPawn;
                case 'N':
                    return Piece.WKnight;
                case 'B':
                    return Piece.WBishop;
                case 'R':
                    return Piece.WRook;
                case 'Q':
                    return Piece.WQueen;
                case 'K':
                    return Piece.WKing;
                case 'p':
                    return Piece.BPawn;
                case 'n':
                    return Piece.BKnight;
                case 'b':
                    return Piece.BBishop;
                case 'r':
                    return Piece.BRook;
                case 'q':
                    return Piece.BQueen;
                case 'k':
                    return Piece.BKing;
                default:
                    return -1;
            }
        }
        public static char GetCharFromPiece(int i)
        {
            switch (i)
            {
                case Piece.WPawn:
                    return 'P';
                case Piece.WKnight:
                    return 'N';
                case Piece.WBishop:
                    return 'B';
                case Piece.WRook:
                    return 'R';
                case Piece.WQueen:
                    return 'Q';
                case Piece.WKing:
                    return 'K';
                case Piece.BPawn:
                    return 'p';
                case Piece.BKnight:
                    return 'n';
                case Piece.BBishop:
                    return 'b';
                case Piece.BRook:
                    return 'r';
                case Piece.BQueen:
                    return 'q';
                case Piece.BKing:
                    return 'k';
                default:
                    return char.MaxValue;
            }
        }
       
       // all the methods Get from FEN/Board only work up too 99 full or half move...
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
                    if (!char.IsNumber(c) && c != ' ')
                        board[boardPointer] = GetPieceFromChar(c);
                }
                if (c == ' ')
                    break;
                boardPointer++;
                FENPointer++;
            }
            FENPointer++;

            if (FENString[FENPointer] == 'w')
                board.playerTurn = 8;
            else
                board.playerTurn = 16;
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
            }
            FENPointer++;

            board.enPassantPiece = 0;
            if (FENString[FENPointer] != '-')
            {
                board.enPassantPiece += FENString[FENPointer] - 'a';
                FENPointer++;
                board.enPassantPiece += 64 - ((FENString[FENPointer] - '0') * 8);
            }
            else
                board.enPassantPiece = 64;
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

            board.InitPiecePoses();

            return board;
        }
        // public static string GetFENFromBoardNEW()
        // {
        //     throw new NotImplementedException();

        //     char[] cString = new char[100];
        //     int cPtr = 0;
        //     void SetChar(char c)
        //     {
        //         cString[cPtr] = c;
        //         cPtr++;
        //     }

        //     for (int rank = 0; rank < 8; rank++)
        //     {
        //         for (int col = 0; col < 8; col++)
        //         {
                    
        //         }
        //     }


        //     char[] temp = new char[cPtr];
        //     for (int i = 0; i < cPtr; i++)
        //     {
        //         temp[i] = cString[i];
        //     }
        //     string FENString = new string(temp);
        //     return FENString;
        // }
        public static string GetFENFromBoard(Board board)
        {
            char[] CString = new char[100];
            int CStringPointer = 0;
            void SetChar(char c)
            {
                CString[CStringPointer] = c;
                CStringPointer++;
            }

            int gap = 0;
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    if (gap > 0)
                    {
                        SetChar((char)(gap + '0'));
                        gap = 0;
                    }
                    SetChar('/');
                }

                if (board[i] == 0)
                {
                    gap++;
                }
                else
                {
                    if (gap > 0)
                    {
                        SetChar((char)(gap + '0'));
                        gap = 0;
                    }
                    CString[CStringPointer] = GetCharFromPiece(board[i]);
                    CStringPointer++;
                }
            }
            if (gap > 0)
                SetChar((char)(gap + '0'));
            SetChar(' ');

            if (board.playerTurn == 8)
                SetChar('w');
            else
                SetChar('b');
            SetChar(' ');

            if ((board.castle & 0b1000) == 0b1000)
                SetChar('K');
            if ((board.castle & 0b0100) == 0b0100)
                SetChar('Q');
            if ((board.castle & 0b0010) == 0b0010)
                SetChar('k');
            if ((board.castle & 0b0001) == 0b0001)
                SetChar('q');
            if (CString[CStringPointer - 1] == ' ') // check if anything has acually placed
                SetChar('-');
            SetChar(' ');

            if (board.enPassantPiece != 64)
            {
                // set the collum with letter
                SetChar((char)((int)'a' + board.enPassantPiece % 8));

                // set the rank
                SetChar((char)(8 - (board.enPassantPiece >> 3) + '0'));
            }
            else
                SetChar('-');
            SetChar(' ');


            if (board.halfMove > 9)
                SetChar((char)((board.halfMove / 10) + (int)'0'));
            SetChar((char)((board.halfMove % 10) + (int)'0'));
            SetChar(' ');

            if (board.fullMove > 9)
                SetChar((char)((board.fullMove / 10) + (int)'0'));
            SetChar((char)((board.fullMove % 10) + (int)'0'));



            char[] temp = new char[CStringPointer];
            for (int i = 0; i < CStringPointer; i++)
            {
                temp[i] = CString[i];
            }
            string FENString = new string(temp);
            return FENString;
        }

        private static bool IsPieceWhite(int p) => (p & Piece.ColorBits) == Piece.White;
        private static bool IsPieceThisPiece(int p, int pType) => (p & Piece.PieceBits) == pType;
    }
}