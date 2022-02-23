namespace Chess.Moves.V2 // V2 neededs to be removed
{
    class Directions
    {
        public static class DirectionOffSets
        {
            public const int North = -8;
            public const int East = 1;
            public const int South = 8;
            public const int West = -1;
            public const int NorthEast = -7;
            public const int SouthEast = 9;
            public const int SouthWest = 7;
            public const int NothWest = -9;
        }
        public static readonly int[,] DirectionValues = new int[64, 8];
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
                    int NorthEast = North < East ? North : East;
                    int SouthEast = South < East ? South : East;
                    int SouthWest = South < West ? South : West;
                    int NothWest = North < West ? North : West;

                    DirectionValues[i + (j << 3), 0] = North;
                    DirectionValues[i + (j << 3), 1] = South;
                    DirectionValues[i + (j << 3), 2] = East;
                    DirectionValues[i + (j << 3), 3] = West;
                    DirectionValues[i + (j << 3), 4] = NorthEast;
                    DirectionValues[i + (j << 3), 5] = SouthEast;
                    DirectionValues[i + (j << 3), 6] = SouthWest;
                    DirectionValues[i + (j << 3), 7] = NothWest;
                }
            }
        }
    }
}