// ---code by Sebastian---


namespace ChessV2
{
    public static class Piece
    {

        public const int None = 0;
        public const int King = 1;
        public const int Pawn = 2;
        public const int Knight = 3;
        public const int Bishop = 5;
        public const int Rook = 6;
        public const int Queen = 7;

        public const int WKing = King + White;
        public const int WPawn = Pawn + White;
        public const int WKnight = Knight + White;
        public const int WBishop = Bishop + White;
        public const int WRook = Rook + White;
        public const int WQueen = Queen + White;

        public const int BKing = King + Black;
        public const int BPawn = Pawn + Black;
        public const int BKnight = Knight + Black;
        public const int BBishop = Bishop + Black;
        public const int BRook = Rook + Black;
        public const int BQueen = Queen + Black;

        public const int White = 8;
        public const int Black = 16;

        public const int typeMask = 0b00111;
        public const int blackMask = 0b10000;
        public const int whiteMask = 0b01000;
        public const int colourMask = whiteMask | blackMask;
    }
}

/*
King	= 0b001
Pawn	= 0b010
Knight	= 0b011
Bishop	= 0b101
Rook	= 0b110
Queen	= 0b111

White   = 0b01000
Black   = 0b10000
*/