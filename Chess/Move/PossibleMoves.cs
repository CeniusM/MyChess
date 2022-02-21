using Chess.ChessBoard;
using Chess.Moves.PieceMovment;

namespace Chess.Moves
{
    class PossibleMoves
    {
        private Board _board;
        public PossibleMoves(Board board)
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
        public List<Move> ReturnPossibleMoves(int StartSquare)
        {
            List<Move> PossibleMoves = new List<Move>();



            return PossibleMoves;
        }

        public bool IsMovePossible(Move move) // checks one move and returns true if its Possible
        {
            if (!Board.IsPiecesSameColor(_board.board[move.StartSquare], _board.PlayerTurn))
                return false;

            bool isMovePossible = false;

            if (Board.IsPiecesSameColor(_board.board[move.StartSquare], _board.board[move.TargetSquare]))
                return false;

            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm))
                isMovePossible = Pawn.IsMovePossible(_board, move, _board.enPassantPiece);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Knight))
                isMovePossible = Knight.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Rook))
                isMovePossible = Rook.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Queen))
                isMovePossible = Queen.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.King))
                isMovePossible = King.IsMovePossible(_board, move);
            else
                isMovePossible = true;

            if (IsKingInCheck())
                isMovePossible = false;

            return isMovePossible;
        }

        private bool IsKingInCheck()
        {
            bool isKingInCheck = false;

            // check if any of the kings is in check

            return isKingInCheck;
        }
    }
}