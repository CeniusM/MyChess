using Chess.ChessBoard;
using Chess.Moves.PieceMovment;

namespace Chess.Moves
{
    class PossibleMoves
    {
        private Board _board;
        public List<Move> possibleMoves
        { get; private set; }
        public PossibleMoves(Board board)
        {
            _board = board;
            possibleMoves = ReturnPossibleMoves();
        }

        public void GeneratePossibleMoves()
        {
            ReturnPossibleMoves();
        }

        public List<Move> ReturnPossibleMoves()
        {
            possibleMoves = new List<Move>();

            possibleMoves.AddRange(Pawn.GetPossibleMoves(_board)); // dosent queit work since just moving the pawn to the back line also counts
            possibleMoves.AddRange(Knight.GetPossibleMoves(_board));
            possibleMoves.AddRange(Bishop.GetPossibleMoves(_board));
            possibleMoves.AddRange(Rook.GetPossibleMoves(_board));
            possibleMoves.AddRange(Queen.GetPossibleMoves(_board));
            possibleMoves.AddRange(King.GetPossibleMoves(_board));

            if (_board.castle != 0)
                possibleMoves.AddRange(Castle.GetPossibleMoves(_board));

            if (_board.enPassantPiece != 64)
                possibleMoves.AddRange(EnPassant.GetPossibleMoves(_board));

            return possibleMoves;
        }
    }
}