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


        // for less derefrencing and bounds checking this is sinlge dimensinel
        // but it takes in 2* argument, one for start and target square
        // and return true if it could be a valid move, attacks that is

        // start square, target square, colour
        public static readonly bool[] ValidMove = new bool[64 * 64 * 2]; // if white no need to add 4096
        // ValidMove[start + (target * 64) + (ColourIndex * (4.096))]
        // is this faster then
        // ValidMove[start, target, colour]


        // maby even add in pawn black and white seperate to get less derefrencing and bounds checking
        public static readonly int[,,] PawnAttackSquares = new int[64, 2, 2]; // square, colour index, right/left
        public static readonly ulong[,] PawnAttacksBitBoard = new ulong[64, 2]; // square, colour index

        // maby even add, returns true if that pawn is allowed the action
        // public static readonly bool[,] PawnPromotion = new bool[64, 2]; // square, colour index
        // public static readonly bool[,] PawnDoubleMove = new bool[64, 2]; // square, colour index

        private static void Init()
        {
            for (int i = 0; i < 64; i++)
            {
                // bitboard
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


                // normal attack
                // white side
                if (BoardRepresentation.IsPieceInBound(i - 7))
                {
                    if ((i - 7) >> 3 == (i >> 3) - 1)
                    {
                        PawnAttackSquares[i, 0, 0] = i - 7;
                        ValidMove[i + ((i - 7) * 64)] = true;
                    }
                    else
                        PawnAttackSquares[i, 0, 0] = InvalidMove;
                }
                else
                    PawnAttackSquares[i, 0, 0] = InvalidMove;
                if (BoardRepresentation.IsPieceInBound(i - 9))
                {
                    if ((i - 9) >> 3 == (i >> 3) - 1)
                    {
                        PawnAttackSquares[i, 0, 1] = i - 9;
                        ValidMove[i + ((i - 9) * 64)] = true;
                    }
                    else
                        PawnAttackSquares[i, 0, 1] = InvalidMove;
                }
                else
                    PawnAttackSquares[i, 0, 1] = InvalidMove;

                // black side
                if (BoardRepresentation.IsPieceInBound(i + 7))
                {
                    if ((i + 7) >> 3 == (i >> 3) + 1)
                    {
                        PawnAttackSquares[i, 1, 0] = i + 7;
                        ValidMove[i + ((i + 7) * 64) + 4096] = true;
                    }
                    else
                        PawnAttackSquares[i, 1, 0] = InvalidMove;
                }
                else
                    PawnAttackSquares[i, 1, 0] = InvalidMove;
                if (BoardRepresentation.IsPieceInBound(i + 9))
                {
                    if ((i + 9) >> 3 == (i >> 3) + 1)
                    {
                        PawnAttackSquares[i, 1, 1] = i + 9;
                        ValidMove[i + ((i + 9) * 64) + 4096] = true;
                    }
                    else
                        PawnAttackSquares[i, 1, 0] = InvalidMove;
                }
                else
                    PawnAttackSquares[i, 1, 0] = InvalidMove;
            }
        }
    }
}