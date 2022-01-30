namespace Chess.ChessBoard
{
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
3 = indecates the color

7 = indecates if its selected

the rest from 31-5    1                  1       010
idk yet             | selected = true  | white | What peicetype it is

int = 137, tror jeg

-----

The castle works so the bits say if you can castle or not... will be implementet later
0000
*/ 