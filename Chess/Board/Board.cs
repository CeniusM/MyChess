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
        public Stack<Move> moves = new Stack<Move>();
        public PieceList piecePoses = new PieceList();
        public int[] Square = new int[64];

        // W KingSide = 0b1000, W QueenSide = 0b0100, B KingSide = 0b0010, B QueenSide = 0b0001
        public int castle = 0b1111;
        public int enPassantPiece = 64;

        // how many moves both players have made since the last pawn advance or piece capture
        public int halfMove = 0;
        public int fullMove  = 0;

        public const int WhiteMask = 0b00001000;
        public const int BlackMask = 0b00010000;
        const int ColorMask = WhiteMask | BlackMask;
        public int playerTurn = 8; // 8 = white, 16 = black;

        // 2 = running
        // 1 = White Won
        // 0 = Draw
        // -1 = Black Won
        public int GameStatus = GameStatusFlag.Running;

        public Board()
        {
            InitPiecePoses();
        }

        public void InitPiecePoses()
        {
            piecePoses = new PieceList(32);
            for (int i = 0; i < 64; i++)
            {
                if (Square[i] != 0)
                    piecePoses.AddPieceAtSquare(i);
            }
        }

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
            if (playerTurn == BlackMask)
                fullMove += 1;

            ChangePlayer();
        }
        
        public void ChangePlayer() => playerTurn ^= ColorMask;
    }
}