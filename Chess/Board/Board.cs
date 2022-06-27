namespace MyChess.ChessBoard
{
    public readonly struct GameStatusFlag
    {
        public const int BlackWin = -1;
        public const int Draw = 0;
        public const int WhiteWin = 1;
        public const int Running = 2;
    }

    /*
    edge cases
    if pawntwoforward flag is on the capturedpiece will store the enpassent place

    */
    class Board
    {
        public Stack<Move> moves = new Stack<Move>();
        public PieceList piecePoses = new PieceList();
        public int[] Square = new int[64];

        // W KingSide = 0b1000, W QueenSide = 0b0100, B KingSide = 0b0010, B QueenSide = 0b0001
        public int castle = 0b1111;
        public int enPassantPiece = 64;

        // how many moves both players have made since the last pawn advance or piece capture
        public int halfMove = 0;
        public int fullMove = 0;

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

        /// <summary> works up too 65535 and down too -65472 </summary>
        public static bool IsPieceOutOfBounce(int pos) => (pos & 0xFFC0) != 0;

        public void MakeMove(Move move)
        {
            // will be reset after everycapture or pawn move
            halfMove += 1;


            // if move was a succes, adds a fullmove after black moved
            if (playerTurn == BlackMask)
                fullMove += 1;


            if (Square[move.TargetSquare] != 0)
                piecePoses.RemovePieceAtSquare(move.TargetSquare);
            piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
            int temp = Square[move.TargetSquare];
            Square[move.TargetSquare] = Square[move.StartSquare];
            Square[move.StartSquare] = 0;
            moves.Push(new(move.StartSquare, move.TargetSquare, move.MoveFlag, temp));


            ChangePlayer();
        }

        public void UnMakeMove()
        {
            if (fullMove == 0 && playerTurn != BlackMask) // check if we are at the begining
                return;
            ChangePlayer();
            Move move = moves.Pop();

            piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
            piecePoses.AddPieceAtSquare(move.TargetSquare);

            Square[move.StartSquare] = Square[move.TargetSquare];
            Square[move.TargetSquare] = move.CapturedPiece;

            if (move.MoveFlag == Move.Flag.PawnTwoForward)
            {
                //enPassantPiece = move.TargetSquare;
            }
            else if (move.MoveFlag == Move.Flag.EnPassantCapture)
                enPassantPiece = move.TargetSquare;



        }

        public void ChangePlayer() => playerTurn ^= ColorMask;
    }
}