using ChessV1;

namespace PreInitializeData
{
    public class ConstBitBoards
    {
        public const ulong WhitePromotionLine = 0xFF00000000000000;
        public const ulong WhiteTwoMoveLine = 0xFF00;
        public const ulong BlackPromotionLine = 0xFF;
        public const ulong BlackTwoMoveLine = 0xFF000000000000;

        public const ulong RightSideIs0 = 0b1111111011111110111111101111111011111110111111101111111011111110;
        public const ulong LeftSideIs0 = 0b0111111101111111011111110111111101111111011111110111111101111111;
        // 0111 // mini version
        // 0111
        // 0111
        // 0111
    }
}