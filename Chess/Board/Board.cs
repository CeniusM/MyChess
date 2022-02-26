using System.Security.Cryptography;
namespace Chess.ChessBoard
{
    class Board
    {
        public int[] board = new int[64];
        public int enPassantPiece = 64; // 0 index is whites piece and 1 is blacks pieces
        public int castle = 0b1111;
        public int halfMoveClock = 0;
        public int fullmoveNumber = 1;
        public int PlayerTurn { get; private set; } = 8; // 8 = white, 16 = black
        public Board()
        {
            
        }
        public Board(int[] board, int castle, int playerTurn, int enPassantPiece, int halfMoveClock, int fullmoveNumber)
        {
            this.board = board;
            this.castle = castle;
        }
        public Board(string FEN)
        {
            Board newBoard = MyFEN.GetBoardFromFEN(FEN);
            this.board = newBoard.board;
            this.enPassantPiece = newBoard.enPassantPiece;
            this.castle = newBoard.castle;
            this.PlayerTurn = newBoard.PlayerTurn;
            this.halfMoveClock = newBoard.halfMoveClock;
            this.fullmoveNumber = newBoard.fullmoveNumber;
        }

        public void Reset()
        {
            Board newBoard = MyFEN.GetBoardFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            this.board = newBoard.board;
            this.enPassantPiece = newBoard.enPassantPiece;
            this.castle = newBoard.castle;
            this.PlayerTurn = newBoard.PlayerTurn;
            this.halfMoveClock = newBoard.halfMoveClock;
            this.fullmoveNumber = newBoard.fullmoveNumber;
        }

        public void ChangePlayer() => PlayerTurn ^= 0b11000; // changes between 8 and 16

        // bools
        public static bool IsPieceOpposite(int piece1, int piece2) // 01 10 00, 101
        { return ((piece1 | piece2) & Piece.ColorBits) == Piece.ColorBits; }
        public static bool IsPieceThisPiece(int piece1, int piece2)
        { return (piece1 & Piece.PieceBits) == piece2; }
        public static bool IsPieceWhite(int piece)
        { return (piece & Piece.ColorBits) == Piece.White; }
        public static bool IsPieceBlack(int piece)
        { return (piece & Piece.ColorBits) == Piece.Black; }
        public static bool IsPiecesSameColor(int piece1, int piece2)
        { return (piece1 & Piece.ColorBits) == (piece2 & Piece.ColorBits); }
        public static bool IsPieceOppositeOrNone(int piece1, int piece2)
        { return (piece1 & piece2 & Piece.ColorBits) != (piece1 & Piece.ColorBits); }
        public static bool IsPieceOnSameLine(int start, int target)
        { return (start >> 3) == (target >> 3); }

        // ints
        public static int LineDifrence(int start, int target) // start line to target line
        { return (target >> 3) - (start >> 3); }
    }
}
/*
the bits in the board int is sorted like this, 0 - 7
0 - 2 = indecates the piece value
3 && 4= indecates the color

the rest from 31-5     01       010
idk yet              | white | What peicetype it is

int = 137, tror jeg

-----

The castle works so the bits say if you can castle or not... will be implementet later
0000

1000 = White KingSite  = (O-O)
0100 = White QueenSite = (O-O-O)
0010 = Black KingSite  = (O-O)
0001 = Black QueenSite = (O-O-O)


pawn   = 1, 001
rook   = 2, 010
knight = 3, 011
bishop = 4, 100
queen  = 5, 101
king   = 6, 110

white  = 8, 01 000
black  = 16,10 000
*/








/*
            board[0] = Piece.Rook + Piece.Black;
            board[1] = Piece.Knight + Piece.Black;
            board[2] = Piece.Bishop + Piece.Black;
            board[3] = Piece.Queen + Piece.Black;
            board[4] = Piece.King + Piece.Black;
            board[5] = Piece.Bishop + Piece.Black;
            board[6] = Piece.Knight + Piece.Black;
            board[7] = Piece.Rook + Piece.Black;

            board[8] = Piece.Pawm + Piece.Black;
            board[9] = Piece.BPawm;
            board[10] = Piece.BPawm;
            board[11] = Piece.BPawm;
            board[12] = Piece.BPawm;
            board[13] = Piece.BPawm;
            board[14] = Piece.BPawm;
            board[15] = Piece.BPawm;

            board[63] = Piece.WRook;
            board[62] = Piece.WKnight;
            board[61] = Piece.WBishop;
            board[60] = Piece.WKing;
            board[59] = Piece.WQueen;
            board[58] = Piece.WBishop;
            board[57] = Piece.WKnight;
            board[56] = Piece.WRook;

            board[55] = Piece.WPawm;
            board[54] = Piece.WPawm;
            board[53] = Piece.WPawm;
            board[52] = Piece.WPawm;
            board[51] = Piece.WPawm;
            board[50] = Piece.WPawm;
            board[49] = Piece.WPawm;
            board[48] = Piece.WPawm;

            for (int i = 16; i < 48; i++)
            {
                board[i] = Piece.None;
            }

            PlayerTurn = 0b1000;
            castle = 0b1111;
*/
