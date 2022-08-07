using PreInitializeDataV2;

namespace ChessV2
{
    public unsafe class PossibleMovesGenerator
    {
        /* contains square
((bitboard << square) & 0x8000000000000000) == 0x8000000000000000
        */
        UnsafeBoard boardRef;

        byte* boardPtr;
        public int Calste;
        public int EPSquare;

        Move[] moves;
        int movesCount;



        int[] pinningPiecesPos = new int[16];
        int[] pinningPieceDirection = new int[16];
        ulong pinnedPieces = 0;
        int* pinningPiecesPosPtr;
        int* pinningPieceDirectionPtr;
        int pinningPiecesCount = 0; // so even with the pinned pieces we dont have to check all sliding pieces


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
        /// (square)
        /// </summary>
        ulong* QueenAttacksBitBoardPtr;

        /// <summary>
        /// (square + (dir * 64))
        /// </summary>
        public ulong* QueenAttacksBitBoardDirectionPtr;

        public bool IsKingInCheck;

        public PossibleMovesGenerator(UnsafeBoard boardRef)
        {
            fixed (ulong* ptr = &SlidingPieces._QueenAttacksBitBoard[0])
                QueenAttacksBitBoardPtr = ptr;
            fixed (ulong* ptr = &SlidingPieces._QueenAttacksBitBoardDirection1d[0])
                QueenAttacksBitBoardDirectionPtr = ptr;


            for (int i = 0; i < 63 + (8 * 64); i++)
            {
                Console.WriteLine(QueenAttacksBitBoardDirectionPtr[i]);
            }










            moves = new Move[UnsafeBoard.MaxMoves];
            movesCount = 0;
            this.boardRef = boardRef;
            boardPtr = boardRef.boardPtr;

            fixed (int* ptr = &pinningPiecesPos[0])
                pinningPiecesPosPtr = ptr;
            fixed (int* ptr = &pinningPieceDirection[0])
                pinningPieceDirectionPtr = ptr;
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


            // int ourKingAttacks = IsSquareAttackedCount(OurKingPos);
            // _KingInCheck = (ourKingAttacks != 0);
            // _DoubleCheck = (ourKingAttacks > 1);
            // if (ourKingAttacks < 2)
            InitPinnedPieces();
            Console.WriteLine(ChessV1.BitBoardHelper.GetBitBoardString(pinnedPieces));
        }

        private void InitPinnedPieces()
        {
            pinnedPieces = 0;
            pinningPiecesCount = 0;
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

                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingPieces.SlidingpieceAttacks[pos + (dir * 64) + (moveCount * 512)];
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
                                pinningPieceDirection[pinningPiecesCount] = dir;
                                pinningPiecesPos[pinningPiecesCount] = pos;
                                pinningPiecesCount++;
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


        }

        public void AddKingMoves()
        {

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