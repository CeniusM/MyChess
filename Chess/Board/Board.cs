namespace Chess.ChessBoard
{
    public static class Piece
    {
        public const int None = 0;
        public const int Pawm = 1;
        public const int Rook = 2;
        public const int Knight = 3;
        public const int Bishop = 4;
        public const int Queen = 5;
        public const int King = 6;
        public const int White = 8;
        public const int Black = 16;
    }

    class Board
    {
        public int[] board = new int[64];
        public int castle = 0;
        public Board()
        {

        }
        public Board(int[] board, int castle)
        {

        }
        public Board(string FENboard)
        {

        }
    }
}
/*
the bits in the board int is sorted like this, 0 - 7
0 - 2 = indecates the piece value
3 && 4= indecates the color

the rest from 31-5     01       010
idk yet              | white | What peicetype it is

int = 137, tror jeg

-----

The castle works so the bits say if you can castle or not... will be implementet later
0000

pawn   = 1, 001
rook   = 2, 010
knight = 3, 011
bishop = 4, 100
queen  = 5, 101
king   = 6, 110

white  = 8, 01 000
black  = 16,10 000
*/ 