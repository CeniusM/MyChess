using ChessV1;

namespace PreInitializeData
{
    public class SlidingPieces
    {
        public const int InvalidMove = int.MinValue;
        static SlidingPieces()
        {
            Init();
        }

        public static readonly int[,,] SlidingpieceAttacks = new int[64, 8, 7];
        public static readonly ulong[] QueenAttacksBitBoard = new ulong[64]; // all directions
        public static readonly ulong[,] QueenAttacksBitBoardDirection = new ulong[64, 8]; // specific direction
        public static readonly ulong[] RookAttacksBitBoard = new ulong[64]; // all directions
        public static readonly ulong[,] RookAttacksBitBoardDirection = new ulong[64, 8]; // specific direction
        public static readonly ulong[] BishopAttacksBitBoard = new ulong[64]; // all directions
        public static readonly ulong[,] BishopAttacksBitBoardDirection = new ulong[64, 8]; // specific direction
        private static void Init()
        {
            int[] lineDiffs =
            {
                -1,
                0,
                1,
                0,
                -1,
                1,
                1,
                -1
            };

            // init SlidingpieceAttacks and 
            // init SlidingpieceAttacksBitBoardDirection
            for (int square = 0; square < 64; square++)
            {
                for (int dir = 0; dir < 8; dir++)
                {
                    int move = square;
                    int lindDiff = 0;
                    for (int moveCount = 0; moveCount < 7; moveCount++)
                    {
                        int temp = move;
                        move += Directions.Value.Indexed[dir];
                        lindDiff = (move >> 3) - (temp >> 3);

                        if (!BoardRepresentation.IsPieceInBound(move))
                        {
                            SlidingpieceAttacks[square, dir, moveCount] = InvalidMove;
                            break;
                        }
                        if (lindDiff != lineDiffs[dir])
                        {
                            SlidingpieceAttacks[square, dir, moveCount] = InvalidMove;
                            break;
                        }

                        SlidingpieceAttacks[square, dir, moveCount] = move;

                        QueenAttacksBitBoardDirection[square, dir] |= 0x8000000000000000 >> move;
                        if (dir < 4)
                            RookAttacksBitBoardDirection[square, dir] |= 0x8000000000000000 >> move;
                        else
                            BishopAttacksBitBoardDirection[square, dir] |= 0x8000000000000000 >> move;
                    }
                }
            }


            // init SlidingpieceAttacksBitBoard
            for (int square = 0; square < 64; square++)
            {
                for (int i = 0; i < 8; i++)
                {
                    QueenAttacksBitBoard[square] |= QueenAttacksBitBoardDirection[square, i];
                    RookAttacksBitBoard[square] |= RookAttacksBitBoardDirection[square, i];
                    BishopAttacksBitBoard[square] |= BishopAttacksBitBoardDirection[square, i];
                }
            }

            // for (int i = 0; i < 64; i++)
            //     MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(BishopAttacksBitBoard[i]));
        }
    }
}