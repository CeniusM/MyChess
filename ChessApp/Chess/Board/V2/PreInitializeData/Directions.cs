using ChessV1;

namespace PreInitializeDataV2
{
    public unsafe class Directions
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
            static Value()
            {
                fixed (int* ptr = &_Indexed[0])
                    Indexed = ptr;
            }
            private static readonly int[] _Indexed = { North, East, South, West, NorthEast, SouthEast, SouthWest, NorthWest };
            public static readonly int* Indexed;
            public const int North = -8;
            public const int East = 1;
            public const int South = 8;
            public const int West = -1;
            public const int NorthEast = -7;
            public const int SouthEast = 9;
            public const int SouthWest = 7;
            public const int NorthWest = -9;
        }
    }
}