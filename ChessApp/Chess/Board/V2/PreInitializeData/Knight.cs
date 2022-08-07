using ChessV1;

namespace PreInitializeDataV2
{
    public class Knight
    {
        public const int InvalidMove = int.MinValue;
        static Knight()
        {
            Init();
        }

        public static readonly int[,] KnightAttacks = new int[64, 8]; // square, direction
        public static readonly int[,] KnightAttacksV2 = new int[64, 8]; // square, direction // refer to king version
        public static readonly ulong[] KnightAttacksBitBoard = new ulong[64]; // square
        private static void Init()
        {
            // KnightAttacks
            void TryMakeMove(int square, int value, int index, int lineDiff)
            {
                if (BoardRepresentation.IsPieceInBound(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        KnightAttacks[square, index] = value;
                    }
                    else
                        KnightAttacks[square, index] = InvalidMove;
                }
                else
                    KnightAttacks[square, index] = InvalidMove;
            }
            for (int i = 0; i < 64; i++)
            {
                TryMakeMove(i, -6, 0, -1);
                TryMakeMove(i, -10, 1, -1);
                TryMakeMove(i, -15, 2, -2);
                TryMakeMove(i, -17, 3, -2);
                TryMakeMove(i, 6, 4, 1);
                TryMakeMove(i, 10, 5, 1);
                TryMakeMove(i, 15, 6, 2);
                TryMakeMove(i, 17, 7, 2);
            }


            // KnightAttacksV2
            void TryMakeMoveV2(int square, int value, int index, int lineDiff)
            {
                if (BoardRepresentation.IsPieceInBound(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        KnightAttacksV2[square, index] = square + value;
                    }
                    else
                        KnightAttacksV2[square, index] = InvalidMove;
                }
                else
                    KnightAttacksV2[square, index] = InvalidMove;
            }
            for (int i = 0; i < 64; i++)
            {
                TryMakeMoveV2(i, -6, 0, -1);
                TryMakeMoveV2(i, -10, 1, -1);
                TryMakeMoveV2(i, -15, 2, -2);
                TryMakeMoveV2(i, -17, 3, -2);
                TryMakeMoveV2(i, 6, 4, 1);
                TryMakeMoveV2(i, 10, 5, 1);
                TryMakeMoveV2(i, 15, 6, 2);
                TryMakeMoveV2(i, 17, 7, 2);
            }


            // KnightAttakcsBitBoard
            void MakeBitBoardMove(ref ulong atc, int square, int value, int index, int lineDiff)
            {
                if (BoardRepresentation.IsPieceInBound(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        atc |= ((ulong)1ul << (63 - (square + value)));
                    }
                }
            }

            for (int i = 0; i < 64; i++)
            {
                ulong atc = 0;

                MakeBitBoardMove(ref atc, i, -6, 0, -1);
                MakeBitBoardMove(ref atc, i, -10, 1, -1);
                MakeBitBoardMove(ref atc, i, -15, 2, -2);
                MakeBitBoardMove(ref atc, i, -17, 3, -2);
                MakeBitBoardMove(ref atc, i, 6, 4, 1);
                MakeBitBoardMove(ref atc, i, 10, 5, 1);
                MakeBitBoardMove(ref atc, i, 15, 6, 2);
                MakeBitBoardMove(ref atc, i, 17, 7, 2);

                KnightAttacksBitBoard[i] = atc;
                // MyLib.DebugConsole.WriteLine("board at square:" + i + '\n' + BitBoardHelper.GetBitBoardString(atc) + '\n');
            }
        }
    }
}