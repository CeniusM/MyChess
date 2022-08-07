// prioritise struckture
// note, if a piece is pinned it can still attack its pinner

using PreInitializeDataV1;

namespace ChessV1
{
    public unsafe class PossibleMovesGeneratorV2
    {
        ulong pinnedPieces = 0;
        int[] pinningPiecesPos = new int[16];
        int[] pinningPieceDirection = new int[16];
        int pinningPiecesCount = 0; // so even with the pinned pieces we dont have to check all sliding pieces



        private UnsafeBoard _board;
        private List<Move> _moves;
        private byte[] squares; // just a ref so no copying
        private byte* squarePtr;

        // used if king is in check a second time its double check, and if it is in double check only king moves can count
        private bool _KingInCheck;
        private bool _DoubleCheck;

        private int OurColour;
        private int OurColourIndex;
        private int EnemyColour;
        private int EnemyColourIndex;
        private bool WhiteToMove;
        private int OurKingPos; // the king on the team that is moving
        private int EnemyKingPos;
        private int Castle;

        // done so we dont have to derefence everytime with the colour index
        #region PieceLists
        public PieceList OurPawns;
        public PieceList OurKnights;
        public PieceList OurBishops;
        public PieceList OurRooks;
        public PieceList OurQueens;
        public PieceList EnemyPawns;
        public PieceList EnemyKnights;
        public PieceList EnemyBishops;
        public PieceList EnemyRooks;
        public PieceList EnemyQueens;
        #endregion

        //only works if the move generation have been run
        public bool IsKingInCheck() => _KingInCheck;

#pragma warning disable 8618
        public PossibleMovesGeneratorV2(UnsafeBoard board)
        {
            _board = board;
            _moves = new List<Move>(0);
            _KingInCheck = false;
            squares = _board.square;
        }
#pragma warning restore 8618

        public Move[] GetMoves()
        {
            Move[] tempMoves = new Move[_moves.Count];
            _moves.CopyTo(tempMoves);
            return tempMoves;
        }

        private void Init()
        {
            squarePtr = _board.squarePtr;

            OurColour = _board.playerTurn;
            OurColourIndex = OurColour >> 4;
            EnemyColour = OurColour ^ 0b11000;
            EnemyColourIndex = (OurColour >> 4) ^ 1;
            WhiteToMove = (OurColour == 8);
            OurKingPos = _board.kingPos[OurColourIndex];
            EnemyKingPos = _board.kingPos[OurColourIndex ^ 1];
            Castle = _board.castle;


            // just set alot of pointer right?
            // isent it faster then everytime you need to get one you need to 
            // both derefrence and get go through a propety everytime
            // ex. for (int i = 0; i < board.pawn[ColourToMoveIndex].Count)
            // with this
            //     for (int i = 0; i < OurPawns.Count)

            // this is still very slow, but not as slow
            OurPawns = _board.pawns[OurColourIndex];
            OurKnights = _board.knights[OurColourIndex];
            OurBishops = _board.bishops[OurColourIndex];
            OurRooks = _board.rooks[OurColourIndex];
            OurQueens = _board.queens[OurColourIndex];
            EnemyPawns = _board.pawns[EnemyColourIndex];
            EnemyKnights = _board.knights[EnemyColourIndex];
            EnemyBishops = _board.bishops[EnemyColourIndex];
            EnemyRooks = _board.rooks[EnemyColourIndex];
            EnemyQueens = _board.queens[EnemyColourIndex];



            _moves = new List<Move>(40);
            // another way could maby be to save the previus moves and just loop over those to just count them
            // but not sure if we get more speed out of checking this cause its very rare...
            // int ourKingAttacks = IsSquareAttackedCount(OurKingPos);
            // _KingInCheck = (ourKingAttacks != 0);
            // _DoubleCheck = (ourKingAttacks > 1);
            // if (ourKingAttacks < 2)
            InitPinnedPieces();
            // Console.WriteLine(BitBoardHelper.GetBitBoardString(pinnedPieces));
        }

        private void InitPinnedPieces()
        {
            pinnedPieces = 0;
            pinningPiecesCount = 0;
            // Queens
            for (int i = 0; i < EnemyQueens.numPieces; i++)
            {
                int pos = EnemyQueens[i];

                // check if king is in any of the attacks
                if (BitBoardHelper.ContainsSquare(SlidingPieces.QueenAttacksBitBoard[pos], OurKingPos))
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 8; dir++)
                    {
                        // find the dir
                        if (BitBoardHelper.ContainsSquare(SlidingPieces.QueenAttacksBitBoardDirection[pos, dir], OurKingPos))
                        {
                            // amount of pieces between king and pinning piece
                            int piecesBetween = 0;

                            // pos of first pin on piece
                            int pinnedPiecePos = -1;

                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingPieces.SlidingpieceAttacks[pos, dir, moveCount];
                                int squareFromMove = squares[move];
                                if (move == OurKingPos)
                                    break;
                                if (squareFromMove != 0)
                                {
                                    piecesBetween++;
                                    if (piecesBetween == 2)
                                        break;
                                    else if (Piece.Colour(squareFromMove) == OurColour && pinnedPiecePos == -1)
                                        pinnedPiecePos = move;
                                    else if (Piece.Colour(squareFromMove) == EnemyColour)
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

            // Rooks
            for (int i = 0; i < EnemyRooks.numPieces; i++)
            {
                int pos = EnemyRooks[i];

                // check if king is in any of the attacks
                if (BitBoardHelper.ContainsSquare(SlidingPieces.RookAttacksBitBoard[pos], OurKingPos))
                {
                    // find the direction of the pin
                    for (int dir = 0; dir < 4; dir++)
                    {
                        // find the dir
                        if (BitBoardHelper.ContainsSquare(SlidingPieces.RookAttacksBitBoardDirection[pos, dir], OurKingPos))
                        {
                            // amount of pieces between king and pinning piece
                            int piecesBetween = 0;

                            // pos of first pin on piece
                            int pinnedPiecePos = -1;

                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingPieces.SlidingpieceAttacks[pos, dir, moveCount];
                                int squareFromMove = squares[move];
                                if (move == OurKingPos)
                                    break;
                                if (squareFromMove != 0)
                                {
                                    piecesBetween++;
                                    if (piecesBetween == 2)
                                        break;
                                    else if (Piece.Colour(squareFromMove) == OurColour && pinnedPiecePos == -1)
                                        pinnedPiecePos = move;
                                    else if (Piece.Colour(squareFromMove) == EnemyColour)
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

            // Bishops
            for (int i = 0; i < EnemyBishops.numPieces; i++)
            {
                int pos = EnemyBishops[i];

                // check if king is in any of the attacks
                if (BitBoardHelper.ContainsSquare(SlidingPieces.BishopAttacksBitBoard[pos], OurKingPos))
                {
                    // find the direction of the pin
                    for (int dir = 4; dir < 8; dir++)
                    {
                        // find the dir
                        if (BitBoardHelper.ContainsSquare(SlidingPieces.BishopAttacksBitBoardDirection[pos, dir], OurKingPos))
                        {
                            // amount of pieces between king and pinning piece
                            int piecesBetween = 0;

                            // pos of first pin on piece
                            int pinnedPiecePos = -1;

                            for (int moveCount = 0; moveCount < 7; moveCount++)
                            {
                                int move = SlidingPieces.SlidingpieceAttacks[pos, dir, moveCount];
                                int squareFromMove = squares[move];
                                if (move == OurKingPos)
                                    break;
                                if (squareFromMove != 0)
                                {
                                    piecesBetween++;
                                    if (piecesBetween == 2)
                                        break;
                                    else if (Piece.Colour(squareFromMove) == OurColour && pinnedPiecePos == -1)
                                        pinnedPiecePos = move;
                                    else if (Piece.Colour(squareFromMove) == EnemyColour)
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
            // make a full check count
            Init();


            AddKingMoves();

            // if (_DoubleCheck) // if double check only king moves count 
            //     return;

            AddPawnMoves();
            // AddKnightMoves();
            // AddQueenMoves();
            // AddBishopMoves();
            // AddRookMoves();
        }

        private void AddPawnMoves()
        {

            // white side
            if (WhiteToMove)
            {
                // get the pawns
                for (int PawnNum = 0; PawnNum < OurPawns.numPieces; PawnNum++)
                {
                    int pawnPos = squares[PawnNum];
                    // piece pinned
                    if (BitBoardHelper.ContainsSquare(pinnedPieces, pawnPos))
                    {

                    }

                    // piece not pinned
                    else
                    {

                    }
                }
            }

            // black side
            else
            {
                // get the pawns
                for (int PawnNum = 0; PawnNum < OurPawns.numPieces; PawnNum++)
                {
                    int pawnPos = squares[PawnNum];
                    // piece pinned
                    if (BitBoardHelper.ContainsSquare(pinnedPieces, pawnPos))
                    {

                    }

                    // piece not pinned
                    else
                    {

                    }
                }
            }
        }

        public void AddKingMoves()
        {
            squares[OurKingPos] = 0; // so it never block it self after checking an ajacent square is atc            
            for (int i = 0; i < 8; i++)
            {
                int move = King.KingAttacksV2[OurKingPos, i];
                if (move == King.InvalidMove)
                    continue; // make it so later that you can just break after an InvalidMove
                else if ((squares[move] & OurColour) != OurColour)
                {
                    byte capturedPiece = squares[move];
                    squares[move] = 0;
                    if (!IsSquareAttacked(move)) // can make anoter IsSqaureAttacked that ONLY checks slidingPiecses
                        _moves.Add(new(OurKingPos, move, 0));
                    squares[move] = capturedPiece;
                }
            }
            squares[OurKingPos] = (byte)(0b1 | OurColour);
        }

        #region IsSqaureAttacked
        public bool IsSquareAttacked(int square)
        {
            if (BitBoardHelper.ContainsSquare(PreInitializeDataV1.King.KingAttacksBitBoard[EnemyKingPos], square))
                return true;

            for (int i = 0; i < EnemyKnights.numPieces; i++)
            {
                if (BitBoardHelper.ContainsSquare(PreInitializeDataV1.Knight.KnightAttacksBitBoard[EnemyKnights[i]], square))
                    return true;
            }

            // pawn, just check if 
            if (WhiteToMove)
            {
                int move = square - 7;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) - 1)
                        if (squares[move] == Piece.BPawn)
                            return true;
                move = square - 9;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) - 1)
                        if (squares[move] == Piece.BPawn)
                            return true;
            }
            else
            {
                int move = square + 7;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) + 1)
                        if (squares[move] == Piece.WPawn)
                            return true;
                move = square + 9;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) + 1)
                        if (squares[move] == Piece.WPawn)
                            return true;
            }

            for (int dir = 0; dir < 8; dir++)
            {
                for (int moveCount = 0; moveCount < 7; moveCount++)
                {
                    int foo = PreInitializeDataV1.SlidingPieces.SlidingpieceAttacks[square, dir, moveCount];
                    if (foo == PreInitializeDataV1.SlidingPieces.InvalidMove)
                        break;
                    int piece = squares[foo];
                    if (piece == 0)
                        continue;
                    if (Piece.Colour(piece) != OurColour)
                    {
                        if (Piece.PieceType(piece) == Piece.Queen)
                            return true;
                        else if (Piece.PieceType(piece) == Piece.Rook && dir < 4)
                            return true;
                        else if (Piece.PieceType(piece) == Piece.Bishop && dir > 3)
                            return true;
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            return false;
        }
        public int IsSquareAttackedCount(int square)
        {
            int attackers = 0;
            if (BitBoardHelper.ContainsSquare(PreInitializeDataV1.King.KingAttacksBitBoard[EnemyKingPos], square))
                attackers++;

            for (int i = 0; i < EnemyKnights.numPieces; i++)
            {
                if (BitBoardHelper.ContainsSquare(PreInitializeDataV1.Knight.KnightAttacksBitBoard[EnemyKnights[i]], square))
                    attackers++;
            }

            // pawns
            if (WhiteToMove)
            {
                int move = square - 7;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) - 1)
                        if (squares[move] == Piece.BPawn)
                            attackers++;
                move = square - 9;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) - 1)
                        if (squares[move] == Piece.BPawn)
                            attackers++;
            }
            else
            {
                int move = square + 7;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) + 1)
                        if (squares[move] == Piece.WPawn)
                            attackers++;
                move = square + 9;
                if (BoardRepresentation.IsPieceInBound(move))
                    if ((move) >> 3 == (square >> 3) + 1)
                        if (squares[move] == Piece.WPawn)
                            attackers++;
            }

            for (int dir = 0; dir < 8; dir++)
            {
                for (int moveCount = 0; moveCount < 7; moveCount++)
                {
                    int foo = PreInitializeDataV1.SlidingPieces.SlidingpieceAttacks[square, dir, moveCount];
                    if (foo == PreInitializeDataV1.SlidingPieces.InvalidMove)
                        break;
                    int piece = squares[foo];
                    if (piece == 0)
                        continue;
                    if (Piece.Colour(piece) != OurColour)
                    {
                        if (Piece.PieceType(piece) == Piece.Queen)
                            attackers++;
                        else if (Piece.PieceType(piece) == Piece.Rook && dir < 4)
                            attackers++;
                        else if (Piece.PieceType(piece) == Piece.Bishop && dir > 3)
                            attackers++;
                        else
                            break;
                    }
                    else
                        break;
                }
            }

            return attackers;
        }
        #endregion
    }
}






























// namespace ChessV1
// {
//     public class PossibleMovesGenerator
//     {
//          Some how use the bitboard cauculations to find all the pieces that pin the king by the enemy
//          so check if king is in there total bitboard, after find the direction, then go all the way and see if a single white piece is in the way
//
//
//
//
//
//
//
//
//         UnsafeBoard board;
//         List<Move> moves;
//         int thisKing;
//         int oppisitKing;

//         // king check
//         bool kingHit;
//         bool kingHitBySlider;
//         int kingHitDirectoion;
//         bool doubleCheck; // set this to true if kingHit != 0

//         // list of the pieces that is hit by sliding piece and could be pinned to the king
//         // later loop over these and see if any of them acually cant move
//         struct HitPiece
//         {
//             int piecePos;
//             int directionFrom;
//         }
//         List<HitPiece> pieces;
//         List<int> pinnedPieces;


//         ulong bitboardOpponentAttacks;

//         public PossibleMovesGenerator(UnsafeBoard boardRef)
//         {
//             board = boardRef;
//         }

//         // only works if the Generation have acured
//         public bool KingInCheck() => kingHit;

//         public void Init()
//         {

//         }

//         public void GenerateMoves()
//         {
//             Init();
//         }

//         public void KingMoves()
//         {

//         }
//     }
// }