

namespace ChessV1
{
    public class BitBoardHelper
    {
        public static bool ContainsSquare(ulong bitboard, int square)
        {
            return ((bitboard >> square) & 1) != 0;
        }

        public static string GetBitBoardString(ulong bitboard)
        {
            string str = "";
            for (int i = 8 - 1; i >= 0; i--)
            {
                byte b = (byte)((bitboard & ((ulong)0b11111111 << (i * 8))) >> (i * 8));
                str += Convert.ToString(b, 2).PadLeft(8, '0') + "\n";
            }
            return str;
        }
    }
}