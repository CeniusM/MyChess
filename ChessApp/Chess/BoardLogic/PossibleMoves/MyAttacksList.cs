using MyChess.ChessBoard;

namespace MyChess.PossibleMoves
{
    public class AttackList
    {
        public class Attacks
        {
            public Attacks(int pieceID, int start, int target)
            {
                this.pieceID = pieceID;
                this.start = start;
                this.target = target;
            }
            public int pieceID; // 0 = king, pawn, knight, 1 = ROOK?BISHOP, 2 = ROOK?BISHOP, 3 = Queen
                                 // make a 3d array, IsSquaresALined[64](Start)[64](Target)[4](SlidingID)
                                 // so you can easely look up if its in the right line or row or any of that
            public int start;
            public int target;
        }
        public int Color;
        public Attacks[] attacks;
        public int[] piecePoses;
        public AttackList(Attacks[] a, PieceList pieces)
        {
            attacks = a;
            pieces.GetOccupiedSquares(piecePoses!);
            // attacks = new Move[moveList.Count];
            // moveList.CopyTo(attacks, 0);
            throw new NotImplementedException();
        }

        public bool IsSquareAttacked(int square)
        {
            throw new NotImplementedException();
        }

        public bool IsSquareAttacked(int square, int movedPieceStart, int movedPieceTarget, int moveFlag) // to see if a square is stil attacked
        {
            // edge case enpassent, moveFlag




            /*
                if the slider id is 0, we can allways asume its a valid attack unless the attacker is the piece that got moved to

                for i in attacks
                    if attacks[i].slidingID == 0   
                        if attacks[i].start == movedPieceTarget
                            return false
                        else
                            return true

                    else // sliding piece




                if(enpassent)
                    doStuff
                    return

                for i in attacks
                    if attacks[i].target = square
                        if attacks[i].slidingID == 0   
                            return

                        else
                            if IsSquaresALined[squre // king][attacks[i].start // attacker][ // slider id]
                                for j in attacksFromThisPiece
                                    check if piece in the way


                for i in attacks // combine later and see if any of the checking code overlaps in funcktionalety
                    if attacks[i].tagert == movedpieceStart
                        if IsSquaresALined[squre // king][attacks[i].start // attacker][ // slider id]
                            check across and if anything is in the way of king
                    
                    if attacks[i].tagert == movedpieceTarget
                        if attacks[i].slidingID != 0   
                            some how ignore this pieces moves
                    


                or maby not use slider id or Attackers Struck and just use the list of moves
                and use a 3d array, IsSquaresALined[64](Start)[64](Target)[17 (last piece BKing = 16)](PieceTypes)


            */


            throw new NotImplementedException();
        }
    }
}

