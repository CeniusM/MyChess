using ChessV1;

namespace PreInitializeDataV2
{
    public unsafe class SlidingPieces
    {
        public const int InvalidMove = int.MinValue;


        public static ulong[] _QueenAttacksBitBoard = new ulong[64]; // all directions
        public static ulong[,] _QueenAttacksBitBoardDirection = new ulong[64, 8]; // specific direction
        public static ulong[] _QueenAttacksBitBoardDirection1d = new ulong[64 * 8]; // specific direction





        public static ulong[] _RookAttacksBitBoard = new ulong[64]; // all directions
        public static ulong[,] _RookAttacksBitBoardDirection = new ulong[64, 8]; // specific direction
        public static ulong[] _RookAttacksBitBoardDirection1d = new ulong[64 * 8]; // specific direction


        public static ulong[] _BishopAttacksBitBoard = new ulong[64]; // all directions
        public static ulong[,] _BishopAttacksBitBoardDirection = new ulong[64, 8]; // specific direction
        public static ulong[] _BishopAttacksBitBoardDirection1d = new ulong[64 * 8]; // specific direction





        public static int[,,] _SlidingpieceAttacks = new int[64, 8, 7];
        public static int[] _SlidingpieceAttacks1D = new int[64 * 8 * 7];


        static SlidingPieces()
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
                            _SlidingpieceAttacks[square, dir, moveCount] = InvalidMove;
                            break;
                        }
                        else if (lindDiff != lineDiffs[dir])
                        {
                            _SlidingpieceAttacks[square, dir, moveCount] = InvalidMove;
                            break;
                        }
                        else
                            _SlidingpieceAttacks[square, dir, moveCount] = move;

                        _QueenAttacksBitBoardDirection[square, dir] |= 0x8000000000000000 >> move;
                        if (dir < 4)
                            _RookAttacksBitBoardDirection[square, dir] |= 0x8000000000000000 >> move;
                        else
                            _BishopAttacksBitBoardDirection[square, dir] |= 0x8000000000000000 >> move;
                    }
                }
            }


            // init SlidingpieceAttacksBitBoard
            for (int square = 0; square < 64; square++)
            {
                for (int i = 0; i < 8; i++)
                {
                    _QueenAttacksBitBoard[square] |= _QueenAttacksBitBoardDirection[square, i];
                    _RookAttacksBitBoard[square] |= _RookAttacksBitBoardDirection[square, i];
                    _BishopAttacksBitBoard[square] |= _BishopAttacksBitBoardDirection[square, i];
                }
            }


            // init 1d arrays for the pointers
            for (int square = 0; square < 64; square++)
            {
                for (int dir = 0; dir < 8; dir++)
                {
                    for (int moveCount = 0; moveCount < 7; moveCount++)
                    {
                        _SlidingpieceAttacks1D[square + (dir * 64) + (moveCount * 512)] = _SlidingpieceAttacks[square, dir, moveCount];
                    }
                }
                for (int dir = 0; dir < 8; dir++)
                {
                    _QueenAttacksBitBoardDirection1d[square + (dir * 64)] = _QueenAttacksBitBoardDirection[square, dir];
                    _RookAttacksBitBoardDirection1d[square + (dir * 64)] = _RookAttacksBitBoardDirection[square, dir];
                    _BishopAttacksBitBoardDirection1d[square + (dir * 64)] = _BishopAttacksBitBoardDirection[square, dir];
                }
            }
        }
    }
}