/*
This will be a bunch of 2 or 3d int arrays where you can go here and get all the possible moves for any piece on any given square

so like if you get the kings on square 1 it will have an array {0, 2, 8, 9, 10, -1, 0, 0}

the queen, bisop, rook will have an exstra dimension the specify the direction

so the number for what square it can go to, and the array ends at -1
*/

namespace MyChess.PossibleMoves
{
    class MovesFromSquare
    {
        private static bool isInit = false;
        public const int InvalidMove = int.MinValue;
        public static int[,] KingMoves = new int[64, 8];
        public static int[,] KnightMoves = new int[64, 8];
        public static void Init()
        {
            if (isInit)
                return;
            InitKing();
            InitKnight();

            isInit = true;
        }
        
        public static bool IsInBounds(int place) => (place < 64 && place > -1);

        private static void InitKing()
        {
            void TryMakeMove(int square, int value, int index, int lineDiff)
            {
                if (IsInBounds(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        KingMoves[square, index] = value;
                    }
                    else
                        KingMoves[square, index] = InvalidMove;
                }
                else
                    KingMoves[square, index] = InvalidMove;
            }
            for (int i = 0; i < 64; i++)
            {
                TryMakeMove(i, Directions.Value.North, Directions.Index.North, -1);
                TryMakeMove(i, Directions.Value.NorthEast, Directions.Index.NorthEast, -1);
                TryMakeMove(i, Directions.Value.East, Directions.Index.East, 0);
                TryMakeMove(i, Directions.Value.SouthEast, Directions.Index.SouthEast, 1);
                TryMakeMove(i, Directions.Value.South, Directions.Index.South, 1);
                TryMakeMove(i, Directions.Value.SouthWest, Directions.Index.SouthWest, 1);
                TryMakeMove(i, Directions.Value.West, Directions.Index.West, 0);
                TryMakeMove(i, Directions.Value.NorthWest, Directions.Index.NorthWest, -1);
            }
        }

        private static void InitKnight()
        {
            bool TryMakeMove(int square, int value, int index, int lineDiff)
            {
                if (IsInBounds(square + value))
                {
                    if (((square + value) >> 3) - (square >> 3) == lineDiff)
                    {
                        KnightMoves[square, index] = value;
                    }
                    else
                        KnightMoves[square, index] = InvalidMove;
                }
                else
                    KnightMoves[square, index] = InvalidMove;

                return true;
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
        }
    }
}