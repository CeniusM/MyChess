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

        public static int[,,] SlidingpieceAttacks = new int[64, 8, 7];
        public static int[,] SlidingpieceAttacksBitBoardDirection = new int[64, 8]; // specific direction
        public static int[] SlidingpieceAttacksBitBoard = new int[64]; // all directions
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

            // init SlidingpieceAttacks
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
                    }
                }
            }

            // init SlidingpieceAttacksBitBoardDirection

            // init SlidingpieceAttacksBitBoard
        }
    }
}