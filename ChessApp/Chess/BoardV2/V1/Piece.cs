// ---code by Sebastian---


namespace ChessV1
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

        const int typeMask = 0b00111;
        const int blackMask = 0b10000;
        const int whiteMask = 0b01000;
        const int colourMask = whiteMask | blackMask;

        public static bool IsColour(int piece, int colour)
        {
            return (piece & colourMask) == colour;
        }

        public static int Colour(int piece)
        {
            return piece & colourMask;
        }

        public static int PieceType(int piece)
        {
            return piece & typeMask;
        }

        public static bool IsRookOrQueen(int piece)
        {
            return (piece & 0b110) == 0b110;
        }

        public static bool IsBishopOrQueen(int piece)
        {
            return (piece & 0b101) == 0b101;
        }

        public static bool IsSlidingPiece(int piece)
        {
            return (piece & 0b100) != 0;
        }
    }
}