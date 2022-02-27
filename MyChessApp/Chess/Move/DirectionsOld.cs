namespace Chess.Moves
{
    public static class DirectionValues
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

                    DirectionValuesArr[(i + (j * 8)), 0] = North;
                    DirectionValuesArr[(i + (j * 8)), 1] = East;
                    DirectionValuesArr[(i + (j * 8)), 2] = South;
                    DirectionValuesArr[(i + (j * 8)), 3] = West;
                    directions[i + (j * 8)] = new DirectionValues(North, South, West, East);
                }
            }
        }
    }
}