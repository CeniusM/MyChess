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

        public bool IsMovePoseble(Move move) // checks one move and returns true if its poseble
        {
            bool isMovePoseble = false;

            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm))
                isMovePoseble = Pawn.IsMovePoseble(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Knight))
                isMovePoseble = Knight.IsMovePoseble(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Rook))
                isMovePoseble = Rook.IsMovePoseble(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.King))
                isMovePoseble = King.IsMovePoseble(_board, move);
            else
                isMovePoseble = true;

            if (IsKingInCheck())
                isMovePoseble = false;

            return isMovePoseble;
        }

        private bool IsKingInCheck()
        {
            bool isKingInCheck = false;

            // check if any of the kings is in check

            return isKingInCheck;
        }
    }
}