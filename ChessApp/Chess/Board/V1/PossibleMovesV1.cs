using PreInitializeData;

namespace ChessV1
{
    public class PossibleMovesGenerator
    {
        private UnsafeBoard _board;
        private List<Move> _moves;
        private byte[] squares; // just a ref so no copying

        // used if king is in check a second time its double check, and if it is in double check only king moves can count
        private bool _KingInCheck;
        private bool _DoubleCheck;

        private int ColourToMove;
        private int ColourToMoveIndex;
        private int EnemyToMove;
        private int EnemyToMoveIndex;
        private bool WhiteToMove;
        private int OurKingPos; // the king on the team that is moving
        private int EnemyKing;
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


        public PossibleMovesGenerator(UnsafeBoard board)
        {
            _board = board;
            _moves = new List<Move>(0);
            _KingInCheck = false;
            squares = _board.square;
        }

        public Move[] GetMoves()
        {
            Move[] tempMoves = new Move[_moves.Count];
            _moves.CopyTo(tempMoves);
            return tempMoves;
        }

        private void Init()
        {
            ColourToMove = _board.playerTurn;
            ColourToMoveIndex = ColourToMove >> 4;
            EnemyToMove = ColourToMove ^ 0b11111;
            EnemyToMoveIndex = (ColourToMove >> 4) ^ 1;
            WhiteToMove = (ColourToMove == 8);
            OurKingPos = _board.kingPos[ColourToMoveIndex];
            EnemyKing = _board.kingPos[ColourToMoveIndex ^ 1];
            Castle = _board.castle;


            // just set alot of pointer right?
            // isent it faster then everytime you need to get one you need to 
            // both derefrence and get go through a propety everytime
            // ex. for (int i = 0; i < board.pawn[ColourToMoveIndex].Count)
            // with this
            //     for (int i = 0; i < OurPawns.Count)
            OurPawns = _board.pawns[ColourToMoveIndex];
            OurKnights = _board.knights[ColourToMoveIndex];
            OurBishops = _board.bishops[ColourToMoveIndex];
            OurRooks = _board.rooks[ColourToMoveIndex];
            OurQueens = _board.queens[ColourToMoveIndex];
            EnemyPawns = _board.pawns[EnemyToMoveIndex];
            EnemyKnights = _board.knights[EnemyToMoveIndex];
            EnemyBishops = _board.bishops[EnemyToMoveIndex];
            EnemyRooks = _board.rooks[EnemyToMoveIndex];
            EnemyQueens = _board.queens[EnemyToMoveIndex];


            _moves = new List<Move>(40);
            // another way could maby be to save the previus moves and just loop over those to just count them
            // but not sure if we get more speed out of checking this cause its very rare...
            int ourKingAttacks = IsSquareAttackedCount(OurKingPos);
            _KingInCheck = (ourKingAttacks != 0);
            _DoubleCheck = (ourKingAttacks > 1);
        }

        public void GenerateMoves()
        {
            Init();

            AddKingMoves();

            if (_DoubleCheck) // if double check only king moves count 
                return;

            AddKnightMove();
            AddPawnMoves();
        }

        public void AddKingMoves()
        {
            squares[OurKingPos] = 0; // so it never block it self after checking an ajacent square is atc            
            for (int i = 0; i < 8; i++)
            {
                int move = King.KingAttacksV2[OurKingPos, i];
                if (move == King.InvalidMove)
                    continue; // make it so later that you can just break after an InvalidMove
                else if ((squares[move] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(move)) // can make anoter IsSqaureAttacked that ONLY checks slidingPiecses
                        _moves.Add(new(OurKingPos, move, 0));
                }
            }
            squares[OurKingPos] = (byte)(0b1 | ColourToMove);
        }

        public void AddKnightMove() // based of AddKingMoves just with a for loop
        {
            for (int i = 0; i < OurKnights.Count; i++)
            {
                int knightPos = OurKnights[i];
                squares[knightPos] = 0;
                for (int j = 0; j < 8; j++)
                {
                    int newPos = Knight.KnightAttacksV2[knightPos, j];
                    if (newPos == Knight.InvalidMove)
                        continue;
                    if ((squares[newPos] & ColourToMove) != ColourToMove)
                    {
                        if (!IsSquareAttacked(OurKingPos))
                            _moves.Add(new(knightPos, newPos, 0));
                    }

                }
                squares[knightPos] = (byte)(Piece.Knight | ColourToMove);
            }
        }

        public void AddPawnMoves()
        {
            // for white
            // MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(0xFF00)); // double move
            // MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(0xFF00000000000000)); // promotion line

            // for black
            // MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(0xFF000000000000)); // double move
            // MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(0xFF)); // promotion line

            int EPSquare = _board.EPSquare;

            if (WhiteToMove)
            {
                for (int i = 0; i < OurPawns.Count; i++)
                {
                    int pawnPos = OurPawns[i];
                    int sinlgeMovePos = pawnPos - 8;
                    int doubleMovePos = pawnPos - 16;

                    int move7 = pawnPos - 7;
                    int move9 = pawnPos - 9;

                    // double move, checked up against a bitboard
                    if (BitBoardHelper.ContainsSquare(0xFF00, pawnPos))
                        if (squares[sinlgeMovePos] == 0)
                            if (squares[doubleMovePos] == 0)
                            {
                                squares[pawnPos] = 0;
                                if (!IsSquareAttacked(OurKingPos))
                                {
                                    _moves.Add(new(pawnPos, doubleMovePos, Move.Flag.PawnTwoForward));
                                }
                                squares[pawnPos] = Piece.WPawn;
                            }

                    // single move
                    if (squares[sinlgeMovePos] == 0)
                    {
                        squares[pawnPos] = 0;
                        if (!IsSquareAttacked(OurKingPos))
                        {
                            // check if it is on promotion line
                            if (BitBoardHelper.ContainsSquare(0xFF00000000000000, sinlgeMovePos))
                            {
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToQueen));
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToKnight));
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToRook));
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToBishop));
                            }
                            else
                                _moves.Add(new(pawnPos, sinlgeMovePos, 0));
                        }
                        squares[pawnPos] = Piece.WPawn;
                    }

                    // enpassent
                    if (EPSquare != 64)
                    {
                        if (move7 == EPSquare)
                        {
                            squares[pawnPos] = 0;
                            // never anything on a EPSquare so dosent override anything, but still need to remove the other taken pawn so it dosent block
                            // also need to remove it from its list if we try the method of going through all the queens/rooks/bishops to see
                            // if any of them attack the king
                            squares[move7] = Piece.WPawn;
                            if (!IsSquareAttacked(OurKingPos))
                                _moves.Add(new(pawnPos, move7, Move.Flag.EnPassantCapture));
                            squares[pawnPos] = Piece.WPawn;
                            squares[move7] = 0;
                        }
                        else if (move9 == EPSquare)
                        {
                            squares[pawnPos] = 0;
                            squares[move9] = Piece.WPawn;
                            if (!IsSquareAttacked(OurKingPos))
                                _moves.Add(new(pawnPos, move9, Move.Flag.EnPassantCapture));
                            squares[pawnPos] = Piece.WPawn;
                            squares[move9] = 0;
                        }
                    }

                    // attacks
                    if (Piece.Colour(squares[move7]) == EnemyToMove)
                    {
                        squares[pawnPos] = 0;
                        squares[move7] = Piece.WPawn; // override on another piece
                        if (!IsSquareAttacked(OurKingPos))
                        {
                            if (BitBoardHelper.ContainsSquare(0xFF00000000000000, move7))
                            {
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToQueen));
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToKnight));
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToRook));
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToBishop));
                            }
                            else
                                _moves.Add(new(pawnPos, move7, 0));
                        }
                        squares[pawnPos] = Piece.WPawn;
                        squares[move7] = 0;
                    }
                    if (Piece.Colour(squares[move9]) == EnemyToMove)
                    {
                        squares[pawnPos] = 0;
                        squares[move9] = Piece.WPawn;
                        if (!IsSquareAttacked(OurKingPos))
                        {
                            if (BitBoardHelper.ContainsSquare(0xFF00000000000000, move9))
                            {
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToQueen));
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToKnight));
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToRook));
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToBishop));
                            }
                            else
                                _moves.Add(new(pawnPos, move9, 0));
                        }
                        squares[pawnPos] = Piece.WPawn;
                        squares[move9] = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < OurPawns.Count; i++)
                {
                    int pawnPos = OurPawns[i];
                    int sinlgeMovePos = pawnPos + 8;
                    int doubleMovePos = pawnPos + 16;

                    int move7 = pawnPos + 7;
                    int move9 = pawnPos + 9;

                    // double move, checked up against a bitboard
                    if (BitBoardHelper.ContainsSquare(0xFF000000000000, pawnPos))
                        if (squares[sinlgeMovePos] == 0)
                            if (squares[doubleMovePos] == 0)
                            {
                                squares[pawnPos] = 0;
                                if (!IsSquareAttacked(OurKingPos))
                                {
                                    _moves.Add(new(pawnPos, doubleMovePos, Move.Flag.PawnTwoForward));
                                }
                                squares[pawnPos] = Piece.BPawn;
                            }

                    // single move
                    if (squares[sinlgeMovePos] == 0)
                    {
                        squares[pawnPos] = 0;
                        if (!IsSquareAttacked(OurKingPos))
                        {
                            // check if it is on promotion line
                            if (BitBoardHelper.ContainsSquare(0xFF, sinlgeMovePos))
                            {
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToQueen));
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToKnight));
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToRook));
                                _moves.Add(new(pawnPos, sinlgeMovePos, Move.Flag.PromoteToBishop));
                            }
                            else
                                _moves.Add(new(pawnPos, sinlgeMovePos, 0));
                        }
                        squares[pawnPos] = Piece.BPawn;
                    }

                    // enpassent
                    if (EPSquare != 64)
                    {
                        if (move7 == EPSquare)
                        {
                            squares[pawnPos] = 0;
                            squares[move7] = Piece.BPawn; // never anything on a EPSquare so dosent override anything
                            if (!IsSquareAttacked(OurKingPos))
                                _moves.Add(new(pawnPos, move7, Move.Flag.EnPassantCapture));
                            squares[pawnPos] = Piece.BPawn;
                            squares[move7] = 0;
                        }
                        else if (move9 == EPSquare)
                        {
                            squares[pawnPos] = 0;
                            squares[move9] = Piece.BPawn; // never anything on a EPSquare so dosent override anything
                            if (!IsSquareAttacked(OurKingPos))
                                _moves.Add(new(pawnPos, move9, Move.Flag.EnPassantCapture));
                            squares[pawnPos] = Piece.BPawn;
                            squares[move9] = 0;
                        }
                    }

                    // attacks
                    if (Piece.Colour(squares[move7]) == EnemyToMove)
                    {
                        squares[pawnPos] = 0;
                        squares[move7] = Piece.BPawn;
                        if (!IsSquareAttacked(OurKingPos))
                        {
                            if (BitBoardHelper.ContainsSquare(0xFF, move7))
                            {
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToQueen));
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToKnight));
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToRook));
                                _moves.Add(new(pawnPos, move7, Move.Flag.PromoteToBishop));
                            }
                            else
                                _moves.Add(new(pawnPos, move7, 0));
                        }
                        squares[pawnPos] = Piece.BPawn;
                        squares[move7] = 0;
                    }
                    if (Piece.Colour(squares[move9]) == EnemyToMove)
                    {
                        squares[pawnPos] = 0;
                        squares[move9] = Piece.BPawn;
                        if (!IsSquareAttacked(OurKingPos))
                        {
                            if (BitBoardHelper.ContainsSquare(0xFF, move9))
                            {
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToQueen));
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToKnight));
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToRook));
                                _moves.Add(new(pawnPos, move9, Move.Flag.PromoteToBishop));
                            }
                            else
                                _moves.Add(new(pawnPos, move9, 0));
                        }
                        squares[pawnPos] = Piece.BPawn;
                        squares[move9] = 0;
                    }
                }
            }
        }

        // returns the amount of attackers
        public int IsSquareAttackedCount(int square)
        {
            int attackers = 0;
            if (BitBoardHelper.ContainsSquare(PreInitializeData.King.KingAttacksBitBoard[EnemyKing], square))
                attackers++;

            for (int i = 0; i < EnemyKnights.Count; i++)
            {
                if (BitBoardHelper.ContainsSquare(PreInitializeData.Knight.KnightAttacksBitBoard[EnemyKnights[i]], square))
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


            return attackers;
        }

        // just says if anyone attacks this square
        public bool IsSquareAttacked(int square)
        {
            if (BitBoardHelper.ContainsSquare(PreInitializeData.King.KingAttacksBitBoard[EnemyKing], square))
                return true;

            for (int i = 0; i < EnemyKnights.Count; i++)
            {
                if (BitBoardHelper.ContainsSquare(PreInitializeData.Knight.KnightAttacksBitBoard[EnemyKnights[i]], square))
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




            /*
                make the 
                SlidingpieceAttacksBitBoardDirection
                and
                SlidingpieceAttacksBitBoard

                make the code first check if the square is under SlidingpieceAttacksBitBoard
                then if it is go around and check SlidingpieceAttacksBitBoardDirection
                first the you do the check in an array like patern to see if it is attacking it
                cause bitboards are way faster and in alot of cases they arent lined up at all
                so no need to even check that direction
            */

            return false;
        }

        // need anoter IsSquareAttacked that only look at sliding piece (if piece have moved only pins can atack the king)
        // IsPiecePinnedToKing()
    }
}






/* Grave Yard
dosent work do to the king just being able to walk to the other side
but could work if you just added lineDiff in there at one of the checks
maby a speed comparison at a later point

public void AddKingMoves() // speedy :D... i hope
        {
            square[OurKingPos] = 0; // so it never block it self after checking an ajacent square is atc
            int newPos = OurKingPos - 8;
            if (((newPos) & 0xFFC0) == 0) // bound check
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 1;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 8;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos - 1;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos - 7;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 9;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 7;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos - 9;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    if (!IsSquareAttacked(newPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            square[OurKingPos] = (byte)(0b1 | ColourToMove);
        }




















// dont have to check every pawn, instead just out from the square (IsSquareAttacked())
            // for (int i = 0; i < EnemyPawns.Count; i++)
            // {
            //     if (BitBoardHelper.ContainsSquare(PreInitializeData.Pawn.PawnAttacksBitBoard[EnemyPawns[i], EnemyToMoveIndex], square))
            //     {
            //         MyLib.DebugConsole.WriteLine(BitBoardHelper.GetBitBoardString(PreInitializeData.Pawn.PawnAttacksBitBoard[EnemyPawns[i], EnemyToMoveIndex]));
            //         return true;
            //     }
            // }

*/
