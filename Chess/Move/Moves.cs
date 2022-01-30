using Chess.ChessBoard;

namespace Chess.Moves
{
    class PosebleMoves
    {
        private Board _board;
        public PosebleMoves(Board board)
        {
            _board = board;
        }
        public struct Move
        {
            public int StartSquare;
            public int TargetSquare;
            public Move()
            {
                StartSquare = 64;
                TargetSquare = 64;
            }
            public Move(int StartSquare, int TargetSquare)
            {
                this.StartSquare = StartSquare;
                this.TargetSquare = TargetSquare;
            }
        }
        public List<Move> ReturnPosebleMoves(int StartSquare)
        {
            List<Move> PosebleMoves = new List<Move>();

            return PosebleMoves;
        }

        public bool IsMovePoseble(Move move)
        {
            return false;
        }
    }
}