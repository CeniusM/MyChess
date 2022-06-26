namespace MyChess.PossibleMoves
{
    class Directions
    {
        public readonly struct Index
        {
            public const int North = 0;
            public const int East = 2;
            public const int South = 4;
            public const int West = 6;
            public const int NorthEast = 1;
            public const int SouthEast = 3;
            public const int SouthWest = 5;
            public const int NorthWest = 7;
        }
        public readonly struct Value
        {
            public static readonly int[] Indexed = { North, East, South, West, NorthEast, SouthEast, SouthWest, NorthWest };
            public const int North = -8;
            public const int East = 1;
            public const int South = 8;
            public const int West = -1;
            public const int NorthEast = -7;
            public const int SouthEast = 9;
            public const int SouthWest = 7;
            public const int NorthWest = -9;
        }

        //                                          Square, Directions
        public static int[,] LenghtToSide = new int[64, 8];
        public static void Init()
        {

        }
    }
}