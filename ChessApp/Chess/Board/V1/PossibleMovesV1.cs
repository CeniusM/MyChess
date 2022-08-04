using PreInitializeData;

namespace ChessV1
{
    public class PossibleMovesGenerator
    {
        private UnsafeBoard _board;
        private List<Move> _moves;
        private byte[] square; // just a ref so no copying

        // used if king is in check a second time its double check, and if it is in double check only king moves can count
        private bool _KingInCheck;
        private bool _DoubleCheck;

        private int ColourToMove;
        private int ColourToMoveIndex;
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
            square = _board.square;
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
            EnemyToMoveIndex = (ColourToMove >> 4) ^ 1;
            WhiteToMove = (ColourToMove == 8);
            OurKingPos = _board.kingPos[ColourToMoveIndex];
            EnemyKing = _board.kingPos[ColourToMoveIndex ^ 1];
            Castle = _board.castle;

            _moves = new List<Move>(40);
            int ourKingAttacks = IsSquareAttackedCount(OurKingPos);
            _KingInCheck = (ourKingAttacks != 0);
            _DoubleCheck = (ourKingAttacks > 1);

            OurPawns = _board.pawns[ColourToMoveIndex];
            OurKnights = _board.pawns[ColourToMoveIndex];
            OurBishops = _board.pawns[ColourToMoveIndex];
            OurRooks = _board.pawns[ColourToMoveIndex];
            OurQueens = _board.pawns[ColourToMoveIndex];
            EnemyPawns = _board.pawns[EnemyToMoveIndex];
            EnemyKnights = _board.pawns[EnemyToMoveIndex];
            EnemyBishops = _board.pawns[EnemyToMoveIndex];
            EnemyRooks = _board.pawns[EnemyToMoveIndex];
            EnemyQueens = _board.pawns[EnemyToMoveIndex];
        }

        public void GenerateMoves()
        {
            Init();

            AddKingMoves();
        }

        public void AddKingMoves() // speedy :D... i hope
        {
            int newPos = OurKingPos - 8;
            if (((newPos) & 0xFFC0) == 0) // bound check
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    // // // only problem with this is that the Move get copyed into moves, but maby worth it ¯\_(ツ)_/¯
                    // // // atlist then we dont have to make a new list and add all the moves that are acually valid

                    // moves the king so the IsSqaureAttacked dosent count the old king pos as blocked
                    // but no more then the kingPos acually needs to be changed so no need for _boad.Make/UnMakeMove
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }

            newPos = OurKingPos + 1;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 8;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos - 1;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos - 7;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 9;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos + 7;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            newPos = OurKingPos - 9;
            if (((newPos) & 0xFFC0) == 0)
            {
                if ((square[newPos] & ColourToMove) != ColourToMove)
                {
                    _board.kingPos[ColourToMoveIndex] = newPos;
                    if (!IsSquareAttacked(OurKingPos))
                        _moves.Add(new(OurKingPos, newPos, 0));
                }
            }
            _board.kingPos[ColourToMoveIndex] = OurKingPos;
        }






        // returns the amount of attackers
        public int IsSquareAttackedCount(int square)
        {
            int attackers = 0;
            if (BitBoardHelper.ContainsSquare(PreInitializeData.Knight.KnightAttacksBitBoard[EnemyKing], square))
                attackers++;

            for (int i = 0; i < EnemyKnights.Count; i++)
            {
                if (BitBoardHelper.ContainsSquare(PreInitializeData.Knight.KnightAttacksBitBoard[EnemyKnights[i]], square))
                    attackers++;
            }

            return attackers;
        }

        // just says if anyone attacks this square
        public bool IsSquareAttacked(int square)
        {
            if (BitBoardHelper.ContainsSquare(PreInitializeData.Knight.KnightAttacksBitBoard[EnemyKing], square))
                return true;

            for (int i = 0; i < EnemyKnights.Count; i++)
            {
                if (BitBoardHelper.ContainsSquare(PreInitializeData.Knight.KnightAttacksBitBoard[EnemyKnights[i]], square))
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
    }
}