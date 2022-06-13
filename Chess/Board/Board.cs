namespace MyChess.ChessBoard
{
    class Board
    {
        /*
        PiecePoses,
        less to loop through

        0 & 1 kings
        2 - 5 bishop
        6 - 9 knights
        10 - 13 rooks
        14 - 29 pawns
        30 & 31 queens
        */        
        public int[] PiecePoses = new int[32];
        public int[] Square = new int[64];
        public int castle = 0b1111;
        public int enPassantPiece = 64;
        public int playerTurn = 1; // 1 = white, 2 = black;

        public int this[int key]
        {
            get => Square[key];
            //set => Square[key] = value;
            set{}
        }


        public void ChangePlayer() => playerTurn ^= 0b11;
    }
}