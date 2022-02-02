using Chess.ChessBoard;
using Chess.Moves;

namespace Chess.Moves.PieceMovment
{
    class knight
    {
        public static bool IsMovePoseble(Board board, PosebleMoves.Move move)
        {
            bool IsMovePoseble = false;

            if (Board.IsPieceWhite(board.board[move.StartSquare]))
            {
                
            }
            else if (Board.IsPieceBlack(board.board[move.StartSquare]))
            {

            }

            return IsMovePoseble;
        }
    }
}