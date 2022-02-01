using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class Pawn
    {
        public static bool IsMovePoseble(Board board, PosebleMoves.Move move)
        {
            bool IsMovePoseble = false;

            if (Board.IsPieceWhite(board.board[move.StartSquare]))
            {
                if ((move.StartSquare - 8) == move.TargetSquare)
                {
                    if (board.board[move.TargetSquare] == 0 || Board.IsPieceBlack(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
                }
                else if ((move.StartSquare - 7) == move.TargetSquare)
                {
                    if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
                }
                else if ((move.StartSquare - 9) == move.TargetSquare)
                {
                    if (Board.IsPieceBlack(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
                }
            }
            else if (Board.IsPieceBlack(board.board[move.StartSquare]))
            {
                if ((move.StartSquare + 8) == move.TargetSquare)
                {
                    if (board.board[move.TargetSquare] == 0 || Board.IsPieceWhite(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
                }
                else if ((move.StartSquare + 7) == move.TargetSquare)
                {
                    if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
                }
                else if ((move.StartSquare + 9) == move.TargetSquare)
                {
                    if (Board.IsPieceWhite(board.board[move.TargetSquare]))
                        IsMovePoseble = true;
                }
            }


            return IsMovePoseble;
        }
    }
}