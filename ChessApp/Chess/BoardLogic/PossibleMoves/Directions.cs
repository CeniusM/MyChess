namespace MyChess.PossibleMoves
{
    public class Directions
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
        public static int[,] LenghtToSide = new int[64, 8];
        public static void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int North = j;
                    int East = 7 - i;
                    int South = 7 - j;
                    int West = i;
                    int NorthEast = North < East ? North : East;
                    int SouthEast = South < East ? South : East;
                    int SouthWest = South < West ? South : West;
                    int NothWest = North < West ? North : West;

                    LenghtToSide[i + (j << 3), 0] = North;
                    LenghtToSide[i + (j << 3), 1] = East;
                    LenghtToSide[i + (j << 3), 2] = South;
                    LenghtToSide[i + (j << 3), 3] = West;
                    LenghtToSide[i + (j << 3), 4] = NorthEast;
                    LenghtToSide[i + (j << 3), 5] = SouthEast;
                    LenghtToSide[i + (j << 3), 6] = SouthWest;
                    LenghtToSide[i + (j << 3), 7] = NothWest;
                }
            }
        }
    }
}