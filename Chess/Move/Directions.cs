namespace Chess.Moves
{
    public static class DirectionNames
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
        public struct DirectionValues
        {
            public readonly int North;
            public readonly int South;
            public readonly int West;
            public readonly int East;
            public DirectionValues(int North, int South, int West, int East)
            {
                this.North = North;
                this.South = South;
                this.West = West;
                this.East = East;
            }
        };
        public static readonly DirectionValues[] directions = new DirectionValues[64];
        public static readonly int[,] DirectionValuesArr = new int[64, 4];
        static Directions()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int North = j;
                    int South = 7 - j;
                    int West = i;
                    int East = 7 - i;

                    DirectionValuesArr[(i + (j * 8)), DirectionNames.North] = North;
                    DirectionValuesArr[(i + (j * 8)), DirectionNames.East] = East;
                    DirectionValuesArr[(i + (j * 8)), DirectionNames.South] = South;
                    DirectionValuesArr[(i + (j * 8)), DirectionNames.West] = West;
                    directions[i + (j * 8)] = new DirectionValues(North, South, West, East);
                }
            }
        }
    }
}