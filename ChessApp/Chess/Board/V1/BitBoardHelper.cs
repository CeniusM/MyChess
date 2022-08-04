

namespace ChessV1
{
    public class BitBoardHelper
    {
        // later implement this in all the places cause i dont know if the get lowerd so it dosent do a method call
        public static bool ContainsSquare(ulong bitboard, int square) 
        {
            return ((bitboard << square) & 0x8000000000000000) == 0x8000000000000000;
        }

        public static string GetBitBoardString(ulong bitboard, string frontPadding = "", string backPadding = "")
        {
            string str = "";
            for (int i = 8 - 1; i >= 0; i--)
            {
                byte b = (byte)((bitboard & ((ulong)0b11111111 << (i * 8))) >> (i * 8));
                str += frontPadding + Convert.ToString(b, 2).PadLeft(8, '0') + backPadding + "\n";
            }
            return str;
        }
    }
}