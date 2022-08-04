using ChessV1;

namespace PreInitializeData
{
    public class BoardDistances
    {
        static BoardDistances()
        {
            Init();
        }

        public static readonly int[] DistanceToNearstEdge =
        {
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 1, 1, 1, 1, 1, 1, 0,
            0, 1, 2, 2, 2, 2, 1, 0,
            0, 1, 2, 3, 3, 2, 1, 0,
            0, 1, 2, 3, 3, 2, 1, 0,
            0, 1, 2, 2, 2, 2, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
        };

        public static readonly int[,] DistanceToEdge = new int[64, 8]; // square, direction
        private static void Init()
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

                    DistanceToEdge[i + (j << 3), 0] = North;
                    DistanceToEdge[i + (j << 3), 1] = East;
                    DistanceToEdge[i + (j << 3), 2] = South;
                    DistanceToEdge[i + (j << 3), 3] = West;
                    DistanceToEdge[i + (j << 3), 4] = NorthEast;
                    DistanceToEdge[i + (j << 3), 5] = SouthEast;
                    DistanceToEdge[i + (j << 3), 6] = SouthWest;
                    DistanceToEdge[i + (j << 3), 7] = NothWest;
                }
            }
        }
    }
}