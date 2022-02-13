using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Pawn
    {
        /*
        the 6th bit is used to see if it can do a double move
        the 7th bit is used to see if it can have used "En passant" on it
        but for now i just check if it is on the right row
        */
        public static bool IsMovePossible(Board board, PossibleMoves.Move move)
        {
            bool IsMovePossible = false;

            if (Board.IsPieceWhite(board.board[move.StartSquare]))
            {
                if ((move.StartSquare - 8) == move.TargetSquare)
                {
                    if (board.board[move.TargetSquare] == 0)
                        IsMovePossible = true;
                }
                else if ((move.StartSquare - 16) == move.TargetSquare)
                {
                    if (move.StartSquare < 56 && move.StartSquare > 47)
                        if (board.board[move.TargetSquare] == 0)
                            if (board.board[move.TargetSquare + 8] == 0)
                                IsMovePossible = true;
                }
                else if ((move.StartSquare - 7) == move.TargetSquare)
                {
                    if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        IsMovePossible = true;
                }
                else if ((move.StartSquare - 9) == move.TargetSquare)
                {
                    if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        IsMovePossible = true;
                }
            }
            else if (Board.IsPieceBlack(board.board[move.StartSquare]))
            {
                if ((move.StartSquare + 8) == move.TargetSquare)
                {
                    if (board.board[move.TargetSquare] == 0)
                        IsMovePossible = true;
                }
                else if ((move.StartSquare + 16) == move.TargetSquare) // can only move if the pawn hasent moved once yet, indecated by the 32value bit
                {
                    if (move.StartSquare > 7 && move.StartSquare < 16)
                        if (board.board[move.TargetSquare] == 0)
                            if (board.board[move.TargetSquare - 8] == 0)
                                IsMovePossible = true;
                }
                else if ((move.StartSquare + 7) == move.TargetSquare)
                {
                    if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        IsMovePossible = true;
                }
                else if ((move.StartSquare + 9) == move.TargetSquare)
                {
                    if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        IsMovePossible = true;
                }
            }


            return IsMovePossible;
        }
    }
}