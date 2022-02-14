namespace Chess.Moves.V2 // V2 neededs to be removed
{
    public static class DirectionsIndex
    {
        public const int North = 0;
        public const int East = 1;
        public const int South = 2;
        public const int West = 3;
        public const int NorthEast = 4;
        public const int SouthEast = 5;
        public const int SouthWest = 6;
        public const int NothWest = 7;
    }
    class Directions
    {
        public static readonly int[] DirectionOffSets =
        {
            -8,
            1,
            8,
            -1
            -7,
            9,
            7,
            -9
        };
        public static readonly int[,] DirectionValues = new int[64, 8]; // HUUUUUUUUUUHH?????????
        static Directions()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int North = j;
                    int South = 7 - j;
                    int East = 7 - i;
                    int West = i;

                    DirectionValues[i + (j << 3), DirectionsIndex.North] = North;
                    DirectionValues[i + (j << 3), DirectionsIndex.South] = South;
                    DirectionValues[i + (j << 3), DirectionsIndex.East] = East;
                    DirectionValues[i + (j << 3), DirectionsIndex.West] = West;

                    // DirectionValuesArr[(i + (j * 8)), DirectionNames.North] = North;
                    // DirectionValuesArr[(i + (j * 8)), DirectionNames.East] = East;
                    // DirectionValuesArr[(i + (j * 8)), DirectionNames.South] = South;
                    // DirectionValuesArr[(i + (j * 8)), DirectionNames.West] = West;
                }
            }
        }
    }
}