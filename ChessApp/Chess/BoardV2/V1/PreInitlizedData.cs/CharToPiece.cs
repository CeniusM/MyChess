using Chess;

namespace PreInitlizedData
{
    public struct CharToPiece
    {
        public static byte GetPiece(char c) => data[c - 'A'];
        private static readonly byte[] data =
        {
            0, // 'A'
            Piece.WBishop, // 'B'
            0, // 'C'
            0, // 'D'
            0, // 'E'
            0, // 'F'
            0, // 'G'
            0, // 'H'
            0, // 'I'
            0, // 'J'
            Piece.WKing, // 'K'
            0, // 'L'
            0, // 'M'
            Piece.WKnight, // 'N'
            0, // 'O'
            Piece.WPawn, // 'P'
            Piece.WQueen, // 'Q'
            Piece.WRook, // 'R'
            0, // 'S'
            0, // 'T'
            0, // 'U'
            0, // 'V'
            0, // 'W'
            0, // 'X'
            0, // 'Y'
            0, // 'Z'
            0, // '['
            0, // '\'
            0, // ']'
            0, // '^'
            0, // '_'
            0, // '`'
            0, // 'a'
            Piece.BBishop, // 'b'
            0, // 'c'
            0, // 'd'
            0, // 'e'
            0, // 'f'
            0, // 'g'
            0, // 'h'
            0, // 'i'
            0, // 'j'
            Piece.BKing, // 'k'
            0, // 'l'
            0, // 'm'
            Piece.BKnight, // 'n'
            0, // 'o'
            Piece.BPawn, // 'p'
            Piece.BQueen, // 'q'
            Piece.BRook, // 'r'
            0, // 's'
            0, // 't'
            0, // 'u'
            0, // 'v'
            0, // 'w'
            0, // 'x'
            0, // 'y'
            0  // 'z'
        };
    }
}