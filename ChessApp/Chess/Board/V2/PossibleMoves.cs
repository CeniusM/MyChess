using PreInitializeDataV2;

namespace ChessV2
{
    public unsafe class PossibleMovesGenerator
    {
        /* contains square
((bitboard << square) & 0x8000000000000000) == 0x8000000000000000


in bounds

(pos & 0xFFC0) == 0

        */
        UnsafeBoard boardRef;

        byte* boardPtr;
        public int Calste;
        public int EPSquare;

        Move[] moves;
        int movesCount;



        ulong[] pinningPiecesAttackDirBitBoard = new ulong[64]; // the square of the pinned piece
        ulong pinnedPieces = 0;
        ulong* pinningPiecesAttackDirBitBoardPtr;


        bool _KingInCheck = false;
        bool _DoubleCheck = false;



        ulong attackerOnKingsBitBoardDirectionAttack;
        int attackerOnKingPos;


        bool WhiteToMove;
        int OurColour;
        int EnemyColour;

        int OurKingPos;
        int EnemyKingPos;

        int* OurPawns;
        int* OurKnights;
        int* OurBishops;
        int* OurRooks;
        int* OurQueens;
        int* EnemyPawns;
        int* EnemyKnights;
        int* EnemyBishops;
        int* EnemyRooks;
        int* EnemyQueens;


        /// <summary>
        /// (square + (dir * 64) + (moveCount * 512))
        /// </summary>
        public static int* SlidingpieceAttacks;

        /// <summary>
        /// (square)
        /// </summary>
        ulong* QueenAttacksBitBoardPtr;

        /// <summary>
        /// (square + (dir * 64))
        /// </summary>
        public ulong* QueenAttacksBitBoardDirectionPtr;

        /// <summary>
        /// (square)
        /// </summary>
        ulong* RookAttacksBitBoardPtr;

        /// <summary>
        /// (square + (dir * 64))
        /// </summary>
        public ulong* RookAttacksBitBoardDirectionPtr;

        /// <summary>
        /// (square)
        /// </summary>
        ulong* BishopAttacksBitBoardPtr;

        /// <summary>
        /// (square + (dir * 64))
        /// </summary>
        public ulong* BishopAttacksBitBoardDirectionPtr;

        /// <summary>
        /// (square)
        /// </summary>
        ulong* KnightAttacksBitBoardPtr;

        /// <summary>
        /// (square)
        /// </summary>
        ulong* KingAttacksBitBoardPtr;

        /// <summary>
        /// (square + (dir * 64))
        /// </summary>
        int* KingAttacksPtr;

        /// <summary>
        /// Ex. PATP[square(num) + IfWhite(+64) + IfRight(+128)]
        /// </summary>
        bool* PawnAttackThisPiece; // square, right side?, white?


        public bool IsKingInCheck;

        public PossibleMovesGenerator(UnsafeBoard boardRef)
        {
            fixed (int* ptr = &SlidingPieces._SlidingpieceAttacks1D[0])
                SlidingpieceAttacks = ptr;
            fixed (ulong* ptr = &SlidingPieces._QueenAttacksBitBoard[0])
                QueenAttacksBitBoardPtr = ptr;
            fixed (ulong* ptr = &SlidingPieces._QueenAttacksBitBoardDirection1d[0])
                QueenAttacksBitBoardDirectionPtr = ptr;
            fixed (ulong* ptr = &SlidingPieces._RookAttacksBitBoard[0])
                RookAttacksBitBoardPtr = ptr;
            fixed (ulong* ptr = &SlidingPieces._RookAttacksBitBoardDirection1d[0])
                RookAttacksBitBoardDirectionPtr = ptr;
            fixed (ulong* ptr = &SlidingPieces._BishopAttacksBitBoard[0])
                BishopAttacksBitBoardPtr = ptr;
            fixed (ulong* ptr = &SlidingPieces._BishopAttacksBitBoardDirection1d[0])
                BishopAttacksBitBoardDirectionPtr = ptr;
            fixed (ulong* ptr = &Knight.KnightAttacksBitBoard[0])
                KnightAttacksBitBoardPtr = ptr;
            fixed (ulong* ptr = &King.KingAttacksBitBoard[0])
                KingAttacksBitBoardPtr = ptr;
            fixed (int* ptr = &King.KingAttacksV2D1[0])
                KingAttacksPtr = ptr;
            fixed (bool* ptr = &Pawn.PawnCanAttackThisPiece[0])
                PawnAttackThisPiece = ptr;



            moves = new Move[UnsafeBoard.MaxMoves];
            movesCount = 0;
            this.boardRef = boardRef;
            boardPtr = boardRef.boardPtr;

            fixed (ulong* ptr = &pinningPiecesAttackDirBitBoard[0])
                pinningPiecesAttackDirBitBoardPtr = ptr;
        }

        public void Init()
        {
            movesCount = 0;

            WhiteToMove = (boardRef.playerTurn == 8);
            OurColour = boardRef.playerTurn;
            EnemyColour = boardRef.playerTurn ^ UnsafeBoard.PlayerTurnSwitch;
            EPSquare = boardRef.EPSquare;
            Calste = boardRef.castle;

            if (WhiteToMove)
            {
                // can even remove the boardRefs by moving the pointers to this class
                OurKingPos = boardRef.kingPosPtr[0];
                EnemyKingPos = boardRef.kingPosPtr[1];

                OurPawns = boardRef.whitePawns;
                OurKnights = boardRef.whiteKnights;
                OurBishops = boardRef.whiteBishops;
                OurRooks = boardRef.whiteRooks;
                OurQueens = boardRef.whiteQueens;
                EnemyPawns = boardRef.blackPawns;
                EnemyKnights = boardRef.blackKnights;
                EnemyBishops = boardRef.blackBishops;
                EnemyRooks = boardRef.blackRooks;
                EnemyQueens = boardRef.blackQueens;
            }
            else
            {
                OurKingPos = boardRef.kingPosPtr[1];
                EnemyKingPos = boardRef.kingPosPtr[0];

                OurPawns = boardRef.blackPawns;
                OurKnights = boardRef.blackKnights;
                OurBishops = boardRef.blackBishops;
                OurRooks = boardRef.blackRooks;
                OurQueens = boardRef.blackQueens;
                EnemyPawns = boardRef.whitePawns;
                EnemyKnights = boardRef.whiteKnights;
                EnemyBishops = boardRef.whiteBishops;
                EnemyRooks = boardRef.whiteRooks;
                EnemyQueens = boardRef.whiteQueens;
            }


            attackerOnKingsBitBoardDirectionAttack = 0;
            attackerOnKingPos = 0;

            int ourKingAttacks = IsSquareAttackedCount(OurKingPos);
            _KingInCheck = (ourKingAttacks != 0);
            _DoubleCheck = (ourKingAttacks > 1);
            if (ourKingAttacks < 2)
                InitPinnedPieces();
            // Console.WriteLine(ChessV1.BitBoardHelper.GetBitBoardString(pinnedPieces));
        }

        private void InitPinnedPieces()
        {
            pinnedPieces = 0;

            // Queens
            for (int i = 0; i < EnemyQueens[-1]; i++)
            {
                int pos = EnemyQueens[i];

                // check if king is in any of the attacks
                if (((QueenAttacksBitBoardPtr[pos] << OurKingPos) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 8; dir++)
                    {
                        // find the dir
                        if (((QueenAttacksBitBoardDirectionPtr[pos + (dir * 64)] << OurKingPos) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            // amount of pieces between king and pinning piece
                            int piecesBetween = 0;

                            // pos of first pin on piece
                            int pinnedPiecePos = -1;

                            int move = 0;
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                int squareFromMove = boardPtr[move];
                                if (move == OurKingPos)
                                    break;
                                if (squareFromMove != 0)
                                {
                                    piecesBetween++;
                                    if (piecesBetween == 2)
                                        break;
                                    else if ((squareFromMove & 0b11000) == OurColour && pinnedPiecePos == -1)
                                        pinnedPiecePos = move;
                                    else if ((squareFromMove & 0b11000) == EnemyColour)
                                    {
                                        piecesBetween = 0;
                                        break;
                                    }
                                }
                            }
                            if (piecesBetween == 1 && piecesBetween != -1)
                            {
                                pinningPiecesAttackDirBitBoardPtr[pinnedPiecePos] = QueenAttacksBitBoardDirectionPtr[pos + (dir * 64)];
                                pinnedPieces |= (0x8000000000000000 >> pinnedPiecePos);
                            }
                            break;
                        }
                    }
                }
            }

            // Rooks
            for (int i = 0; i < EnemyRooks[-1]; i++)
            {
                int pos = EnemyRooks[i];

                // check if king is in any of the attacks
                if (((RookAttacksBitBoardPtr[pos] << OurKingPos) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 4; dir++)
                    {
                        // find the dir
                        if (((RookAttacksBitBoardDirectionPtr[pos + (dir * 64)] << OurKingPos) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            // amount of pieces between king and pinning piece
                            int piecesBetween = 0;

                            // pos of first pin on piece
                            int pinnedPiecePos = -1;

                            int move = 0;
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                int squareFromMove = boardPtr[move];
                                if (move == OurKingPos)
                                    break;
                                if (squareFromMove != 0)
                                {
                                    piecesBetween++;
                                    if (piecesBetween == 2)
                                        break;
                                    else if ((squareFromMove & 0b11000) == OurColour && pinnedPiecePos == -1)
                                        pinnedPiecePos = move;
                                    else if ((squareFromMove & 0b11000) == EnemyColour)
                                    {
                                        piecesBetween = 0;
                                        break;
                                    }
                                }
                            }
                            if (piecesBetween == 1 && piecesBetween != -1)
                            {
                                pinningPiecesAttackDirBitBoardPtr[pinnedPiecePos] = QueenAttacksBitBoardDirectionPtr[pos + (dir * 64)];
                                pinnedPieces |= (0x8000000000000000 >> pinnedPiecePos);
                            }
                            break;
                        }
                    }
                }
            }

            // Bishops
            for (int i = 0; i < EnemyBishops[-1]; i++)
            {
                int pos = EnemyBishops[i];

                // check if king is in any of the attacks
                if (((BishopAttacksBitBoardPtr[pos] << OurKingPos) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 4; dir < 8; dir++)
                    {
                        // find the dir
                        if (((BishopAttacksBitBoardDirectionPtr[pos + (dir * 64)] << OurKingPos) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            // amount of pieces between king and pinning piece
                            int piecesBetween = 0;

                            // pos of first pin on piece
                            int pinnedPiecePos = -1;

                            int move = 0;
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                int squareFromMove = boardPtr[move];
                                if (move == OurKingPos)
                                    break;
                                if (squareFromMove != 0)
                                {
                                    piecesBetween++;
                                    if (piecesBetween == 2)
                                        break;
                                    else if ((squareFromMove & 0b11000) == OurColour && pinnedPiecePos == -1)
                                        pinnedPiecePos = move;
                                    else if ((squareFromMove & 0b11000) == EnemyColour)
                                    {
                                        piecesBetween = 0;
                                        break;
                                    }
                                }
                            }
                            if (piecesBetween == 1 && piecesBetween != -1)
                            {
                                pinningPiecesAttackDirBitBoardPtr[pinnedPiecePos] = QueenAttacksBitBoardDirectionPtr[pos + (dir * 64)];
                                pinnedPieces |= (0x8000000000000000 >> pinnedPiecePos);
                            }
                            break;
                        }
                    }
                }
            }
        }

        public void GenerateMoves()
        {
            Init();

            AddKingMoves();

            if (_DoubleCheck)
                return;

            if (_KingInCheck)
            {

            }
            else
            {
                AddPawnMoves();
            }

            // i need to add to difrent code roads, one for when the king it in check and one when it isent
            // becous when the king is in check you need to check all the pieces if it can block the pin / kill the attacer
            // make a bitboard of the attacker/attacks, if any of the moves land in it, it is a valid move
            // its only one more bitboard check per move, maby no need to split up the moves
            // and when it is a double check only king moves work

            // need to add the piece attack to a single storge,
            // dosent matter if it ovverides becous of multible attackers caouse then kin is in double check
            // and only the king moves count anyway

        }

        private void AddPawnMoves()
        {
            // 4 secktors

            // white pawn
            if (WhiteToMove)
            {
                for (int pawnNum = 0; pawnNum < OurPawns[-1]; pawnNum++)
                {
                    int pawnPos = OurPawns[pawnNum];

                    int sinlgeMovePos = pawnPos - 8;
                    int doubleMovePos = pawnPos - 16;
                    int move7 = pawnPos - 7;
                    int move9 = pawnPos - 9;

                    // pinned piece
                    if (((pinnedPieces << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                    {
                        // single move
                        if ((sinlgeMovePos & 0xFFC0) == 0)
                            if (boardPtr[sinlgeMovePos] == 0)
                            {
                                if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << sinlgeMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    // if promoting
                                    if (((ConstBitBoards.WhitePromotionLine << sinlgeMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                    {
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToQueen);
                                        movesCount++;
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToKnight);
                                        movesCount++;
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToRook);
                                        movesCount++;
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToBishop);
                                        movesCount++;
                                    }
                                    else
                                    {
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, 0);
                                        movesCount++;
                                    }
                                }
                            }

                        // double move
                        if (((ConstBitBoards.WhiteTwoMoveLine << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            if ((doubleMovePos & 0xFFC0) == 0)
                                if (boardPtr[sinlgeMovePos] == 0)
                                    if (boardPtr[doubleMovePos] == 0)
                                    {
                                        if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << doubleMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                        {
                                            moves[movesCount] = new Move(pawnPos, doubleMovePos, Move.Flag.PawnTwoForward);
                                            movesCount++;
                                        }
                                    }


                        // EnPassent
                        if (move7 == EPSquare)
                        {
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move7) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    moves[movesCount] = new Move(pawnPos, move7, Move.Flag.EnPassantCapture);
                                    movesCount++;
                                }
                        }
                        else if (move9 == EPSquare)
                        {
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move9) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    moves[movesCount] = new Move(pawnPos, move9, Move.Flag.EnPassantCapture);
                                    movesCount++;
                                }
                        }

                        // attack move7/right
                        if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            if ((move7 & 0xFFC0) == 0)
                                if ((boardPtr[move7] & 0b11000) == EnemyColour)
                                {
                                    if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move7) & 0x8000000000000000) == 0x8000000000000000)
                                    {
                                        moves[movesCount] = new Move(pawnPos, move7, 0);
                                        movesCount++;
                                    }
                                }

                        // attack move9/left
                        if ((move9 & 0xFFC0) == 0)
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move9] & 0b11000) == EnemyColour)
                                {
                                    if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move9) & 0x8000000000000000) == 0x8000000000000000)
                                    {
                                        moves[movesCount] = new Move(pawnPos, move9, 0);
                                        movesCount++;
                                    }
                                }
                    }

                    // unpinned piece
                    else
                    {
                        // single move
                        if ((sinlgeMovePos & 0xFFC0) == 0)
                            if (boardPtr[sinlgeMovePos] == 0)
                            {
                                // if promoting
                                if (((ConstBitBoards.WhitePromotionLine << sinlgeMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToQueen);
                                    movesCount++;
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToKnight);
                                    movesCount++;
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToRook);
                                    movesCount++;
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToBishop);
                                    movesCount++;
                                }
                                else
                                {
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, 0);
                                    movesCount++;
                                }
                            }

                        // double move
                        if (((ConstBitBoards.WhiteTwoMoveLine << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            if ((doubleMovePos & 0xFFC0) == 0)
                                if (boardPtr[sinlgeMovePos] == 0)
                                    if (boardPtr[doubleMovePos] == 0)
                                    {
                                        moves[movesCount] = new Move(pawnPos, doubleMovePos, Move.Flag.PawnTwoForward);
                                        movesCount++;
                                    }


                        // EnPassent
                        if (move7 == EPSquare)
                        {
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                moves[movesCount] = new Move(pawnPos, move7, Move.Flag.EnPassantCapture);
                            movesCount++;
                        }
                        else if (move9 == EPSquare)
                        {
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                moves[movesCount] = new Move(pawnPos, move9, Move.Flag.EnPassantCapture);
                            movesCount++;
                        }

                        // attack move7/right
                        if ((move7 & 0xFFC0) == 0)
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move7] & 0b11000) == EnemyColour)
                                {
                                    moves[movesCount] = new Move(pawnPos, move7, 0);
                                    movesCount++;
                                }

                        // attack move9/left
                        if ((move9 & 0xFFC0) == 0)
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move9] & 0b11000) == EnemyColour)
                                {
                                    moves[movesCount] = new Move(pawnPos, move9, 0);
                                    movesCount++;
                                }
                    }
                }
            }

            // black pawn
            else
            {
                for (int pawnNum = 0; pawnNum < OurPawns[-1]; pawnNum++)
                {
                    int pawnPos = OurPawns[pawnNum];

                    int sinlgeMovePos = pawnPos + 8;
                    int doubleMovePos = pawnPos + 16;
                    int move7 = pawnPos + 7;
                    int move9 = pawnPos + 9;

                    // pinned piece
                    if (((pinnedPieces << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                    {
                        // single move
                        if ((sinlgeMovePos & 0xFFC0) == 0)
                            if (boardPtr[sinlgeMovePos] == 0)
                            {
                                if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << sinlgeMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    // if promoting
                                    if (((ConstBitBoards.BlackPromotionLine << sinlgeMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                    {
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToQueen);
                                        movesCount++;
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToKnight);
                                        movesCount++;
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToRook);
                                        movesCount++;
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToBishop);
                                        movesCount++;
                                    }
                                    else
                                    {
                                        moves[movesCount] = new Move(pawnPos, sinlgeMovePos, 0);
                                        movesCount++;
                                    }
                                }
                            }

                        // double move
                        if (((ConstBitBoards.BlackTwoMoveLine << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            if ((doubleMovePos & 0xFFC0) == 0)
                                if (boardPtr[sinlgeMovePos] == 0)
                                    if (boardPtr[doubleMovePos] == 0)
                                    {
                                        if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << doubleMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                        {
                                            moves[movesCount] = new Move(pawnPos, doubleMovePos, Move.Flag.PawnTwoForward);
                                            movesCount++;
                                        }
                                    }


                        // EnPassent
                        if (move7 == EPSquare)
                        {
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move7) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    moves[movesCount] = new Move(pawnPos, move7, Move.Flag.EnPassantCapture);
                                    movesCount++;
                                }
                        }
                        else if (move9 == EPSquare)
                        {
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move9) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    moves[movesCount] = new Move(pawnPos, move9, Move.Flag.EnPassantCapture);
                                    movesCount++;
                                }
                        }

                        // attack move7/right
                        if ((move7 & 0xFFC0) == 0)
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move7] & 0b11000) == EnemyColour)
                                {
                                    if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move7) & 0x8000000000000000) == 0x8000000000000000)
                                    {
                                        moves[movesCount] = new Move(pawnPos, move7, 0);
                                        movesCount++;
                                    }
                                }

                        // attack move9/left
                        if ((move9 & 0xFFC0) == 0)
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move9] & 0b11000) == EnemyColour)
                                {
                                    if (((pinningPiecesAttackDirBitBoardPtr[pawnPos] << move9) & 0x8000000000000000) == 0x8000000000000000)
                                    {
                                        moves[movesCount] = new Move(pawnPos, move9, 0);
                                        movesCount++;
                                    }
                                }
                    }

                    // unpinned piece
                    else
                    {
                        // single move
                        if ((sinlgeMovePos & 0xFFC0) == 0)
                            if (boardPtr[sinlgeMovePos] == 0)
                            {
                                // if promoting
                                if (((ConstBitBoards.BlackPromotionLine << sinlgeMovePos) & 0x8000000000000000) == 0x8000000000000000)
                                {
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToQueen);
                                    movesCount++;
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToKnight);
                                    movesCount++;
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToRook);
                                    movesCount++;
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, Move.Flag.PromoteToBishop);
                                    movesCount++;
                                }
                                else
                                {
                                    moves[movesCount] = new Move(pawnPos, sinlgeMovePos, 0);
                                    movesCount++;
                                }
                            }

                        // double move
                        if (((ConstBitBoards.BlackTwoMoveLine << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            if ((doubleMovePos & 0xFFC0) == 0)
                                if (boardPtr[sinlgeMovePos] == 0)
                                    if (boardPtr[doubleMovePos] == 0)
                                    {
                                        moves[movesCount] = new Move(pawnPos, doubleMovePos, Move.Flag.PawnTwoForward);
                                        movesCount++;
                                    }


                        // EnPassent
                        if (move7 == EPSquare)
                        {
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            {
                                moves[movesCount] = new Move(pawnPos, move7, Move.Flag.EnPassantCapture);
                                movesCount++;
                            }
                        }
                        else if (move9 == EPSquare)
                        {
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                            {
                                moves[movesCount] = new Move(pawnPos, move9, Move.Flag.EnPassantCapture);
                                movesCount++;
                            }
                        }

                        // attack move7/right
                        if ((move7 & 0xFFC0) == 0)
                            if (((ConstBitBoards.LeftSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move7] & 0b11000) == EnemyColour)
                                {
                                    moves[movesCount] = new Move(pawnPos, move7, 0);
                                    movesCount++;
                                }

                        // attack move9/left
                        if ((move9 & 0xFFC0) == 0)
                            if (((ConstBitBoards.RightSideIs0 << pawnPos) & 0x8000000000000000) == 0x8000000000000000)
                                if ((boardPtr[move9] & 0b11000) == EnemyColour)
                                {
                                    moves[movesCount] = new Move(pawnPos, move9, 0);
                                    movesCount++;
                                }
                    }
                }

            }
        }

        public void AddKingMoves()
        {
            boardPtr[OurKingPos] = 0;
            for (int moveCount = 0; moveCount < 8; moveCount++)
            {
                int move = KingAttacksPtr[OurKingPos + (moveCount * 64)];
                if (move == King.InvalidMove)
                    continue;
                int piece = boardPtr[move];
                if (piece == 0)
                {
                    if (!IsSquareAttacked(move))
                    {
                        moves[movesCount] = new(OurKingPos, move, 0);
                        movesCount++;
                    }
                }
                else if ((piece & 0b11000) == OurColour)
                    continue;
                else
                {
                    if (!IsSquareAttacked(move))
                    {
                        moves[movesCount] = new(OurKingPos, move, 0);
                        movesCount++;
                    }
                }
            }
            boardPtr[OurKingPos] = (byte)(Piece.King | OurColour);
        }

        public bool IsSquareAttacked(int square)
        {
            // check king atc
            if (((KingAttacksBitBoardPtr[EnemyKingPos] << square) & 0x8000000000000000) == 0x8000000000000000)
                return true;

            // check the knights bitboards
            for (int knightNum = 0; knightNum < EnemyKnights[-1]; knightNum++)
            {
                if (((KnightAttacksBitBoardPtr[EnemyKnights[knightNum]] << square) & 0x8000000000000000) == 0x8000000000000000)
                    return true;
            }

            // pawn, just check if 
            if (WhiteToMove)
            {
                int move = square - 7;
                if (PawnAttackThisPiece[square + 64])
                    if (boardPtr[move] == Piece.BPawn)
                        return true;
                move = square - 9;
                if (PawnAttackThisPiece[square])
                    if (boardPtr[move] == Piece.BPawn)
                        return true;
            }
            else
            {
                int move = square + 9;
                if (PawnAttackThisPiece[square + 64 + 128])
                    if (boardPtr[move] == Piece.WPawn)
                        return true;
                move = square + 7;
                if (PawnAttackThisPiece[square + 128])
                    if (boardPtr[move] == Piece.WPawn)
                        return true;
            }

            // Queens
            for (int i = 0; i < EnemyQueens[-1]; i++)
            {
                int pos = EnemyQueens[i];

                // check if king is in any of the attacks
                if (((QueenAttacksBitBoardPtr[pos] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 8; dir++)
                    {
                        // find the dir
                        if (((QueenAttacksBitBoardDirectionPtr[pos + (dir * 64)] << square) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                if (move == square)
                                    return true;
                                if (boardPtr[move] != 0)
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            // Rooks
            for (int i = 0; i < EnemyRooks[-1]; i++)
            {
                int pos = EnemyRooks[i];

                // check if king is in any of the attacks
                if (((RookAttacksBitBoardPtr[pos] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 4; dir++)
                    {
                        // find the dir
                        if (((RookAttacksBitBoardDirectionPtr[pos + (dir * 64)] << square) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                if (move == square)
                                    return true;
                                if (boardPtr[move] != 0)
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            // Bishops
            for (int i = 0; i < EnemyBishops[-1]; i++)
            {
                int pos = EnemyBishops[i];

                // check if king is in any of the attacks
                if (((BishopAttacksBitBoardPtr[pos] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 4; dir < 8; dir++)
                    {
                        // find the dir
                        if (((BishopAttacksBitBoardDirectionPtr[pos + (dir * 64)] << square) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                if (move == square)
                                    return true;
                                if (boardPtr[move] != 0)
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            return false;
        }


        // is only used for the king so we need to add these attacks to some storage, so instead of all piece
        // moves need to check if king is safe after, just check if its attakcing the attacker or its bitmap attack
        public int IsSquareAttackedCount(int square)
        {
            int attackers = 0;

            // check king atc
            if (((KingAttacksBitBoardPtr[EnemyKingPos] << square) & 0x8000000000000000) == 0x8000000000000000)
            {
                attackers++;
                attackerOnKingPos = EnemyKingPos;
            }

            // check the knights bitboards
            for (int knightNum = 0; knightNum < EnemyKnights[-1]; knightNum++)
            {
                if (((KnightAttacksBitBoardPtr[EnemyKnights[knightNum]] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    attackers++;
                    attackerOnKingPos = EnemyKnights[knightNum];
                }
            }

            // pawn, just check if 
            if (WhiteToMove)
            {
                int move = square - 7;
                if (PawnAttackThisPiece[square + 64 + 128])
                    if (boardPtr[move] == Piece.BPawn)
                    {
                        attackers++;
                        attackerOnKingPos = move;
                    }
                move = square - 9;
                if (PawnAttackThisPiece[square + 64])
                    if (boardPtr[move] == Piece.BPawn)
                    {
                        attackers++;
                        attackerOnKingPos = move;
                    }
            }
            else
            {
                int move = square + 9;
                if (PawnAttackThisPiece[square + 128])
                    if (boardPtr[move] == Piece.WPawn)
                    {
                        attackers++;
                        attackerOnKingPos = move;
                    }
                move = square + 7;
                if (PawnAttackThisPiece[square])
                    if (boardPtr[move] == Piece.WPawn)
                    {
                        attackers++;
                        attackerOnKingPos = move;
                    }
            }

            // Queens
            for (int i = 0; i < EnemyQueens[-1]; i++)
            {
                int pos = EnemyQueens[i];

                // check if king is in any of the attacks
                if (((QueenAttacksBitBoardPtr[pos] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 8; dir++)
                    {
                        // find the dir
                        if (((QueenAttacksBitBoardDirectionPtr[pos + (dir * 64)] << square) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                if (move == square)
                                {
                                    attackerOnKingPos = pos;
                                    attackers++;
                                    break;
                                }
                                if (boardPtr[move] != 0)
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            // Rooks
            for (int i = 0; i < EnemyRooks[-1]; i++)
            {
                int pos = EnemyRooks[i];

                // check if king is in any of the attacks
                if (((RookAttacksBitBoardPtr[pos] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 4; dir++)
                    {
                        // find the dir
                        if (((RookAttacksBitBoardDirectionPtr[pos + (dir * 64)] << square) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                if (move == square)
                                {
                                    attackerOnKingPos = pos;
                                    attackers++;
                                    break;
                                }
                                if (boardPtr[move] != 0)
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            // Bishops
            for (int i = 0; i < EnemyBishops[-1]; i++)
            {
                int pos = EnemyBishops[i];

                // check if king is in any of the attacks
                if (((BishopAttacksBitBoardPtr[pos] << square) & 0x8000000000000000) == 0x8000000000000000)
                {
                    // find the direction of the pin
                    for (int dir = 4; dir < 8; dir++)
                    {
                        // find the dir
                        if (((BishopAttacksBitBoardDirectionPtr[pos + (dir * 64)] << square) & 0x8000000000000000) == 0x8000000000000000)
                        {
                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
                                if (move == square)
                                {
                                    attackers++;
                                    attackerOnKingPos = pos;
                                    break;
                                }
                                if (boardPtr[move] != 0)
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            return attackers;
        }

        public Move[] GetMoves()
        {
            Move[] newMoves = new Move[movesCount];
            fixed (Move* oldMovesPtr = &moves[0])
            {
                fixed (Move* newMovesPtr = &moves[0])
                {
                    for (int i = 0; i < movesCount; i++)
                        newMoves[i] = oldMovesPtr[i];
                }
            }
            return newMoves;
        }
    }
}