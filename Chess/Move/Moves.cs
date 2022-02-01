using Chess.ChessBoard;
using Chess.Moves.PieceMovment;

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
            bool IsMovePoseble = false;

            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm))
                IsMovePoseble = Pawn.IsMovePoseble(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Knight))
                IsMovePoseble = Knight.IsMovePoseble(_board, move);
            else
                IsMovePoseble = true;

            return IsMovePoseble;
        }
    }
}