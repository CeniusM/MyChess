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
        public List<Move> ReturnPossibleMoves(int StartSquare)
        {
            List<Move> PossibleMoves = new List<Move>();



            return PossibleMoves;
        }

        public bool IsMovePossible(Move move) // checks one move and returns true if its Possible
        {
            if (move.StartSquare > 63 || move.StartSquare < 0)
                return false;
            if (move.TargetSquare > 63 || move.TargetSquare < 0)
                return false;
            if (!Board.IsPiecesSameColor(_board.board[move.StartSquare], _board.PlayerTurn)) // check player turn
                return false;
            if (Board.IsPiecesSameColor(_board.board[move.StartSquare], _board.board[move.TargetSquare]))
                return false;

            bool isMovePossible = false;

            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm))
                isMovePossible = Pawn.IsMovePossible(_board, move, _board.enPassantPiece);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Knight))
                isMovePossible = Knight.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Bishop))
                isMovePossible = Bishop.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Rook))
                isMovePossible = Rook.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Queen))
                isMovePossible = Queen.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.King))
                isMovePossible = King.IsMovePossible(_board, move);
            else
                isMovePossible = false;

            if (!isMovePossible) // if the move isent valid no need to check for check
                return false;
            else if (IsKingInCheck(_board, move))
                isMovePossible = false;

            _board.enPassantPiece = 64;
            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm) && isMovePossible == true) // enpassant
                if (Math.Abs(move.StartSquare - move.TargetSquare) == 16)
                    _board.enPassantPiece = move.TargetSquare;

            return isMovePossible;
        }

        private bool IsKingInCheck(Board board, Move move)
        {
            return Check.IsKingInCheck(board, move);
        }
    }
}