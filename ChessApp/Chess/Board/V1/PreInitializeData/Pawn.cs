using ChessV1;

namespace PreInitializeData
{
    public class Pawn
    {
        public const int InvalidMove = int.MinValue;
        static Pawn()
        {
            Init();
        }

        public static readonly ulong[,] PawnAttacksBitBoard = new ulong[64, 2]; // square, colour index

        // maby even add, returns true if that pawn is allowed the action
        // public static readonly bool[,] PawnPromotion = new bool[64, 2]; // square, colour index
        // public static readonly bool[,] PawnDoubleMove = new bool[64, 2]; // square, colour index

        private static void Init()
        {
            for (int i = 0; i < 64; i++)
            {
                // white side
                ulong atc = 0;
                if (BoardRepresentation.IsPieceInBound(i - 7))
                    if ((i - 7) >> 3 == (i >> 3) - 1)
                        atc |= ((ulong)1ul << (63 - (i - 7)));
                if (BoardRepresentation.IsPieceInBound(i - 9))
                    if ((i - 9) >> 3 == (i >> 3) - 1)
                        atc |= ((ulong)1ul << (63 - (i - 9)));
                PawnAttacksBitBoard[i, 0] = atc;

                // black side
                atc = 0;
                if (BoardRepresentation.IsPieceInBound(i + 7))
                    if ((i + 7) >> 3 == (i >> 3) + 1)
                        atc |= ((ulong)1ul << (63 - (i + 7)));
                if (BoardRepresentation.IsPieceInBound(i + 9))
                    if ((i + 9) >> 3 == (i >> 3) + 1)
                        atc |= ((ulong)1ul << (63 - (i + 9)));
                PawnAttacksBitBoard[i, 1] = atc;
            }
        }
    }
}