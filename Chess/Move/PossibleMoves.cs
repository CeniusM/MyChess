using Chess.ChessBoard;
using Chess.Moves.PieceMovment;

namespace Chess.Moves
{
    public struct Move
    {
        public int StartSquare;
        public int TargetSquare;
        public Move(int StartSquare, int TargetSquare)
        {
            this.StartSquare = StartSquare;
            this.TargetSquare = TargetSquare;
        }
    }
    class PossibleMoves
    {
        private Board _board;
        public PossibleMoves(Board board)
        {
            _board = board;
        }
        public List<Move> ReturnPossibleMoves(int startSquare)
        {
            List<Move> PossibleMoves = new List<Move>();

            PossibleMoves.AddRange(Pawn.GetPossibleMoves(_board));
            PossibleMoves.AddRange(Knight.GetPossibleMoves(_board));
            PossibleMoves.AddRange(Bishop.GetPossibleMoves(_board));
            PossibleMoves.AddRange(Rook.GetPossibleMoves(_board));
            PossibleMoves.AddRange(Queen.GetPossibleMoves(_board));
            PossibleMoves.AddRange(King.GetPossibleMoves(_board));

            return PossibleMoves;
        }

        private bool IsKingInCheck(Board board, Move move)
        {
            return Check.IsKingInCheck(board, move);
        }
    }
}