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

            // Castle
            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.King))
            {
                if (Board.IsPieceWhite(_board.board[move.StartSquare]))
                {
                    if (move.StartSquare - move.TargetSquare == -2) // white king site
                    {
                        if ((_board.castle & 8) == 8)
                        {
                            if (_board.board[62] == 0 && _board.board[61] == 0)
                            {
                                _board.board[move.TargetSquare] = _board.board[move.StartSquare];
                                _board.board[move.StartSquare] = 0;
                                _board.board[61] = _board.board[63];
                                _board.board[63] = 0;
                            }
                            _board.castle = _board.castle ^ 8;
                        }
                    }
                    if (move.StartSquare - move.TargetSquare == 2) // white queen site
                    {
                        if ((_board.castle & 4) == 4)
                        {
                            if (_board.board[59] == 0 && _board.board[58] == 0 && _board.board[57] == 0)
                            {
                                _board.board[move.TargetSquare] = _board.board[move.StartSquare];
                                _board.board[move.StartSquare] = 0;
                                _board.board[59] = _board.board[56];
                                _board.board[56] = 0;
                            }
                            _board.castle = _board.castle ^ 4;
                        }
                    }
                }
                else
                {

                }
            }





            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm)) // needs to contain the enpassanent code
                isMovePossible = Pawn.IsMovePossible(_board, move, _board.enPassantPiece);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Knight))
                isMovePossible = Knight.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Bishop))
                isMovePossible = Bishop.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Rook))
                isMovePossible = Rook.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Queen))
                isMovePossible = Queen.IsMovePossible(_board, move);
            else if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.King)) // need to contain the castle code
                isMovePossible = King.IsMovePossible(_board, move);
            else
                isMovePossible = false;

            if (!isMovePossible) // if the move isent valid no need to check for check
                return false;
            else if (IsKingInCheck(_board, move))
                isMovePossible = false;


            // enpassant
            if (Math.Abs(move.StartSquare - move.TargetSquare) != 8)
                if (Math.Abs(move.StartSquare - move.TargetSquare) != 16)
                    if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm) && _board.board[move.TargetSquare] == 0)
                    {
                        if (Board.IsPieceWhite(_board.board[move.StartSquare]))
                            _board.board[move.TargetSquare + 8] = 0;
                        else
                            _board.board[move.TargetSquare - 8] = 0;
                    }
            _board.enPassantPiece = 64;
            if (Board.IsPieceThisPiece(_board.board[move.StartSquare], Piece.Pawm) && isMovePossible == true)
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