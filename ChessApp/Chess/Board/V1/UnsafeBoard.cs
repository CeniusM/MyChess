// il add in fithy move count rule, repetetion and half move rule in later
// then the moves from gameStateHistory probely need to go in their own stack

namespace ChessV1
{
    public unsafe class UnsafeBoard
    {
        #region Consts
        public const int WhiteIndex = 0;
        public const int BlackIndex = 1;

        public const int WhiteCastleRights = 0b1100;
        public const int WhiteKingSideCastleRight = 0b1000;
        public const int WhiteQueenSideCastleRight = 0b0100;
        public const int BlackCastleRights = 0b0011;
        public const int BlackKingSideCastleRight = 0b0010;
        public const int BlackQueenSideCastleRight = 0b0001;
        #endregion

        // --- by Sebastian Lague ---
        // Bits 0-3 store castles
        // Bits 4-9 store ep square (64 = nothing)
        // Bits 10-14 captured piece
        // Bits 15-20 startSquare
        // Bits 21-26 targetSquare
        // Bits 27-30 moveFlag
        // Bit 31 free <3 and technecly add 1 more from ep square
        public Stack<int> gameStateHistory = new Stack<int>(100);

        #region BoardInfo

        #region PieceLists
        public int[] kingPos = new int[2];
        public PieceList[] pawns;
        public PieceList[] knights;
        public PieceList[] bishops;
        public PieceList[] rooks;
        public PieceList[] queens;
        public PieceList[] allPieceLists;
        PieceList GetPieceList(int piece) => GetPieceList(Piece.PieceType(piece), Piece.Colour(piece));
        PieceList GetPieceList(int pieceType, int colour)
        {
            return allPieceLists[(colour >> 4) * 8 + pieceType];
        }
        #endregion

        public byte* squarePtr;
        public byte[] square = new byte[64];
        public int castle = 0b1111;
        public int EPSquare = 0;
        public int playerTurn = 8; // 8 = white, 16 = black;
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

        // this board will not have to check or validate the Move getting send in
        // it will allways asume the move is valid and will probly crash if not...
        public void MakeMove(Move move) // needs castle and promotions
        {
            // move
            int startSquare = move.StartSquare;
            int targetSquare = move.TargetSquare;
            int moveFlag = move.MoveFlag;

            // piece
            int movingPieceType = square[startSquare] & 0b00111;
            int movingPieceColour = square[startSquare] & 0b11000;
            int capturedPiece = square[targetSquare];
            bool pieceCaptured = capturedPiece != 0; // exluting enpasent capture

            // colours
            int colour = playerTurn;
            int colourIndex = playerTurn >> 4;
            int opponentColour = playerTurn ^ 0b11000;
            int opponentColourIndex = opponentColour >> 4;
            bool whiteToMove = playerTurn == 8;

            // king
            int ourKingpPos = kingPos[colourIndex];
            int opponentKingPos = kingPos[opponentColourIndex];

            // push GameStateHistory
            gameStateHistory.Push(((castle) | (EPSquare << 4) | (capturedPiece << 10) | (startSquare << 15) | (targetSquare << 21) | (moveFlag << 27)));
            EPSquare = 0;

            square[targetSquare] = square[startSquare];
            square[startSquare] = 0;

            // King Move
            if (startSquare == ourKingpPos)
            {
                if (capturedPiece != 0) // king can never atc king
                    GetPieceList(capturedPiece).RemovePieceAtSquare(targetSquare);
                else if (moveFlag == Move.Flag.Castling) // cant castle if attacking
                {
                    // also move the rook and change the caltle stuff
                }
                kingPos[colourIndex] = targetSquare;
                EPSquare = 0;
                if (whiteToMove)
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
                        GetPieceList(capturedPiece).RemovePieceAtSquare(targetSquare);
                    GetPieceList(movingPieceType, movingPieceColour).MovePiece(startSquare, targetSquare);

                    // remove castle rights
                    if (whiteToMove)
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
                    GetPieceList(movingPieceType, movingPieceColour).MovePiece(startSquare, targetSquare);
                    if (whiteToMove)
                        EPSquare = targetSquare + 8;
                    else
                        EPSquare = targetSquare - 8;
                }
                else if (moveFlag == Move.Flag.EnPassantCapture)
                {
                    GetPieceList(movingPieceType, movingPieceColour).MovePiece(startSquare, targetSquare);
                    if (whiteToMove)
                        pawns[BlackIndex].RemovePieceAtSquare(targetSquare + 8);
                    else
                        pawns[WhiteIndex].RemovePieceAtSquare(targetSquare - 8);
                }
                // casteling is done at king code
                else // promotion
                {
                    if (moveFlag == Move.Flag.PromoteToQueen)
                    {
                        square[targetSquare] = (byte)(Piece.Queen | colour);
                        GetPieceList(Piece.Queen, colour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, colour).RemovePieceAtSquare(startSquare);
                    }
                    else if (moveFlag == Move.Flag.PromoteToKnight)
                    {
                        square[targetSquare] = (byte)(Piece.Knight | colour);
                        GetPieceList(Piece.Knight, colour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, colour).RemovePieceAtSquare(startSquare);
                    }
                    else if (moveFlag == Move.Flag.PromoteToRook)
                    {
                        square[targetSquare] = (byte)(Piece.Rook | colour);
                        GetPieceList(Piece.Rook, colour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, colour).RemovePieceAtSquare(startSquare);
                    }
                    else if (moveFlag == Move.Flag.PromoteToBishop)
                    {
                        square[targetSquare] = (byte)(Piece.Bishop | colour);
                        GetPieceList(Piece.Bishop, colour).AddPieceAtSquare(targetSquare);
                        GetPieceList(Piece.Pawn, colour).RemovePieceAtSquare(startSquare);
                    }
                    if (whiteToMove)
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

            ChangePlayer();
        }

        public void UnMakeMove()
        {
            ChangePlayer();
            int gameState = gameStateHistory.Pop();
            castle = gameState & 0b1111;
            EPSquare = (gameState >> 4) & 0b111111;
            int capturedPiece = (gameState >> 10) & 0b11111;
            int startSquare = (gameState >> 15) & 0b111111;
            int targetSquare = (gameState >> 21) & 0b111111;
            int moveFlag = (gameState >> 27) & 0b111;

            square[startSquare] = square[targetSquare];
            square[targetSquare] = (byte)capturedPiece;
            if (capturedPiece != 0)
                GetPieceList(capturedPiece).AddPieceAtSquare(targetSquare);

            // colour
            bool isWhite = (playerTurn ^ 011000) == 8;
            int colourIndex = playerTurn >> 4;

            // pieces
            int ourKingpPos = kingPos[colourIndex];

            if (targetSquare == ourKingpPos)
            {
                if (moveFlag == Move.Flag.Castling)
                {

                }
                kingPos[colourIndex] = startSquare;
            }
            else
            {
                GetPieceList(square[startSquare]).MovePiece(targetSquare, startSquare);
                if (moveFlag == Move.Flag.EnPassantCapture)
                {
                    if (isWhite)
                    {
                        square[targetSquare + 8] = Piece.WPawn;
                        pawns[WhiteIndex].AddPieceAtSquare(targetSquare - 8);
                    }
                    else
                    {
                        square[targetSquare + 8] = Piece.BPawn;
                        pawns[BlackIndex].AddPieceAtSquare(targetSquare + 8);
                    }
                }
                else if (moveFlag > 3) // promotion
                {
                    if (moveFlag == Move.Flag.PromoteToQueen)
                    {
                        if (isWhite)
                            square[startSquare] = Piece.WPawn;
                        else
                            square[startSquare] = Piece.Black;
                    }
                    else if (moveFlag == Move.Flag.PromoteToKnight)
                    {
                        if (isWhite)
                            square[startSquare] = Piece.WPawn;
                        else
                            square[startSquare] = Piece.Black;
                    }
                    else if (moveFlag == Move.Flag.PromoteToRook)
                    {
                        if (isWhite)
                            square[startSquare] = Piece.WPawn;
                        else
                            square[startSquare] = Piece.Black;
                    }
                    else if (moveFlag == Move.Flag.PromoteToBishop)
                    {
                        if (isWhite)
                            square[startSquare] = Piece.WPawn;
                        else
                            square[startSquare] = Piece.Black;
                    }
                }
            }
        }


        public void ChangePlayer() => playerTurn ^= Piece.colourMask;
        public void InitFEN(string FEN)
        {
            string[] sections = FEN.Split(' ');
            int boardPtr = 0;
            foreach (char c in sections[0])
            {
                if (c == '/')
                    continue;
                else if (c < 58) // a num
                    boardPtr += c - 49;
                else
                    square[boardPtr] = PreInitializeData.CharToPiece.GetPiece(c);
                boardPtr++;
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

            fixed (byte* ptr = &square[0])
                squarePtr = ptr;

            InitPiecePoses();
        }

        public void InitPiecePoses()
        {
            // init kings
            for (int i = 0; i < 64; i++)
            {
                if (Piece.PieceType(square[i]) == Piece.King)
                    kingPos[Piece.Colour(square[i]) >> 4] = i;
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
                int piece = square[i];
                if (piece == 0 || Piece.IsKing(piece))
                    continue;
                GetPieceList(piece).AddPieceAtSquare(i);
            }
        }

        public unsafe override int GetHashCode()
        {
            // note, remeber to test it to see how many collisions acure and speed

            // zobist hashing
            // https://en.wikipedia.org/wiki/Zobrist_hashing
            // fixed (byte* ptr = &square[0])
            // {

            // }

            throw new NotImplementedException("Hashing have not been implementet");
        }
    }
}