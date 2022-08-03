// il add in fithy move count rule, repetetion and half move rule in later

namespace ChessV1
{
    public unsafe class UnsafeBoard
    {
        public const int WhiteIndex = 0;
        public const int BlackIndex = 1;

        // --- by Sebastian Lague ---
        // Bits 0-3 store castles
        // Bits 4-7 store file of ep square (starting at 1, so 0 = no ep square)
        // Bits 8-13 captured piece
        // Bits 14-... fifty mover counter
        public Stack<uint> gameStateHistory = new Stack<uint>(100);

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

        public byte[] square = new byte[64];
        public int castle = 0b1111;
        public int EPFile = 0;
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
        public void MakeMove(Move move)
        {
            // move
            int startSquare = move.StartSquare;
            int targetSquare = move.TargetSquare;
            int moveFlag = move.MoveFlag;

            // piece
            int moveingPieceType = square[startSquare] & 0b00111;
            int moveingPieceColour = square[startSquare] & 0b11000;
            int capturedPiece = square[targetSquare];

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
            gameStateHistory.Push((uint)((castle) | (EPFile << 4) | (capturedPiece << 8)));

            // King Move
            if (startSquare == ourKingpPos)
            {
                ourKingpPos = targetSquare;
                if (capturedPiece != 0) // king can never atc king
                    GetPieceList(capturedPiece).RemovePieceAtSquare(targetSquare);
                EPFile = 0;
                if (whiteToMove)
                    castle &= 0b0011;
                else
                    castle &= 0b1100;
            }

            // Peice move
            else
            {
                if (moveFlag == Move.Flag.PawnTwoForward)
                {
                    GetPieceList(capturedPiece).MovePiece(startSquare, targetSquare);
                    EPFile = startSquare % 8 + 1;
                }

            }

            ChangePlayer();
        }

        public void UnMakeMove()
        {

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
                EPFile = sections[3][0] - 'a' + 1;
            else
                EPFile = 0;

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