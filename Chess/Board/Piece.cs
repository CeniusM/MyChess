namespace Chess.ChessBoard
{
    public static class Piece
    {
        public const int ColorBits = 24;
        public const int PieceBits = 7;
        public const int None = 0;
        public const int Pawm = 1;
        public const int Rook = 2;
        public const int Knight = 3;
        public const int Bishop = 4;
        public const int Queen = 5;
        public const int King = 6;
        public const int White = 8;
        public const int Black = 16;
        public const int BPawm = 1 + Black;
        public const int BRook = 2 + Black;
        public const int BKnight = 3 + Black;
        public const int BBishop = 4 + Black;
        public const int BQueen = 5 + Black;
        public const int BKing = 6 + Black;
        public const int WPawm = 1 + White;
        public const int WRook = 2 + White;
        public const int WKnight = 3 + White;
        public const int WBishop = 4 + White;
        public const int WQueen = 5 + White;
        public const int WKing = 6 + White;
    }
}