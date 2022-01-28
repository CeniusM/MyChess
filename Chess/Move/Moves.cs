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
        }
        public List<Move> ReturnPosebleMoves(int pos)
        {
            List<Move> PosebleSquares = new List<Move>();

            return PosebleSquares;
        }

        public bool IsMovePoseble(Move move)
        {
            return false;
        }
    }
}