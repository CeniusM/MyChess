namespace MyChess.ChessBoard
{
    public readonly struct GameStatusFlag
    {
        public const int BlackWin = -1;
        public const int Draw = 0;
        public const int WhiteWin = 1;
        public const int Running = 2;
    }
    public readonly struct CASTLE
    {
        public const int W_King_Side = 0b1000;
        public const int W_Queen_Side = 0b0100;
        public const int B_King_Side = 0b0010;
        public const int B_Queen_Side = 0b0001;
    }

    public struct DataICouldentGetToWork
    {
        public readonly int fullMove;
        public readonly int halfMove;
        public readonly int castle;
        public DataICouldentGetToWork(int Full, int Half, int Castle)
        {
            fullMove = Full;
            halfMove = Half;
            castle = Castle;
        }
    }

    /*
    edge cases
    if pawntwoforward flag is on the capturedpiece will store the enpassent place

    */
    class Board
    {
        public Stack<DataICouldentGetToWork> gameData = new Stack<DataICouldentGetToWork>();
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
        public const int ColorMask = WhiteMask | BlackMask;
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
            for (int i = 0; i < 64; i++) // put in kings first
            {
                if ((Square[i] & Piece.PieceBits) == Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
            for (int i = 0; i < 64; i++)
            {
                if (Square[i] != 0 && (Square[i] & Piece.PieceBits) != Piece.King)
                    piecePoses.AddPieceAtSquare(i);
            }
        }

        public int this[int key]
        {
            get => Square[key];
            set => Square[key] = value;
        }

        /// <summary> works up too 65535 and down too -65472 </summary>
        public static bool IsPieceInBound(int pos) => (pos & 0xFFC0) == 0;
        public static string IntToLetterNum(int pos)
        {
            return (char)((int)'a' + pos % 8) + "" + (char)(8 - (pos >> 3) + '0');
        }



        // move methods need
        // half/fullMove
        //

        public void MakeMove(Move move)
        {
            // halfMove += 1;
            // // if move was a succes, adds a fullmove after black moved
            // if (playerTurn == BlackMask)
            //     fullMove += 1;

            moves.Push(move);
            gameData.Push(new(fullMove, halfMove, castle));



            // can avoid some of these if you dont put them in fx PawnTwoForward, EnPassantCapture, Castle
            if (move.StartSquare == 60 || move.TargetSquare == 60)
                castle ^= CASTLE.W_Queen_Side;
            if (move.StartSquare == 63 || move.TargetSquare == 63)
                castle ^= CASTLE.W_King_Side;
            if (move.StartSquare == 0 || move.TargetSquare == 0)
                castle ^= CASTLE.B_Queen_Side;
            if (move.StartSquare == 7 || move.TargetSquare == 0)
                castle ^= CASTLE.B_King_Side;
            if (move.StartSquare == 4) // move.TargetSqure... bigger problem then castle if the king is there
                castle ^= (CASTLE.B_King_Side & CASTLE.B_Queen_Side);
            if (move.StartSquare == 60)
                castle ^= (CASTLE.W_King_Side & CASTLE.W_Queen_Side);



            if (move.MoveFlag == Move.Flag.None)
            {
                // pawn move or capture
                // if ((Square[move.StartSquare] & Piece.PieceBits) == Piece.Pawn || Square[move.TargetSquare] != 0)
                //     halfMove = 0;

                if (move.CapturedPiece != 0)
                    piecePoses.RemovePieceAtSquare(move.TargetSquare);

                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                Square[move.TargetSquare] = Square[move.StartSquare];
                Square[move.StartSquare] = 0;
                enPassantPiece = 64;
            }
            else if (move.MoveFlag == Move.Flag.PawnTwoForward)
            {
                halfMove = 0;
                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                Square[move.TargetSquare] = Square[move.StartSquare];
                Square[move.StartSquare] = 0;

                if (playerTurn == WhiteMask)
                    enPassantPiece = move.TargetSquare + 8;
                else
                    enPassantPiece = move.TargetSquare - 8;
            }
            else if (move.MoveFlag == Move.Flag.EnPassantCapture)
            {
                halfMove = 0;
                if (playerTurn == WhiteMask)
                {
                    piecePoses.RemovePieceAtSquare(move.TargetSquare + 8);
                    Square[move.StartSquare] = 0;
                    Square[move.TargetSquare] = Piece.WPawn;
                    Square[move.TargetSquare + 8] = 0;
                }
                else
                {
                    piecePoses.RemovePieceAtSquare(move.TargetSquare - 8);
                    Square[move.StartSquare] = 0;
                    Square[move.TargetSquare] = Piece.BPawn;
                    Square[move.TargetSquare - 8] = 0;
                }
                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                enPassantPiece = 64;
            }
            else if (move.MoveFlag == Move.Flag.Castling)
            {
                // // white king side
                // if (move.TargetSquare == 62)
                // {
                //     piecePoses.MovePiece(60, 62);
                //     piecePoses.MovePiece(63, 61);
                //     Square[60] = 0;
                //     Square[61] = Piece.WRook;
                //     Square[62] = Piece.WKing;
                //     Square[63] = 0;
                //     castle ^= (CASTLE.W_King_Side & CASTLE.W_Queen_Side);
                // }







                // enPassantPiece = 64;
            }
            else // promotion
            {
                halfMove = 0;

                if (move.CapturedPiece != 0)
                    piecePoses.RemovePieceAtSquare(move.TargetSquare);

                piecePoses.MovePiece(move.StartSquare, move.TargetSquare);
                Square[move.TargetSquare] = move.PromotionPiece() | playerTurn;
                Square[move.StartSquare] = 0;
                enPassantPiece = 64;
            }

            ChangePlayer();
        }

        public void UnMakeMove()
        {
            // if (fullMove == 0 && playerTurn != BlackMask || moves.Count == 0) // check if we are at the begining
            //     return;
            // else
            //     fullMove--;
            Move move = moves.Pop();
            DataICouldentGetToWork data = gameData.Pop();
            fullMove = data.fullMove;
            halfMove = data.halfMove;
            castle = data.castle;


            ChangePlayer();

            if (move.MoveFlag == Move.Flag.None)
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                if (move.CapturedPiece != 0)
                    piecePoses.AddPieceAtSquare(move.TargetSquare);

                Square[move.StartSquare] = Square[move.TargetSquare];
                Square[move.TargetSquare] = move.CapturedPiece;
            }
            else if (move.MoveFlag == Move.Flag.PawnTwoForward)
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                Square[move.StartSquare] = Square[move.TargetSquare];
                Square[move.TargetSquare] = 0;
                enPassantPiece = 64;
            }
            else if (move.MoveFlag == Move.Flag.EnPassantCapture)
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                Square[move.StartSquare] = Square[move.TargetSquare];
                Square[move.TargetSquare] = 0;
                if (playerTurn == WhiteMask)
                {
                    Square[move.TargetSquare + 8] = Piece.BPawn;
                    piecePoses.AddPieceAtSquare(move.TargetSquare + 8);
                }
                else
                {
                    Square[move.TargetSquare - 8] = Piece.WPawn;
                    piecePoses.AddPieceAtSquare(move.TargetSquare - 8);
                }
                enPassantPiece = move.TargetSquare;
            }
            else if (move.MoveFlag == Move.Flag.Castling)
            {
                // white king side
                if (move.TargetSquare == 62)
                {
                    piecePoses.MovePiece(62, 60);
                    piecePoses.MovePiece(61, 63);
                    Square[60] = Piece.WKing;
                    Square[61] = 0;
                    Square[62] = 0;
                    Square[63] = Piece.WRook;
                }
            }
            else // promotion
            {
                piecePoses.MovePiece(move.TargetSquare, move.StartSquare);
                // if (move.CapturedPiece != 0)
                if (move.CapturedPiece != 0)
                    piecePoses.AddPieceAtSquare(move.TargetSquare);

                Square[move.StartSquare] = Piece.Pawn | playerTurn;
                Square[move.TargetSquare] = move.CapturedPiece;
            }

            if (moves.Count != 0)
                if (moves.Peek().MoveFlag == Move.Flag.PawnTwoForward)
                {
                    if (playerTurn == WhiteMask)
                        enPassantPiece = moves.Peek().TargetSquare - 8;
                    else
                        enPassantPiece = moves.Peek().TargetSquare + 8;
                }
        }

        public void ChangePlayer() => playerTurn ^= ColorMask;
    }
}