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
        public static void Init()
        {
            if (isInit)
                return;
            InitKing();

            isInit = true;
        }
        
        public static bool IsInBounds(int place) => (place < 64 && place > -1);

        private static void InitKing()
        {
            
            bool MakeMove(int square, int value, int index)
            {
                if (IsInBounds(square + value))
                {
                    if (true) 
                    {
                        throw new NotImplementedException("Needs to check if the king goes to the other side of the board");
                        KingMoves[square, index] = value;
                    }
                    else
                        KingMoves[square, index] = InvalidMove;
                }
                else
                    KingMoves[square, index] = InvalidMove;

                return true;
            }

            for (int i = 0; i < 64; i++)
            {
                if (!MakeMove(i, Directions.Value.North, Directions.Index.North))
                    continue;
                if (!MakeMove(i, Directions.Value.NorthEast, Directions.Index.NorthEast))
                    continue;
                if (!MakeMove(i, Directions.Value.East, Directions.Index.East))
                    continue;
                if (!MakeMove(i, Directions.Value.SouthEast, Directions.Index.SouthEast))
                    continue;
                if (!MakeMove(i, Directions.Value.South, Directions.Index.South))
                    continue;
                if (!MakeMove(i, Directions.Value.SouthWest, Directions.Index.SouthWest))
                    continue;
                if (!MakeMove(i, Directions.Value.West, Directions.Index.West))
                    continue;
                if (!MakeMove(i, Directions.Value.NorthWest, Directions.Index.NorthWest))
                    continue;
            }

        }
    }
}