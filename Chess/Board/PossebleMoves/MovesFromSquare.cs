/*
This will be a bunch of 2 or 3d int arrays where you can go here and get all the possible moves for any piece on any given square

so like if you get the kings on square 1 it will have an array {0, 2, 8, 9, 10, -1, 0, 0}

the queen, bisop, rook will have an exstra dimension the specify the direction

so the number for what square it can go to, and the array ends at -1
*/

namespace MyChess.Chess.Board
{
    class MovesFromSquare
    {
        public static int[,] KingMoves = new int[64, 8];
        public static void Init()
        {
            InitKing();
        }

        private static void InitKing()
        {
            for (int i = 0; i < 64; i++)
            {
                int index = 0;
                for (var x = -1; x < 2; x++)
                {
                    for (var y = -1; y < 2; y++)
                    {
                        if (i + x + y > 0 && i + x + y < 64)
                        {
                            KingMoves[i, index] = i + x + y;
                            index++;
                        }
                    }
                }
                if (index < 8)
                    KingMoves[i, index] = -1;
            }
        }
    }
}