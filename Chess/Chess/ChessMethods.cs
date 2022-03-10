using Chess.Moves;
using Chess.ChessBoard;

namespace Chess
{
    partial class ChessGame
    {

        public bool MakeMove(Move move)
        {
            if ((_board.board[move.StartSquare] & Piece.ColorBits) != _board.PlayerTurn)
                return false;
            if (move.StartSquare > 63 || move.StartSquare < 0 || move.TargetSquare > 63 || move.TargetSquare < 0)
                return false;

            if (_PossibleMoves.possibleMoves.Count != 0)
            {
                int startSquare = move.StartSquare;
                int targetSquare = move.TargetSquare;

                if (move.MoveFlag == Move.Flag.None)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;

                }
                else if (move.MoveFlag == Move.Flag.PawnTwoForward)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;
                    _board.enPassantPiece = targetSquare;
                }
                else if (move.MoveFlag == Move.Flag.EnPassantCapture)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;
                    if (Board.IsPieceWhite(_board.board[targetSquare]))
                        _board.board[targetSquare + 8] = 0;
                    else
                        _board.board[targetSquare - 8] = 0;
                }
                else if (move.MoveFlag == Move.Flag.Castling)
                {
                    if (move.StartSquare == 8)
                    {
                        _board.board[61] = _board.board[63];
                        _board.board[62] = _board.board[60];
                        _board.board[60] = 0;
                        _board.board[63] = 0;
                    }
                }
                else // promotions
                {
                    _board.board[targetSquare] = move.PromotionPieceType + _board.PlayerTurn;
                    _board.board[startSquare] = 0;
                }
                _board.ChangePlayer();
                _PossibleMoves.GeneratePossibleMoves();


                // Just for testing atm, needs to be removed later
                if (_PossibleMoves.possibleMoves.Count == 0)
                {
                    _board.Reset();
                    _PossibleMoves.GeneratePossibleMoves();
                }
                return true;
            }
            return false;
        }

        public void UnmakeMove()
        {
            GameMove move = gameMoves[gameMoves.Count - 1];
            gameMoves.RemoveAt(gameMoves.Count - 1);

            _board.board[move.StartSquare] = _board.board[move.TargetSquare];
            _board.board[move.TargetSquare] = move.CapturedPiece;

            if (move.MoveFlag == Move.Flag.PromoteToQueen)
                _board.board[move.StartSquare] = Piece.Pawm + _board.PlayerTurn;


            _board.ChangePlayer();
            _PossibleMoves.GeneratePossibleMoves();
        }
    }
}