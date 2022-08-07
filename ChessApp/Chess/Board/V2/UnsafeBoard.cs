

namespace ChessV2
{
    /// <summary>
    /// this board only works up to move 200, hope it is enough
    /// and can only have a max of 16 pieces on each side
    /// </summary>
    public unsafe class UnsafeBoard
    {
        #region Consts
        public const int MaxMoves = 200;
        public const int WhiteIndex = 0;
        public const int BlackIndex = 1;

        public const int WhiteCastleRights = 0b1100;
        public const int WhiteKingSideCastleRight = 0b1000;
        public const int WhiteQueenSideCastleRight = 0b0100;
        public const int BlackCastleRights = 0b0011;
        public const int BlackKingSideCastleRight = 0b0010;
        public const int BlackQueenSideCastleRight = 0b0001;

        public const int PlayerTurnSwitch = 0b11000;
        #endregion

        // !!! Only public varibles are allowed to be touched ater init !!!


        // Bits 0-3 store castles
        // Bits 4-9 store ep square (64 = nothing)
        // Bits 10-14 captured piece
        // Bits 15-20 startSquare
        // Bits 21-26 targetSquare
        // Bits 27-30 moveFlag
        private int[] gameStateHistory;
        public int* gameStateHistoryPtr;
        public int gameStateHistoryCount = 0;

        private byte[] boardArray;
        public byte* boardPtr;
        public int castle;
        public int EPSquare;
        public int playerTurn;

        int[] kingPosArray;
        public int* kingPosPtr;

        #region everything pieceList
        private PieceList[] pawns;
        private PieceList[] knights;
        private PieceList[] bishops;
        private PieceList[] rooks;
        private PieceList[] queens;
        private PieceList[] allPieceLists;

        public int* whitePawns;
        public int* whiteKnights;
        public int* whiteBishops;
        public int* whiteRooks;
        public int* whiteQueens;
        public int* blackPawns;
        public int* blackKnights;
        public int* blackBishops;
        public int* blackRooks;
        public int* blackQueens;
        #endregion

        #region Constructors
#pragma warning disable 8618
        public UnsafeBoard()
        {
            InitFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }

        public UnsafeBoard(string FEN)
        {
            InitFEN(FEN);
        }
#pragma warning restore 8618
        #endregion

        public void MakeMove(Move move)
        {
            int startSquare = move.StartSquare;
            int targetSquare = move.TargetSquare;
            int moveFlag = move.MoveFlag;
            int movingPiece = boardPtr[startSquare];
            int capturedPiece = boardPtr[targetSquare];
            bool pieceCaptured = (capturedPiece != 0);

            bool WhiteToMove = (playerTurn == 8);
            int OurColour = playerTurn;
            int OurColourIndex = playerTurn >> 4;

            int OurKingPos = kingPosPtr[OurColourIndex];

            gameStateHistoryPtr[gameStateHistoryCount] =
                (castle) + (EPSquare << 4) + (capturedPiece << 10) + (startSquare << 15) + (targetSquare << 21) + (moveFlag << 27);
            gameStateHistoryCount++;
            EPSquare = 64;

            boardPtr[targetSquare] = boardPtr[startSquare];
            boardPtr[startSquare] = 0;

            if (startSquare == OurKingPos)
            {
                if (capturedPiece != 0) // king can never atc king
                    GetPieceList(capturedPiece & 0b00111, capturedPiece & 0b11000).RemovePieceAtSquare(targetSquare);
                else if (moveFlag == Move.Flag.Castling) // cant castle if attacking
                {
                    // also move the rook and change the caltle stuff
                }
                kingPosPtr[OurColourIndex] = targetSquare;
                EPSquare = 0;
                if (WhiteToMove)
                {
                    castle ^= WhiteCastleRights;
                }
                else
                {
                    castle ^= BlackCastleRights;
                }
            }
            // piece move
            else
            {
                if (moveFlag == Move.Flag.None)
                {
                    if (pieceCaptured)
                        GetPieceList(capturedPiece & 0b00111, capturedPiece & 0b11000).RemovePieceAtSquare(targetSquare);
                    GetPieceList(movingPiece & 0b00111, OurColour).MovePiece(startSquare, targetSquare);

                    // remove castle rights
                    if (WhiteToMove)
                    {
                        if (startSquare == 56 || targetSquare == 56)
                            castle ^= WhiteQueenSideCastleRight;
                        else if (startSquare == 63 || targetSquare == 63)
                            castle ^= WhiteKingSideCastleRight;
                    }
                    else
                    {
                        if (startSquare == 0 || targetSquare == 0)
                            castle ^= BlackQueenSideCastleRight;
                        else if (startSquare == 7 || targetSquare == 7)
                            castle ^= BlackKingSideCastleRight;
                    }
                }
                else if (moveFlag == Move.Flag.PawnTwoForward)
                {
                    GetPieceList(movingPiece & 0b00111, OurColour).MovePiece(startSquare, targetSquare);
                    if (WhiteToMove)
                        EPSquare = targetSquare + 8;
                    else
                        EPSquare = targetSquare - 8;
                }
                else if (moveFlag == Move.Flag.EnPassantCapture)
                {
                    GetPieceList(movingPiece & 0b00111, OurColour).MovePiece(startSquare, targetSquare);
                    if (WhiteToMove)
                    {
                        pawns[BlackIndex].RemovePieceAtSquare(targetSquare + 8);
                        boardPtr[targetSquare + 8] = 0;
                    }
                    else
                    {
                        pawns[WhiteIndex].RemovePieceAtSquare(targetSquare - 8);
                        boardPtr[targetSquare - 8] = 0;
                    }
                }
                // casteling is done at king code
                else // promotion
                {
                    if (moveFlag == Move.Flag.PromoteToQueen)
                    {
                        boardPtr[targetSquare] = (byte)(Piece.Queen | OurColour);
                        GetPieceList(Piece.Queen, OurColour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, OurColour).RemovePieceAtSquare(startSquare);
                    }
                    else if (moveFlag == Move.Flag.PromoteToKnight)
                    {
                        boardPtr[targetSquare] = (byte)(Piece.Knight | OurColour);
                        GetPieceList(Piece.Knight, OurColour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, OurColour).RemovePieceAtSquare(startSquare);
                    }
                    else if (moveFlag == Move.Flag.PromoteToRook)
                    {
                        boardPtr[targetSquare] = (byte)(Piece.Rook | OurColour);
                        GetPieceList(Piece.Rook, OurColour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, OurColour).RemovePieceAtSquare(startSquare);
                    }
                    else if (moveFlag == Move.Flag.PromoteToBishop)
                    {
                        boardPtr[targetSquare] = (byte)(Piece.Bishop | OurColour);
                        GetPieceList(Piece.Bishop, OurColour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, OurColour).RemovePieceAtSquare(startSquare);
                    }
                    if (WhiteToMove)
                    {
                        if (targetSquare == 56)
                            castle ^= WhiteQueenSideCastleRight;
                        else if (targetSquare == 63)
                            castle ^= WhiteKingSideCastleRight;
                    }
                    else
                    {
                        if (targetSquare == 0)
                            castle ^= BlackQueenSideCastleRight;
                        else if (targetSquare == 7)
                            castle ^= BlackKingSideCastleRight;
                    }
                }
            }




            playerTurn ^= 0b11000;
        }

        public void UnMakeMove()
        {
            playerTurn ^= 0b11000;
            gameStateHistoryCount--;
            int gameState = gameStateHistoryPtr[gameStateHistoryCount];
            castle = gameState & 0b1111;
            EPSquare = (gameState >> 4) & 0b111111;
            int capturedPiece = (gameState >> 10) & 0b11111;
            int startSquare = (gameState >> 15) & 0b111111;
            int targetSquare = (gameState >> 21) & 0b111111;
            int moveFlag = (gameState >> 27) & 0b111;

            boardPtr[startSquare] = boardPtr[targetSquare];
            boardPtr[targetSquare] = (byte)capturedPiece;
            if (capturedPiece != 0)
                GetPieceList(capturedPiece & 0b00111, capturedPiece & 0b11000).AddPieceAtSquare(targetSquare);

            bool WhiteToMove = (playerTurn == 8);
            int OurColour = playerTurn;
            int OurColourIndex = playerTurn >> 4;

            int OurKingPos = kingPosPtr[OurColourIndex];



            if (targetSquare == OurKingPos)
            {
                if (moveFlag == Move.Flag.Castling)
                {

                }
                kingPosPtr[OurColourIndex] = startSquare;
            }
            else
            {
                GetPieceList(boardPtr[startSquare] & 0b00111, boardPtr[startSquare] & 0b11000).MovePiece(targetSquare, startSquare);
                if (moveFlag == Move.Flag.EnPassantCapture)
                {
                    if (WhiteToMove)
                    {
                        boardPtr[targetSquare + 8] = Piece.WPawn;
                        pawns[WhiteIndex].AddPieceAtSquare(targetSquare - 8);
                    }
                    else
                    {
                        boardPtr[targetSquare + 8] = Piece.BPawn;
                        pawns[BlackIndex].AddPieceAtSquare(targetSquare + 8);
                    }
                }
                else if (moveFlag > 3) // promotion
                {
                    if (moveFlag == Move.Flag.PromoteToQueen)
                    {
                        if (WhiteToMove)
                            boardPtr[startSquare] = Piece.WPawn;
                        else
                            boardPtr[startSquare] = Piece.Black;
                    }
                    else if (moveFlag == Move.Flag.PromoteToKnight)
                    {
                        if (WhiteToMove)
                            boardPtr[startSquare] = Piece.WPawn;
                        else
                            boardPtr[startSquare] = Piece.Black;
                    }
                    else if (moveFlag == Move.Flag.PromoteToRook)
                    {
                        if (WhiteToMove)
                            boardPtr[startSquare] = Piece.WPawn;
                        else
                            boardPtr[startSquare] = Piece.Black;
                    }
                    else if (moveFlag == Move.Flag.PromoteToBishop)
                    {
                        if (WhiteToMove)
                            boardPtr[startSquare] = Piece.WPawn;
                        else
                            boardPtr[startSquare] = Piece.Black;
                    }
                }

            }
        }

        private void InitFEN(string FEN)
        {
            gameStateHistory = new int[MaxMoves];
            fixed (int* ptr = &gameStateHistory[0])
                gameStateHistoryPtr = ptr;

            boardArray = new byte[64];
            fixed (byte* ptr = &boardArray[0])
                boardPtr = ptr;

            kingPosArray = new int[2];
            fixed (int* ptr = &kingPosArray[0])
                kingPosPtr = ptr;

            castle = 0;
            EPSquare = 0;
            playerTurn = 8;

            string[] sections = FEN.Split(' ');
            int fenBoardPtr = 0;
            foreach (char c in sections[0])
            {
                if (c == '/')
                    continue;
                else if (c < 58) // a num
                    fenBoardPtr += c - 49;
                else
                    boardPtr[fenBoardPtr] = PreInitializeDataV1.CharToPiece.GetPiece(c);
                fenBoardPtr++;
            }
            if (sections[1][0] == 'b')
                playerTurn = 16;

            if (sections[2].Contains('K'))
                castle |= 0b1000;
            if (sections[2].Contains('Q'))
                castle |= 0b0100;
            if (sections[2].Contains('k'))
                castle |= 0b0010;
            if (sections[2].Contains('q'))
                castle |= 0b0001;

            if (sections[3][0] != '-')
                EPSquare = sections[3][0] - 'a' + 1;
            else
                EPSquare = 0;

            InitPiecePoses();
        }

        public void InitPiecePoses()
        {
            // init kings
            for (int i = 0; i < 64; i++)
            {
                if (ChessV1.Piece.PieceType(boardPtr[i]) == Piece.King)
                    kingPosPtr[boardPtr[i] >> 4] = i;
            }

            // init PieceLists
            pawns = new PieceList[] { new PieceList(8), new PieceList(8) };
            knights = new PieceList[] { new PieceList(10), new PieceList(10) };
            rooks = new PieceList[] { new PieceList(10), new PieceList(10) };
            bishops = new PieceList[] { new PieceList(10), new PieceList(10) };
            queens = new PieceList[] { new PieceList(9), new PieceList(9) };
            PieceList emptyList = new PieceList(0);
            allPieceLists = new PieceList[] {
                emptyList,
                emptyList,
                pawns[WhiteIndex],
                knights[WhiteIndex],
                emptyList,
                bishops[WhiteIndex],
                rooks[WhiteIndex],
                queens[WhiteIndex],
                emptyList,
                emptyList,
                pawns[BlackIndex],
                knights[BlackIndex],
                emptyList,
                bishops[BlackIndex],
                rooks[BlackIndex],
                queens[BlackIndex],
            };

            for (int i = 0; i < 64; i++)
            {
                int piece = boardPtr[i];
                if (piece == 0 || ChessV1.Piece.IsKing(piece))
                    continue;
                GetPieceList(piece & 0b111, piece & 0b11000).AddPieceAtSquare(i);
            }

            whitePawns = pawns[0].occupiedPtr;
            whiteKnights = knights[0].occupiedPtr;
            whiteBishops = bishops[0].occupiedPtr;
            whiteRooks = rooks[0].occupiedPtr;
            whiteQueens = queens[0].occupiedPtr;
            blackPawns = pawns[1].occupiedPtr;
            blackKnights = knights[1].occupiedPtr;
            blackBishops = bishops[1].occupiedPtr;
            blackRooks = rooks[1].occupiedPtr;
            blackQueens = queens[1].occupiedPtr;
        }
        PieceList GetPieceList(int pieceType, int colour) // dont like this, method call + c# array propety call
        {
            return allPieceLists[(colour >> 4) * 8 + pieceType];
        }
    }
}