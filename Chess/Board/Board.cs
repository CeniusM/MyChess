namespace MyChess.ChessBoard
{
    public readonly struct GameStatusFlag
    {
        public const int BlackWin = -1;
        public const int Draw = 0;
        public const int WhiteWin = 1;
        public const int Running = 2;
    }
    class Board
    {
        /*
        PiecePoses,
        less to loop through

        0 & 1 kings
        2 - 5 bishop
        6 - 9 knights
        10 - 13 rooks
        14 - 29 Pawns
        30 & 31 queens
        */
        public int[] PiecePoses = new int[32];
        public int[] Square = new int[64];

        // W KingSide = 0b1000, W QueenSide = 0b0100, B KingSide = 0b0010, B QueenSide = 0b0001
        public int castle = 0b1111;
        public int enPassantPiece = 64;

        // how many moves both players have made since the last pawn advance or piece capture
        public int halfMove = 0;
        public int fullMove  = 0;
        public int playerTurn = 1; // 1 = white, 2 = black;

        // 2 = running
        // 1 = White Won
        // 0 = Draw
        // -1 = Black Won
        public int GameStatus = GameStatusFlag.Running;

        public int this[int key]
        {
            get => Square[key];
            set => Square[key] = value;
        }

        public void MakeMove(Move move)
        {
            // will be reset after everycapture or pawn move
            halfMove += 1;
            

            // if move was a succes, adds a fullmove after black moved
            if (playerTurn == 2)
                fullMove += 1;

            ChangePlayer();
        }
        
        public void ChangePlayer() => playerTurn ^= 0b11;
    }
}