using Chess.Moves;
using Chess.ChessBoard;

namespace Chess
{
    partial class ChessGame
    {

        public bool MakeMove(Move move)
        {
            int startSquare = move.StartSquare;
            int targetSquare = move.TargetSquare;

            if ((_board.board[startSquare] & Piece.ColorBits) != _board.PlayerTurn)
                return false;
            if (startSquare > 63 ||startSquare < 0 || startSquare > 63 || targetSquare < 0)
                return false;

            if (_PossibleMoves.possibleMoves.Count != 0)
            {
                if (move.MoveFlag == Move.Flag.None)
                {
                    _board.board[targetSquare] = _board.board[startSquare];
                    _board.board[startSquare] = 0;


                    // use some kind of hasing method later

                    //checking for the king or rook moving
                    // kings
                    if (startSquare == 4)
                        _board.castle = _board.castle & 12;
                    else if (startSquare == 60)
                        _board.castle = _board.castle & 3;
                    // rooks
                    else if (startSquare == 63)
                        _board.castle = _board.castle ^ 8;
                    else if (startSquare == 56)
                        _board.castle = _board.castle ^ 4;
                    else if (startSquare == 7)
                        _board.castle = _board.castle ^ 2;
                    else if (startSquare == 0)
                        _board.castle = _board.castle ^ 1;
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
                    if (move.TargetSquare == 62)
                    {
                        _board.board[61] = _board.board[63];
                        _board.board[62] = _board.board[60];
                        _board.board[60] = 0;
                        _board.board[63] = 0;
                        _board.castle ^= 8;
                    }
                    else if (move.TargetSquare == 58)
                    {
                        _board.board[58] = _board.board[60];
                        _board.board[59] = _board.board[56];
                        _board.board[60] = 0;
                        _board.board[56] = 0;
                        _board.castle ^= 4;
                    }
                    else if (move.TargetSquare == 6)
                    {
                        _board.board[6] = _board.board[4];
                        _board.board[5] = _board.board[7];
                        _board.board[4] = 0;
                        _board.board[7] = 0;
                        _board.castle ^= 2;
                    }
                    else if (move.TargetSquare == 2)
                    {
                        _board.board[2] = _board.board[4];
                        _board.board[3] = _board.board[0];
                        _board.board[4] = 0;
                        _board.board[0] = 0;
                        _board.castle ^= 1;
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