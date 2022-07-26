using MyChess.ChessBoard;

namespace MyChess.PossibleMoves
{
    public class AttackList
    {
        public struct Attacks
        {
            public Attacks(bool slider, int start, int target)
            {
                this.slider = slider;
                this.start = start;
                this.target = target;
            }
            public bool slider;
            public int start;
            public int target;
        }
        public Move[] attacks;
        public AttackList(List<Move> moveList)
        {
            attacks = new Move[moveList.Count];
            moveList.CopyTo(attacks, 0);
        }

        public bool IsSquareAttacked()
        {
            throw new NotImplementedException();
        }

        public bool IsSquareAttacked(int movedPieceStart, int movedPieceTarget) // to see if a square is stil attacked
        {
            throw new NotImplementedException();
        }
    }
}